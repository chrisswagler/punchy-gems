using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcoShapeBehavior : MonoBehaviour
{
    public GameObject smallBrokenPrefab;
    public GameObject mediumBrokenPrefab;
    public GameObject largeBrokenPrefab;
    public OVRInput.Controller correctController;
    public Material leftMaterial;
    public Material rightMaterial;
    public int basePoints = 50;
    public DebugCanvasBehavior debugCanvas;
    public float travelSpeed = 0.1f;
    public AudioClip correctSFX;
    public AudioClip incorrectSFX;


    MeshRenderer _renderer;
    Rigidbody _rigidbody;
    bool stopMove = false;

    private void Awake()
    {
        _renderer = GetComponent<MeshRenderer>();
        _rigidbody = GetComponent<Rigidbody>();
        stopMove = false;
    }

    private void Update()
    {
        if (!stopMove)
        {
            Move();
        }
    }

    // The colliding controller will call this method on collision
    public void HandleCollision(OVRInput.Controller collidingController)
    {
        stopMove = true;
        // Check if the controller is not the correct one
        if (collidingController != correctController)
        {
            AudioSource.PlayClipAtPoint(incorrectSFX, transform.position);
            // Deduct points
            GameObject.FindObjectOfType<LevelManager>().UpdateScore(-1 * basePoints);
            Invoke("DestroyGameObject", 2.0f);
            _rigidbody.AddForce(Vector3.up + Vector3.forward, ForceMode.Impulse);
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

        // The punch was too weak
        if (controllerVelocityMagnitude < 0.5)
        {
            Invoke("DestroyGameObject", 2.0f);
            _rigidbody.AddForce(controllerVelocity, ForceMode.Impulse);
            return;
        }

        // Update points
        int points = basePoints + (int)(10 * controllerVelocityMagnitude);
        GameObject.FindObjectOfType<LevelManager>().UpdateScore(points);

        AudioSource.PlayClipAtPoint(correctSFX, transform.position);
        
        // The punch was aight
        if (controllerVelocityMagnitude < 1)
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

    public void DestroyGameObject()
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

    // Public method to assign which gem type this is
    public void AssignGem(OVRInput.Controller controller)
    {
        if (controller == OVRInput.Controller.LTouch)
        {
            correctController = controller;
            _renderer.material = leftMaterial;
        }
        else if (controller == OVRInput.Controller.RTouch)
        {
            correctController = controller;
            _renderer.material = rightMaterial;
        }
    }

    // move the target towards the end of the platform
    public void Move()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * travelSpeed);
    }
}
