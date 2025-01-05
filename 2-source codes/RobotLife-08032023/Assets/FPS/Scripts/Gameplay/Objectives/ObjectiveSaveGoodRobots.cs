using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.FPS.Game;

namespace Unity.FPS.Gameplay
{
    public class ObjectiveSaveGoodRobots : Objective
    {
        [Tooltip("Number of enemy that you could kill by mistake")]
        private int mistakesAllowed = 3;

        private int mistakesDone = 0;

        [SerializeField]
        [Tooltip("Player health reference")]
        private Health player;

        private List<GameObject> damagedByMistake = new List<GameObject>();

        // Affiliation Static values
        private static int GOOD = 0;
        // Affiliation Static values
        private static int EVIL = 1;

        // Start is called before the first frame update
        protected override void Start()
        {
            Title = "Ignore the Good Robots";
            Description = "Don't damage more than " + (mistakesAllowed - 1) + " Good robots";

            // Always a Secondary Objective, paired with ObjectiveKillEvilRobots
            IsOptional = true;

            EventManager.AddListener<DamageEvent>(OnDamage);

            base.Start();
        }

        void OnDamage(DamageEvent evt)
        {

            if (evt.enemyAffiliation == GOOD)
            {
                
                if (!damagedByMistake.Contains(evt.EnemyDamaged))
                {
                    damagedByMistake.Add(evt.EnemyDamaged);
                    mistakesDone++;

                    UpdateObjective(string.Empty, mistakesDone + "/" + mistakesAllowed, "You can damage a good robot " + (mistakesAllowed - mistakesDone) + " time left");

                    if (mistakesDone == mistakesAllowed)
                        player.Kill();
                }

                HitGoodRobotEvent hit = new HitGoodRobotEvent();
                EventManager.Broadcast(hit);
            }

            else if (evt.enemyAffiliation == EVIL)
            {
                HitEvilRobotEvent hit = new HitEvilRobotEvent();
                EventManager.Broadcast(hit);
            }
        }

        void OnDestroy()
        {
            EventManager.RemoveListener<DamageEvent>(OnDamage);
        }
    }
}
