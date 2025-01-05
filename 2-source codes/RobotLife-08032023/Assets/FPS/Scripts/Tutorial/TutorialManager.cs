using System.Collections;
using System.Collections.Generic;
using Unity.FPS.Game;
using Unity.FPS.Gameplay;
using UnityEngine;
using Unity.FPS.UI;
using UnityEngine.SceneManagement;


    public class TutorialManager : MonoBehaviour
    {
        [Tooltip("UI rectangle where to display the messages")]
        public UITable DisplayMessageRect;

    [SerializeField]
    TutorialNotification startNotification;

    [SerializeField]
    PlayerCharacterController m_CharacterController;

    [SerializeField]
    PlayerInputHandler m_InputHandler;

    private void Start()
    {
        // initial notification
        startNotification.Initialized = true;
        DisplayMessageRect.UpdateTable(startNotification.gameObject);
    }

    private void Update()
    {
        Vector3 worldspaceMoveInput = m_CharacterController.transform.TransformVector(m_InputHandler.GetMoveInput());
        if (worldspaceMoveInput != Vector3.zero) // if the player moved
            startNotification.endTrigger = true;
    }

    public void DisplayTutorialRule(TutorialNotification tutorialNotification)
        {
            tutorialNotification.Initialized = true;
            DisplayMessageRect.UpdateTable(tutorialNotification.gameObject);
        }
}
