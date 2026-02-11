using UnityEngine;
using UnityEngine.SceneManagement;

/*
This script just sets the death screen to be active when you die
*/
public class DeathUI : MonoBehaviour
{
    public GameObject deathPanel;

    void Awake()
    {
        deathPanel.SetActive(false);
    }

    public void ShowDeathScreen()
    {
        deathPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void GoToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
