using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayedLevelCounter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // reset number of level played
        SceneFlowManager.LevelPlayed = 0;
    }
}
