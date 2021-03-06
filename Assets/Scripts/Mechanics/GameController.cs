using Platformer.Core;
using Platformer.Model;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;


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

        PlayerController winner, loser;

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
                winner = crewmate;
                loser = impostor;
                StartCoroutine(CheckForWinner());
            }
            else if (!crewmate.health.IsAlive && !crewmate.isDead) {
                // crewmate is dead
                crewmate.Dead();
                impostor.incrementScore();
                winner = impostor;
                loser = crewmate;
                StartCoroutine(CheckForWinner());
            }
        }

        IEnumerator CheckForWinner() {
            yield return new WaitForSeconds(3f);
            if(impostor.playerScore >= 3) {
                victoryScreen.SetActive(true);
                // declare impostor as Winner
                victoryScreen.transform.GetChild(1).GetComponent<Image>().color = impostor.color;
                // set crewmate as loser sprite
                victoryScreen.transform.GetChild(2).GetComponent<Image>().color = crewmate.color;

            }
            else if (crewmate.playerScore >= 3) {
                victoryScreen.SetActive(true);
                // declare crewmate as Winner
                victoryScreen.transform.GetChild(1).GetComponent<Image>().color = crewmate.color;
                // set crewmate as loser sprite
                victoryScreen.transform.GetChild(2).GetComponent<Image>().color = impostor.color;

            }
            else {
                powerupMenu.SetWinnerAndLoser(winner, loser);
                // new round - display powerups
                powerupMenu.Show();
                yield return new WaitForSeconds(0.5f);
                impostor.Respawn();
                crewmate.Respawn();
            }
        }

        public void PlayAgain() {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public void Quit() {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
    }
}