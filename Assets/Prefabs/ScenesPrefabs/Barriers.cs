using UnityEngine;

public class Barriers : MonoBehaviour
{
    [SerializeField] private WavesList _creatorWave;
    [SerializeField] private WavesList _destructorWave;
    [SerializeField] private IntVariable _currentWave;
    [SerializeField] private BoolVariable _currentWaveIsActive;

    private Collider2D _collider;
    private bool _isActive;

    void Awake()
    {
        _collider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_isActive)
        {
            if (_currentWave.Value > (int)_destructorWave)
            {
                _collider.enabled = false;
                Destroy(gameObject);
            }
        } else if (_currentWaveIsActive.Value && _currentWave.Value == (int)_creatorWave)
        {
            Debug.Log(_currentWaveIsActive.Value);
            Debug.Log(_currentWave.Value);
            _isActive = true;
            _collider.enabled = true;
        }
    }
}