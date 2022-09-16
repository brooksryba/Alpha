using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ToastSystem : MonoBehaviour
{
    private static ToastSystem _instance;
    public static ToastSystem instance { get { return _instance; } }

    int timeout = 5;

    private void Awake() { _instance = this; }

    void Start()
    {
        Close();
    }

    public void Open(string message, bool doTimeout=true)
    {
        transform.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(0).transform.GetChild(0).GetComponent<TMP_Text>().SetText(message);
        if(doTimeout)
        {
            StopCoroutine(TimedClose());
            StartCoroutine(TimedClose());
        }
    }

    public void Close()
    {
        transform.GetChild(0).transform.GetChild(0).GetComponent<TMP_Text>().SetText("");
        transform.GetChild(0).gameObject.SetActive(false);
    }

    IEnumerator TimedClose()
    {
        yield return new WaitForSeconds(timeout);
        Close();
    }
}
