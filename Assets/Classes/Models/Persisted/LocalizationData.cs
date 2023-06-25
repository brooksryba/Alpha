using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class LocalizationData
{
    private static string path;
    public static Dictionary<string, string> data;
    public static string[] dataColumns;
    
    static LocalizationData()
    {
        path = "Assets/Classes/Models/Persisted/";
        if(Application.systemLanguage == SystemLanguage.English){
            path += "en.csv";
        } 
        else {
            path += "en.csv";
        }
        ReadCSV(path);
    }


    static void ReadCSV(string csvFile, char delimiter=',')
    {
        data = new Dictionary<string, string>();
        string[] Lines = File.ReadAllLines(csvFile);
        dataColumns = Lines[0].Split(delimiter);
        for (int i=1; i<=Lines.Length-1; i++)
        {
            string[] splitData = Lines[i].Split(delimiter);
            data.Add(splitData[0], splitData[1]);
        }
    }

}
