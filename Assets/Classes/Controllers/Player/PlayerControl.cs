using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour
{
    public void OnReset() {
        StateSystem.instance.Reset();
        SaveSystem.ResetAndDeregister();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);        
    }

    public void OnSave() {
        SaveSystem.Save();
        ToastSystem.instance.Open("Saving...");
    }

    public void OnInteract() {
        GameObject.Find("Player").GetComponent<PlayerInteraction>().HandleCollisionInteraction();
    }

    public void OnOptions() {
        InputSystem.instance.ToggleInventory();
    }
}

