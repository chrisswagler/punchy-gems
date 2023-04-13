using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBehavior : MonoBehaviour
{
    public Material doorMaterial;
    public float travelSpeed = 0.1f;

    MeshRenderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<MeshRenderer>();
        _renderer.material = doorMaterial;
    }

    void Update()
    {
        Move();
    }

    // move the target towards the end of the platform
    public void Move()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * travelSpeed);
    }

    public void DestroyGameObject()
    {
        Destroy(gameObject);
    }
}
