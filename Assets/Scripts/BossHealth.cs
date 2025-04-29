using UnityEngine;

public class BossHealth : MonoBehaviour
{
    public int maxHealth;
    public int baseHp = 50;
    private int currentHealth;
    public float hpGrowthRate = 1.5f;

    //HP바
    private MonsterHpBar hpBarScript;

    //텍스트
    private DamageText textScript;

    void Start()
    {
        maxHealth = (int)Mathf.Round(baseHp * Mathf.Pow(hpGrowthRate, GameManager.Instance.textStage));
        currentHealth = maxHealth;

        //Hpbar 스크립트 불러오기
        hpBarScript = GetComponent<MonsterHpBar>();
        //text 스크립트 불러오기
        textScript = GetComponent<DamageText>();


        
    }

    public void TakeDamage(int attackDamage, bool isCritical){
        currentHealth -= attackDamage;

        Debug.Log("몬스터의 남은 체력" + currentHealth);
        
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
        GetComponent<Collider2D>().enabled = false;
        gameObject.layer = LayerMask.NameToLayer("DeadMonster");
        hpBarScript.DestroyHpBar(); 
        GameManager.Instance.gold += 10;
        UIManager.Instance.UpdateGold(); 
        Debug.Log("코인 개수 = "+ GameManager.Instance.gold);

        Debug.Log("Boss defeated!");

        //암전 효과
        ScreenFade screenFade = FindAnyObjectByType<ScreenFade>();
        if(screenFade != null)
        screenFade.FadeOut(1f, () => {
            Destroy(gameObject);
            GameManager.Instance.LoadNextStage();
        });
        else{
            Debug.LogError("ScreenFade를 찾지 못했습니다!");
            Destroy(gameObject); // 예외처리
            GameManager.Instance.LoadNextStage();
        }
        
    }
}
