using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int gold;
    public int currentStage = 0; 
    public string[] stageScenes = { "Stage1", "Stage2","Stage3"};

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

    public void LoadNextStage(){
        currentStage++;

        if (currentStage >= stageScenes.Length){
            currentStage = 0;
        }
        SceneManager.LoadScene(stageScenes[currentStage]);
    }

}
