using UnityEngine;

namespace Unity.FPS.Game
{
    // The Game Events used across the Game.
    // Anytime there is a need for a new event, it should be added here.

    public static class Events
    {
        public static ObjectiveUpdateEvent ObjectiveUpdateEvent = new ObjectiveUpdateEvent();
        public static AllObjectivesCompletedEvent AllObjectivesCompletedEvent = new AllObjectivesCompletedEvent();
        public static GameOverEvent GameOverEvent = new GameOverEvent();
        public static PlayerDeathEvent PlayerDeathEvent = new PlayerDeathEvent();
        public static EnemyKillEvent EnemyKillEvent = new EnemyKillEvent();
        public static PickupEvent PickupEvent = new PickupEvent();
        public static PickupResourceEvent PickupResourceEvent = new PickupResourceEvent();
        public static DropResourceEvent DropResourceEvent = new DropResourceEvent();
        public static AmmoPickupEvent AmmoPickupEvent = new AmmoPickupEvent();
        public static DamageEvent DamageEvent = new DamageEvent();
        public static DisplayMessageEvent DisplayMessageEvent = new DisplayMessageEvent();
        public static TrainingEvent TrainingEvent = new TrainingEvent();
        public static CorrectTrainingEvent CorrectTrainingEvent = new CorrectTrainingEvent();
        public static WrongTrainingEvent WrongTrainingEvent = new WrongTrainingEvent();
        public static HitGoodRobotEvent HitGoodRobotEvent = new HitGoodRobotEvent();
        public static HitEvilRobotEvent HitEvilRobotEvent = new HitEvilRobotEvent();
        public static KillEvilRobotEvent KillEvilRobotEvent = new KillEvilRobotEvent();
        public static KillGoodRobotEvent KillGoodRobotEvent = new KillGoodRobotEvent();
        public static StartTrialEvent StartTrialEvent = new StartTrialEvent();
        public static EndTrialEvent EndTrialEvent = new EndTrialEvent();
        public static StartTrainingEvent StartTrainingEvent = new StartTrainingEvent();
        public static EndTrainingEvent EndTrainingEvent = new EndTrainingEvent();
        public static QuestionnaireAnswerEvent SendQuestionnaireAnswer = new QuestionnaireAnswerEvent();
        public static StartTutorialEvent startTutorialEvent = new StartTutorialEvent();
        public static EndTutorialEvent endTutorialEvent = new EndTutorialEvent();
    }

    public class ObjectiveUpdateEvent : GameEvent
    {
        public Objective Objective;
        public string DescriptionText;
        public string CounterText;
        public bool IsComplete;
        public string NotificationText;
    }

    public class AllObjectivesCompletedEvent : GameEvent { }

    public class GameOverEvent : GameEvent
    {
        public bool Win;
    }

    public class PlayerDeathEvent : GameEvent { }

    public class EnemyKillEvent : GameEvent
    {
        public GameObject Enemy;
        public int RemainingEnemyCount;
    }

    public class PickupEvent : GameEvent
    {
        public GameObject Pickup;
    }

    public class PickupResourceEvent : GameEvent
    {
        public GameObject Pickup;
    }

    public class DropResourceEvent : GameEvent{}

    public class AmmoPickupEvent : GameEvent
    {
        public WeaponController Weapon;
    }

    public class DamageEvent : GameEvent
    {
        public GameObject EnemyDamaged;
        public int enemyAffiliation;
    }

    public class DisplayMessageEvent : GameEvent
    {
        public string Message;
        public float DelayBeforeDisplay;
    }

    public class TrainingEvent : GameEvent
    {
        public bool evil;
    }

    public class CorrectTrainingEvent: GameEvent { }

    public class WrongTrainingEvent: GameEvent { }

    public class HitGoodRobotEvent : GameEvent { }

    public class HitEvilRobotEvent : GameEvent { }

    public class KillGoodRobotEvent : GameEvent { }

    public class KillEvilRobotEvent : GameEvent { }

    public class StartTrialEvent : GameEvent { }

    public class EndTrialEvent : GameEvent { }

    public class EndTrainingEvent : GameEvent { }

    public class StartTrainingEvent : GameEvent { }

    public class QuestionnaireAnswerEvent : GameEvent {
        public string answer;
        public string questionIndex;
    }

    public class StartTutorialEvent : GameEvent { }

    public class EndTutorialEvent : GameEvent { }

}
