using UnityEngine;

public class EnemiesIA : MonoBehaviour
{
    private Transform _target;
    private float _timerForRandomize;
    private float _randomizeDelay = 1f;
    public Vector2 _direction; public Vector2 Direction { get => _direction; }

    private void Awake()
    {
        _target = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void Start()
    {
        _timerForRandomize = Time.time + _randomizeDelay;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > _timerForRandomize)
        {
            int _percent = Random.Range(1, 100);
            if (_percent <= 25)
            {
                _direction = (Random.Range(0, 1) == 0) ? Vector2.left : Vector2.right;
            }
            else
            {
                _direction = _target.position;
            }
            _timerForRandomize = Time.time + _randomizeDelay;
        }
    }
}