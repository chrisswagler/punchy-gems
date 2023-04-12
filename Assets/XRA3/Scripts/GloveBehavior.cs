using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GloveBehavior : MonoBehaviour
{
    public HapticFeedback haptics;
    public AudioClip collisionSFX;
    public OVRInput.Controller controller;

    private void OnTriggerEnter(Collider other)
    {
        // Play the collision sfx
        AudioSource.PlayClipAtPoint(collisionSFX, transform.position);
        // Apply haptic feedback to this controller
        haptics.ApplyHapticFeedback(controller, collisionSFX);

        // If we collide with an IcoShape, call its handle collision
        if (other.CompareTag("IcoShape"))
        {
            other.gameObject.GetComponent<IcoShapeBehavior>().HandleCollision(controller);
        }
        else if (other.CompareTag("MenuIcoShape"))
        {
            other.gameObject.GetComponent<MenuIcoShapeBehavior>().HandleCollision(controller);
        }
    }
}
