using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconPivot : MonoBehaviour
{
    [Tooltip("The floating icon pivot transform")]
    public Transform iconPivot;

    // Update is called once per frame
    void Update()
    {
        // rotate health bar to face the camera/player
        iconPivot.LookAt(Camera.main.transform.position);
    }
}
