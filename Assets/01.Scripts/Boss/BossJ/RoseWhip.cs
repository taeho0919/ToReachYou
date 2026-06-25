using DG.Tweening;
using UnityEngine;

enum RoseWhipDir
{
    Left,
    Right,
}
public class RoseWhip : MonoBehaviour
{
     [SerializeField] private RoseWhipDir _dirType;
    [SerializeField] private float _moveDistance = 1f;
    [SerializeField] private float _moveDuration = 0.5f;
    [SerializeField] private Ease _easeType = Ease.OutQuad;

    private Vector2 _startPos;

    private void Awake()
    {
        _startPos = transform.position; // 최초 1회만 저장
    }

    private void OnEnable()
    {
        // _startPos 재저장 제거
        transform.position = _startPos; // 켜질 때 항상 원래 위치로 강제 이동

        Vector2 target = GetTargetPosition(_dirType);

        transform.DOMove(target, _moveDuration)
                 .SetEase(_easeType)
                 .OnComplete(() =>
                 {
                     transform.DOMove(_startPos, _moveDuration)
                              .SetEase(_easeType)
                              .SetDelay(5f)
                              .OnComplete(() => gameObject.SetActive(false));
                 });
    }

    private void OnDisable()
    {
        transform.DOKill();
        // position 초기화 제거 (OnEnable에서 처리)
    }
    private Vector2 GetTargetPosition(RoseWhipDir dir)
    {
        return dir switch
        {
            RoseWhipDir.Left => _startPos + Vector2.left * _moveDistance,
            RoseWhipDir.Right => _startPos + Vector2.right * _moveDistance,
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
