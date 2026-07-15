using DG.Tweening;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerHealth.Instance.TakeDamage(1);
            Destroy(gameObject);
        }
    }
    private void OnDestroy()
    {
        DOTween.Kill(transform);
    }
}
