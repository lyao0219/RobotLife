using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.FPS.Game;

namespace Unity.FPS.Gameplay
{
    public class DropResourcePoint : MonoBehaviour
    {
        [Tooltip("ObjectiveResourcesPickUp game object")]
        public ObjectiveResourcesPickUp objective;
        [Tooltip("UI element for the drop command")]
        public GameObject dropResourcesCommand;

        [Tooltip("UI element for the drop error message")]
        public GameObject dropErrorMessage;

        PlayerCharacterController pickingPlayer;
        bool isCompleted = false;

        private void Start()
        {
            dropResourcesCommand.SetActive(false);
            dropErrorMessage.SetActive(false);
            EventManager.AddListener<DropResourceEvent>(OnDropEvent);
        }

        private void OnTriggerStay(Collider other)
        {
            pickingPlayer = other.GetComponent<PlayerCharacterController>();

            if (pickingPlayer != null)
            {
                if (objective.numberToPickUp == objective.currentPickup)
                    dropResourcesCommand.SetActive(true);
                else
                    dropErrorMessage.SetActive(true);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            dropResourcesCommand.SetActive(false);
            dropErrorMessage.SetActive(false);
            pickingPlayer = null;
        }

        void OnDropEvent(DropResourceEvent evt)
        {
            if (objective.currentPickup < objective.numberToPickUp)
                return;

            if (objective.allPickedUp() && !isCompleted)
            {
                Debug.Log("x");
                isCompleted = true;
                objective.CompleteObjective(string.Empty, string.Empty, "Objective complete : " + objective.Title);
            }
            else if(!objective.allPickedUp())
            {
                PlayerDeathEvent deathEvent = new PlayerDeathEvent();
                EventManager.Broadcast(deathEvent);
            }
        }

        private void OnDestroy()
        {
            EventManager.RemoveListener<DropResourceEvent>(OnDropEvent);
        }
    }
}
