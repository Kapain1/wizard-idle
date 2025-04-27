using System;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    //플레이어 감지 및 이동속도
    public float moveSpeed = 2f;
    public float detectRange = 2f;
    public LayerMask monsterLayer;
    private bool isFighting = false;

    //플레이어 전투
    public float attackInterval = 1f;
    private float attackTimer = 0f;
    public int attackDamage = 10;
    public Transform attackPoint;
    public float attackRange = 1f;
    public GameObject hitEffectPrefab;
    public float hitEffectDestoryTime = 0.2f;

    //크리티컬
    float criticalChance = 0.2f;
    int criticalMultiplier = 2;

    void Update()
    {
        //플레이어 감지 및 이동
        if (!isFighting){
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, detectRange, monsterLayer);

            if(hit.collider != null){
                isFighting = true;
                Debug.Log("몬스터 발견");
                return;
            }
            transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
        }
        //공격 시간에 맞춰 공격
        else{
            attackTimer += Time.deltaTime;
            if(attackTimer >= attackInterval){
                //초기화
                attackTimer = 0f;
                attackDamage = 10;

                Attack();
            }
        }
        
    }
    //전투 종료
    public void EndCombat(){
        isFighting = false;
        
    }
    //공격
    void Attack(){
        Collider2D hit = Physics2D.OverlapCircle(attackPoint.position, attackRange, monsterLayer);
        if (hit != null){
            GetComponent<Animator>().SetTrigger("Attack");
        }
    }

    //디버그 확인용
    private void OnDrawGizmosSelected(){
        if(attackPoint != null){
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(attackPoint.position,attackRange); 
        }
    }

    //데미지 프레임 맞추기
    public void DoDamage(){
        Collider2D monsterHit = Physics2D.OverlapCircle(attackPoint.position, attackRange, monsterLayer);
        
        //크리티컬 체크 및 데미지
        bool isCritical = UnityEngine.Random.value <= criticalChance;
        int actualDamage = isCritical ? attackDamage * criticalMultiplier : attackDamage;
        

        if(monsterHit != null){
        var monster = monsterHit.GetComponent<MonsterHealth>();
        var boss = monsterHit.GetComponent<BossHealth>();
        int layer = monsterHit.gameObject.layer;

            
                if(monster != null && layer == LayerMask.NameToLayer("Monster")){
                    monster.TakeDamage(actualDamage, isCritical);
                }
                
            else if(boss != null){
                if(boss != null && layer == LayerMask.NameToLayer("Boss")){
                    boss.TakeDamage(actualDamage, isCritical);
                }
            }
            

            if (hitEffectPrefab != null){
                GameObject effect = Instantiate(hitEffectPrefab, monsterHit.transform.position, Quaternion.identity);
                Destroy(effect,hitEffectDestoryTime); 
                }
            

        } 
        
    }
    
}
 