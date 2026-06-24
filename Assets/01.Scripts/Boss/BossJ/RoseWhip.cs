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

    private Vector2 GetTargetPosition(RoseWhipDir dir)
    {
        return dir switch
        {
            RoseWhipDir.Left => _startPos + Vector2.left * _moveDistance,
            RoseWhipDir.Right => _startPos + Vector2.right * _moveDistance,
            _ => _startPos
        };
    }

}
