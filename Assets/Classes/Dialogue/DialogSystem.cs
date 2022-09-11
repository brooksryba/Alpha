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
    int timeout = 2;
    private Story currentStory;
    public bool dialogueIsPlaying { get; private set; }
    public bool needToMakeChoice;
    public string choiceString;



    void Start()
    {
        Close();
        needToMakeChoice = false;
        dialogueIsPlaying = false;
    }

    private void Update()
    {
        if (!dialogueIsPlaying){
            return;
        }

    // TODO this should be handled elsewhere, but needs to be in the update
        if (Input.GetKeyUp(KeyCode.E)) {
            ContinueStory();
        }
    }

    public void Open(string message, Action callback = null)
    {
        if(transform.GetChild(0).gameObject.active){
            Close();
        }
        
        transform.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(0).transform.GetChild(0).GetComponent<TMP_Text>().SetText(message);
        //StartCoroutine(TimedClose(timeout, callback));
    }

    public void Next(Character character, Action callback = null)
    {
        if(character.dialogText.Count > 0) {
            string message = character.title + ": " + character.dialogText[character.dialogIndex];
            if(character.dialogIndex + 1 < character.dialogText.Count) {
                character.dialogIndex++;
            }

            Open(message, callback);
        } else {
            callback();
        }
    }

    public void Close()
    {
        transform.GetChild(0).transform.GetChild(0).GetComponent<TMP_Text>().SetText("");
        transform.GetChild(0).gameObject.SetActive(false);
    }

    IEnumerator TimedClose(int duration, Action callback = null)
    {
        yield return new WaitForSeconds(duration);
        Close();

        if(callback != null) { callback(); }
    }

    public void EnterDialogueMode(TextAsset inkJSON){
        if (!dialogueIsPlaying){
            currentStory = new Story(inkJSON.text);
            dialogueIsPlaying = true;
            ContinueStory();
        }
    }

    private void ExitDialogueMode(){
        dialogueIsPlaying = false;
        Close();
    }

    private void ContinueStory()
    {
        if (currentStory.canContinue && !needToMakeChoice){
            string displayText = currentStory.Continue();
            Open(displayText, null);
            CreateChoiceMenu();
        }
        else if (!currentStory.canContinue && !needToMakeChoice) {
            ExitDialogueMode();
        }
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
    }
    public void CreateChoiceMenu()
    {
        List<Choice> currentChoices = currentStory.currentChoices;
        if (currentChoices.Count == 0) return;

        needToMakeChoice = true;

        if( GameObject.Find("Menu(Clone)") ) { return; }
        
        GameObject obj = Instantiate(Resources.Load("Prefabs/Menu"), transform.position, transform.rotation) as GameObject;
        DynamicMenu menu = obj.GetComponent<DynamicMenu>();

        Dictionary<string, Action> choiceDictionary = new Dictionary<string, Action>();

        foreach(Choice choice in currentChoices){
            choiceDictionary.Add(choice.text, () =>  {SetChoiceString(choice.text); MakeChoice();} );
        }

        menu.Open(choiceDictionary);
    }


}
