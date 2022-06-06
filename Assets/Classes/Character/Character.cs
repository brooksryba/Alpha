using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{

    public string title;
    public int level;

    public List<ItemData> items;

    public int damage;

    public int maxHP;
    public int currentHP;

    public int maxMana;
    public int currentMana;

    public int speed;
    public int earnedXp;

    public List<string> partyMembers;
    public List<string> attackNames;
    public List<string> spellNames;

    public int dialogIndex;
    public List<string> dialogText;

    void Start()
    {
        LoadState();
    }

    public void SaveState()
    {
        SaveSystem.SaveState<CharacterData>(new CharacterData(this), this.title);
    }

    public void LoadState()
    {
        CharacterData data = SaveSystem.LoadState<CharacterData>(this.title) as CharacterData;
        if (data != null)
        {
            this.level = data.level;
            this.currentHP = data.currentHP;
            this.currentMana = data.currentMana;
            this.speed = data.speed;
            this.earnedXp = data.earnedXp;
            this.partyMembers = data.partyMembers;
            this.dialogIndex = data.dialogIndex;
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
			this.currentHP = this.level * 10;
        	this.currentMana = this.level * 5;
            this.speed = this.level * 3;
		}
		this.maxHP = this.level * 10;
		this.maxMana = this.level * 5;
    }

    public void AddInventoryItem(InventoryItem item)
    {
        items.Add(new ItemData(item));
    }
    public bool multiplyHP(double hp) 
    {
        int newHP = currentHP + (int)((double)maxHP * hp);
        if( newHP > 0 ) {
            currentHP = Math.Min(newHP, maxHP);
            return true;
        } else {
            return false;
        }
    }

    public bool multiplyMana(double mana)
    {
        int newMana = currentMana + (int)((double)maxMana * mana);
        if( newMana > 0 ) {
            currentMana = Math.Min(newMana, maxMana);
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
        if (currentHP > maxHP)
            currentHP = maxHP;
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

}