using UnityEngine;
using TMPro;
using Unity.VisualScripting;
public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public TMP_Text stageText;
    public TMP_Text goldText;


    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        stageText.text = "STAGE " + GameManager.Instance.textStage.ToString();
        goldText.text = "G " + GameManager.Instance.gold.ToString();
    }

    public void UpdateGold(){
        goldText.text = "G " + GameManager.Instance.gold.ToString();
    }
}
