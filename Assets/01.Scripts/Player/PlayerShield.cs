using UnityEngine;

public class PlayerShield : MonoBehaviour
{
    [SerializeField] private Transform pivot;
    [SerializeField] private float _angleOffset = 0f;
    [SerializeField] private float _minDistance = 1f; // 이 거리 안으로 들어오면 멈춤

    private void Update()
    {
        Aim();
    }

    public void Aim()
    {
        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 dir = mouseWorld - transform.position;

        if (dir.magnitude < _minDistance) return; // 너무 가까우면 회전 안함

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        pivot.rotation = Quaternion.Euler(0f, 0f, angle + _angleOffset);
    }
}
