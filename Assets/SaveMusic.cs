using UnityEngine;


//This makes a singleton, to save music states when switching scenes
public class SaveMusic : MonoBehaviour
{
    public static SaveMusic Instance;

    // "awake runs before unity creates duplicated and before Start() is called on any object"
    void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        
    }
}
