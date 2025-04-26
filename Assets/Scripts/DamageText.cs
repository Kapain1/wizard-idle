using UnityEngine;

public class DamageText : MonoBehaviour
{
    //텍스트
    public GameObject damageTextPrefab;
    public Transform canvasTransform;
    void Start()
    {
        canvasTransform = GameObject.Find("FontCanvas").transform;
        
    }

    public void TextRecall(int attackDamage, bool isCritical){
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
    }
}
