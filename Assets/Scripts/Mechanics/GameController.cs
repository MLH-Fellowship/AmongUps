using Platformer.Core;
using Platformer.Model;
using System.Collections;
using UnityEngine.UI;
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

        public PowerupMenuController powerupMenu;

        public GameObject victoryScreen;


        void OnEnable()
        {
            Instance = this;
        }

        void OnDisable()
        {
            if (Instance == this) Instance = null;
        }

        void Start() {
            victoryScreen.SetActive(false);
        }

        void Update()
        {
            if (!impostor.health.IsAlive && !impostor.isDead) {
                // impostor is dead
                impostor.Dead();
                crewmate.incrementScore();
                powerupMenu.SetWinnerAndLoser(crewmate, impostor);
                StartCoroutine(CheckForWinner());
            }
            else if (!crewmate.health.IsAlive && !crewmate.isDead) {
                // crewmate is dead
                crewmate.Dead();
                impostor.incrementScore();
                powerupMenu.SetWinnerAndLoser(impostor, crewmate);
                StartCoroutine(CheckForWinner());
            }
        }

        IEnumerator CheckForWinner() {
            yield return new WaitForSeconds(3f);
            if(impostor.playerScore >= 3) {
                // declare impostor as Winner
                victoryScreen.SetActive(true);
                victoryScreen.transform.GetChild(1).GetComponent<Image>().color = new Color32(255, 243, 0, 255);
            }
            else if (crewmate.playerScore >= 3) {
                // declare crewmate as Winner
                victoryScreen.SetActive(true);
                victoryScreen.transform.GetChild(1).GetComponent<Image>().color = new Color32(0, 255, 255, 255);
            }
            else {
                // new round - display powerups
                powerupMenu.Show();

                impostor.Respawn();
                crewmate.Respawn();
            }
        }
    }
}