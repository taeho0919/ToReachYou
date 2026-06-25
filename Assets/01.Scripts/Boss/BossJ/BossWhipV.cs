using DG.Tweening;
using UnityEngine;

enum RoseWhipDirV
{
    Up,
    Down,
}

public class BossWhipV : MonoBehaviour
{
    [SerializeField] private RoseWhipDirV _dirTypeV;
    [SerializeField] private float _moveDistance = 12f;
    [SerializeField] private float _moveDuration = 0.5f;
    [SerializeField] private float _warningDelay = 2f;
    [SerializeField] private float _returnDelay = 1f;
    [SerializeField] private Ease _easeType = Ease.OutQuad;

    private Vector2 _startPos;
    private Transform _player;

    private void Awake()
    {
        _startPos = transform.position;
        _player = GameObject.FindWithTag("Player").transform;
    }

    private void OnEnable()
    {
        _startPos = transform.position;
        DoWarning();
    }

    private void OnDisable()
    {
        transform.DOKill();
        transform.position = _startPos;
    }

    // 1단계: 플레이어 방향으로 예고 이동
    private void DoWarning()
    {
        Vector2 warningPos = GetWarningPosition();

        transform.DOMove(warningPos, _moveDuration)
                 .SetEase(_easeType)
                 .OnComplete(() => DoThrust(warningPos));
    }

    // 2단계: 대기 후 돌진
    private void DoThrust(Vector2 from)
    {
        Vector2 thrustTarget = GetTargetPosition(from);

        transform.DOMove(thrustTarget, _moveDuration)
                 .SetEase(_easeType)
                 .SetDelay(_warningDelay)
                 .OnComplete(() => DoReturn(thrustTarget));
    }

    // 3단계: Y 후퇴 후 X 복귀
    private void DoReturn(Vector2 from)
    {
        Vector2 yOnlyReturn = new Vector2(from.x, _startPos.y);

        transform.DOMove(yOnlyReturn, _moveDuration)
                 .SetEase(_easeType)
                 .SetDelay(_returnDelay)
                 .OnComplete(() =>
                 {
                     transform.DOMove(_startPos, _moveDuration)
                              .SetEase(_easeType)
                              .OnComplete(() => gameObject.SetActive(false));
                 });
    }

    private Vector2 GetWarningPosition()
    {
        float playerX = _player != null ? _player.position.x : _startPos.x;
        float signX = playerX > _startPos.x ? 1f : -1f;
        return new Vector2(_startPos.x + signX * 1f, _startPos.y);
    }

    private Vector2 GetTargetPosition(Vector2 from)
    {
        return _dirTypeV switch
        {
            RoseWhipDirV.Up => from + Vector2.up * _moveDistance,
            RoseWhipDirV.Down => from + Vector2.down * _moveDistance,
            _ => from
        };
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth.Instance.TakeDamage(1);
        }
    }
}


