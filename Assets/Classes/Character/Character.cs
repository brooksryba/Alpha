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

    public List<string> partyMembers;

    public void SaveState()
    {
        SaveSystem.SaveState<CharacterData>(new CharacterData(this), gameObject.name);
    }

    public void LoadState()
    {

        CharacterData data = SaveSystem.LoadState<CharacterData>(gameObject.name) as CharacterData;
        if (data != null)
        {
			this.level = data.level;
			this.currentHP = data.currentHP;
        	this.currentMana = data.currentHP;

        }
		else 
		{
			this.currentHP = this.level * 10;
        	this.currentMana = this.level * 5;
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

}