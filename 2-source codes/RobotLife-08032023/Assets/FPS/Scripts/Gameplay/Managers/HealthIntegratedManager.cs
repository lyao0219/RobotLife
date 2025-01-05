using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unity.FPS.Game
{
    public class HealthIntegratedManager : MonoBehaviour
    {
        public Material[] materials;
        public GameObject[] robotParts;

        // Update is called once per frame
        void Update()
        {
            Health health = this.GetComponent<Health>();
            float healthPercentage = health.CurrentHealth / health.MaxHealth;

            foreach(GameObject robotPart in robotParts)
            {
                Renderer rend = robotPart.GetComponent<Renderer>();
                rend.material.SetFloat("_HighPerc", healthPercentage);
                rend.material.SetColor("_HighlightColor", Color.red);

            }
        }
    }
}
