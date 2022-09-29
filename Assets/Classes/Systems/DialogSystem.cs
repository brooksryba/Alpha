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
    private bool needToMakeChoice;
    private string choiceString;
    private Action<String> eventCallback;
    private Action exitCallback;

    public GameObject dialogObject;
    public GameObject textObject;

    private void Awake() { _instance = this; }

    void Start()
    {
        needToMakeChoice = false;
        dialogueIsPlaying = false;
    }

    public void Open(string message, Action callback = null)
    {
        if(dialogObject.activeInHierarchy){
            dialogObject.GetComponent<RectTransform>().anchoredPosition = new Vector3(0f, 50f, 0f);
        } else {
            dialogObject.GetComponent<RectTransform>().anchoredPosition = new Vector3(0f, -50f, 0f);
            dialogObject.SetActive(true);
            LeanTween.moveY(dialogObject.GetComponent<RectTransform>(), 50f, 1f);               
        }
        
        textObject.GetComponent<TMP_Text>().SetText(message);
    }

    public void Close()
    {
        LeanTween.moveY(dialogObject.GetComponent<RectTransform>(), -50f, 1f).setOnComplete(() => dialogObject.gameObject.SetActive(false));
    }

    public void EnterDialogueMode(TextAsset inkJSON, Action<String> _eventCallback, Action _exitCallback){
        if (!dialogueIsPlaying){
            currentStory = new Story(inkJSON.text);
            dialogueIsPlaying = true;
            eventCallback = null;
            exitCallback = null;
            eventCallback = _eventCallback;
            exitCallback = _exitCallback;
        }
    }

    public void ContinueStory()
    {
        if(CutsceneSystem.instance.cutsceneIsPlaying && CutsceneSystem.instance.cutsceneInEvent) { return; }

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
            choiceDictionary.Add("> "+choice.text, () =>  {SetChoiceString(choice.text); MakeChoice();} );
        }

        menu.OpenWithTag(choiceDictionary);
        PositionChoiceMenu();
    }

    public void PositionChoiceMenu()
    {
        if( !GameObject.Find("Menu(Clone)") ) { return; }
        GameObject obj = GameObject.Find("MenuList");
        Camera cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        obj.transform.position = GameObject.Find("DialogItem").transform.position;
        obj.transform.position += new Vector3(0, 75 * obj.transform.lossyScale.y, 0);
    }    

    public void RunTextActions() {
        foreach(String tag in currentStory.currentTags) {
            eventCallback(tag);
        }
    }
}
