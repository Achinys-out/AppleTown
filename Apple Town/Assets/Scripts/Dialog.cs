// based on https://www.youtube.com/watch?v=_nRzoTzeyxU

using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.SceneManagement;

[System.Serializable]
public class Dialog
{
    public string NPCName;
    [TextArea(3,10)]
    public string[] sentences;

}
