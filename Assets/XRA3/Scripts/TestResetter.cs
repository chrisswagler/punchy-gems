using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestResetter : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.Get(OVRInput.Button.One))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
