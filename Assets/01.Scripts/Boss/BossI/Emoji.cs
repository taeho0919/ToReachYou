using UnityEngine;

public enum AttackType
{
    good,
    Bad
}

public class Emoji : MonoBehaviour
{
    [SerializeField]private AttackType type;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((type == AttackType.good && collision.CompareTag("Player"))
    || collision.CompareTag("PlayerS"))
        { 
            GoodAttackType();
        }
        else if ((type == AttackType.Bad && collision.CompareTag("Player"))
    || collision.CompareTag("PlayerS"))
        {
            BadAttackType();
        }
    }


    private void GoodAttackType()
    {
        PlayerHealth.Instance.Heal(1);
        Debug.Log("힐");
        Destroy(gameObject);
    }
    private void BadAttackType()
    {
        PlayerHealth.Instance.TakeDamage(1);
        Debug.Log("데미지");
        Destroy(gameObject);
    }
}
