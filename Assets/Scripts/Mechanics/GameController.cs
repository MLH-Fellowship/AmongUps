using Platformer.Core;
using Platformer.Model;
using UnityEngine;

namespace Platformer.Mechanics
{
    /// <summary>
    /// This class exposes the the game model in the inspector, and ticks the
    /// simulation.
    /// </summary> 
    public class GameController : MonoBehaviour
    {
        public static GameController Instance { get; private set; }
        public PlayerController impostor, crewmate;

        void OnEnable()
        {
            Instance = this;
        }

        void OnDisable()
        {
            if (Instance == this) Instance = null;
        }

        void Update()
        {
            if(impostor.health.IsAlive && crewmate.health.IsAlive) {
                // both players alive and fighting
            }
            else if (!impostor.health.IsAlive && !impostor.playerDead) {
                // impostor is dead
                crewmate.incrementScore();
                impostor.Dead();
                CheckForWinner();
            }
            else if (!crewmate.health.IsAlive && !crewmate.playerDead) {
                // crewmate is dead
                impostor.incrementScore();
                crewmate.Dead();
                CheckForWinner();
            }
        }

        void CheckForWinner() {
            if(impostor.playerScore >= 3) {
                // declare impostor as Winner
            }
            else if (crewmate.playerScore >= 3) {
                // declare crewmate as Winner
            }
            else {
                // new round - display powerups
                impostor.Respawn();
                crewmate.Respawn();
            }
        }
    }
}