using TMPro;
using UnityEngine;
using DG.Tweening;

public class DamageFont : MonoBehaviour
{
    public float upSpeed = 1f;
    public float lifeTime = 1f;

    private TextMeshProUGUI tmp;


    void Awake() 
    {
        tmp = GetComponentInChildren<TextMeshProUGUI>();
    }

    
    public void SetDamage(int damage,Color color){
        tmp.color = color;
        tmp.text = damage.ToString();

        transform.DOMoveY(transform.position.y + 1f, 0.5f);
        tmp.DOFade(0f, 1.7f);

        Destroy(gameObject,lifeTime);
        
    }
}
