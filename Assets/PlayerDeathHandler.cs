using UnityEngine;

public class PlayerDeathHandler : MonoBehaviour
{

    public DeathUI deathPanel;

    public void OnPlayerDeath()
    {
        deathPanel.ShowDeathScreen();
    }
}
