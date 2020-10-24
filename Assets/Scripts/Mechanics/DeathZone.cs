using System.Collections;
using System.Collections.Generic;
using Platformer.Gameplay;
using UnityEngine;
using static Platformer.Core.Simulation;

namespace Platformer.Mechanics
{
    /// <summary>
    /// DeathZone components mark a collider which will schedule a
    /// PlayerEnteredDeathZone event when the player enters the trigger.
    /// </summary>
    public class DeathZone : MonoBehaviour
    {
        void OnTriggerEnter2D(Collider2D collider)
        {
            var p = collider.gameObject.GetComponent<PlayerController>();
            var health = collider.gameObject.GetComponent<Health>();
            if (p != null)
            {
                var ev = Schedule<PlayerEnteredDeathZone>(0);
                ev.deathzone = this;
                health.Die();
            }
            
        }
    }
}