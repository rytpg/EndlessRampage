using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public HealthManager playerHealth;
    public Slider slider;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        slider.maxValue = playerHealth.maxHealth;
        slider.value = playerHealth.GetHealth();

        playerHealth.onDamage.AddListener(UpdateUI);
        playerHealth.onDeath.AddListener(UpdateUI); //safety? shouldnt need it though since i have ondamage? but maybe for instantdeath just in case
        
    }

    void UpdateUI()
    {
        slider.value = playerHealth.GetHealth();
    }
}
