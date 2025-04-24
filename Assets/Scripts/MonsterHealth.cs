using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MonsterHealth : MonoBehaviour
{
    //체력
    public int maxHealth = 10;
    private int currentHealth;

    //체력바
    public GameObject hpBarPrefab;
    private Image hpFill;
    private Transform hpBar;

    //애니메이션
    private Animator animator;

    void Start()
    {
        currentHealth = maxHealth;

        //몬스터 애니 저장
        animator = GetComponent<Animator>();

        //체력바 생성
        GameObject bar = Instantiate(hpBarPrefab, transform.position + new Vector3(0, 0.65f, 0),
        Quaternion.identity);
        hpBar = bar.transform;
        hpFill = bar.transform.Find("HpFill").GetComponent<Image>(); 
        
    }

    public void TakeDamage(int attackDamage){
        currentHealth -= attackDamage;
        Debug.Log("몬스터 체력 : " + currentHealth);

        // 피격 애니 재생
        animator?.SetTrigger("Hit");

        UpdateHpBar();
        if(currentHealth <= 0){
            Die();
        }
    }

    void Die(){
        GameObject.FindWithTag("Player").GetComponent<PlayerCombat>().EndCombat();
        Destroy(hpBar.gameObject);

        GetComponent<Collider2D>().enabled = false;
        gameObject.layer = LayerMask.NameToLayer("DeadMonster");
        Destroy(gameObject);
        
    }

    void UpdateHpBar(){
        float ratio = (float)currentHealth / maxHealth;
        hpFill.fillAmount = ratio;

        if(hpBar != null){
            hpBar.position = transform.position + new Vector3(0,0.65f, 0);
        } 
    }

}
