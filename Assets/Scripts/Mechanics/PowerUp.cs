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

        public PowerUp(string name, string text, Sprite icon) {
            powerUpName = name;
            powerUpText = text;
            powerUpIcon = icon;
        }

        public void Execute(PlayerController player) {
            if (powerUpName == "speed") {
                player.maxSpeed += 2;
            }
            else if (powerUpName == "damage") {
                player.attackDamage += 1;
            }
            else if (powerUpName == "health") {
                player.health.maxHP += 5;
            }
            else if (powerUpName == "jump") {
                player.jumpTakeOffSpeed += 2;
            }
            else if (powerUpName == "attackRange") {
                player.attackRange += 0.25f;
            }
        }
    }
}
