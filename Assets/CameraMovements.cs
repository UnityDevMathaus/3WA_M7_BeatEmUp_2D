using UnityEngine;

public class CameraMovements : MonoBehaviour
{
    [SerializeField] private Transform _player1Transform;
    [SerializeField] private bool _safeZone;
    [SerializeField] private float _levelLimit = 54.98f;
    [SerializeField] private float[] _checkpointsPosition
    = new float[] { -50f, -40f, -29f, -16f, 2f, 23f, 50f };
    //-50; -40; -29; -16; 2; 23; 50

    private Camera _camera;
    private Vector3 _cameraPosition;


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
        if (CameraCanMove() && _safeZone)
        {
            _cameraPosition.x = _player1Transform.position.x;
            _camera.transform.position = _cameraPosition;
        }
    }

    private bool CameraCanMove()
    {
        return transform.position.x >= -_levelLimit && transform.position.x <= _levelLimit;
    }
}