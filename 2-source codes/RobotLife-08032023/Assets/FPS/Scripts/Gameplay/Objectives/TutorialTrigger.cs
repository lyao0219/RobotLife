using UnityEngine;
using System.Collections;
using Unity.FPS.Gameplay;

public class TutorialTrigger : MonoBehaviour
{
    public int triggerNumber = 0;
    public bool instructionNeeded = false;

    ObjectiveTutorial tutorialManager;

    private void Start()
    {
        tutorialManager = FindObjectOfType<ObjectiveTutorial>();
    }

    private void OnTriggerEnter(Collider other)
    {
        tutorialManager.displayNewInstruction(triggerNumber, instructionNeeded);
    }
}
