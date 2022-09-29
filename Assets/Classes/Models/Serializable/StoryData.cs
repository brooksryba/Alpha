using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StoryData {
    public int chapter;
    public int mark;

    public StoryData(StorySystem story) {
        chapter = story.chapter;
        mark = story.mark;
    }
}
