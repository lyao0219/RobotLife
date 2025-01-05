using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{

    Image progressBar;
    float factor = 0.5f;

    private void Start()
    {
        progressBar = GetComponent<Image>();
        progressBar.fillAmount = 0f;
    }

    public void increaseBarValue()
    {
        progressBar.fillAmount += factor;
    }


}
