using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ToastSystem : MonoBehaviour
{
    public static ToastSystem instance { get; private set; }
    private void OnEnable() { instance = this; }

    public GameObject toastObject;
    public GameObject textObject;

    private int timeout = 5;
    public bool complete = true;
    private List<string> queue = new List<string>();


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

    public void Queue(string message)
    {
        queue.Add(message);
        if(queue.Count == 1)
            StartCoroutine(TimedQueue());
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

    IEnumerator TimedQueue()
    {
        if(queue.Count > 0) {
            complete = false;
            textObject.GetComponent<TMP_Text>().SetText(queue[0]);

            yield return new WaitForSeconds(timeout/2);
            queue.RemoveAt(0);

            if(queue.Count > 0) {
                StartCoroutine(TimedQueue());
            } else {
                complete = true;
            }
        }
    }
}
