using UnityEngine;
using UnityEngine.SceneManagement;

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
        SceneManager.LoadScene("MainMenu");
    }
}
