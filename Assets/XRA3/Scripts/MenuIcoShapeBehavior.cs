using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuIcoShapeBehavior : MonoBehaviour
{
    public enum ButtonType
    {
        LoadSong1,
        LoadSong2,
        LoadSong3,
        Replay,
        Return
    }

    public GameObject brokenPrefab;
    public ButtonType type;
    public float floatSpeed = 1f;
    public float floatHeight = 0.2f;

    MeshRenderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Float();
    }

    void Float()
    {
        //get the objects current position and put it in a variable so we can access it later with less code
        Vector3 pos = transform.position;
        //calculate what the new Y position will be
        float newY = pos.y + Mathf.Sin(Time.time * floatSpeed) * floatHeight;
        //set the object's Y to the new calculated Y
        transform.position = new Vector3(pos.x, newY, pos.z);
    }

    public void HandleCollision(OVRInput.Controller collidingController)
    {
        Vector3 controllerVelocity = OVRInput.GetLocalControllerVelocity(collidingController);
        float controllerVelocityMagnitude = controllerVelocity.magnitude;

        if (controllerVelocityMagnitude < 0.5)
        {
            return;
        }

        BreakToPieces();
        // Give the pieces breaking a sec before starting
        Invoke("TriggerMenuAction", 1.0f);
    }

    private void TriggerMenuAction()
    {
        // Load the correct scene based on this type of button
        switch (type)
        {
            case ButtonType.LoadSong1:
                SceneManager.LoadScene(1);
                break;
            case ButtonType.LoadSong2:
                SceneManager.LoadScene(2);
                break;
            case ButtonType.LoadSong3:
                SceneManager.LoadScene(3);
                break;
            case ButtonType.Replay:
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                break;
            case ButtonType.Return:
                SceneManager.LoadScene(0);
                break;
        }
    }

    private void BreakToPieces()
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
        }
    }
}
