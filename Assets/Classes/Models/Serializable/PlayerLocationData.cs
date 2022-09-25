using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class PlayerLocationData {

    public float[] position;
    public string scene;


    public PlayerLocationData(CharacterMovement player) {
        scene = SceneManager.GetActiveScene().name;

        position = new float[2];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
    }

    public PlayerLocationData(Vector3 transform, string sceneID) {
        scene = sceneID;
        
        position = new float[2];
        position[0] = transform.x;
        position[1] = transform.y;
    }
  

}
