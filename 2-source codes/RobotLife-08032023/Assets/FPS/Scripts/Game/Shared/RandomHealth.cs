using UnityEngine;
using UnityEngine.Events;

namespace Unity.FPS.Game
{
    public class RandomHealth : MonoBehaviour
    {
        [Tooltip("Maximum amount of health")] public float MaxHealth = 10f;

        [Tooltip("Current amount of health")] public float CurrentHealth = 10f;

        [Tooltip("Health ratio at which the critical health vignette starts appearing")]
        public float CriticalHealthRatio = 0.3f;

        [Tooltip("The Character dies at a specifical health range and not at zero")]
        public bool hasRandomHealthKillRange = false;

        // @Fede Health Kill Range Modification
        public float HealthKillRangeMin = 0.3f;
        public float HealthKillRangeMax = 0.6f;

        public UnityAction<float, GameObject> OnDamaged;
        public UnityAction<float> OnHealed;
        public UnityAction OnDie;

        //public float CurrentHealth { get; set; }
        public bool Invincible { get; set; }
        public bool CanPickup() => CurrentHealth < MaxHealth;

        public float GetRatio() => CurrentHealth / MaxHealth;
        public bool IsCritical() => GetRatio() <= CriticalHealthRatio;

        bool m_IsDead;

        [Tooltip("Health Current Value is provided randomly at the start of the game")]
        public bool m_isRandom = false;

        void Start()
        {
            if(m_isRandom)
            {
                CurrentHealth = Random.Range(10, MaxHealth);
                randomizeHealthKillRange(CurrentHealth);
            }
            else
            {
                CurrentHealth = MaxHealth;
            }
            Debug.Log(CurrentHealth);
        }

        private void randomizeHealthKillRange(float CurrentHealth)
        {
            int range = Random.Range(1, 3);

            if (CurrentHealth > 0f && CurrentHealth < 30f)
            {
                // current health in range 0 - 30 % 
                switch (range)
                { 
                    case 1:
                        setHealthRange(30f, 70f);
                        break;

                    case 2:
                        setHealthRange(70f, 100f);
                        break;
                }
            }
            else if(CurrentHealth > 30f && CurrentHealth < 70f)
            {
                // current health in range 30% - 70%
                switch (range)
                {
                    case 1:
                        setHealthRange(0f, 30f);
                        break;

                    case 2:
                        setHealthRange(70f, 100f);
                        break;
                }
            }
            else if(CurrentHealth > 70f && CurrentHealth < 100f)
            {
                // current health in range 7x0 - 100 % 
                switch (range)
                {
                    case 1:
                        setHealthRange(0f, 30f);
                        break;

                    case 2:
                        setHealthRange(30f, 70f);
                        break;
                }
            }
        }

        private void setHealthRange(float min, float max) {
            HealthKillRangeMin = min;
            HealthKillRangeMax = max;
        }

        public void Heal(float healAmount)
        {
            float healthBefore = CurrentHealth;
            CurrentHealth += healAmount;
            CurrentHealth = Mathf.Clamp(CurrentHealth, 0f, MaxHealth);

            // call OnHeal action
            float trueHealAmount = CurrentHealth - healthBefore;
            if (trueHealAmount > 0f)
            {
                OnHealed?.Invoke(trueHealAmount);
            }
        }

        public void TakeDamage(float damage, GameObject damageSource)
        {
            if (Invincible)
                return;

            float healthBefore = CurrentHealth;
            CurrentHealth -= damage;
            CurrentHealth = Mathf.Clamp(CurrentHealth, 0f, MaxHealth);

            // call OnDamage action
            float trueDamageAmount = healthBefore - CurrentHealth;
            if (trueDamageAmount > 0f)
            {
                OnDamaged?.Invoke(trueDamageAmount, damageSource);
            }

            HandleDeath();
        }

        public void Kill()
        {
            CurrentHealth = 0f;

            // call OnDamage action
            OnDamaged?.Invoke(MaxHealth, null);

            HandleDeath();
        }

        public void HandleDeath()
        {
            if (m_IsDead)
                return;

            // @Fede Overriding dying method
            if (hasRandomHealthKillRange && CurrentHealth > HealthKillRangeMin && CurrentHealth < HealthKillRangeMax)
            {
                invokeDeath();
            }
            else if(!hasRandomHealthKillRange && CurrentHealth <= 0f)//  Handling death at currentHealth = 0 -> call OnDie action
            {
                invokeDeath();
            }
            
        }

        public void invokeDeath()
        {
            m_IsDead = true;
            OnDie?.Invoke();
        }
    }
}