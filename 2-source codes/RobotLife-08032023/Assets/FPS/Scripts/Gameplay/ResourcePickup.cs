using Unity.FPS.Game;
using UnityEngine;
using UnityEngine.UI;

namespace Unity.FPS.Gameplay
{
    public class ResourcePickup : Pickup
    {
        // Enumeration for the type of resources possible
        public enum Type {
            Bolt,
            Gear,
            Power
        };

        // type of the resourse
        public Type type;

        public GameObject pickupCommand;

        protected override void Start()
        {
            base.Start();

            pickupCommand.SetActive(false);

            // Set all children layers to default (to prefent seeing weapons through meshes)
            foreach (Transform t in GetComponentsInChildren<Transform>())
            {
                if (t != transform)
                    t.gameObject.layer = 0;
            }
        }

        protected override void OnPicked(PlayerCharacterController byPlayer)
        {
            PlayPickupFeedback();
        }

        void OnTriggerStay(Collider other)
        {
            PlayerCharacterController pickingPlayer = other.GetComponent<PlayerCharacterController>();

            if (pickingPlayer != null)
            {
                pickupCommand.SetActive(true);
            }

        }

        private void OnTriggerExit(Collider other)
        {
            PlayerCharacterController pickingPlayer = other.GetComponent<PlayerCharacterController>();

            if (pickingPlayer != null)
            {
                pickupCommand.SetActive(false);
            }
        }
    }
}
