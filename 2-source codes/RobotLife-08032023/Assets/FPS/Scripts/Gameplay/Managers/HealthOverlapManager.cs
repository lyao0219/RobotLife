using UnityEngine;
using System.Collections;

public class HealthOverlapManager : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Transform camera = Camera.main.transform;
        var lookPos = camera.position - transform.position;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime);

        // rotate health bar to face the camera/player
        //this.transform.LookAt(Camera.main.transform.position);
    }
}
