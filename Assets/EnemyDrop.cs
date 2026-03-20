using UnityEngine;

public class EnemyDrop : MonoBehaviour
{
    public GameObject healthPickupPrefab;
    private HealthManager healthManager;
    private DDAController dda;
    
    [Range(0f,1f)] public float normalDropChance = 0.5f;
    void Start()
    {
        healthManager = GetComponent<HealthManager>();
        dda = FindFirstObjectByType<DDAController>();
        healthManager.onDeath.AddListener(RollChance);
    }

    void RollChance()
    {
        float ddaDropChance = normalDropChance;
        ddaDropChance = dda.healthPickupDropChance;
        float rolled = Random.value;
        if(rolled <= ddaDropChance)
        {
            Instantiate(healthPickupPrefab, transform.position, Quaternion.identity);
        }
        Debug.Log($"{gameObject} rolled {rolled}, chance = {ddaDropChance}");
    }
}
