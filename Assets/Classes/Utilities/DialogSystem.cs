using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogSystem : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Close();
    }

    public void Open(string message, Action callback = null)
    {
        transform.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(0).transform.GetChild(0).GetComponent<TMP_Text>().SetText(message);
        StartCoroutine(TimedClose(6, callback));
    }

    public void Next(Character character, Action callback = null)
    {
        if(character.dialogText.Count > 0) {
            string message = character.title + ": " + character.dialogText[character.dialogIndex];
            if(character.dialogIndex + 1 < character.dialogText.Count) {
                character.dialogIndex++;
                character.SaveState();
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
}
