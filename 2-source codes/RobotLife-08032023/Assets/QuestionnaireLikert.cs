using System.Collections;
using System.Collections.Generic;
using Unity.FPS.Game;
using UnityEngine;
using UnityEngine.UI;

public class QuestionnaireLikert : MonoBehaviour
{
    public QuestionnaireToggleGroup[] toggleGroups;
    int likertSize = 7;

    [SerializeField]
    Button button; 

    CanvasGroup canvasGroup;
    float transitionTime = 4f;
    bool startTransition = false;
    float time = 0f;

    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        button.interactable = false;
    }

    public void sendAnswers()
    {
        int[] answers = new int[likertSize];

        for (int i = 0; i < toggleGroups.Length; i++)
        {
            QuestionnaireAnswerEvent evt = new QuestionnaireAnswerEvent();
            evt.answer = toggleGroups[i].getAnswer();
            evt.questionIndex = toggleGroups[i].questionIndex.ToString();
            EventManager.Broadcast(evt);
        }
    }




    public void startCanvasTransition()
    {
        startTransition = true;
    }

    private void Update()
    {
        bool filled = false;

        foreach (QuestionnaireToggleGroup group in toggleGroups)
        {
            filled = group.isOneActive();
            if (!filled)
                return;

        }

        if (filled)
            button.interactable = true;

        if (startTransition)
        {
            time += Time.deltaTime;
            canvasGroup.alpha -= time / transitionTime;

            if(canvasGroup.alpha == 0)
                this.gameObject.SetActive(false);
        }

    }

}
