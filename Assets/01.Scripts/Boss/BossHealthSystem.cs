using DG.Tweening;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BossHealthSystem : MonoBehaviour
{
    [Header("HP")]
    [SerializeField] private int hp = 5;
    private int _curHp;

    [Header("Hit Effect")]
    [SerializeField] private float _shakeDuration = 0.2f;
    [SerializeField] private float _shakeStrength = 0.3f;
    [SerializeField] private float _hitStopDuration = 0.05f;

    [Header("HP Bar")]
    [SerializeField] private Image _hpBarImage;
    [SerializeField] private float _hpBarDuration = 0.3f;

    private void Awake()
    {
        _curHp = hp;

        if (_hpBarImage != null)
            _hpBarImage.fillAmount = 1f;
    }

    public void TakeDamage(int damage)
    {
        _curHp -= damage;
        _curHp = Mathf.Max(_curHp, 0);

        UpdateHpBar();
        CameraShake();
        StartCoroutine(HitStop());

        if (_curHp <= 0)
        {
            Time.timeScale = 1f; // 죽기 전에 타임스케일 복구
            Destroy(gameObject);
        }
    }
    private void UpdateHpBar()
    {
        if (_hpBarImage == null) return;

        float targetFill = (float)_curHp / hp;

        _hpBarImage.DOKill();
        _hpBarImage.DOFillAmount(targetFill, _hpBarDuration)
                   .SetEase(Ease.OutCubic);
    }

    private void CameraShake()
    {
        Camera.main.DOShakePosition(_shakeDuration, _shakeStrength);
    }

    private IEnumerator HitStop()
    {
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(_hitStopDuration);
        Time.timeScale = 1f;
    }
}
