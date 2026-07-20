using DG.Tweening;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class TextArrowSpawn : MonoBehaviour
{
    [Header("스폰")]
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private GameObject[] textArrow;

    [Header("스폰 간격 (랜덤)")]
    [SerializeField] private float minInterval = 0.3f;
    [SerializeField] private float maxInterval = 1f;

    [Header("등장/퇴장 연출")]
    [SerializeField] private float enterDuration = 0.5f;
    [SerializeField] private float exitDuration = 0.5f;
    [SerializeField] private float moveDistance = 3f;      // 이동할 거리
    [SerializeField] private Ease enterEase = Ease.OutCubic;
    [SerializeField] private Ease exitEase = Ease.InCubic;
    [SerializeField] private float stayDuration = 4f;      // 화면에 머무는 시간 (기존 LifeTime 대체)

    private Coroutine spawnRoutine;
    private Vector3 originPos;   // 원래(등장 완료) 위치
    private Tween moveTween;

    private void Awake()
    {
        originPos = transform.localPosition;
    }

    private void OnEnable()
    {
        StartCoroutine(PlaySequence());
    }

    private void OnDisable()
    {
        if (spawnRoutine != null)
        {
            StopCoroutine(spawnRoutine);
            spawnRoutine = null;
        }

        moveTween?.Kill();
    }

    private IEnumerator PlaySequence()
    {
        // 1) 아래쪽에서 시작 위치로 세팅
        Vector3 startPos = originPos + Vector3.down * moveDistance;
        transform.localPosition = startPos;

        // 2) 아래 -> 원위치로 등장 (밑에서 위로 올라옴)
        moveTween = transform.DOLocalMove(originPos, enterDuration).SetEase(enterEase);
        yield return moveTween.WaitForCompletion();

        // 3) 등장 완료 후 스폰 시작
        spawnRoutine = StartCoroutine(SpawnLoop());

        // 4) 머무는 시간 대기
        yield return new WaitForSeconds(stayDuration);

        // 5) 스폰 중지
        if (spawnRoutine != null)
        {
            StopCoroutine(spawnRoutine);
            spawnRoutine = null;
        }

        // 6) 다시 아래로 내려가며 퇴장
        Vector3 exitPos = originPos + Vector3.down * moveDistance;
        moveTween = transform.DOLocalMove(exitPos, exitDuration).SetEase(exitEase);
        yield return moveTween.WaitForCompletion();

        // 7) 퇴장 완료 후 비활성화
        gameObject.SetActive(false);
    }

    private IEnumerator SpawnLoop()
    {
        while (true)
        {
            float wait = Random.Range(minInterval, maxInterval);
            yield return new WaitForSeconds(wait);

            if (textArrow != null && spawnPoint != null && textArrow.Length > 0)
            {
                Instantiate(textArrow[Random.Range(0, textArrow.Length)], spawnPoint.position, spawnPoint.rotation);
            }
        }
    }


}
