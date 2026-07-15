using DG.Tweening;
using UnityEngine;

public class CloudWave : MonoBehaviour
{
    [SerializeField] private float moveDistance = 10f;
    [SerializeField] private float moveDuration = 3f;

    [SerializeField] private float waveHeight = 0.5f;
    [SerializeField] private float waveSpeed = 5f;

    [SerializeField] private SpriteRenderer spriteRenderer;

    private bool moveLeft = true;
    private Vector3 startPos;
    private Tween moveTween;

    private void OnEnable()
    {
        startPos = transform.position;

        Vector3 targetPos = startPos + (moveLeft ? Vector3.left : Vector3.right) * moveDistance;

        spriteRenderer.flipX = moveLeft;

        moveTween = transform.DOMove(targetPos, moveDuration)
            .SetEase(Ease.Linear)
            .OnComplete(() =>
            {
                moveLeft = !moveLeft;
                gameObject.SetActive(false);
            });
    }

    private void Update()
    {
        float yOffset = Mathf.Sin(Time.time * waveSpeed) * waveHeight;

        transform.position = new Vector3(
            transform.position.x,
            startPos.y + yOffset,
            transform.position.z);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerHealth.Instance.TakeDamage(1);
        }
    }

    private void OnDisable()
    {
        moveTween?.Kill();
    }
}
