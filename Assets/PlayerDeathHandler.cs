using UnityEngine;

// This script is hooked up to health manager on Death and uses DeathUI
// to show the death screen
public class PlayerDeathHandler : MonoBehaviour
{

    public DeathUI deathPanel;

    public void OnPlayerDeath()
    {
        deathPanel.ShowDeathScreen();
    }
}
