using UnityEngine;

public class EnemiesIA : MonoBehaviour
{
    [SerializeField] private Transform _parent;
    private Transform _target;
    private float _timerForRandomize;
    private float _randomizeDelayForWaling = 0.5f;
    private Vector2 _direction; public Vector2 Direction { get => _direction; }
    private bool _isStarted; public bool IsStarted { get => _isStarted; }
    private bool _isWalking; public bool IsWalking { get => _isWalking; }

    private void Awake()
    {
        _target = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void Start()
    {
        _timerForRandomize = Time.time + _randomizeDelayForWaling;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > _timerForRandomize)
        {
            if (!_isStarted) _isStarted = true;
            int _percent = Random.Range(1, 100);
            float a = transform.position.x - _target.position.x;
            _parent.right = (a > 0) ? Vector3.left : Vector3.right;
            
            _isWalking = true;
            if (_percent%2 != 0)
            {
                Vector2 left = _parent.position + Vector3.left;
                Vector2 right = _parent.position + Vector3.right;
                if (_percent <= 3)
                {
                    _direction = (a > 0) ? right : left;
                    _parent.right = -_parent.right;
                    _randomizeDelayForWaling = 1f;
                } else if (_percent > 3 && _percent <= 20)
                {
                    _direction = _parent.position;
                    _isWalking = false;
                    _randomizeDelayForWaling = 1.5f;
                } else if (_percent >= 97)
                {
                    _direction = (a > 0) ? left : right;
                    _randomizeDelayForWaling = 1f;
                }
            } else
            {
                _direction = _target.position;
                _randomizeDelayForWaling = 0.5f;
            }
            _timerForRandomize = Time.time + _randomizeDelayForWaling;
        }
    }
}