using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameUpdateCaller : MonoBehaviour
{
    private static MinigameUpdateCaller instance = null;
    public static System.Action OnUpdate;
 
    void Awake()
    {
        if( instance == null )
        {
            instance = this;
            DontDestroyOnLoad( this );
        }
        else if( this != instance )
            Destroy( this );
    }
 
    void Update()
    {
        if( OnUpdate != null )
            OnUpdate();
    }
}
