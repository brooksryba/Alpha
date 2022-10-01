using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorySystem : MonoBehaviour
{
    private static StorySystem _instance;
    public static StorySystem instance { get { return _instance; } }

    public int chapter = 1;
    public int mark = 1;

    private Dictionary<int, int> _index;

    private void Awake() { _instance = this; }

    public void Start()
    {
        SaveSystem.Register("StoryData", () => { SaveState(); });

        BuildIndex();
        LoadState();
        UpdateFromChapter();
    }

    public void SaveState()
    {
        SaveSystem.SaveState<StoryData>(new StoryData(this), "StoryData");
    }

    public void LoadState()
    {
        StoryData data = SaveSystem.LoadState<StoryData>("StoryData") as StoryData;

        if (data != null)
        {
            chapter = data.chapter;
            mark = data.mark;
        }
    }    

    public void BuildIndex()
    {
        _index = new Dictionary<int, int>();

        foreach (TextAsset asset in Resources.LoadAll("Dialogue/Chapters", typeof(TextAsset))) {
            string[] parts = asset.name.Split('_');
            int chapter = int.Parse(parts[1]);
            int mark = int.Parse(parts[2]);
            if(!_index.ContainsKey(chapter)) { _index.Add(chapter, 0);}
            _index[chapter] += 1;
        }
    }

    public void MoveToNextMark()
    {
        if(_index.Count < chapter)
            return;
            
        if(_index[chapter] > mark) {
            mark += 1;
        } else {
            chapter += 1;
            mark = 1;
        }

        UpdateFromChapter();
    }

    public void UpdateFromChapter()
    {
        UpdateBlocks();
        UpdateCutscenes();
    }

    private void UpdateBlocks()
    {
        GameObject parent = GameObject.Find("Blocks");
        if(parent != null) {
            foreach(Component block in parent.GetComponentsInChildren(typeof(Transform), true))
            {
                if(block.gameObject.name == "Blocks") { continue; }
                block.gameObject.SetActive(block.gameObject.name == "Block_Chapter_"+chapter+"_"+mark);
            }
        }
    }

    private void UpdateCutscenes()
    {
        GameObject parent = GameObject.Find("Cutscenes");
        if(parent != null) {
            foreach(Component cutscene in parent.GetComponentsInChildren(typeof(Transform), true))
            {
                if(cutscene.gameObject.name == "Cutscenes") { continue; }
                cutscene.gameObject.SetActive(cutscene.gameObject.name == "Cutscene_Chapter_"+chapter+"_"+mark);
            }
        }
    }
}
