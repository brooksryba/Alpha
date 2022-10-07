using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleCondition : ScriptableObject
{
    public GameObject attacker = null;
    public int attackerID;
    public string attackerName = "";
    public GameObject defender = null;
    public int defenderID;
    public string defenderName = "";

    public Dictionary<int, Vector3> originalPositions = new Dictionary<int, Vector3>();
    public Dictionary<int, Color> originalSpriteColors = new Dictionary<int, Color>();

    public List<int> playerParty = new List<int>();
    public List<int> enemyParty = new List<int>();
    public List<int> allPlayers = new List<int>();
    public List<int> deadPlayerList = new List<int>();

    public List<Character> characterTurnOrder = new List<Character>();
    public int turnIndex = -1;
    public int overallTurnNumber;
}
