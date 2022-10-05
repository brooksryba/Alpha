using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LocalizationData : MonoBehaviour
{
    public string lookupCSV;
    public Dictionary<string, string> nameLookup;
    public string[] nameLookupColumns;
    void Start()
    {
        lookupCSV = "/Assets/Classes/Models/Persisted/";
        if(Application.systemLanguage == SystemLanguage.English){
            lookupCSV += "en.csv";
        } 
        else {
            lookupCSV += "en.csv";
        }
        ReadCSV(lookupCSV);
    }


    void ReadCSV(string csvFile, char delimiter=',')
    {
        nameLookup = new Dictionary<string, string>();
        string[] Lines = File.ReadAllLines(csvFile);
        nameLookupColumns = Lines[0].Split(delimiter);
        for (int i=1; i<=Lines.Length-1; i++)
        {
            string[] splitData = Lines[i].Split(delimiter);
            nameLookup.Add(splitData[0], splitData[1]);
        }
    }

}
