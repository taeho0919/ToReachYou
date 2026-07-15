using DG.Tweening;
using System.Collections;
using UnityEngine;

public class Clouds : MonoBehaviour
{
    [SerializeField] private GameObject cloudPrefab;
    [SerializeField] private Transform player;

    [SerializeField] private int count = 8;
    [SerializeField] private float radius = 15f;
    [SerializeField] private float moveDuration = 2f;

    private void OnEnable()
    {
        if (player != null)
        {
            SpawnClouds();
            StartCoroutine(DisableAfterDelay());
        }
    }

    private void SpawnClouds()
    {
        Vector3 center = player.position;

        for (int i = 0; i < count; i++)
        {
            float angle = i * Mathf.PI * 2f / count;

            Vector3 spawnPos = center + new Vector3(
                Mathf.Cos(angle),
                Mathf.Sin(angle),
                0f
            ) * radius;

            GameObject cloud = Instantiate(cloudPrefab, spawnPos, Quaternion.identity);

            cloud.transform.DOMove(center, moveDuration)
                .SetEase(Ease.InQuad)
                .OnComplete(() =>
                {
                    if (cloud != null) Destroy(cloud);
                });
        }
    }

    private IEnumerator DisableAfterDelay()
    {
        yield return new WaitForSeconds(moveDuration);
        gameObject.SetActive(false);
    }
}
