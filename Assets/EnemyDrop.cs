using UnityEngine;

public class EnemyDrop : MonoBehaviour
{
    public GameObject healthPickupPrefab;
    private HealthManager healthManager;
    
    [Range(0f,1f)] public float dropChance = 0.5f;
    void Start()
    {
        healthManager = GetComponent<HealthManager>();
        healthManager.onDeath.AddListener(RollChance);
    }

    void RollChance()
    {
        float rolled = Random.value;
        if(rolled >= dropChance)
        {
            Instantiate(healthPickupPrefab, transform.position, Quaternion.identity);
        }
        Debug.Log($"{gameObject} rolled {rolled}, chance = {dropChance}");
    }
}
