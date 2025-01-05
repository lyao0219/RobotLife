using UnityEngine;
using System;
using Unity.FPS.Game;
using UnityEngine.UI;
using Unity.FPS.UI;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

namespace Unity.FPS.Gameplay
{

    public class ObjectiveTutorial : Objective
    {
        [SerializeField]
        private List<GameObject> objectives = new List<GameObject>();

        [SerializeField]
        private List<GameObject> objectivesInstructions = new List<GameObject>();

        [SerializeField]
        private MissionWaypoint waypoint;

        [SerializeField]
        private int numberOfTriggers = 10;

        int currentObjectiveIndex = 0;
        int currentInstructionIndex = 0;

        // instruction related
        float fadeInTime = 4f;
        float time = 0f;
        bool updateInstruction = false;

        protected override void Start()
        {
            //base.Start();

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            // assign first objective
            if (objectives.Count != 0)
            {
                waypoint.changeObjective(objectives[0]);
                updateInstruction = true;
            }

            EventManager.Broadcast(new StartTutorialEvent());

        }

        public void displayNewInstruction(int triggerNumber, bool instructionNeeded)
        {
            if (triggerNumber == numberOfTriggers)
            {
                EventManager.Broadcast(new EndTutorialEvent());
                SceneManager.LoadScene("IntroductionTraining");
            }

            if(triggerNumber == currentObjectiveIndex)
            {
                // update objective
                currentObjectiveIndex += 1;
                waypoint.changeObjective(objectives[currentObjectiveIndex]);

                //display new Instruction
                if (instructionNeeded)
                {
                    currentInstructionIndex += 1;
                    updateInstruction = true;
                }
            }
        }

        void Update()
        {
            if (updateInstruction)
            { 
                // Display cursor
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;

                // fade in instruction panel
                objectivesInstructions[currentInstructionIndex].SetActive(true);
                time += Time.deltaTime;
                CanvasGroup canvasGroup = objectivesInstructions[currentInstructionIndex].GetComponent<CanvasGroup>();
                canvasGroup.alpha += time / fadeInTime;

                if(canvasGroup.alpha == 1)
                    updateInstruction = false;

            }
        }


        public void disableCursor()
        {
            // deactivate cursor
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            time = 0f;
        }
    }
}
