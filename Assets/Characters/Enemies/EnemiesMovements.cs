using UnityEngine;

public class EnemiesMovements : MonoBehaviour
{
    [SerializeField] private Transform _parent;
    [SerializeField] EnemiesIA _enemyIA;
    private Transform _target;
    private int _direction;
    private Vector3 _v;

    private void Awake()
    {
        _target = GameObject.FindGameObjectWithTag("Player").transform;
        _v = _parent.position;
    }
    private void Start()
    {
        _direction = (_parent.transform.position.x - _target.position.x < 0) ? 1 : -1;
        _parent.right = _direction * Vector3.right;
    }
    void Update()
    {
        _direction = (_parent.transform.position.x - _target.position.x < 0) ? 1 : -1;
        if (!_enemyIA.IsBusy)
        {
            _parent.right = _direction * Vector3.right;
        }
        _parent.position = Vector2.MoveTowards(_parent.position, _v, 0.002f);
    }

    public void MoveForward()
    {
        _enemyIA.IsMoving = true;
        _v = _parent.position + (Vector3.right * _direction * 10);
    }

    public void MoveBackward()
    {
        _enemyIA.IsMoving = true;
        _parent.right = -_parent.right;
        _v = _parent.position + (Vector3.right * -_direction * 10);
    }

    public void MoveToTarget()
    {
        _enemyIA.IsMoving = true;
        _v =  _target.position;
    }

    public void DontMove()
    {
        _enemyIA.IsMoving = false;
        _v = _parent.position;
    }
}
#region OLD
//[SerializeField] private Transform _parent;
//private Transform _target;
//private float _timerForRandomize;
//private float _randomizeDelayForWaling = 0.5f;
//private Vector2 _direction; public Vector2 Direction { get => _direction; }
//private bool _isStarted; public bool IsStarted { get => _isStarted; }
//private bool _isWalking; public bool IsWalking { get => _isWalking; }


//void Start()
//{
//    _timerForRandomize = Time.time + _randomizeDelayForWaling;
//}

//// Update is called once per frame
//void Update()
//{
//    if (Time.time > _timerForRandomize)
//    {
//        if (!_isStarted) _isStarted = true;
//        int _percent = Random.Range(1, 100);
//        float a = transform.position.x - _target.position.x;
//        _parent.right = (a > 0) ? Vector3.left : Vector3.right;

//        _isWalking = true;
//        if (_percent%2 != 0)
//        {
//            Vector2 left = _parent.position + Vector3.left;
//            Vector2 right = _parent.position + Vector3.right;
//            if (_percent <= 3)
//            {
//                _direction = (a > 0) ? right : left;
//                _parent.right = -_parent.right;
//                _randomizeDelayForWaling = 1f;
//            } else if (_percent > 3 && _percent <= 20)
//            {
//                _direction = _parent.position;
//                _isWalking = false;
//                _randomizeDelayForWaling = 1.5f;
//            } else if (_percent >= 97)
//            {
//                _direction = (a > 0) ? left : right;
//                _randomizeDelayForWaling = 1f;
//            }
//        } else
//        {
//            _direction = _target.position;
//            _randomizeDelayForWaling = 0.5f;
//        }
//        _timerForRandomize = Time.time + _randomizeDelayForWaling;
//    }
//}
#endregion