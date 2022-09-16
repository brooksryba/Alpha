using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BattleState
{
    public BattleState newState;
    public string newMessage;
    public BattleObjectManager _manager = BattleObjectManager.instance;
    public BattleSystemUtils battleSystemUtils = new BattleSystemUtils();


    virtual public IEnumerator execute() {
        newState = this;
        newMessage = "";
        yield return new WaitForSeconds(0f);
    }
}



