using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ToastSystem : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Close();
    }

    public void Open(string message, int duration = 2)
    {
        transform.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(0).transform.GetChild(0).GetComponent<TMP_Text>().SetText(message);
        StartCoroutine(TimedClose(duration));
    }

    public void Close()
    {
        transform.GetChild(0).transform.GetChild(0).GetComponent<TMP_Text>().SetText("");
        transform.GetChild(0).gameObject.SetActive(false);
    }

    IEnumerator TimedClose(int duration)
    {
        yield return new WaitForSeconds(duration);
        Close();
    }
}
