using UnityEngine;

    public class SpawnHNeedle : MonoBehaviour
    {
        [SerializeField] private GameObject _needlePrefab;
        [SerializeField] private float _spawnInterval = 0.5f;
        [SerializeField] private float _activeDuration = 5f; // 활성화 지속 시간
        [SerializeField] private int _number;
        [SerializeField] private float _needleSpeed;
    [SerializeField] private Transform _parent;

        private float _timer;
        private float _activeTimer;
        private bool _spawnTop = true;

        private void OnEnable()
        {
            _activeTimer = 0f;
            _timer = 0f;
        }

        private void Update()
        {
            _activeTimer += Time.deltaTime;
            if (_activeTimer >= _activeDuration)
            {
                gameObject.SetActive(false);
                return;
            }

            _timer += Time.deltaTime;
            if (_timer >= _spawnInterval)
            {
                _timer = 0f;
                SpawnNeedle();
            }
        }

        private void SpawnNeedle()
        {
            Vector2 spawnPos = GetSpawnPosition();
            Vector2 dir = _number == 1 ? Vector2.right : Vector2.left;

            GameObject needle = Instantiate(_needlePrefab, spawnPos, Quaternion.identity,_parent.transform);

            if (dir.x > 0)
            {
                needle.transform.Rotate(0, 180f, 0);
            }

            needle.GetComponent<Rigidbody2D>().linearVelocity = dir * _needleSpeed;

            _spawnTop = !_spawnTop;
        }

        private Vector2 GetSpawnPosition()
        {
            float offsetY = _spawnTop ? 1f : -1f;
            return new Vector2(transform.position.x, transform.position.y + offsetY);
        }

    }

