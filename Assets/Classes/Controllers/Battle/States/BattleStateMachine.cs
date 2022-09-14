using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleStateMachine : MonoBehaviour
{
    public BattleState state;
    public bool stateLock = false;

    public void Start() {
        state = new BattleStateSetup();
    }

    public void Update()
    {
        if (state != null && !stateLock) {
            StartCoroutine(Step());
        }
    }

    public void Transition(BattleState newState)
    {
        state = newState;
    }

    IEnumerator Step()
    {
        stateLock = true;
        yield return state.execute();
        if(state.GetType().Name != state.newState.GetType().Name)
            Debug.Log("From: " + state.GetType().Name + " To: " + state.newState.GetType().Name);
        state = state.newState;
        yield return new WaitForSeconds(0f);
        stateLock = false;
    }
}
