using UnityEditor;
using UnityEditor.ShaderGraph;
using UnityEngine;

public class BossIShield : MonoBehaviour
{
    [Header("플레이어 따라가기")]
    [SerializeField] private Transform player;
    [SerializeField] private float moveSpeed = 5f;   // 따라가는 속도
    [SerializeField] private float offsetY = 0f;      // 필요하면 y축 오프셋
    [SerializeField] private float offsetZ = 0f;      // 필요하면 z축 오프셋

    [Header("방패 체력")]
    [SerializeField]private int hp = 5;
    private int curentHp;

    [SerializeField]private Color[] hitColor;
    private Color Ocolor;

    private SpriteRenderer sr;
    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        Ocolor=sr.color;    
    }
    private void OnEnable()
    {
        curentHp=hp;
        sr.color = Ocolor;
    }

    private void Update()
    {
        if (player == null) return;

        // 목표 위치: x는 플레이어를 따라가고, y/z는 현재 값(또는 오프셋) 유지
        Vector3 targetPos = new Vector3(player.position.x, transform.position.y, transform.position.z);

        // 부드럽게 이동
        transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerW"))
        {
            BossHealthSystem.instance.CameraShake();
            TakeShieldDamage(1);   // ← 여기서 curentHp가 5 → 4로 감소
            Destroy(collision.gameObject);
        }
    }


    private void TakeShieldDamage(int damage)
    {
        curentHp-=damage;
        NextColor();           // ← curentHp가 아직 감소하기 전! (여전히 5)
        if (curentHp == 0)
        {
           
            curentHp = hp;
            gameObject.SetActive(false);
        }
    }
    private void NextColor()
    {
        int index = curentHp - 1;
        index = Mathf.Clamp(index, 0, hitColor.Length - 1);
        sr.color = hitColor[index];
    }
    private void OnDisable()
    {

    }
}
