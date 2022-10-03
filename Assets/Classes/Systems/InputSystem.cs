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
    private bool inventoryOpen = false;
    public GameObject overlayObject;
    public GameObject hudObject;
    public GameObject inventoryObject;
    public List<GameObject> partySlots;
    public List<GameObject> itemSlots;
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

        InvokeRepeating("RefreshHUD", 0.0f, 1f);    
    }

    void RefreshHUD()
    {
        GameObject player = GameObject.Find("Player");
        if(player == null)
            return;

        Character character = player.GetComponent<Character>();
        if(character.characterClass == null)
            return;

        if(HUDLevelText != null) {
            HUDLevelText.GetComponent<TMP_Text>().SetText("Lvl. "+character.level);
        }

        if(HUDHealthSlider != null) {
            HUDHealthSlider.minValue = 0;
            HUDHealthSlider.maxValue = character.characterClass.maxHP;
            HUDHealthSlider.value = character.currentHP;
        }

        if(HUDManaSlider != null) {
            HUDManaSlider.minValue = 0;
            HUDManaSlider.maxValue = character.characterClass.maxMana;
            HUDManaSlider.value = character.currentMana;   
        }
    }

    void RefreshInventory()
    {
        GameObject player = GameObject.Find("Player");
        if(player == null)
            return;

        Character character = player.GetComponent<Character>();

        for(int index=0; index<itemSlots.Count; index++) {
            Transform slotTransform = itemSlots[index].transform;
            if(slotTransform.childCount > 0)
                Destroy(slotTransform.GetChild(0).gameObject);

            if(index < character.items.Count) {
                ItemData item = character.items[index];
                GameObject prefab = Resources.Load("Prefabs/Items/"+item.title) as GameObject;
                GameObject inst = GameObject.Instantiate(prefab, slotTransform); 
                Destroy(inst.GetComponent<InventoryItem>());
            }
        }     

        for(int index=0; index<partySlots.Count; index++) {
            Transform slotTransform = partySlots[index].transform;
            if(slotTransform.childCount > 0)
                Destroy(slotTransform.GetChild(0).gameObject);

            if(index < character.partyMembers.Count) {
                string path = character.partyMembers[index];
                GameObject prefab = Resources.Load("Prefabs/"+path) as GameObject;
                GameObject inst = GameObject.Instantiate(prefab, slotTransform); 
                Destroy(inst.GetComponent<CharacterMovement>());
                Destroy(inst.GetComponent<BattleMovement>());
                Destroy(inst.GetComponent<BattleSpriteController>());
            }
        }             
    }

    public void UseItem(int index) {
        GameObject player = GameObject.Find("Player");
        if(player == null)
            return;

        Character character = player.GetComponent<Character>();  

        if(index >= character.items.Count)
            return;

        string message = "";
        if( WorldItems.lookup.ContainsKey(character.items[index].title) ) {
            InventoryItemData itemData = WorldItems.lookup[character.items[index].title];
            message = itemData.Execute(character);
        } else {
            message = "Can't use this item now.";
        }

        ToastSystem.instance.Open(message);
        ToggleInventory();
    }

    public void ToggleInventory()
    {   
        RefreshInventory();
        inventoryOpen = !inventoryOpen;
        inventoryObject.SetActive(!inventoryObject.activeInHierarchy);
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
        inventoryObject.SetActive(false);
        if(useOverlay && overlayObject != null) {
            overlayObject.SetActive(false);
        }
    }

    public void EnableControls()
    {
        hudObject.SetActive(true);
        inventoryObject.SetActive(inventoryOpen);
        if(useOverlay && overlayObject != null) {
            overlayObject.SetActive(true);
        }
    }
}
