using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Ink.Runtime;
using UnityEngine.SceneManagement;

public class DialogSystem : MonoBehaviour
{
    private static DialogSystem _instance;
    public static DialogSystem instance { get { return _instance; } }

    private Story currentStory;
    public bool dialogueIsPlaying { get; private set; }
    public bool needToMakeChoice;
    public string choiceString;
    public Action<String> eventCallback;
    public Action exitCallback;

    private void Awake() { _instance = this; }

    void Start()
    {
        Close();
        needToMakeChoice = false;
        dialogueIsPlaying = false;
    }

    public void Open(string message, Action callback = null)
    {
        if(transform.GetChild(0).gameObject.activeInHierarchy){
            Close();
        }
        
        transform.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(0).transform.GetChild(0).GetComponent<TMP_Text>().SetText(message);
    }

    public void Close()
    {
        transform.GetChild(0).transform.GetChild(0).GetComponent<TMP_Text>().SetText("");
        transform.GetChild(0).gameObject.SetActive(false);
    }

    public void EnterDialogueMode(TextAsset inkJSON, Action<String> _eventCallback, Action _exitCallback){
        if (!dialogueIsPlaying){
            currentStory = new Story(inkJSON.text);
            dialogueIsPlaying = true;
            eventCallback = _eventCallback;
            exitCallback = _exitCallback;
        }
    }

    public void ContinueStory()
    {
        if (currentStory.canContinue && !needToMakeChoice){
            string displayText = currentStory.Continue();
            Open(displayText, null);
            RunTextActions();
            CreateChoiceMenu();
        }
        else if (!currentStory.canContinue && !needToMakeChoice) {
            ExitDialogueMode();
        }
    }

    public string GetStoryCurrentPath()
    {
        return currentStory.currentFlowName;
    }
    
    public void SetStoryCurrentPath(string path) 
    {
        currentStory.ChoosePathString(path);
    }

    private void ExitDialogueMode(){
        dialogueIsPlaying = false;
        Close();
        exitCallback();
    }

    public void SetChoiceString(string choice){
        choiceString = choice;
    }

    public void MakeChoice(){
        int index = 0;
        List<Choice> currentChoices = currentStory.currentChoices;
        foreach(Choice choice in currentChoices){
            if(choice.text == choiceString){
                currentStory.ChooseChoiceIndex(index);
            }
            index++;
        }
        
        needToMakeChoice = false;
        ContinueStory();
    }
    public void CreateChoiceMenu()
    {
        List<Choice> currentChoices = currentStory.currentChoices;
        if (currentChoices.Count == 0) return;

        needToMakeChoice = true;

        if( GameObject.Find("Menu(Clone)") ) { return; }
        
        GameObject obj = Instantiate(Resources.Load("Prefabs/Widgets/Menu"), transform.position, transform.rotation) as GameObject;
        DynamicMenu menu = obj.GetComponent<DynamicMenu>();

        Dictionary<string, Action> choiceDictionary = new Dictionary<string, Action>();

        foreach(Choice choice in currentChoices){
            choiceDictionary.Add(choice.text, () =>  {SetChoiceString(choice.text); MakeChoice();} );
        }

        menu.Open(choiceDictionary);
    }

    public void RunTextActions() {
        foreach(String tag in currentStory.currentTags) {
            eventCallback(tag);
        }
    }
}
