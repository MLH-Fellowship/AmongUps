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
        public Sprite speedIcon, damageIcon, healthIcon, jumpIcon, attackRangeIcon;
        
        void Awake() {
            // Declare powerups
            PowerUp speed, damage, health, jump, attackRange;

            // Initialize powerups
            speed = new PowerUp("speed", "+ Speed", speedIcon);
            damage = new PowerUp("damage", "+ Damage", damageIcon);
            health = new PowerUp("health", "+ Health", healthIcon);
            jump = new PowerUp("jump", "+ Jump", jumpIcon);
            attackRange = new PowerUp("attackRange", "+ Attack Range", attackRangeIcon);

            // Initialize list & Add powerups to it
            powerUpsList = new List<PowerUp>() {speed, damage, health, jump, attackRange};

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
