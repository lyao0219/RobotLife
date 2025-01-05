using System;
using System.Collections.Generic;
using Unity.FPS.Game;
using UnityEngine;

public class SceneFlowManager
{
    public static string NextScene { get; set; }
    public static List<char> testCondition = new List<char>();
    public static char currentCondition = 'z';
    public static int LevelPlayed = 0;

    public SceneFlowManager()
    {
        testCondition.Add('A');
        testCondition.Add('B');
        testCondition.Add('C');
    }

    public static char getNextCondition()
    {

        if (currentCondition.Equals('z'))
        {
            currentCondition = testCondition[0];
        }
        else
        {
            int index = testCondition.IndexOf(currentCondition);
            if (index + 1 >= testCondition.Count)
                currentCondition = 'e';
            else 
                currentCondition = testCondition[index + 1];
        }

        return currentCondition;
    }

    public static bool needUpdateCondition()
    {
        if (LevelPlayed == GameConstants.number_of_trials)
            return true;
        else
            return false;
    }

    public static string getTestCondition()
    {
        // handling default for Logging
        if (testCondition.Count != 0)
            return testCondition[0].ToString() + testCondition[1].ToString() + testCondition[2].ToString();
        else
            return "XXX";
    }
}
