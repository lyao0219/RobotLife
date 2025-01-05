using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.FPS.Game;

namespace Unity.FPS.Gameplay
{
    public class IncrementLevelPlayed : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            SceneFlowManager.LevelPlayed++;   
        }

    }
}
