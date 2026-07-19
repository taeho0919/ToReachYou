
using System.Collections;
using UnityEngine;

public class BossGoldSpawn : MonoBehaviour
{
    [Header("스폰할 공격 프리팹들")]
    [SerializeField] private GameObject[] attackPrefabs;

    [Header("왼쪽 라인 (시작점 ~ 끝점)")]
    [SerializeField] private Transform leftLineStart;
    [SerializeField] private Transform leftLineEnd;

    [Header("오른쪽 라인 (시작점 ~ 끝점)")]
    [SerializeField] private Transform rightLineStart;
    [SerializeField] private Transform rightLineEnd;

    [SerializeField] private GameObject pObject;

    [Header("스폰 주기 (초)")]
    [SerializeField] private float spawnInterval = 2f;

    [SerializeField] private float lifeTime;

    private float timer;

    private void OnEnable()
    {
        timer = 0f;
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            timer = 0f;
            SpawnAttack();
        }
        StartCoroutine(Dead());
    }

    private void SpawnAttack()
    {
        if (attackPrefabs == null || attackPrefabs.Length == 0)
        {
            Debug.LogWarning("attackPrefabs가 비어있습니다!");
            return;
        }

        // 0 또는 1을 랜덤으로 뽑음 (0: 왼쪽, 1: 오른쪽)
        int side = Random.Range(0, 2);

        Vector3 spawnPos;
        Vector3 moveDirection;

        if (side == 0)
        {
            // 왼쪽 라인에서 스폰 -> 오른쪽으로 이동
            spawnPos = GetRandomPointOnLine(leftLineStart.position, leftLineEnd.position);
            moveDirection = Vector3.right;
        }
        else
        {
            // 오른쪽 라인에서 스폰 -> 왼쪽으로 이동
            spawnPos = GetRandomPointOnLine(rightLineStart.position, rightLineEnd.position);
            moveDirection = Vector3.left;
        }

        // 여러 프리팹 중 하나를 랜덤으로 선택
        GameObject prefabToSpawn = attackPrefabs[Random.Range(0, attackPrefabs.Length)];

        GameObject spawned = Instantiate(prefabToSpawn, spawnPos, Quaternion.identity,pObject.transform);

        // 스폰된 오브젝트에 BossGold 스크립트가 붙어있다면 이동 방향 전달
        BossGold bossGold = spawned.GetComponent<BossGold>();
        if (bossGold != null)
        {
            bossGold.SetMoveDirection(moveDirection);
        }
    }

    private Vector3 GetRandomPointOnLine(Vector3 start, Vector3 end)
    {
        float t = Random.Range(0f, 1f);
        return Vector3.Lerp(start, end, t);
    }
    private IEnumerator Dead()
    {
        yield return new WaitForSeconds(lifeTime);
        gameObject.SetActive(false);
    }
}
