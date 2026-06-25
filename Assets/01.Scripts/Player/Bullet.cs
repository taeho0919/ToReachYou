using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed;

    private Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = Vector2.up * _speed; // deltaTime 제거!
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("BossMain"))
        {
            if (collision.TryGetComponent<BossHealthSystem>(out BossHealthSystem bossHealth))
            {
                bossHealth.TakeDamage(1);
                Destroy(gameObject);
            }

            
        }
    }
}
