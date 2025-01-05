using System.Collections.Generic;
using Unity.FPS.Game;
using UnityEngine;
using UnityEngine.UI;

namespace Unity.FPS.Gameplay
{
    public class ObjectiveResourcesPickUp : Objective
    {
        [Tooltip("Total number of resources to pickup to complete the objective")]
        public int numberToPickUp;

        [Tooltip("Max number of pickup actions allowed")]
        public int maxPickup;

        [Tooltip("Image for the pickup actions")]
        public Image pickupImage;

        [Tooltip("The transform for the resources allocation positions")]
        public Transform[] resourcePositions;

        [Tooltip("The UI element for the pickup command")]
        public GameObject pickupCommand;

        [Tooltip("The text element for bolt counter")]
        public Text boltCounter;

        [Tooltip("Image for the bolt counter")]
        public Image boltImage;

        [Tooltip("The text element for gear counter")]
        public Text gearCounter;

        [Tooltip("Image for the gear counter")]
        public Image gearImage;

        [Tooltip("The text element for battery counter")]
        public Text batteryCounter;

        [Tooltip("Image for the battery counter")]
        public Image batteryImage;

        [Tooltip("The text element for pickup counter")]
        public Text pickupCounter;

        [Tooltip("Audio source ")]
        public AudioSource AudioSource;

        [Tooltip("Sound played when there are not more pickupActions available")]
        public AudioClip noMorePickupSound;


        public GameObject bolt;
        public GameObject gear;
        public GameObject power;

        // current number of pickup actions done
        public int currentPickup = 0;
        // List of items to pickup
        private List<ResourcePickup.Type> toPickup =  new List<ResourcePickup.Type>();
        // list of items picked up
        private List<ResourcePickup.Type> pickedUp = new List<ResourcePickup.Type>();


        private int countGear;
        private int countBolt;
        private int countBattery;

        private int pickedBolt = 0;
        private int pickedGear = 0;
        private int pickedBattery = 0;

        bool reachedMaxPickup = false;


        protected override void Start()
        {
            base.Start();

            EventManager.AddListener<PickupResourceEvent>(OnPickupEvent);

            createPickupList();
            displayResources();

            pickupCounter.text = maxPickup.ToString();
        }


        void displayResources()
        {
            int i = 0;
            //make sure the elements to complete the objective are all there always
            foreach(ResourcePickup.Type resource in toPickup)
            {
                if ((int)resource == (int)ResourcePickup.Type.Bolt)
                {
                    GameObject newResource = Instantiate(bolt, resourcePositions[i].position, Quaternion.identity);
                    newResource.transform.parent = resourcePositions[i];
                    newResource.transform.parent.GetComponent<ResourcePickup>().type = ResourcePickup.Type.Bolt;
                }
                else if ((int)resource == (int)ResourcePickup.Type.Gear)
                {
                    GameObject newResource = Instantiate(gear, resourcePositions[i].position, Quaternion.identity);
                    newResource.transform.parent = resourcePositions[i];
                    newResource.transform.parent.GetComponent<ResourcePickup>().type = ResourcePickup.Type.Gear;
                }
                else if ((int)resource == (int)ResourcePickup.Type.Power)
                {
                    GameObject newResource = Instantiate(power, resourcePositions[i].position, Quaternion.identity);
                    newResource.transform.parent = resourcePositions[i];
                    newResource.transform.parent.GetComponent<ResourcePickup>().type = ResourcePickup.Type.Power;
                }
                i++;
            }

                // display the remaining resources in the scene randomly
            for (i = numberToPickUp; i < resourcePositions.Length; i++)
            {
                ResourcePickup.Type resource = (ResourcePickup.Type)Random.Range(0, 3);

                if ((int)resource == (int)ResourcePickup.Type.Bolt)
                {
                    GameObject newResource = Instantiate(bolt, resourcePositions[i].position, Quaternion.identity);
                    newResource.transform.parent = resourcePositions[i];
                    newResource.transform.parent.GetComponent<ResourcePickup>().type = ResourcePickup.Type.Bolt;
                }
                else if ((int)resource == (int)ResourcePickup.Type.Gear)
                {
                    GameObject newResource = Instantiate(gear, resourcePositions[i].position, Quaternion.identity);
                    newResource.transform.parent = resourcePositions[i];
                    newResource.transform.parent.GetComponent<ResourcePickup>().type = ResourcePickup.Type.Gear;
                }
                else if ((int)resource == (int)ResourcePickup.Type.Power)
                {
                    GameObject newResource = Instantiate(power, resourcePositions[i].position, Quaternion.identity);
                    newResource.transform.parent = resourcePositions[i];
                    newResource.transform.parent.GetComponent<ResourcePickup>().type = ResourcePickup.Type.Power;
                }
            }

        }

        void createPickupList()
        {
            // fill the list of items to pick up randomly
            for (int i = 0; i < numberToPickUp; i++)
            {
                ResourcePickup.Type resource = (ResourcePickup.Type)Random.Range(0, 3);
                toPickup.Add(resource);

                if ((int)resource == (int)ResourcePickup.Type.Bolt)
                {
                    countBolt++;
                }
                else if ((int)resource == (int)ResourcePickup.Type.Gear)
                {
                    countGear++;
                }
                else if ((int)resource == (int)ResourcePickup.Type.Power)
                {
                    countBattery++;
                }
            }

            boltCounter.text = "0/" + countBolt;
            gearCounter.text = "0/" + countGear;
            batteryCounter.text = "0/" + countBattery;
        }

        void OnPickupEvent(PickupResourceEvent evt)
        { 
                
            if (IsCompleted)
                return;

            // if there are no more pickup actions available
            if (reachedMaxPickup)
            {
                AudioSource.PlayOneShot(noMorePickupSound, 1F);
            }
            else
            {
                // handle pickup
                ResourcePickup picked = (ResourcePickup)evt.Pickup.GetComponent<ResourcePickup>();

                pickedUp.Add(picked.type);
                updatePickUpCounting(picked.type);

                // update counters
                boltCounter.text = pickedBolt + "/" + countBolt;
                gearCounter.text = pickedGear + "/" + countGear;
                batteryCounter.text = pickedBattery + "/" + countBattery;

                pickupCommand.SetActive(false);
                Destroy(evt.Pickup);
                currentPickup++;
                pickupCounter.text = (maxPickup - currentPickup).ToString();

                if (currentPickup == maxPickup)
                {
                    reachedMaxPickup = true;
                    pickupImage.color = Color.red;

                    // If objective not reached already: player dies
                    if (!allPickedUp())
                    {
                        PlayerDeathEvent deathEvent = new PlayerDeathEvent();
                        EventManager.Broadcast(deathEvent);
                    }
                        
                }
            }
        }


        void updatePickUpCounting(ResourcePickup.Type picked)
        {
            if ((int)picked == (int)ResourcePickup.Type.Bolt)
            {
                pickedBolt++;
                if (pickedBolt == countBolt)
                    boltImage.color = Color.green;
            }
            else if ((int)picked == (int)ResourcePickup.Type.Gear)
            {
                pickedGear++;
                if (pickedGear == countGear)
                    gearImage.color = Color.green;
            }
            else if ((int)picked == (int)ResourcePickup.Type.Power)
            {
                pickedBattery++;
                if (pickedBattery == countBattery)
                    batteryImage.color = Color.green;
            }
        }       

        public bool allPickedUp()
        {
            return pickedBolt >= countBolt && pickedGear >= countGear && pickedBattery >= countBattery;
        }

        void OnDestroy()
        {
            EventManager.RemoveListener<PickupResourceEvent>(OnPickupEvent);
        }
    }
}