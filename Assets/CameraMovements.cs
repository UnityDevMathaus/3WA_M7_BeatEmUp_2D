using UnityEngine;

public class CameraMovements : MonoBehaviour
{
    [SerializeField] private Transform _player1Transform;
    [SerializeField] private bool _safeZone; public bool SafeZone { get => _safeZone; set => _safeZone = value; }
    [SerializeField] private float _levelLimit = 54.97f;
    [SerializeField] private float[] _checkpointsPosition
    = new float[] { -54.97f, -44.97f, -35.27f, -19.97f, -2.45f, 18.8f, 45.06f };
    //-50; -40; -29; -16; 2; 23; 50

    private Camera _camera;
    private Vector3 _cameraPosition;
    private float _positionX;
    [SerializeField] private int _cameraStep; public int CameraStep { get => _cameraStep; set => _cameraStep = value; }
    [SerializeField] private IntVariable _currentCheckpoint;
    private int _lastCheckpoint; public int LastCheckpoint { get => _lastCheckpoint; set => _lastCheckpoint = value; }


    void Awake()
    {
        _camera = Camera.main;
    }
    void Start()
    {
        _cameraPosition = _camera.transform.position;
    }
    void Update()
    {
        _positionX = _player1Transform.position.x;
        if (_lastCheckpoint > _currentCheckpoint.Value)
        {
            X();
        } else
        {
            _safeZone = false;
        }
        if (!_safeZone && _camera.transform.position.x != _checkpointsPosition[_cameraStep])
        {
            _cameraPosition.x = _checkpointsPosition[_cameraStep];
            _camera.transform.position = Vector3.MoveTowards(_camera.transform.position, _cameraPosition, 0.01f);
        }
    }

    private bool CameraCanMove()
    {
        
        return _player1Transform.position.x >= -_levelLimit && _player1Transform.position.x <= _checkpointsPosition[_cameraStep];
    }

    private void X()
    {

        
        if (CameraCanMove() && _safeZone)
        {
            float x = Mathf.Abs(_positionX - _camera.transform.position.x);
            _positionX = Mathf.Clamp(_positionX, -_levelLimit, _checkpointsPosition[_cameraStep]);
            _cameraPosition.x = _positionX;

            if (x > 0.05f)
            {
                _camera.transform.position = Vector3.MoveTowards(_camera.transform.position, _cameraPosition, 0.05f);
            }
            else
            {
                _camera.transform.position = _cameraPosition;
            }

        }
    }
}