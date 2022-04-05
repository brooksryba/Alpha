using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class DynamicMenuItem : MonoBehaviour
{
    public GameObject menuText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetLabel(string label)
    {
        menuText.GetComponent<TMP_Text>().SetText(label);
    }

    public void SetCallback(Action callback)
    {
        transform.GetComponent<Button>().onClick.AddListener(new UnityAction(callback));
    }    
}
