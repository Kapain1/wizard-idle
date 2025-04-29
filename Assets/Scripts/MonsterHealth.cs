using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MonsterHealth : MonoBehaviour
{
    //체력
    public int maxHealth;
    public float baseHp = 10f;
    private int currentHealth;
    public float hpGrowthRate = 1.15f;


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
        Destroy(gameObject);
        
        GameManager.Instance.gold += 1;
        UIManager.Instance.UpdateGold(); 

        Debug.Log("코인 개수 = "+ GameManager.Instance.gold);

        hpBarScript.DestroyHpBar(); 

        
    }

    

}
