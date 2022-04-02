using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{

	public string unitName;
	public int unitLevel;

	public int damage;

	public int maxHP;
	public int currentHP;

	public int maxMana;
	public int currentMana;

	public List<GameObject> attackList;

	public List<string> attackNames;

	void Start()
   {
	   foreach(Object attackName in attackList)
	   {
		   attackNames.Add(attackName.name);
	   }

   }



	public bool TakeDamage(int dmg)
	{
		currentHP -= dmg;

		if (currentHP <= 0)
			return true;
		else
			return false;
	}

	public void Heal(int amount)
	{
		currentHP += amount;
		if (currentHP > maxHP)
			currentHP = maxHP;
	}

}