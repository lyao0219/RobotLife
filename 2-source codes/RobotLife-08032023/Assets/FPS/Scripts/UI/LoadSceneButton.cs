using Unity.FPS.Game;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

namespace Unity.FPS.UI
{
    public class LoadSceneButton : MonoBehaviour
    {

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
            if (SceneFlowManager.needUpdateCondition())
            {
                SceneFlowManager.getNextCondition();
                if (SceneFlowManager.currentCondition.Equals('e'))
                    SceneManager.LoadScene("EndGameScene");
            }

            SceneManager.LoadScene(SceneFlowManager.NextScene);
        }

    }
}