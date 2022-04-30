using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{

    public string title;
    public int level;

    public int damage;

    public int maxHP;
    public int currentHP;

    public int maxMana;
    public int currentMana;

    public int speed;
    public int earnedXp;

    public List<string> partyMembers;
    public List<string> attackNames;

    private CharacterManager _manager;

    public void Init(CharacterManager manager) {
        _manager = manager;
    }

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
            Debug.Log(data.level);
            Debug.Log(data.currentHP);
            this.level = data.level;
            this.currentHP = data.currentHP;
            this.currentMana = data.currentMana;
            this.speed = data.speed;
            this.earnedXp = data.earnedXp;
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

    public Dictionary<string, AttackData> getAttacks()
    {
        Dictionary<string, AttackData> attackDictionary = new Dictionary<string, AttackData>();
        foreach(var attack in this.attackNames)
        {
            attackDictionary.Add(attack, Attacks.lookup[attack]);
        }
        return attackDictionary;
    }

}