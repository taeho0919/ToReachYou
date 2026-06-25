using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private float _speed;
    [SerializeField] private Vector2 _offset = new Vector2(1f, 1f);

    [Header("Dash")]
    [SerializeField] private float _dashForce = 10f;
    [SerializeField] private float _dashDuration = 0.15f;
    [SerializeField] private float _dashCooldown = 1f;

    private Vector2 _dir;
    private bool _isDashing;
    private float _dashTimer;
    private float _cooldownTimer;
    private bool _isNoDash;
    private Collider2D _collider;

    void Start()
    {
        _collider = GetComponent<Collider2D>();
    }

    void Update()
    {
        if (_isDashing)
        {
            _dashTimer -= Time.deltaTime;
            if (_dashTimer <= 0f)
            {
                _isDashing = false;
                _collider.enabled = true; // 무적 해제
            }
        }
        else
        {
            _rb.linearVelocity = _dir * _speed;
        }

        _cooldownTimer -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.LeftShift) && _cooldownTimer <= 0f)
        {
            Dash();
        }

        ClampToCamera();
    }

        private void Dash()
        {
              if (_isNoDash) return;

            _isDashing = true;
            _dashTimer = _dashDuration;
            _cooldownTimer = _dashCooldown;

            _collider.enabled = false;

            _rb.linearVelocity = Vector2.zero;
            if (_dir.x != 0 || _dir.y != 0)
                _rb.AddForce(_dir * _dashForce, ForceMode2D.Impulse);
            else
                _rb.AddForce(Vector2.up * _dashForce, ForceMode2D.Impulse); // 입력 없으면 위쪽으로 대쉬
        }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Boss")|| collision.gameObject.CompareTag("BossMain"))
        {
            _isNoDash = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Boss")||collision.gameObject.CompareTag("BossMain"))
        {
            _isNoDash = false;
        }
    }
    private void ClampToCamera()
    {
        Camera cam = Camera.main;
        Vector3 min = cam.ViewportToWorldPoint(new Vector2(0, 0));
        Vector3 max = cam.ViewportToWorldPoint(new Vector2(1, 1));

        Vector2 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, min.x + _offset.x, max.x - _offset.x);
        pos.y = Mathf.Clamp(pos.y, min.y + _offset.y, max.y - _offset.y);
        transform.position = pos;
    }

    private void OnMove(InputValue value)
    {
        _dir = value.Get<Vector2>();
    }
}