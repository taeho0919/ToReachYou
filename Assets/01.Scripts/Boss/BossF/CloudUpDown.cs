using DG.Tweening;
using UnityEngine;

public enum CloudAttackType
{
    Up,
    Down
}

public class CloudUpDown : MonoBehaviour
{
    public CloudAttackType type;
    [SerializeField] private float moveDistance = 10f;
    [SerializeField] private float duration = 2f;
    [SerializeField] private float returnDuration = 2f;
    [SerializeField] private Ease ease = Ease.InOutSine;
    [SerializeField] private Ease returnEase = Ease.InOutSine;

    private Vector3 startPos;
    private Sequence moveSequence;

    private void Awake()
    {
        startPos = transform.position;
    }

    private void OnEnable()
    {
        // 풀링 재사용 시 시작 위치를 매번 갱신하고 싶다면 아래 줄 활성화
        // startPos = transform.position;

        transform.position = startPos;

        Vector3 targetPos = startPos + GetDirection(type) * moveDistance;

        moveSequence = DOTween.Sequence();
        moveSequence.Append(transform.DOMove(targetPos, duration).SetEase(ease));
        moveSequence.Append(transform.DOMove(startPos, returnDuration).SetEase(returnEase));
        moveSequence.OnComplete(() =>
        {
            gameObject.SetActive(false);
        });
    }

    private void OnDisable()
    {
        moveSequence?.Kill();
        transform.position = startPos;
    }

    private Vector3 GetDirection(CloudAttackType attackType)
    {
        switch (attackType)
        {
            case CloudAttackType.Up:
                return Vector3.up;
            case CloudAttackType.Down:
                return Vector3.down;
            default:
                return Vector3.zero;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth.Instance.TakeDamage(1);
        }
    }
}
