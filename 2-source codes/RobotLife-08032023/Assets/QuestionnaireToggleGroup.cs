using UnityEngine;
using Unity.FPS.Game;
using System.Collections;
using UnityEngine.UI;
using System.Linq;

public class QuestionnaireToggleGroup : MonoBehaviour
{
    public int questionIndex;
    ToggleGroup toggleGroup;

    private void Start()
    {
        toggleGroup = GetComponent<ToggleGroup>();
    }

    public string getAnswer()
    {
        Toggle activeToggle = toggleGroup.GetFirstActiveToggle();
        Debug.Log(activeToggle.gameObject.name);
        return activeToggle.name;
    }

    public bool isOneActive()
    {
        return toggleGroup.ActiveToggles().Count() != 0;
    }

}
