using TMPro;
using UnityEngine;

public class DamageFont : MonoBehaviour
{
    public float upSpeed = 1f;
    public float lifeTime = 1f;

    private TextMeshProUGUI tmp;


    void Awake() 
    {
        tmp = GetComponentInChildren<TextMeshProUGUI>();
    }
    void Start()
    {
        Destroy(gameObject,lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * upSpeed * Time.deltaTime);
    }
    //데미지 내용을 MonsterHealth에서 가져오고 색깔을 입힘
    public void SetDamage(int amount,Color color){
        tmp.text = amount.ToString();
        tmp.color = color;
    }
}
