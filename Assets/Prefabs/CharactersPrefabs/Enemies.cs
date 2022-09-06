using UnityEngine;

public class Enemies : MonoBehaviour
{
    [SerializeField] private EnemiesIdentities _enemyIdentity;
    [SerializeField] private IntVariable _enemiesCount;
    private CharactersRenderers _renderer;

    void Awake()
    {
        _renderer = GetComponentInChildren<CharactersRenderers>();
    }

    void Start()
    {
        _renderer.SetEnemiesSprites(_enemyIdentity);
    }



    void OnDestroy()
    {
        _enemiesCount.Value--;
    }
}