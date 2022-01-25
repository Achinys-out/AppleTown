using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{

    public string text = "Press <E> to interact.";
    public bool inRange = false;

    public KeyCode interactKey; // E key
    public UnityEvent interactAction;

    public bool interactionStarted = false;
    public KeyCode nextSentenceKey; //space key
    public UnityEvent interactAction2;

    GameObject[] bubbles;
    SpriteRenderer spBubble;

    private void Update()
    {
        if (inRange && Input.GetKeyDown(interactKey))
        {
            interactionStarted = true;
            interactAction.Invoke();
        }
        if (interactionStarted && Input.GetKeyDown(nextSentenceKey))
        {
            interactAction2.Invoke();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            inRange = true;
            Debug.Log("Player is in range of interactable object.");

            bubbles = GameObject.FindGameObjectsWithTag("PopUpText");

            Vector2 newPosition = this.transform.position;
            bubbles[0].transform.position = new Vector2(newPosition.x, newPosition.y + 0.35f);

            spBubble = bubbles[0].GetComponent<SpriteRenderer>();
            spBubble.enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            inRange = false;
            Debug.Log("Player left the range of interactable object.");

            spBubble.enabled = false;
        }
    }

}