using System.Collections;
using System.Collections.Generic;
using Unity.FPS.Gameplay;
using UnityEngine;

public class JumpTrigger : TriggerTutorial
{
    [SerializeField]
    PlayerInputHandler m_InputHandler;

    protected override void CheckForCommand()
    {
        if(m_InputHandler.GetJumpInputDown())
        {
            tutorialRule.endTrigger = true;
            Destroy(this.gameObject);
        }
    }
}
