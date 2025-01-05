using System.Collections;
using System.Collections.Generic;
using Unity.FPS.Game;
using UnityEngine;
using UnityEngine.UI;

namespace Unity.FPS.Gameplay
{
    public class TrainingManager : MonoBehaviour
    {
        private Transform respawnPoint;

        [SerializeField]
        private GameObject quitUI;

        [SerializeField]
        private GameObject goodButton;

        [SerializeField]
        private GameObject evilButton;

        [SerializeField]
        private GameObject correctUI;

        [SerializeField]
        private GameObject wrongUI;

        [SerializeField]
        private Text healthValue;

        [SerializeField]
        private GameObject healthValueUI;

        [SerializeField]
        private GameObject nextButton;

        [SerializeField]
        private GameObject enemyPrefab;

        int trainingCorrect = 0;
        GameObject currentEnemy;

         void Start()
        {

            // set the respawn point 
            respawnPoint = this.transform;

            EventManager.AddListener<TrainingEvent>(OnTrainingEvent);

            // instantiate the first enemy
            currentEnemy = Instantiate(enemyPrefab, respawnPoint.position, Quaternion.identity);
            // initialize buttons
            goodButton.SetActive(true);
            evilButton.SetActive(true);
            healthValueUI.SetActive(false);

            StartTrainingEvent evt = new StartTrainingEvent();
            EventManager.Broadcast(evt);
        }

        void OnTrainingEvent(TrainingEvent evt)
        {
            // initialize buttons
            goodButton.SetActive(false);
            evilButton.SetActive(false);


            // validate answer
            if(currentEnemy != null)
            {
                Health currentEnemyHealth = currentEnemy.GetComponent<Health>();

                if ((evt.evil && currentEnemyHealth.CurrentHealth > 66f) || (!evt.evil && currentEnemyHealth.CurrentHealth < 66f))
                {
                    correctUI.SetActive(true);
                    trainingCorrect++;
                    // logging a correct answer in training
                    CorrectTrainingEvent correctLog = new CorrectTrainingEvent();
                    EventManager.Broadcast(correctLog);
                }
                else
                {
                    wrongUI.SetActive(true);
                    WrongTrainingEvent wrongEvt = new WrongTrainingEvent();
                    EventManager.Broadcast(wrongEvt);
                }

                healthValueUI.SetActive(true);
                healthValue.text = " Health Value: " + (int)currentEnemyHealth.CurrentHealth + "%";
                nextButton.SetActive(true);


                if (trainingCorrect >= GameConstants.minTrainingNumber)
                    quitUI.SetActive(true);
            }

            
        }

        public void DisplayNextTraining()
        {
            // destroy current enemy
            //Destroy(currentEnemy.gameObject);

            currentEnemy.gameObject.SetActive(false);

            // reset UI
            healthValueUI.SetActive(false);
            correctUI.SetActive(false);
            wrongUI.SetActive(false);
            nextButton.SetActive(false);

            // instantiate the next enemy
            currentEnemy = Instantiate(enemyPrefab, respawnPoint.position, Quaternion.identity);
            currentEnemy.gameObject.SetActive(true);
            // initialize buttons
            goodButton.SetActive(true);
            evilButton.SetActive(true);
        }

        void OnDestroy()
        {
            EventManager.RemoveListener<TrainingEvent>(OnTrainingEvent);

            EndTrainingEvent evt = new EndTrainingEvent();
            EventManager.Broadcast(evt);
        }

    }
}
