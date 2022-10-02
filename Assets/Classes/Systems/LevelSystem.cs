using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LevelSystem
{
    public static Dictionary<int, string> levelUpMoveLookup = new Dictionary<int, string>();
    public static List<int> experienceLookup = new List<int>();
    public static float xpIncrease = 0.1f;
    public static int firstLevelXp = 100;
    public static int levelCap = 100;
    public static int xpCap;
    
    static LevelSystem() {
        int runningTotal = 0;
        for(int level = 1; level <= levelCap; level++){
            int nextLevelAmount = Mathf.FloorToInt(firstLevelXp*Mathf.Pow(1.0f + xpIncrease, level - 1));
            runningTotal += nextLevelAmount;
            experienceLookup.Add(runningTotal);
        }
        xpCap = runningTotal;
    }

    public static int GetLevel(int xp){
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

    public static int GetExperienceFromEnemy(int enemyXp){
        // @TODO - this should be some function to translate the xp amount to an xp earned (eg sqrt)
        return Mathf.FloorToInt(enemyXp);
    }

    public static List<int> GetXpInterval(int xp){
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
