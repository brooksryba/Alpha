using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleData
{
    public string enemy;
    public string scene;
    public string scenePath;

    public BattleData(string enemyID, string sceneID, string scenePathID) {
        enemy = enemyID;
        scene = sceneID;
        scenePath = scenePathID;
    }
}
