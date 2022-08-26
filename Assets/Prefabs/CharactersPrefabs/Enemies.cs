using UnityEngine;

public class Enemies : MonoBehaviour
{
    [SerializeField] private EnemiesIdentities _enemyIdentity;

    void Awake()
    {
        GetComponentInChildren<CharactersRenderers>().SetEnemiesSprites(_enemyIdentity);
    }
}