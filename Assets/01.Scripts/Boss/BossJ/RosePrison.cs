using UnityEngine;
using DG.Tweening;
using System.Collections;

enum RosePrisonDir
{
    Up,
    Down,
    Left,
    Right
}

public class RosePrison : MonoBehaviour
{
    [SerializeField] private RosePrisonDir _dirType;
    [SerializeField] private float _moveDistance = 1f;
    [SerializeField] private float _moveDuration = 0.5f;
    [SerializeField] private Ease _easeType = Ease.OutQuad;

    private Vector2 _startPos;

    private void Awake()
    {
        _startPos = transform.position;
    }

    private void OnEnable()
    {
        Vector2 target = GetTargetPosition(_dirType);

        transform.DOMove(target, _moveDuration)
                 .SetEase(_easeType)
                 .OnComplete(() =>
                 {
                     // 5초 대기 후 원래 위치로 복귀
                     transform.DOMove(_startPos, _moveDuration)
                              .SetEase(_easeType)
                              .SetDelay(5f)
                              .OnComplete(() => gameObject.SetActive(false));
                 });
    }
    private void OnDisable()
    {
        transform.DOKill();
        transform.position = _startPos;
    }

    private Vector2 GetTargetPosition(RosePrisonDir dir)
    {
        return dir switch
        {
            RosePrisonDir.Up => _startPos + Vector2.up * _moveDistance,
            RosePrisonDir.Down => _startPos + Vector2.down * _moveDistance,
            RosePrisonDir.Left => _startPos + Vector2.left * _moveDistance,
            RosePrisonDir.Right => _startPos + Vector2.right * _moveDistance,
            _ => _startPos
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
