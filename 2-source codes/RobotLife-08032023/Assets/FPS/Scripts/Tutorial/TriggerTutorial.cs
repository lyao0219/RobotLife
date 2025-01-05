using System.Collections;
using System.Collections.Generic;
using Unity.FPS.UI;
using UnityEngine;

public class TriggerTutorial : MonoBehaviour
{
    [SerializeField]
    public TutorialManager tutorialManager;

    [SerializeField]
    public TutorialNotification tutorialRule;

    bool isTriggered;

    private void Update()
    {
        if (isTriggered)
            CheckForCommand();
    }

    void OnTriggerEnter(Collider other)
    {
        isTriggered = true;
        tutorialManager.DisplayTutorialRule(tutorialRule);
    }

    protected virtual void CheckForCommand() {}
}
