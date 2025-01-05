using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.FPS.Game;

namespace Unity.FPS.Gameplay
{
    public class CountDown : MonoBehaviour
    {
        public float timeRemaining = 3;
        private bool timerIsRunning = false;
        public Text counter;
        public GameObject player;


        private void Start()
        {
            // Starts the timer automatically
            timerIsRunning = true;
        }


        void Update()
        {
            if (timerIsRunning)
            {
                if (timeRemaining > 0)
                {
                    timeRemaining -= Time.deltaTime;
                    DisplayTime(timeRemaining);
                }
                else
                {
                    timeRemaining = 0;
                    timerIsRunning = false;
                    DisplayTime(timeRemaining);

                    // objective failed, kill the player
                    Health health = player.GetComponent<Health>();
                    health.CurrentHealth = 0;
                    health.HandleDeath();
                }
            }

        }

        void DisplayTime(float timeToDisplay)
        {
            float minutes = Mathf.FloorToInt(timeToDisplay / 60);
            float seconds = Mathf.FloorToInt(timeToDisplay % 60);
            counter.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }
}
