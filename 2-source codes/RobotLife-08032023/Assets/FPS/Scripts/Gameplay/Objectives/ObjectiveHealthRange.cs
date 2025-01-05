using System.Collections.Generic;
using Unity.FPS.Game;
using UnityEngine;
using UnityEngine.UI;

namespace Unity.FPS.Gameplay
{
    public class ObjectiveHealthRange : Objective
    {
        [Tooltip("List of enemies")]
        public List<GameObject> enemies;

        [Tooltip("Upperbound of the first range of the enemies' health ranges categories")]
        public int firstRangeUp;

        [Tooltip("Upperbound of the second range of the enemies' health ranges categories")]
        public int secondRangeUp;

        public Text rangeDisplay; 

        private int firstRangeNumber;
        private int secondRangeNumber;
        private int thirdRangeNumber;

        private int firstRangeGoal;
        private int secondRangeGoal;
        private int thirdRangeGoal;

        private int enemiesNumber = 0;



        protected override void Start()
        {
            base.Start();

            EventManager.AddListener<EnemyKillEvent>(OnEnemyKilled);

            // set a title and description specific for this type of objective, if it hasn't one
            if (string.IsNullOrEmpty(Title))
                Title = "Hit the enemies ";

            if (string.IsNullOrEmpty(Description))
                Description = "";

            // Generate the Ranges Goals Randomly
            enemiesNumber = enemies.Count;
            firstRangeGoal = Random.Range(1, enemiesNumber - 3);
            secondRangeGoal = Random.Range(1, enemiesNumber - firstRangeGoal - 3);
            thirdRangeGoal = Random.Range(1, enemiesNumber - firstRangeGoal - secondRangeGoal - 3);

            // Display the ranges in the HUD
            rangeDisplay.text = "Range 0" + "/" + firstRangeGoal + "\n Range 0" + "/" + secondRangeGoal + "\n Range " + enemiesNumber + "/" + thirdRangeGoal;
        }

        private void Update()
        {
            if (IsCompleted)
                return;

            firstRangeNumber = 0;
            secondRangeNumber = 0;
            thirdRangeNumber = 0;

            // counting the values of the ranges
            foreach (GameObject enemy in enemies)
            {
                Health health = enemy.GetComponent<Health>();

                if (health.CurrentHealth < firstRangeUp)
                {
                    firstRangeNumber++;
                }
                else if (health.CurrentHealth > firstRangeUp && health.CurrentHealth < secondRangeUp)
                {
                    secondRangeNumber++;
                }
                else if (health.CurrentHealth > secondRangeUp)
                {
                    thirdRangeNumber++;
                }
            }

            // check if the objective is complete
            if (firstRangeNumber == firstRangeGoal && secondRangeNumber == secondRangeGoal && thirdRangeNumber == thirdRangeGoal)
                CompleteObjective(string.Empty, string.Empty, "Objective complete : " + Title);

            // update the HUD
            rangeDisplay.text = "Range " + firstRangeNumber + "/" + firstRangeGoal + "\n Range " + secondRangeNumber + "/" + secondRangeGoal + "\n Range " + thirdRangeNumber + "/" + thirdRangeGoal;
        }

        void OnEnemyKilled(EnemyKillEvent evt)
        {
            if (IsCompleted)
                return;

            enemies.Remove(evt.Enemy);
            enemiesNumber = enemies.Count;

            if (enemiesNumber < firstRangeGoal + secondRangeGoal + thirdRangeGoal)
                CompleteObjective(string.Empty, string.Empty, "Objective failed");
        }

        private void OnDestroy()
        {
            EventManager.RemoveListener<EnemyKillEvent>(OnEnemyKilled);
        }
    }
}