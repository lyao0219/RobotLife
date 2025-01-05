using UnityEngine;
using UnityEngine.UI;

namespace Unity.FPS.Gameplay
{
    public class HealthKillRangeUI:MonoBehaviour
    {
        [Tooltip("Text to display the random health kill range")]
        public Text rangeDisplay;


        // Use this for initialization
        void Start()
        {
            // Deactivate the UI element
            this.gameObject.SetActive(false);
            rangeDisplay.text = "";

        }

        public void showKillRange(float min, float max)
        {
            // Color version
            switch (min)
            {
                case 0:
                    rangeDisplay.text = "Red";
                    break;
                case 30:
                    rangeDisplay.text = "Yellow";
                    break;
                case 70:
                    rangeDisplay.text = "Green";
                    break;
            }
            

            // numeric version
            //rangeDisplay.text = max + " - " + min + " %";
            this.gameObject.SetActive(true);
        }

        public void hideKillRange()
        {
            this.gameObject.SetActive(false);
            rangeDisplay.text = "";
        }
    }
}