using UnityEngine;

public class EnemiesCollisions : MonoBehaviour
{
    [SerializeField] private GameObject _enemyGameObject;
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("PlayersHits"))
        {
            Destroy(_enemyGameObject);
        }
    }
}
