using UnityEngine;

public class BossGold : MonoBehaviour
{
    [Header("이동 속도")]
    [SerializeField] private float moveSpeed = 5f;

    [Header("Z축 회전 속도 (도/초)")]
    [SerializeField] private float rotateSpeed = 360f;

    // 이동 방향 (Vector3.left 또는 Vector3.right)
    private Vector3 moveDirection = Vector3.left;

    // 외부(스포너)에서 이동 방향을 설정하는 함수
    public void SetMoveDirection(Vector3 direction)
    {
        moveDirection = direction;
    }

    private void Update()
    {
        // 이동
        transform.position += moveDirection * moveSpeed * Time.deltaTime;

        // Z축으로 계속 회전
        transform.Rotate(0f, 0f, rotateSpeed * Time.deltaTime);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth.Instance.TakeDamage(1);
        }
    }
}
