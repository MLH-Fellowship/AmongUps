using System;
using Platformer.Gameplay;
using UnityEngine;
using UnityEngine.UI;
using static Platformer.Core.Simulation;

namespace Platformer.Mechanics
{
    /// <summary>
    /// Represebts the current vital statistics of some game entity.
    /// </summary>
    public class Health : MonoBehaviour
    {
        /// <summary>
        /// The maximum hit points for the entity.
        /// </summary>
        public int maxHP;
        public Slider slider;
        public Gradient gradient;
        public Image fill;

        /// <summary>
        /// Indicates if the entity should be considered 'alive'.
        /// </summary>
        public bool IsAlive => currentHP > 0;

        public const int DEFAULT_HP = 20;


        int currentHP;

        /// <summary>
        /// Increment the HP of the entity.
        /// </summary>
        public void Increment()
        {
            currentHP = Mathf.Clamp(currentHP + 1, 0, maxHP);
            slider.value = currentHP;
            fill.color = gradient.Evaluate(slider.normalizedValue);
        }

        /// <summary>
        /// Decrement the HP of the entity. Will trigger a HealthIsZero event when
        /// current HP reaches 0.
        /// </summary>
        public void Decrement(int damageTaken)
        {
            currentHP = Mathf.Clamp(currentHP - damageTaken, 0, maxHP);
            if (currentHP <= 0)
            {
                var ev = Schedule<HealthIsZero>();
                ev.health = this;
            }
            slider.value = currentHP; 
            fill.color = gradient.Evaluate(slider.normalizedValue);
        }

        /// <summary>
        /// Decrement the HP of the entitiy until HP reaches 0.
        /// </summary>
        public void Die()
        {
            Decrement(currentHP);
        }

        public int getHP()
        {
            return currentHP;
        }

        public void Respawn()
        {
            Reset();
        }

        void Reset() {
            currentHP = maxHP;
            slider.maxValue = maxHP;
            slider.value = currentHP; 
            fill.color = gradient.Evaluate(slider.normalizedValue);
        }

        void Awake()
        {
            maxHP = DEFAULT_HP;
            Reset();
        }
    }
}
