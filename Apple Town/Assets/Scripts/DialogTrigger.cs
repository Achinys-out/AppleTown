// based on https://www.youtube.com/watch?v=_nRzoTzeyxU

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
    public Dialog dialog;

    public void TriggerDialog() {
        FindObjectOfType<DialogManager>().StartDialog(dialog);
    }
}
