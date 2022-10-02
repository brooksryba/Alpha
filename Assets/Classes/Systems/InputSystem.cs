using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class InputSystem : MonoBehaviour
{
    public static InputSystem instance { get; private set; }
    public void OnEnable() { instance = this; }

    private bool useOverlay = true;
    public GameObject overlayObject;
    public GameObject hudObject;
    public GameObject HUDLevelText;
    public Slider HUDHealthSlider;
    public Slider HUDManaSlider;
    public GameObject HUDCurrencyText;

    void Start()
    {
        if(SystemInfo.deviceType != DeviceType.Handheld){
            useOverlay = false;
            overlayObject.SetActive(false);
        }    

        InvokeRepeating("Refresh", 0.0f, 1f);    
    }

    void Refresh()
    {
        GameObject player = GameObject.Find("Player");
        if(player == null)
            return;

        Character character = player.GetComponent<Character>();

        HUDLevelText.GetComponent<TMP_Text>().SetText("Lvl. "+character.level);

        HUDHealthSlider.minValue = 0;
        HUDHealthSlider.maxValue = character.characterClass.maxHP;
        HUDHealthSlider.value = character.currentHP;

        HUDManaSlider.minValue = 0;
        HUDManaSlider.maxValue = character.characterClass.maxMana;
        HUDManaSlider.value = character.currentMana;   
    }

    public void ToggleMute() {
        SettingsSystem.instance.ToggleMute();
    }

    public void MainMenu() {
        SaveSystem.SaveAndDeregister();
        SceneManager.LoadScene(sceneName: "Menu");
    }

    public void DisableControls()
    {
        hudObject.SetActive(false);
        if(useOverlay && overlayObject != null) {
            overlayObject.SetActive(false);
        }
    }

    public void EnableControls()
    {
        hudObject.SetActive(true);
        if(useOverlay && overlayObject != null) {
            overlayObject.SetActive(true);
        }
    }
}
