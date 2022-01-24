using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogManager : MonoBehaviour
{
    public TextMeshProUGUI NPCName;
    public TextMeshProUGUI DialogText;
    public GameObject canvas;

    private Queue<string> sentences;

    GameObject[] bubbles;
    SpriteRenderer spBubble;

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartDialog(Dialog dialog) {
        Debug.Log("Conversation started");

        sentences.Clear();
        NPCName.text = dialog.NPCName;

        foreach (string sentence in dialog.sentences) {
            sentences.Enqueue(sentence);
        }

        canvas.SetActive(true);

        DisplayNextSentence();
    }

    public void DisplayNextSentence() {
        if (sentences.Count == 0) {
            EndDialog();
            return;
        }
        string sentence = sentences.Dequeue();
        Debug.Log(sentence);
        DialogText.text = sentence;
    }

    void EndDialog() {
        canvas.SetActive(false);
        Debug.Log("Conversation ended");
    }
}
