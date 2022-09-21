using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceSystem : MonoBehaviour
{
    private static ExperienceSystem _instance;
    public static ExperienceSystem instance { get { return _instance; } }
    public Dictionary<int, string> levelUpMoveLookup;
    public List<int> experienceLookup;
    public float xpIncrease = 0.1f;
    public int firstLevelXp = 100;
    public int levelCap = 100;
    public int xpCap;

    public void Awake(){
        _instance = this;
        int runningTotal = 0;
        for(int level = 1; level <= levelCap+1; level++){
            int nextLevelAmount = Mathf.FloorToInt(firstLevelXp*Mathf.Pow(1.0f + xpIncrease, level - 1));
            runningTotal += nextLevelAmount;
            experienceLookup.Add(runningTotal);
        }
        xpCap = runningTotal;
    }


    public int GetLevel(int xp){
        for(int level = 1; level <= levelCap; level++){
            if(xp < experienceLookup[level-1]){
                return level-1;
            }
        }
        return 1;
    }


}
