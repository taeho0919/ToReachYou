using UnityEngine;
using System.Collections;

public class Eye : MonoBehaviour
{
    [Header("눈동자")]
    [SerializeField] private Transform player;
    [SerializeField] private Transform pupil;
    [SerializeField] private float pupilDistance = 0.15f;

    [Header("레이저")]
    [SerializeField] private GameObject laserPrefab;
    [SerializeField] private int laserCount = 8;
    [SerializeField] private float rotateAngle = 22.5f;
    [SerializeField] private float fireInterval = 1f;
    [SerializeField] private GameObject pObject;

    private float currentAngle;
    private Vector3 pupilStartPos;

    private void Awake()
    {
        pupilStartPos = pupil.localPosition;
    }

    private void OnEnable()
    {
        currentAngle = 0;
        StartCoroutine(FirePattern());
       StartCoroutine(LifeTime());
    }

    private void Update()
    {
        EyeLook();
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private void EyeLook()
    {
        if (player == null) return;

        Vector2 dir = (player.position - transform.position).normalized;

        pupil.localPosition = pupilStartPos + (Vector3)(dir * pupilDistance);
    }

    IEnumerator FirePattern()
    {
        yield return new WaitForSeconds(2f);
        while (true)
        {
        
            Fire();

            currentAngle += rotateAngle;

            yield return new WaitForSeconds(fireInterval);
        }
    }

    void Fire()
    {
        float step = 360f / laserCount;

        for (int i = 0; i < laserCount; i++)
        {
            float angle = currentAngle + step * i;

            Vector2 dir = new Vector2(
                Mathf.Cos(angle * Mathf.Deg2Rad),
                Mathf.Sin(angle * Mathf.Deg2Rad));

            GameObject laser = Instantiate(
                laserPrefab,
                transform.position,
                Quaternion.identity,
                pObject.transform);

            laser.GetComponent<Laser>().Init(dir);
        }
    }

    private IEnumerator LifeTime()
    {
        yield return new WaitForSeconds(5f);
        BossIEffect.Instance.CloseEye();
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
    }
}