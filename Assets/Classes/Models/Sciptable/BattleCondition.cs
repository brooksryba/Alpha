using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleCondition : ScriptableObject
{
    public GameObject attacker = null;
    public string attackerName = "";
    public GameObject defender = null;
    public string defenderName = "";

    public Dictionary<string, Vector3> originalPositions = new Dictionary<string, Vector3>();
    public Dictionary<string, Color> originalSpriteColors = new Dictionary<string, Color>();

    public List<string> playerParty = new List<string>();
    public List<string> enemyParty = new List<string>();
    public List<string> allPlayers = new List<string>();
    public List<string> deadPlayerList = new List<string>();

    public List<Character> characterTurnOrder = new List<Character>();
    public int turnIndex = -1;
    public int overallTurnNumber;
}
