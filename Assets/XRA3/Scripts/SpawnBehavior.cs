using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBehavior : MonoBehaviour
{
    public GameObject gemPrefab;
    public GameObject doorPrefab;
    public Transform playerTransform;
    public float buffer = 4.0f;
    public Transform groundTransform;
    public float floorBuffer = 1.0f;

    // We don't want the obstacles to spawn on every single beat
    float obstacleSpawnChance = 0.2f;

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

    public void SpawnObstacleChance()
    {
        if (Random.Range(0.0f, 1.0f) <= obstacleSpawnChance)
        {
            SpawnObstacle();
        }
    }

    public void SpawnObstacle()
    {
        // Randomize x, y position based on the player's transform
        // Make sure they don't spawn below the ground
        Vector3 doorPos = transform.position;
        doorPos.x = Random.Range(playerTransform.position.x - buffer, playerTransform.position.x + buffer);
        doorPos.y = Random.Range(playerTransform.position.y, playerTransform.position.y);

        // Door has a weird transform. Correct offset in software
        doorPos.y -= 1.5f;
        doorPos.z += 10.0f;

        GameObject door = Instantiate(doorPrefab, doorPos, transform.rotation);
    }
}
