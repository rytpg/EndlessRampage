using UnityEngine;
using Pathfinding;

public class EnemyTargetSetter : MonoBehaviour
{
    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        AIDestinationSetter setter = GetComponent<AIDestinationSetter>();
        if(setter != null)
        {
            setter.target = player.transform;
        }
        
    }


}
