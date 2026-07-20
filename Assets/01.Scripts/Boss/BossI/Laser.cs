using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private float speed = 15f;
    [SerializeField] private float lifeTime = 2f;

    private Vector2 dir;

    public void Init(Vector2 direction)
    {
        dir = direction.normalized;

        transform.right = dir;
    }

    private void Update()
    {
        transform.position += (Vector3)(dir * speed * Time.deltaTime);
    }

    private void OnEnable()
    {
        Destroy(gameObject, lifeTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerHealth.Instance.TakeDamage(1);
        }
    }
}
