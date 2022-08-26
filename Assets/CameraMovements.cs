using UnityEngine;

public class CameraMovements : MonoBehaviour
{

    [SerializeField] private Transform _player1Transform;
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
        _cameraPosition.x = _player1Transform.position.x;
        _camera.transform.position = _cameraPosition;
    }
}