using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;


public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int hp = 1;
    [SerializeField] private float _flashDuration = 0.1f;
    [SerializeField] private float _shakeDuration = 0.3f;
    [SerializeField] private float _shakeStrength = 0.5f;
    [SerializeField] private Image _flashImage;
    [SerializeField] private Image _hpBarImage; // fillAmount 방식 HP바 Image
    [SerializeField] private float _hpBarDuration = 0.3f; // HP바 줄어드는 애니메이션 시간

    private int _curhp;
    private SpriteRenderer _sr;

    public static PlayerHealth Instance;

    private void Awake()
    {
        Instance = this;
        _curhp = hp;
        _flashImage.color = new Color(1, 0, 0, 0);
        _sr = GetComponent<SpriteRenderer>();

        // HP바 초기화
        if (_hpBarImage != null)
        {
            _hpBarImage.fillAmount = 1f;
        }
    }

    public void TakeDamage(int damage)
    {
        _curhp -= damage;
        _curhp = Mathf.Max(_curhp, 0); // 0 아래로 내려가지 않도록

        Flash();
        CameraShake();
        UpdateHpBar();

        if (_curhp <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    private void UpdateHpBar()
    {
        if (_hpBarImage == null) return;

        float targetFill = (float)_curhp / hp;

        _hpBarImage.DOKill();
        _hpBarImage.DOFillAmount(targetFill, _hpBarDuration)
                   .SetEase(Ease.OutCubic);
    }

    private void Flash()
    {
        _flashImage.DOKill();
        _flashImage.DOFade(0.5f, _flashDuration)
                   .OnComplete(() => _flashImage.DOFade(0f, _flashDuration));
    }

    private void CameraShake()
    {
        Camera.main.DOShakePosition(_shakeDuration, _shakeStrength);
    }
}
