using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSystem : MonoBehaviour
{
    private static LevelSystem _instance;
    public static LevelSystem instance { get { return _instance; } }
    public Dictionary<int, string> levelUpMoveLookup;
    public List<int> experienceLookup;
    public float xpIncrease = 0.1f;
    public int firstLevelXp = 100;
    public int levelCap = 100;
    public int xpCap;
    

    public void Awake(){
        _instance = this;
        int runningTotal = 0;
        for(int level = 1; level <= levelCap; level++){
            int nextLevelAmount = Mathf.FloorToInt(firstLevelXp*Mathf.Pow(1.0f + xpIncrease, level - 1));
            runningTotal += nextLevelAmount;
            experienceLookup.Add(runningTotal);
        }
        xpCap = runningTotal;
    }


    public int GetLevel(int xp){
        for(int level = 1; level <= levelCap; level++){
            if(xp < experienceLookup[level-1]){
                return level;
            }
        }
        if(xp >= experienceLookup[levelCap]){
            return levelCap;
        }
        return 1;
    }


    public int GetExperienceFromEnemy(int enemyXp){
        // @TODO - this should be some function to translate the xp amount to an xp earned (eg sqrt)
        return Mathf.FloorToInt(enemyXp);
    }

    public List<int> GetXpInterval(int xp){
        List<int> xpInterval = new List<int>();
        int level = GetLevel(xp);
        int lowerLimit = 0;
        if(level > 1){
            lowerLimit = experienceLookup[level-2];
        }

        int xpForNextLevel = experienceLookup[level-1];
        int xpProgress = xp - lowerLimit;
        xpForNextLevel -= lowerLimit;

        xpInterval.Add(xpProgress);
        xpInterval.Add(xpForNextLevel);

        return xpInterval;
    }


}
