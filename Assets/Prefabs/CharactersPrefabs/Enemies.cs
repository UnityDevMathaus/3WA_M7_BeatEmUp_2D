using UnityEngine;

public class Enemies : MonoBehaviour
{
    [SerializeField] private EnemiesIdentities _enemyIdentity;
    private CharactersRenderers _renderer;

    void Awake()
    {
        _renderer = GetComponentInChildren<CharactersRenderers>();
    }

    void Start()
    {
        _renderer.SetEnemiesSprites(_enemyIdentity);
    }
}