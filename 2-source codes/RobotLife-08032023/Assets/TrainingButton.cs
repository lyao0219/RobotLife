using System.Collections;
using System.Collections.Generic;
using Unity.FPS.Game;
using UnityEngine;

public class TrainingButton : MonoBehaviour
{
    [SerializeField]
    private bool evil = false;

    public void DispatchTrainingEvent()
    {
        TrainingEvent evt = new TrainingEvent();
        evt.evil = evil;
        EventManager.Broadcast(evt);
    }
}
