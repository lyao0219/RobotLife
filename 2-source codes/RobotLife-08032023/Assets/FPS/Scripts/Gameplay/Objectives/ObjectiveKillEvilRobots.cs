using System.Collections.Generic;
using Unity.FPS.Game;
using UnityEngine;
using UnityEngine.UI;

namespace Unity.FPS.Gameplay
{
    public class ObjectiveKillEvilRobots : Objective
    {

        [SerializeField]
        [Tooltip("Total number of evil robot to eliminate")]
        private int evilRobotNumber = 8;

        private int evilRobotEliminated = 0;

        [SerializeField]
        [Tooltip("All the enemies in the scenario")]
        private GameObject[] robots;


        // Affiliation Static values
        private static int GOOD = 0;
        private static int EVIL = 1;

        private static int[] healthValues = { 18, 32, 43, 58, 72, 83 };
        private int currentIndex = 0;
        private int robotPerValue = 4;
        private float healthThreeshold = 66f;

        // Use this for initialization
        protected override void Start()
        {

            Title = "Eliminate " + evilRobotNumber + " evil robots";
            Description = "Their health must be higher than 66%";

            base.Start();

            EventManager.AddListener<EnemyKillEvent>(OnKill);

            // initialize the robots
            assignRobotsAffiliation();
        }

        void assignRobotsAffiliation()
        {
            // shuffle robot list
            robots = Shuffle(robots);

            // for each health value instatiate #robotPerValue robots
            // assigning affiliation and health
            foreach (int healthValue in healthValues)
            {
                for(int i = currentIndex; i < currentIndex + robotPerValue; i++)
                {
                    generateHealth(i, healthValue);
                }
                currentIndex += robotPerValue;
                
            }
        }

        void generateHealth(int i, int healthValue)
        {
            Actor actor = robots[i].GetComponent<Actor>();
            Health health = robots[i].GetComponent<Health>();
            health.CurrentHealth = healthValue;

            if (healthValue > healthThreeshold)
                actor.Affiliation = EVIL;
            else
                actor.Affiliation = GOOD;
        }

        public GameObject[] Shuffle(GameObject[] objectList)
        {
            GameObject tempGO;

            for (int i = 0; i < objectList.Length; i++)
            {
                int rnd = Random.Range(0, objectList.Length);
                tempGO = objectList[rnd];
                objectList[rnd] = objectList[i];
                objectList[i] = tempGO;
            }

            return objectList;
        }

        void OnKill(EnemyKillEvent evt)
        {
            GameObject enemy = evt.Enemy.gameObject;
            int affiliation = enemy.GetComponent<Actor>().Affiliation;

            if (affiliation == EVIL)
            {
                // decrease number of robot to eliminate
                evilRobotEliminated++;

                // log event
                KillEvilRobotEvent killEvent = new KillEvilRobotEvent();
                EventManager.Broadcast(killEvent);

                //check Winnind Conditions
                checkWinningCondition();
            }
            else if(affiliation == GOOD)
            {
                // log event
                KillGoodRobotEvent killEvent = new KillGoodRobotEvent();
                EventManager.Broadcast(killEvent);
            }

        }

        void checkWinningCondition()
        {
            if (evilRobotEliminated < evilRobotNumber)
                UpdateObjective(string.Empty, evilRobotEliminated + "/" + evilRobotNumber, evilRobotNumber - evilRobotEliminated + " evil robots left");
            else
                CompleteObjective(string.Empty, string.Empty, "Objective complete : " + Title);

        }

        void OnDestroy()
        {
            EventManager.RemoveListener<EnemyKillEvent>(OnKill);
        }
    }
}
