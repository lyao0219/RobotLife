using Unity.FPS.Game;
using UnityEngine;
using UnityEngine.UI;

namespace Unity.FPS.UI
{
    public class IntegratedHealthColor : MonoBehaviour
    {
        [Tooltip("Health component to track")]
        public Health Health;

        public Material orange;
        public Material red;

        SkinnedMeshRenderer meshRenderer;

        // Use this for initialization
        void Start()
        {
            meshRenderer = GetComponent<SkinnedMeshRenderer>();
        }

        void Update()
        {
            // update health bar value
            float healthRatio = Health.CurrentHealth / Health.MaxHealth;

            if (healthRatio > 0.3 && healthRatio < 0.6)
            {
                // Set the new material on the GameObject
                meshRenderer.material = orange;
            }
            else if (healthRatio < 0.3)
            {
                // Set the new material on the GameObject
                meshRenderer.material = red;
            }
        }
    }
}