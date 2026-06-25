using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShield : MonoBehaviour
{

    [Header("Shield Aim")]
    [SerializeField] private Transform pivot;
    [SerializeField] private float _angleOffset = 0f;
    [SerializeField] private float _minDistance = 1f;

    [Header("Gauge & Bullet")]
    [SerializeField] private float _maxGauge = 3f;
    [SerializeField] private float _gaugePerBlock = 1f;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _bulletSpawnPoint;

    [Header("Gauge UI")]
    [SerializeField] private Image _gaugeBarImage;
    [SerializeField] private float _gaugeFillDuration = 0.2f;
    [SerializeField] private float _gaugeResetDuration = 0.15f;


    private float _currentGauge = 0f;
    private bool _isReadyToFire = false;

    public float CurrentGauge => _currentGauge;
    public float MaxGauge => _maxGauge;

    private void Awake()
    {
        if (_gaugeBarImage != null)
            _gaugeBarImage.fillAmount = 0f;
    }

    private void Update()
    {
        Aim();
        HandleFireInput();
    }

    private void HandleFireInput()
    {
        if (Input.GetKeyDown(KeyCode.Q) && _isReadyToFire)
        {
            SpawnBullet();
            ResetGauge();
        }
    }

    public void Aim()
    {
        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 dir = mouseWorld - transform.position;

        if (dir.magnitude < _minDistance) return;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        pivot.rotation = Quaternion.Euler(0f, 0f, angle + _angleOffset);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("BossW"))
        {
            Destroy(collision.gameObject);
            AddGauge(_gaugePerBlock);
        }
    }



    private void AddGauge(float amount)
    {
        if (_isReadyToFire) return;

        _currentGauge += amount;

        if (_currentGauge >= _maxGauge)
        {
            _currentGauge = _maxGauge;
            _isReadyToFire = true;
            UpdateGaugeBar(_currentGauge, _gaugeFillDuration);
        }
        else
        {
            UpdateGaugeBar(_currentGauge, _gaugeFillDuration);
        }
    }

    private void ResetGauge()
    {
        _currentGauge = 0f;
        _isReadyToFire = false;
        UpdateGaugeBar(0f, _gaugeResetDuration);
    }

    private void UpdateGaugeBar(float gaugeValue, float duration, TweenCallback onComplete = null)
    {
        if (_gaugeBarImage == null) return;

        float targetFill = gaugeValue / _maxGauge;

        _gaugeBarImage.DOKill();
        var tween = _gaugeBarImage.DOFillAmount(targetFill, duration)
                                  .SetEase(Ease.OutCubic);

        if (onComplete != null)
            tween.OnComplete(onComplete);
    }

    private void SpawnBullet()
    {
        if (_bulletPrefab == null || _bulletSpawnPoint == null)
        {
            Debug.LogWarning("Bullet Prefab 또는 SpawnPoint가 설정되지 않았습니다.");
            return;
        }

        GameObject bullet = Instantiate(_bulletPrefab, _bulletSpawnPoint.position, Quaternion.identity);

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            Vector2 shootDir = pivot.right;
            float bulletSpeed = 10f;
            rb.linearVelocity = shootDir * bulletSpeed;
        }
    }
}
