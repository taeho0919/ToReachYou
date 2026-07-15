using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class blindness : MonoBehaviour
{
    [SerializeField] private Volume globalVolume;
    [SerializeField] private float targetIntensity = 0.5f;
    [SerializeField] private float duration = 2f;
    [SerializeField]private float delayTime = 1f;

    private Vignette vignette;
    private Tween tween;

    private void Awake()
    {
        globalVolume.profile.TryGet(out vignette);
    }

    private void OnEnable()
    {
        StartCoroutine(Blind());
    }

    private void OnDisable()
    {
        tween?.Kill();
    }

    private IEnumerator Blind()
    {
        FadeIn();
        yield return new WaitForSeconds(delayTime);
        FadeOut();
        yield return new WaitForSeconds(duration); // FadeOut 트윈이 끝날 때까지 대기
        gameObject.SetActive(false);
    }

    public void FadeIn()
    {
        tween?.Kill();

        tween = DOTween.To(
            () => vignette.intensity.value,
            x => vignette.intensity.value = x,
            targetIntensity,
            duration);
    }

    public void FadeOut()
    {
        tween?.Kill();

        tween = DOTween.To(
            () => vignette.intensity.value,
            x => vignette.intensity.value = x,
            0f,
            duration);
    }
}
