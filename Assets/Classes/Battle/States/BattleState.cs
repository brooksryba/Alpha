using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BattleState
{
    public BattleState newState;
    public BattleObjectManager battleObjManager = GameObject.Find("BattleObjectManager").GetComponent<BattleObjectManager>();
    public BattleSystemUtils battleSystemUtils = new BattleSystemUtils();

    virtual public IEnumerator execute() {
        newState = this;
        yield return new WaitForSeconds(0f);
    }
}



