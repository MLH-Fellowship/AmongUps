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
    public class PowerUp
    {
        public string powerUpName;
        public string powerUpText;
        public Sprite powerUpIcon;

        const int SPEED_INCREMENT = 3;
        const int DAMAGE_INCREMENT = 1;
        const int HEALTH_INCREMENT = 5;
        const int JUMP_INCREMENT = 2;
        const float RANGE_INCREMENT = 0.5f;
        const float RATE_INCREMENT = 1f;
        

        public PowerUp(string name, string text, Sprite icon) {
            powerUpName = name;
            powerUpText = text;
            powerUpIcon = icon;
        }

        public void Execute(PlayerController player) {
            if (powerUpName == "speed") {
                player.maxSpeed += SPEED_INCREMENT;
            }
            else if (powerUpName == "damage") {
                player.attackDamage += DAMAGE_INCREMENT;
            }
            else if (powerUpName == "health") {
                player.health.maxHP += HEALTH_INCREMENT;
            }
            else if (powerUpName == "jump") {
                player.jumpTakeOffSpeed += JUMP_INCREMENT;
            }
            else if (powerUpName == "attackRange") {
                player.attackRange += RANGE_INCREMENT;
            }
            else if (powerUpName == "attackRate") {
                player.attackRate += RATE_INCREMENT;
            }
        }
    }
}
