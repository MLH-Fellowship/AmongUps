using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Platformer.Mechanics;
using TMPro;

public class PowerupMenuController : MonoBehaviour
{
    public static bool PowerupScreen = false;

    public GameObject powerupMenuUI;

    PowerUps powerUps;

    public TextMeshProUGUI leftTextBox, rightTextBox, topText, bottomText;

    public Image winnerText;

    public Image leftImage, rightImage;

    PowerUp leftPowerUp, rightPowerUp;

    PlayerController winner, loser;

    // Start is called before the first frame update
    void Awake()
    {
        powerUps = GetComponent<PowerUps>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (PowerupScreen) {
                Hide();
            } else {
                Show();
            }
        }
    }

    public void SetWinnerAndLoser(PlayerController playerWon, PlayerController playerLost) {
        winner = playerWon;
        loser = playerLost;
        //topText.text = $"Choose A PowerUp";
        topText.text = $"{winner.name} takes the round!\nChoose A PowerUp";
        bottomText.text = $"Careful! The PowerUp you don't choose will go to {loser.name}";
    }

    public void Show () {
        GetPowerUps();
        UpdatePowerUpUI();
        powerupMenuUI.SetActive(true);
        Time.timeScale = 0f;
        PowerupScreen = true;
    }

    public void GetPowerUps() {
        List<PowerUp> generatedPowerUps = powerUps.ChooseRandomPowerUps();
        leftPowerUp = generatedPowerUps[0];
        rightPowerUp = generatedPowerUps[1];
    }

    public void UpdatePowerUpUI() {
        // Set winner color for sprites in powerup screen
        powerupMenuUI.transform.GetChild(0).GetChild(0).GetComponent<Image>().color = winner.color;
        powerupMenuUI.transform.GetChild(0).GetChild(1).GetComponent<Image>().color = winner.color;

        // Update power up icons
        leftImage.sprite = leftPowerUp.powerUpIcon;
        rightImage.sprite = rightPowerUp.powerUpIcon;

        // Update position of icons
        leftImage.rectTransform.localPosition = leftPowerUp.renderPosition;
        rightImage.rectTransform.localPosition = rightPowerUp.renderPosition;

        // Update power up text
        leftTextBox.text = leftPowerUp.powerUpText;
        rightTextBox.text = rightPowerUp.powerUpText;
    }

    void Hide () {
        powerupMenuUI.SetActive(false);
        Time.timeScale = 1f;
        PowerupScreen = false;
    }

    public void PowerupSelectLeft() {
        winner.AddPowerUp(leftPowerUp);
        loser.AddPowerUp(rightPowerUp);
        Hide();
    }

    public void PowerupSelectRight() {
        winner.AddPowerUp(rightPowerUp);
        loser.AddPowerUp(leftPowerUp);
        Hide();
    }
}
