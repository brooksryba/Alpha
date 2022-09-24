using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{

    public string title;
    public string className;

    [System.NonSerialized]
    public int level;
    [System.NonSerialized]
    public List<ItemData> items;
    [System.NonSerialized]
    public int damage;
    [System.NonSerialized]
    public int currentHP;
    [System.NonSerialized]
    public int currentMana;
    [System.NonSerialized]
    public int earnedXp;

    [System.NonSerialized]
    public List<string> attackNames;

    [System.NonSerialized]
    public List<string> spellNames;

    public List<string> partyMembers;

    public BaseCharacterClass characterClass;

    [Header("Ink JSON")]
    [SerializeField] public TextAsset inkJSON;

    void Start()
    {
        SaveSystem.instance.Register(this.title, () => { SaveState(); });
        LoadCharacterClass();
        LoadState();

    }

    void OnDestroy()
    {
        SaveSystem.instance.Unregister(this.title);
    }

    public void SaveState()
    {
        SaveSystem.instance.SaveState<CharacterData>(new CharacterData(this), this.title);
    }

    public void LoadCharacterClass()
    {
        this.characterClass = BaseCharacterClass.GetCharacterClass(className);
    }


    public void LoadState()
    {
        CharacterData data = SaveSystem.instance.LoadState<CharacterData>(this.title) as CharacterData;

        if (data != null)
        {
            gameObject.SetActive(data.active);
            
            
            this.currentHP = data.currentHP;
            this.currentMana = data.currentMana;
            this.earnedXp = data.earnedXp;
            this.level = LevelSystem.instance.GetLevel(this.earnedXp);
            this.partyMembers = data.partyMembers;
            this.attackNames = data.attackNames;
            this.spellNames = data.spellNames;
            characterClass.SetStats(this.level);


            this.items = new List<ItemData>();

            if( data.items != null ) {
                foreach( var item in data.items )
                {
                    this.items.Add(item);
                }            
            }
        }
		else 
		{
            this.items = new List<ItemData>();
            this.earnedXp = 200;
            this.level = LevelSystem.instance.GetLevel(this.earnedXp);
            characterClass.SetStats(this.level);
            this.currentHP = characterClass.maxHP;
            this.currentMana = characterClass.maxMana;
            this.attackNames = characterClass.GetBaseAttackNames(this.level);
            this.spellNames = characterClass.GetBaseSpellNames(this.level);
		}

    }

    public Dictionary<string, int> GetInventoryItemCounts() {
        Dictionary<string, int> itemCount = new Dictionary<string, int>();
        foreach( var item in items ) {
            if( itemCount.ContainsKey(item.title) ) {
                itemCount[item.title] += 1;
            } else {
                itemCount.Add(item.title, 1);
            }
        }
        return itemCount;
    }

    public Dictionary<string, ItemData> GetInventoryItemRefs() {
        Dictionary<string, ItemData> itemRefs = new Dictionary<string, ItemData>();
        foreach( var item in items ) {
            itemRefs[item.title] = item;
        }
        return itemRefs;
    }

    public void AddInventoryItem(InventoryItem item)
    {
        items.Add(new ItemData(item));
    }
    public bool multiplyHP(double hp) 
    {
        int newHP = currentHP + (int)((double)characterClass.maxHP * hp);
        if( newHP > 0 ) {
            currentHP = Math.Min(newHP, characterClass.maxHP);
            return true;
        } else {
            return false;
        }
    }

    public bool MultiplyMana(double mana)
    {
        int newMana = currentMana + (int)((double)characterClass.maxMana * mana);
        if( newMana > 0 ) {
            currentMana = Math.Min(newMana, characterClass.maxMana);
            return true;
        } else {
            return false;
        }
    }


    public bool TakeDamage(int dmg)
    {
        currentHP -= dmg;

        if (currentHP <= 0)
        {
            currentHP = 0;
            return true;
        }
        else
            return false;
    }

    public void Heal(int amount)
    {
        currentHP += amount;
        if (currentHP > characterClass.maxHP)
            currentHP = characterClass.maxHP;
    }

    public bool UseMana(int amount)
    {
        if (currentMana - amount < 0)
            return false;
        else
        {
            currentMana -= amount;
            return true;
        }
    }

    public void AddMana(int amount)
    {
        currentMana += amount;
        if (currentMana > characterClass.maxMana)
            currentMana = characterClass.maxMana;
    }

    // @TODO - this might need to be an Ienumerator to confirm user receives all messages
    public IEnumerator AddXp(int amount){
        earnedXp += amount;
        int newLevel = LevelSystem.instance.GetLevel(earnedXp);
        for(int lvl = this.level; lvl < newLevel; lvl++){
            this.level += 1;
            ToastSystem.instance.Open(this.name + " is now level " + this.level.ToString() + "!");
            yield return new WaitForSeconds(1f);
            characterClass.SetStats(this.level);
            if(characterClass.attackProgression.ContainsKey(this.level)){
                if(!attackNames.Contains(characterClass.attackProgression[this.level])){
                    this.attackNames.Add(characterClass.attackProgression[this.level]);
                    ToastSystem.instance.Open(this.name + " has learned attack " + characterClass.attackProgression[this.level] + "!");
                    yield return new WaitForSeconds(1f);
                }

            }

            if(characterClass.spellProgression.ContainsKey(this.level)){
                if(!spellNames.Contains(characterClass.spellProgression[this.level])){
                    this.spellNames.Add(characterClass.spellProgression[this.level]);
                    ToastSystem.instance.Open(this.name + " has learned spell " + characterClass.spellProgression[this.level] + "!");
                    yield return new WaitForSeconds(1f);
                }

            }

        }
        yield return new WaitForSeconds(0f);
    }

}