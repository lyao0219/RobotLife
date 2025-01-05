using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialNotification : MonoBehaviour
{
    [Tooltip("Canvas used to fade in and out the content")]
    public CanvasGroup CanvasGroup;
    [Tooltip("How long it will stay visible")]
    public float VisibleDuration;
    [Tooltip("Duration of the fade in")]
    public float FadeInDuration = 0.5f;
    [Tooltip("Duration of the fade out")]
    public float FadeOutDuration = 2f;

    float m_InitTime;
    public bool Initialized { get; set; }
    public bool endTrigger { get; set; }

    private void Start()
    {
        endTrigger = false;
    }

    private void Update()
    {
        if (Initialized && !endTrigger)
        {
            fadeIn();
        }
        else if (endTrigger)
        {
            fadeOut();
        }
    }

    void fadeIn()
    {
        CanvasGroup.alpha += 0.02f;
    }

    void fadeOut()
    {
        CanvasGroup.alpha -= 0.05f;

        if (CanvasGroup.alpha == 0)
            Destroy(this.gameObject);
    }
}
