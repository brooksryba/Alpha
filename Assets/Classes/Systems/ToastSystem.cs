using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ToastSystem : MonoBehaviour
{
    private static ToastSystem _instance;
    public static ToastSystem instance { get { return _instance; } }

    public GameObject toastObject;
    public GameObject textObject;

    private int timeout = 5;

    private void Awake() { _instance = this; }

    void Start()
    {
        Close();
    }

    public void Open(string message, bool doTimeout=true)
    {
        textObject.GetComponent<TMP_Text>().SetText(message);

        if(doTimeout)
        {
            toastObject.GetComponent<RectTransform>().anchoredPosition = new Vector3(0f, 50f, 0f);
            toastObject.SetActive(true);
            LeanTween.moveY(toastObject.GetComponent<RectTransform>(), -50f, .2f);            

            StopCoroutine(TimedClose());
            StartCoroutine(TimedClose());
        } else {
            toastObject.GetComponent<RectTransform>().anchoredPosition = new Vector3(0f, -50f, 0f);
            toastObject.SetActive(true);            
        }
    }

    public void Close()
    {
        LeanTween.moveY(toastObject.GetComponent<RectTransform>(), 50f, .2f).setOnComplete(() =>  toastObject.gameObject.SetActive(false));
    }

    IEnumerator TimedClose()
    {
        yield return new WaitForSeconds(timeout);
        Close();
    }
}
