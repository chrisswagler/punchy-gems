using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBehavior : MonoBehaviour
{
    public GameObject gemPrefab;
    public Transform playerTransform;
    public float buffer = 4.0f;
    public Transform groundTransform;
    public float floorBuffer = 1.0f;

    public void SpawnGem()
    {
        // Randomize x, y position based on the player's transform
        // Make sure they don't spawn below the ground
        Vector3 gemPos = transform.position;
        gemPos.x = Random.Range(playerTransform.position.x - buffer, playerTransform.position.x + buffer);
        gemPos.y = Random.Range(playerTransform.position.y - buffer, playerTransform.position.y + buffer);
        gemPos.y = Mathf.Max(gemPos.y, groundTransform.position.y + floorBuffer);


        GameObject gem = Instantiate(gemPrefab, gemPos, transform.rotation);

        var randomController = Random.Range(0, 2) == 1 ? OVRInput.Controller.LTouch : OVRInput.Controller.RTouch;
        gem.GetComponent<IcoShapeBehavior>().AssignGem(randomController);
    }

    public void SpawnObstacle()
    {

    }
}
