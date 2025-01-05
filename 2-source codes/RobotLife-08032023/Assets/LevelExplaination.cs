using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.FPS.Game;

public class LevelExplaination : MonoBehaviour
{
    public CanvasGroup left_group;
    public CanvasGroup middle_group;
    public CanvasGroup right_group;

    public CanvasGroup left_group_BW;
    public CanvasGroup middle_group_BW;

    public GameObject startButton;
    public GameObject clickToContinue;

    float intervalDuration = 5f;
    float intervalTime = 0f;
    int current = 0;
    bool acceptInput = false;

    private void Start()
    {
        startButton.SetActive(false);
        clickToContinue.SetActive(false);
    }

    private void Update()
    {
        intervalTime += Time.deltaTime;

        if (intervalTime >= intervalDuration && current < 3)
        {
            acceptInput = true;
            clickToContinue.SetActive(true);
            intervalTime = 0f;
        }

        if(current == 0)
        {
            left_group.alpha += Time.deltaTime;
        }
        else if (current == 1)
        {
            left_group_BW.alpha += Time.deltaTime;
            middle_group.alpha += Time.deltaTime;
        }
        else if(current == 2)
        {
            middle_group_BW.alpha += Time.deltaTime;
            right_group.alpha += Time.deltaTime;
        }
        else if(current == 3)
        {
            middle_group_BW.alpha -= Time.deltaTime;
            left_group_BW.alpha -= Time.deltaTime;
            startButton.SetActive(true);
            clickToContinue.SetActive(false);
        }

        HandleInput();
    }

    void HandleInput()
    {
        if (acceptInput && Input.GetMouseButtonDown(0))
        {
            current++;
            acceptInput = false;
            clickToContinue.SetActive(false);
        }
    }





}
