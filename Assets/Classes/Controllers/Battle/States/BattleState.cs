using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BattleState
{
    public BattleState newState;
    public string newMessage;
    public BattleObjectManager _manager = BattleObjectManager.instance;
    public BattleSystemUtils battleSystemUtils = new BattleSystemUtils();

    public void Transition(BattleState state) {
        newState = state;
    }

    public void Toast(string text) {
        ToastSystem.instance.Open(text, false);
    }

    virtual public IEnumerator enter(float delay) {
        yield return new WaitForSeconds(delay);
    }

    virtual public IEnumerator enter() {
        yield return new WaitForSeconds(0f);
    }
    
    virtual public IEnumerator execute(float delay) {
        if(newState == null)
            newState = this;
        yield return new WaitForSeconds(delay);
    }

    virtual public IEnumerator execute() {
        if(newState == null)
            newState = this;
        yield return new WaitForSeconds(0f);
    }

    virtual public IEnumerator exit() {
        yield return new WaitForSeconds(0f);
    }
}



