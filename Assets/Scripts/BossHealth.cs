using UnityEngine;

public class BossHealth : MonoBehaviour
{
    public int maxHealth = 50;
    private int currentHealth;


    //HP바
    private MonsterHpBar hpBarScript;

    //텍스트
    private DamageText textScript;

    //애니메이션
    private Animator animator;
    void Start()
    {
        currentHealth = maxHealth;

        //Hpbar 스크립트 불러오기
        hpBarScript = GetComponent<MonsterHpBar>();
        //text 스크립트 불러오기
        textScript = GetComponent<DamageText>();
        //몬스터 애니 저장
        animator = GetComponent<Animator>(); 
    }

    public void TakeDamage(int attackDamage, bool isCritical){
        currentHealth -= attackDamage;

        Debug.Log("몬스터의 남은 체력" + currentHealth);
        // 피격 애니 재생
        animator?.SetTrigger("Hit");
        //텍스트
        textScript.TextRecall(attackDamage,isCritical);
        //Hp바
        hpBarScript.UpdateHpBar(currentHealth, maxHealth);
        if(currentHealth <= 0){
            Die();
        }
    }

    void Die(){ 

        GameObject.FindWithTag("Player").GetComponent<PlayerCombat>().EndCombat();
        Destroy(gameObject);
        hpBarScript.DestroyHpBar(); 
        GameManager.Instance.gold += 10;
        Debug.Log("코인 개수 = "+ GameManager.Instance.gold);

        Debug.Log("Boss defeated!");

        GameManager.Instance.currentStage++;
    }
}
