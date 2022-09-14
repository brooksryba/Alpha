using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ToastSystem : MonoBehaviour
{
    int timeout = 2;
    // Start is called before the first frame update
    void Start()
    {
        Close();
    }

    public void Open(string message)
    {
        transform.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(0).transform.GetChild(0).GetComponent<TMP_Text>().SetText(message);
        StartCoroutine(TimedClose());
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
