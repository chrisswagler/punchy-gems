using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HapticFeedback : MonoBehaviour
{

    public void ApplyHapticFeedback(OVRInput.Controller controller, AudioClip hapticSFX)
    {
        OVRHapticsClip hapticsClip = new OVRHapticsClip(hapticSFX);

        if (controller == OVRInput.Controller.LTouch)
        {
            OVRHaptics.LeftChannel.Preempt(hapticsClip);
        }
        else
        {
            OVRHaptics.RightChannel.Preempt(hapticsClip);
        }
    }
}
