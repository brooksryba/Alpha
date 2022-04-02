using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyData {

    public bool active;

    public EnemyData(Enemy enemy) {
        active = enemy.gameObject.activeSelf;
    }

}
