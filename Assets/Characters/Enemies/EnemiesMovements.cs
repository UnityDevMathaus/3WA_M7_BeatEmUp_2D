using UnityEngine;

public class EnemiesMovements : MonoBehaviour
{
    [SerializeField] private Transform _parent;
    [SerializeField] Enemies _enemy;
    private Transform _target;
    private int _direction;
    private Vector3 _v;
    //################################################################################################################################
    #region UNITY API
    void Awake()
    {
        _target = GameObject.FindGameObjectWithTag("Player").transform;
        _v = _parent.position;
    }
    void Start()
    {
        _direction = (_parent.transform.position.x - _target.position.x < 0) ? 1 : -1;
        _parent.right = _direction * Vector3.right;
    }
    void Update()
    {
        if (!_enemy.IsDying)
        {
            _direction = (_parent.transform.position.x - _target.position.x < 0) ? 1 : -1;
            if (!_enemy.EnemyIA.IsBusy)
            {
                _parent.right = _direction * Vector3.right;
            }
            _parent.position = Vector2.MoveTowards(_parent.position, _v, 0.002f);
        }
    }
    #endregion
    //################################################################################################################################
    public void MoveForward()
    {
        _enemy.IsMoving = true;
        _v = _parent.position + (Vector3.right * _direction * 10);
    }
    public void MoveBackward()
    {
        _enemy.IsMoving = true;
        _parent.right = -_parent.right;
        _v = _parent.position + (Vector3.right * -_direction * 10);
    }
    public void MoveToTarget()
    {
        _enemy.IsMoving = true;
        _v =  _target.position;
    }
    public void DontMove()
    {
        _enemy.IsMoving = false;
        _v = _parent.position;
    }
    //################################################################################################################################
}