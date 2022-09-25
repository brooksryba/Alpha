using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public StoryData storyData;
    public PlayerLocationData playerLocationData;
    public List<CharacterData> characterData;
    public List<ItemData> itemData;

    public void ToDisk()
    {
        if(storyData != null)
            SaveSystem.SaveState<StoryData>(storyData, "StoryData");
        if(playerLocationData != null)
            SaveSystem.SaveState<PlayerLocationData>(playerLocationData, "PlayerLocation");
        if(characterData != null) {
            foreach(CharacterData data in characterData) {
                SaveSystem.SaveState<CharacterData>(data, data.name);
            }
        }
        if(itemData != null) {
            foreach(ItemData data in itemData) {
                SaveSystem.SaveState<ItemData>(data, data.title);
            }            
        }
    }
}
