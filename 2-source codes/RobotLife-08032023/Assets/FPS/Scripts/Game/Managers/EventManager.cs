using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Unity.FPS.Game
{

    public class GameEvent
    {
    }


    // A simple Event System that can be used for remote systems communication
    public static class EventManager
    {
        static readonly Dictionary<Type, Action<GameEvent>> s_Events = new Dictionary<Type, Action<GameEvent>>();

        static readonly Dictionary<Delegate, Action<GameEvent>> s_EventLookups =
            new Dictionary<Delegate, Action<GameEvent>>();

        public static string participantID;
        public static string conditions;
        public static string testCase;
        public static string filePath;
        public static int eventID = 1;


        public static void AddListener<T>(Action<T> evt) where T : GameEvent
        {
            if (!s_EventLookups.ContainsKey(evt))
            {
                Action<GameEvent> newAction = (e) => evt((T) e);
                s_EventLookups[evt] = newAction;

                if (s_Events.TryGetValue(typeof(T), out Action<GameEvent> internalAction))
                    s_Events[typeof(T)] = internalAction += newAction;
                else
                    s_Events[typeof(T)] = newAction;
            }
        }

        public static void RemoveListener<T>(Action<T> evt) where T : GameEvent
        {
            if (s_EventLookups.TryGetValue(evt, out var action))
            {
                if (s_Events.TryGetValue(typeof(T), out var tempAction))
                {
                    tempAction -= action;
                    if (tempAction == null)
                        s_Events.Remove(typeof(T));
                    else
                        s_Events[typeof(T)] = tempAction;
                }

                s_EventLookups.Remove(evt);
            }
        }

        public static void Broadcast(GameEvent evt)
        {
            // Questionnare answers

            if (evt.GetType().Name == "QuestionnaireAnswerEvent")
                addAnswer((QuestionnaireAnswerEvent)evt);
            else         // Logging events
                addRecord(evt.GetType().Name);

            if (s_Events.TryGetValue(evt.GetType(), out var action))
            {
                action.Invoke(evt);
            }
        }

        public static void Clear()
        {
            s_Events.Clear();
            s_EventLookups.Clear();
        }

        public static void addRecord(string eventType)
        {
                DateTime dt = DateTime.Now;
                string log = GameConstants.participantID + "," + dt.ToString("dd-MM-yyyy") + "," + SceneFlowManager.getTestCondition() + "," + SceneFlowManager.currentCondition + "," + eventID + "," + dt.ToString("HH:mm:ss") + "." + dt.Millisecond + "," + eventType + "\n";
                File.AppendAllText(GameConstants.logFilePath, log);
                eventID++;
        }

        public static void addAnswer(QuestionnaireAnswerEvent evt)
        {
            DateTime dt = DateTime.Now;
            string log = GameConstants.participantID + "," + dt.ToString("dd-MM-yyyy") + "," + SceneFlowManager.getTestCondition() + "," + SceneFlowManager.currentCondition + "," + dt.ToString("HH:mm:ss") + "." + dt.Millisecond + "," + evt.answer + "," + evt.questionIndex + "\n";
            File.AppendAllText(GameConstants.questionnaireFilePath, log);

        }
    }
}