using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ScreenFade : MonoBehaviour
{
    public Image fadeImage;

    void Start()
    {
        fadeImage.gameObject.SetActive(true);
        fadeImage.color = new Color(0,0,0,1);
        FadeIn(1f);
    }

    public void FadeOut(float duration, System.Action onComplete = null){
        fadeImage.color = new Color(0,0,0,0);

        fadeImage.DOFade(1, duration).OnComplete(() => {
            onComplete?.Invoke();
        });
        
    }

    public void FadeIn(float duration, System.Action onComplete = null){
        fadeImage.color = new Color(0,0,0,1);

        fadeImage.DOFade(0,duration).OnComplete(() => {
            onComplete?.Invoke();
        });
        
    }
}
