/* 
    ------------------- Code Monkey -------------------

    Thank you for downloading this package
    I hope you find it useful in your projects
    If you have any questions let me know
    Cheers!

               unitycodemonkey.com
    --------------------------------------------------
 */

using System;
using System.Collections;
using System.Collections.Generic;
using Unity.FPS.Game;
using Unity.FPS.UI;
using UnityEngine;
using UnityEngine.UI;

/*
 * Added to Player to Hold keys
 * */
public class DoorKeyHolder : MonoBehaviour 
{
    
    public event EventHandler OnDoorKeyAdded;
    public event EventHandler OnDoorKeyUsed;
    // The corresponding door of the key
    public GameObject door;
    // UI image of the key for feeback purposes
    public GameObject keyImage;



    [Header("Key Holder")]
    [Tooltip("List of Keys currently being held")]
    public List<Key> doorKeyHoldingList = new List<Key>();


    private void Start()
    {
        keyImage.SetActive(false);
    }

    void OnTriggerEnter(Collider collider) 
    {
        DoorKey doorKey = collider.GetComponent<DoorKey>();
        if (doorKey != null) 
        {
            // add the key to the list of keys of the player
            doorKeyHoldingList.Add(doorKey.key);

            // Display the Key Image on the UI for feedback
            keyImage.SetActive(true);
            DisplayMessageEvent evt = new DisplayMessageEvent();
            evt.Message = "Key picked up! Go find the Door";
            evt.DelayBeforeDisplay = 0f;
            EventManager.Broadcast(evt);

            // get the missionWaypoint component from the player instance to change the objective
            MissionWaypoint mission = GetComponent<MissionWaypoint>();
            // change the mission
            mission.changeObjective(door);

            //destroy the key
            doorKey.DestroySelf();
            OnDoorKeyAdded?.Invoke(this, EventArgs.Empty);
        }

        DoorLock doorLock = collider.GetComponent<DoorLock>();
        if (doorLock != null) 
        {
            if (doorKeyHoldingList.Contains(doorLock.key)) 
            {
                // Has key! Open door!
                doorLock.OpenDoor();
                if (doorLock.removeKeyOnUse) 
                {
                    doorKeyHoldingList.Remove(doorLock.key);
                }
                OnDoorKeyUsed?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    }

