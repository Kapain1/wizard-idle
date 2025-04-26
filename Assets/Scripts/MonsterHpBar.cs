using UnityEngine;
using UnityEngine.UI;
public class MonsterHpBar : MonoBehaviour
{
    //체력바
    public GameObject hpBarPrefab;
    private Image hpFill;
    private Transform hpBar; 
    void Start()
    {
        //체력바 생성
        GameObject bar = Instantiate(hpBarPrefab, transform.position + new Vector3(0, 0.65f, 0),
        Quaternion.identity);
        hpBar = bar.transform;
        hpFill = bar.transform.Find("HpFill").GetComponent<Image>(); 
    }

    public void UpdateHpBar(int currentHealth, int maxHealth){
        float ratio = (float)currentHealth / maxHealth;
        hpFill.fillAmount = ratio;

        if(hpBar != null){
            hpBar.position = transform.position + new Vector3(0,0.65f, 0);
        } 
    }

    public void DestroyHpBar(){
        if(hpBar != null)
        Destroy(hpBar.gameObject);
    }
}
