using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{

    public string text = "Press <E> to interact.";
    public bool inRange = false;
    public KeyCode interactKey;
    public UnityEvent interactAction;

    private void Update()
    {
        if (inRange && Input.GetKeyDown(interactKey)) {
            interactAction.Invoke();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) {
            inRange = true;
            Debug.Log("Player is in range of interactable object.");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            inRange = false;
            Debug.Log("Player left the range of interactable object.");
        }
    }
}

