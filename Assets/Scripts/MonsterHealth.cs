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

    //텍스트
    public GameObject damageTextPrefab;
    public Transform canvasTransform;

    

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
        
        canvasTransform = GameObject.Find("FontCanvas").transform;
    }

    public void TakeDamage(int attackDamage, bool isCritical){
        

        currentHealth -= attackDamage;
        
        
        Debug.Log("몬스터의 남은 체력" + currentHealth);
        // 피격 애니 재생
        animator?.SetTrigger("Hit");

        //텍스트
        Vector3 worldPos = transform.position + new Vector3(0, 0.6f, 0); //머리 위 위치 월드좌표 계산
        Vector2 screenPos = Camera.main.WorldToScreenPoint(worldPos); //월드좌표를 스크린좌표로 변환해서 저장
        GameObject clone = Instantiate(damageTextPrefab, canvasTransform); //캔버스를 부모로 텍스트 프리팹 소환
        RectTransform canvasRect = canvasTransform.GetComponent<RectTransform>(); //캔버스 좌표 컴퍼넌트 저장
        RectTransform cloneRect = clone.GetComponent<RectTransform>(); //텍스트 프리팹 클론 좌표 컴퍼넌트 저장
        Vector2 anchoredPos; //변환할 위치를 담을 변수 선언
        //스크린 좌표를 UI 로컬 좌표로 변환해주는 함수               기준 캔버스 , 스크린 좌표 , 사용중인 카메라 ,변환을 받을 변수
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, screenPos, Camera.main, out anchoredPos);
        cloneRect.anchoredPosition = anchoredPos; //변환된 UI 좌표를 cloneRect(클론 좌표)에 넣어줌

        DamageFont text = clone.GetComponent<DamageFont>();
        text.SetDamage(attackDamage, isCritical ? Color.red : Color.white);

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
        
        GameManager.Instance.CoinIncrease();
        Debug.Log("코인 개수 = "+ GameManager.Instance.coinCount); 
    }

    void UpdateHpBar(){
        float ratio = (float)currentHealth / maxHealth;
        hpFill.fillAmount = ratio;

        if(hpBar != null){
            hpBar.position = transform.position + new Vector3(0,0.65f, 0);
        } 
    }

}
