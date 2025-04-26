using UnityEngine;

public class GameManager : MonoBehaviour 
{
    public static GameManager Instance;

    public float coinCount;
    
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

    public void CoinIncrease(){
        coinCount++;
    }
}
