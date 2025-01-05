using UnityEngine;
using System.Collections;

public class TutorialText : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        // rotate health bar to face the camera/player
        this.transform.LookAt(Camera.main.transform.position);
    }
}
