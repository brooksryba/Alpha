using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{

    public string title;
    public int level;
    public string className;

    public List<ItemData> items;

    public int damage;


    public int currentHP;


    public int currentMana;

    public int earnedXp;



    public List<string> partyMembers;
    public List<string> attackNames;
    public List<string> spellNames;

    public int dialogIndex;
    public List<string> dialogText;
    public BaseCharacterClass characterClass;

    [Header("Ink JSON")]
    [SerializeField] public TextAsset inkJSON;


    void Start()
    {
        SaveSystem.Register(this.title, () => { SaveState(); });
        LoadCharacterClass();
        LoadState();

    }

    public void SaveState()
    {
        SaveSystem.SaveState<CharacterData>(new CharacterData(this), this.title);
    }

    public void LoadCharacterClass()
    {
        this.characterClass = BaseCharacterClass.GetCharacterClass(className);
    }


    public void LoadState()
    {
        CharacterData data = SaveSystem.LoadState<CharacterData>(this.title) as CharacterData;

        if (data != null)
        {
            gameObject.SetActive(data.active);
            
            this.level = data.level;
            this.currentHP = data.currentHP;
            this.currentMana = data.currentMana;
            this.earnedXp = data.earnedXp;
            this.partyMembers = data.partyMembers;
            this.dialogIndex = data.dialogIndex;
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
            characterClass.SetStats(this.level);
            this.currentHP = characterClass.maxHP;
            this.currentMana = characterClass.maxMana;
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

    public bool multiplyMana(double mana)
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

    public bool useMana(int amount)
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

}