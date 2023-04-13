using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorColliderBehavior : MonoBehaviour
{
    public int pointPenalty = -100;

    public void HandleCollision(OVRInput.Controller controller)
    {
        GameObject.FindObjectOfType<LevelManager>().UpdateScore(pointPenalty);
    }
}
