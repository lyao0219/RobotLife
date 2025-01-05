using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.FPS.Game;
using UnityEngine.SceneManagement;

public class QuestionnaireManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] conditionImages;

    [SerializeField]
    private GameObject greetings;

    [SerializeField]
    private GameObject activeCanva;



    private void Start()
    {

        // cursor setting
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // set visualization image in the canva
        setCurrentImage();
    }

    public void setCurrentImage()
    {
        switch (SceneFlowManager.currentCondition)
        {
            case 'A':
                conditionImages[0].SetActive(true);
                break;
            case 'B':
                conditionImages[1].SetActive(true);
                break;
            case 'C':
                conditionImages[2].SetActive(true);
                break;

        }
    }

    public void displayGreetings()
    {
        char condition = SceneFlowManager.getNextCondition();

        if (condition.Equals('e'))
            SceneManager.LoadScene("EndGameScene");
        else
        {
            activeCanva.SetActive(false);
            greetings.SetActive(true);
        }
    }
}
