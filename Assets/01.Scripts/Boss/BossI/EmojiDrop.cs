using System.Collections;
using UnityEngine;

public class EmojiDrop : MonoBehaviour
{
    [Header("생성할 프리팹들")]
    [SerializeField] private GameObject[] prefabs;

    [Header("생성 개수")]
    [SerializeField] private int spawnCount = 5;

    [Header("X축 간격")]
    [SerializeField] private float spacing = 2f;

    [Header("부모 오브젝트")]
    [SerializeField] private Transform parent;

    [Header("지속시간")]
    [SerializeField] private float lifeTime;
    private void OnEnable()
    {
        SpawnObjects();
        StartCoroutine(LifeTime());
    }

    private void SpawnObjects()
    {
        if (prefabs == null || prefabs.Length == 0) return;

        float startX = transform.position.x - ((spawnCount - 1) * spacing) * 0.5f;
        float y = transform.position.y;

        for (int i = 0; i < spawnCount; i++)
        {
            Vector3 pos = new Vector3(startX + i * spacing, y, transform.position.z);

            GameObject prefab = prefabs[Random.Range(0, prefabs.Length)];

            Instantiate(prefab, pos, Quaternion.identity, parent);
        }
    }

    private IEnumerator LifeTime()
    {
        yield return new WaitForSeconds(lifeTime);
        gameObject.SetActive(false);
    }
}
