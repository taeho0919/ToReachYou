using UnityEngine;


public class RoseArrow : MonoBehaviour
{
    [SerializeField] private float _lifetime = 3f;

    private void OnEnable()
    {
        Invoke(nameof(DestroySelf), _lifetime);
    }

    private void DestroySelf()
    {
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerHealth.Instance.TakeDamage(1);
        }
    }
}
