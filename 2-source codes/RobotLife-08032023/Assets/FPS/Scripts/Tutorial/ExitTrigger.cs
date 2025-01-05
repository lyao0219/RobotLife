using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitTrigger : TriggerTutorial
{
    protected override void CheckForCommand()
    {
        StartCoroutine(PrepareScene());
        SceneManager.LoadScene("IntroductionTraining");
    }

    IEnumerator PrepareScene()
    {
        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(3);
    }
}
