using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int gold;
    public int currentStage; 
    
    void Awake()
    {
        if (Instance == null){
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else{
            Destroy(gameObject);
        }
    }

}
