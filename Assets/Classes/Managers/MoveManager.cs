using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MoveManager 
{
    public static Dictionary<string, Move> refs;

    public static void LoadData(){
        refs = CsvReader.ReadMoveCsv();
    }
    public static Move Get(string id) 
    {
        return refs[id];
    }

}
