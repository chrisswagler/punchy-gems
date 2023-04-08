using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundaryBehavior : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("IcoShape"))
        {
            other.gameObject.GetComponent<IcoShapeBehavior>().DestroyGameObject();
        }
    }
}
