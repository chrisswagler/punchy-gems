using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcoShapeBehavior : MonoBehaviour
{
    public GameObject smallBrokenPrefab;
    public GameObject mediumBrokenPrefab;
    public GameObject largeBrokenPrefab;
    public OVRInput.Controller correctController;
    public int basePoints = 50;
    public DebugCanvasBehavior debugCanvas;

    MeshRenderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<MeshRenderer>();
    }

    // The colliding controller will call this method on collision
    public void HandleCollision(OVRInput.Controller collidingController)
    {
        // Check if the controller is not the correct one
        if (collidingController != correctController)
        {
            //
            print("incorrect controller");
            Debug("incorrect controller");
            return;
        }

        // The controller was correct, now execute behavior

        // Get the velocity and magnitude of it from the controller
        Vector3 controllerVelocity = OVRInput.GetLocalControllerVelocity(collidingController);
        float controllerVelocityMagnitude = controllerVelocity.magnitude;
        print("controller v mag: " + controllerVelocityMagnitude);
        Debug("controller v mag: " + controllerVelocityMagnitude);
        int points = basePoints + (int)(10 * controllerVelocityMagnitude);

        // The punch was too weak
        if (controllerVelocityMagnitude < 0.5)
        {
            
        }
        // The punch was aight
        else if (controllerVelocityMagnitude < 1)
        {
            // hide this object and actually destroy later
            BreakToPieces(smallBrokenPrefab);
        }
        // Decent punch
        else if (controllerVelocityMagnitude < 2)
        {
            BreakToPieces(mediumBrokenPrefab);
        }
        // Huge punch
        else
        {
            BreakToPieces(largeBrokenPrefab);
        }
    }

    private void BreakToPieces(GameObject brokenPrefab)
    {
        // hide this object and actually destroy later
        gameObject.SetActive(false);
        Invoke("DestroyGameObject", 3.0f);

        // break into pieces
        GameObject exploded = Instantiate(brokenPrefab, transform.position, transform.rotation);

        // set the color of the pieces to the same as this

        var rendererPieces = exploded.GetComponentsInChildren<MeshRenderer>();

        foreach (MeshRenderer renderer in rendererPieces)
        {
            renderer.material = _renderer.material;
        }

        var rbPieces = exploded.GetComponentsInChildren<Rigidbody>();

        foreach (Rigidbody rb in rbPieces)
        {
            rb.AddExplosionForce(10.0f, transform.position, 5.0f, 0.0f, ForceMode.Impulse);
            Debug("applying the explosion force");
        }
    }

    private void DestroyGameObject()
    {
        Destroy(gameObject);
    }

    private void Debug(string message)
    {
        if (debugCanvas)
        {
            debugCanvas.Debug(message);
        }
    }
}
