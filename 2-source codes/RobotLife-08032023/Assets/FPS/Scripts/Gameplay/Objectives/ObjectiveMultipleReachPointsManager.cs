using Unity.FPS.Game;
using UnityEngine;

namespace Unity.FPS.Gameplay
{
    [RequireComponent(typeof(Collider))]
    public class ObjectiveMultipleReachPointsManager : Objective
    {
        [Tooltip("Collection of objective's reach points")]
        public Transform[] checkpoints;

        public int numberOfCheckpoints;

        public string errorMessage;

        int nextObjective = 0;



        public void completeObjectivePoint(Transform point, Collider other)
        {
            int currentObjective = System.Array.IndexOf(checkpoints, point);

            if (currentObjective == nextObjective) // valid 
            {
                nextObjective++;

                if(nextObjective == numberOfCheckpoints) // all completed
                {
                    var player = other.GetComponent<PlayerCharacterController>();
                    // test if the other collider contains a PlayerCharacterController, then complete
                    if (player != null)
                    {
                        CompleteObjective(string.Empty, string.Empty, "Objective complete : " + Title);
                    }
                }

            }
            else // not valid
            {
                UpdateObjective(errorMessage, string.Empty, string.Empty);
            }
        }
    }
}