using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DebugCanvasBehavior : MonoBehaviour
{
    public TMP_Text debugText;

    public void Debug(string message)
    {
        debugText.text = message;
    }
}
