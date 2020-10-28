using System;
using Platformer.Gameplay;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using static Platformer.Core.Simulation;

namespace Platformer.Mechanics
{
    /// <summary>
    /// Represents the current vital statistics of some game entity.
    /// </summary>
    public class PowerUps : MonoBehaviour
    {
        List<PowerUp> powerUpsList;
        public Sprite speedIcon, damageIcon, healthIcon, jumpIcon, attackRangeIcon, attackRateIcon;
        
        void Awake() {
            // Declare powerups
            PowerUp speed, damage, health, jump, attackRange, attackRate;

            // Initialize powerups
            speed = new PowerUp("speed", "+ Speed", speedIcon, new Vector3(-6, 25, 1));
            damage = new PowerUp("damage", "+ Damage", damageIcon, new Vector3(0, 20, 1));
            health = new PowerUp("health", "+ Health", healthIcon, new Vector3(12, 60, 1));
            jump = new PowerUp("jump", "+ Jump", jumpIcon, new Vector3(0, 100, 1));
            attackRange = new PowerUp("attackRange", "+ Attack Range", attackRangeIcon, new Vector3(-6, 80, 1));
            attackRate = new PowerUp("attackRate", "+ Attack Rate", attackRateIcon, new Vector3(0, 60, 1));

            // Initialize list & Add powerups to it
            powerUpsList = new List<PowerUp>() {health, jump, speed, damage, attackRange, attackRate};

        }

        public List<PowerUp> ChooseRandomPowerUps() {
            System.Random rnd = new System.Random();
            // Generate a random index between 0 and size of powerups list
            int randomIdx = rnd.Next(0, powerUpsList.Count);

            // Generate a second random index that is different from the first index
            int randomIdx2 = rnd.Next(0, powerUpsList.Count);
            while (randomIdx2 == randomIdx) {
                randomIdx2 = rnd.Next(0, powerUpsList.Count);
            }

            // Return the random powerups
            List<PowerUp> randomPowerUps = new List<PowerUp>();
            randomPowerUps.Add(powerUpsList[randomIdx]);
            randomPowerUps.Add(powerUpsList[randomIdx2]);
            return randomPowerUps;
        }
    }
}
