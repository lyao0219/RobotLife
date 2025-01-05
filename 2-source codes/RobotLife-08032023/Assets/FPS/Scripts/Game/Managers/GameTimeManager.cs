using UnityEngine;
using System.Collections;
using Unity.FPS.Game;
using UnityEngine.SceneManagement;

public class GameTimeManager : MonoBehaviour
{

    bool expired = false;

    // Use this for initialization
    void Start()
    {
        StartTrialEvent evt = new StartTrialEvent();
        EventManager.Broadcast(evt);
    }

    // Update is called once per frame
    void Update()
    {
        if (expired)
            return;

        GameConstants.playedTrialTime += Time.deltaTime;

        if (GameConstants.playedTrialTime >= GameConstants.totalTrialTime)
        {
            GameOver();
        }
    }


    void GameOver()
    {
        expired = true;
        EndTrialEvent evt = new EndTrialEvent();
        EventManager.Broadcast(evt);

        // reset timer
        SceneFlowManager.LevelPlayed = 0;
        GameConstants.playedTrialTime = 0f;


        // go to in game questionnaire
        SceneManager.LoadScene("QuestionnaireScene");
    }
}
