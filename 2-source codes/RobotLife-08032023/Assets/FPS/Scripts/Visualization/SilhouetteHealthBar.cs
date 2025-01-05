using Unity.FPS.Game;
using UnityEngine;
using UnityEngine.UI;

namespace Unity.FPS.UI
{
    public class SilhouetteHealthBar : MonoBehaviour
    {
        [Tooltip("Health component to track")]
        public Health Health;

        public Color green;
        public Color orange;
        public Color red;

        void Update()
        {
            // update health bar value
            float healthRatio = Health.CurrentHealth / Health.MaxHealth;

            if (healthRatio > 0.3 && healthRatio < 0.6)
            {
                this.GetComponent<Outline>().OutlineColor = new Color(orange.r, orange.g, orange.b, 1f);
            }
            else if (healthRatio < 0.3)
            {
                this.GetComponent<Outline>().OutlineColor = new Color(red.r, red.g, red.b, 1f);
            }
        }
    }
}