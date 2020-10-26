using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupMenu : MonoBehaviour
{
    public static bool PowerupScreen = false;

    public GameObject powerupMenuUI;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (PowerupScreen) {
                PowerupScreenHide();
            } else {
                PowerupScreenShow();
            }
        }
    }

    public void PowerupScreenShow () {
        powerupMenuUI.SetActive(true);
        Time.timeScale = 0f;
        PowerupScreen = true;
    }

    void PowerupScreenHide () {
        powerupMenuUI.SetActive(false);
        Time.timeScale = 1f;
        PowerupScreen = false;
    }

    public void PowerupSelectLeft() {
        Debug.Log("Left");
    }

    public void PowerupSelectRight() {
        Debug.Log("Right");
    }
}
