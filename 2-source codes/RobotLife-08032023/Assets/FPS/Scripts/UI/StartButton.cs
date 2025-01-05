using Unity.FPS.Game;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

namespace Unity.FPS.UI
{
    public class StartButton : MonoBehaviour
    {
        public string SceneName = "";

        void Update()
        {
            if (EventSystem.current.currentSelectedGameObject == gameObject
                && Input.GetButtonDown(GameConstants.k_ButtonNameSubmit))
            {
                LoadTargetScene();
            }
        }

        public void LoadTargetScene()
        {
            SceneManager.LoadScene(SceneName);
        }

        public void getNextScene(string sceneType)
        {
            if (SceneFlowManager.currentCondition.Equals('e'))
                SceneManager.LoadScene("EndGameScene");
            else 
                SceneManager.LoadScene(sceneType + SceneFlowManager.currentCondition);
        }

        public void getNextSceneUpdatingCondition(string sceneType)
        {
            char condition = SceneFlowManager.getNextCondition();

            Debug.Log(SceneFlowManager.currentCondition);
            if (condition.Equals('e'))
                SceneManager.LoadScene("EndGameScene");
            else
                SceneManager.LoadScene(sceneType + condition);
        }

        public void goToPreviousScene()
        {
            SceneManager.LoadScene("Health" + SceneFlowManager.currentCondition);     
        }
    }
}