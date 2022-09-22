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
        toastObject.gameObject.SetActive(true);
        textObject.GetComponent<TMP_Text>().SetText(message);
        if(doTimeout)
        {
            StopCoroutine(TimedClose());
            StartCoroutine(TimedClose());
        }
    }

    public void Close()
    {
        textObject.GetComponent<TMP_Text>().SetText("");
        toastObject.gameObject.SetActive(false);
    }

    IEnumerator TimedClose()
    {
        yield return new WaitForSeconds(timeout);
        Close();
    }
}
