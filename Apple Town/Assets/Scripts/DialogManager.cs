using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    public Text NPCName;
    public Text DialogText;
    public GameObject canvas;
    private PlayerController player;
    private float playerSpeed;
    

    private Queue<string> sentences;

    GameObject[] bubbles;
    SpriteRenderer spBubble;

    private void Awake()
    {
        player = PlayerManager.instance.player.GetComponent<PlayerController>();
        playerSpeed = player.speed;
    }

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialog(Dialog dialog) {
        Debug.Log("Conversation started");
        player.speed = 0;

        sentences.Clear();
        NPCName.text = dialog.NPCName;

        foreach (string sentence in dialog.sentences) {
            sentences.Enqueue(sentence);
        }

        canvas.SetActive(true);

        DisplayNextSentence();
    }

    public void DisplayNextSentence() {
        Debug.Log("SENTENCES COUNT: " + sentences.Count);
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
        player.speed = playerSpeed;
        Debug.Log("Conversation ended");
    }
}
