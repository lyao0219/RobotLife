using Unity.FPS.Game;
using UnityEngine;
using UnityEngine.UI;

namespace Unity.FPS.UI
{
    public class WorldspaceHealthBar : MonoBehaviour
    {
        [Tooltip("Health component to track")] public Health Health;

        [Tooltip("Image component displaying health left")]
        public Image HealthBarImage;

        [Tooltip("The floating healthbar pivot transform")]
        public Transform HealthBarPivot;

        [Tooltip("Whether the health bar is visible when at full health or not")]
        public bool HideFullHealthBar = true;

        [Tooltip("Is overlapping or not")]
        public bool isOverlapping = false;

        [SerializeField]
        private Canvas canvas;

        private void Start()
        {
            HealthBarImage.color = Color.red;
        }

        private float angle;

        void Update()
        {
            // update health bar value
            float healthPercentage = Health.CurrentHealth / Health.MaxHealth;
            HealthBarImage.fillAmount = healthPercentage;

            // rotate health bar to face the camera/player
            HealthBarPivot.LookAt(Camera.main.transform.position);

            // hide health bar if needed
            if (HideFullHealthBar)
                HealthBarPivot.gameObject.SetActive(HealthBarImage.fillAmount != 1);

            if (isOverlapping)
            {
                Vector3 relativePos = Camera.main.transform.position - transform.position;

                // the second argument, upwards, defaults to Vector3.up
                Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
                //canvas.transform.rotation = rotation;
                canvas.transform.rotation = new Quaternion(0, rotation.y, rotation.z, rotation.w);
                HealthBarPivot.transform.rotation = new Quaternion(0, HealthBarPivot.transform.rotation.y, HealthBarPivot.transform.rotation.z, HealthBarPivot.transform.rotation.w);
            }
            
        }
    }
}