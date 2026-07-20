using DG.Tweening;
using UnityEngine;

public class TextArrow : MonoBehaviour
{
    [Header("예비 동작 (위/아래/가운데 랜덤)")]
    [SerializeField] private float telegraphDistance = 0.5f;
    [SerializeField] private float telegraphDuration = 0.2f;
    [SerializeField] private Ease telegraphEase = Ease.OutQuad;

    [Header("돌진 (오른쪽)")]
    [SerializeField] private float dashDistance = 10f;
    [SerializeField] private float dashDuration = 0.3f;
    [SerializeField] private Ease dashEase = Ease.InQuad;

    [Header("수명")]
    [SerializeField] private float destroyDelay = 0.5f; // 돌진 끝난 후 파괴까지 여유시간

    private enum TelegraphDir { Up, Down, Center }

    private void Start()
    {
        PlaySequence();
    }

    private void PlaySequence()
    {
        // 위/아래/가운데 중 랜덤 선택
        TelegraphDir dir = (TelegraphDir)Random.Range(0, 3);

        Vector3 telegraphOffset = Vector3.zero;
        switch (dir)
        {
            case TelegraphDir.Up:
                telegraphOffset = Vector3.up * telegraphDistance;
                break;
            case TelegraphDir.Down:
                telegraphOffset = Vector3.down * telegraphDistance;
                break;
            case TelegraphDir.Center:
                telegraphOffset = Vector3.zero; // 제자리 (혹은 원한다면 작은 딜레이만 주기)
                break;
        }

        Vector3 telegraphPos = transform.localPosition + telegraphOffset;

        Sequence seq = DOTween.Sequence();

        // 1) 예비 동작 (위/아래/가운데)
        if (dir != TelegraphDir.Center)
        {
            seq.Append(transform.DOLocalMove(telegraphPos, telegraphDuration).SetEase(telegraphEase));
        }
        else
        {
            seq.AppendInterval(telegraphDuration); // 가운데는 제자리에서 잠깐 대기
        }

        // 2) 오른쪽으로 빠르게 돌진
        Vector3 dashPos = transform.localPosition + Vector3.right * dashDistance;
        seq.Append(transform.DOLocalMove(dashPos, dashDuration).SetEase(dashEase));

        // 3) 돌진 끝나면 파괴
        seq.AppendCallback(() =>
        {
            Destroy(gameObject, destroyDelay);
        });
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerS")) // 방패
        {
            Destroy(gameObject);
            return;
        }

        if (collision.CompareTag("Player"))
        {
            PlayerHealth.Instance.TakeDamage(1);
            Destroy(gameObject);
        }
    }

}