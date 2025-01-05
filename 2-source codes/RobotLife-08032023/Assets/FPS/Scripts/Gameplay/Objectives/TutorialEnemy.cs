using UnityEngine;
using System.Collections;
using Unity.FPS.Game;
using Unity.FPS.Gameplay;
using Unity.FPS.UI;

public class TutorialEnemy : MonoBehaviour
{
    public int triggerNumber = 0;
    public bool instructionNeeded = false;

    ObjectiveTutorial tutorialManager;

    // Use this for initialization
    void Start()
    {
        EventManager.AddListener<EnemyKillEvent>(OnKill);
        tutorialManager = FindObjectOfType<ObjectiveTutorial>();
    }

   
    void OnKill(EnemyKillEvent evt)
    {
        tutorialManager.displayNewInstruction(triggerNumber, instructionNeeded);
    }


    void OnDestroy()
    {
        EventManager.RemoveListener<EnemyKillEvent>(OnKill);
    }
}
