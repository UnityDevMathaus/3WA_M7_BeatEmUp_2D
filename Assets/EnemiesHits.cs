using UnityEngine;

public class EnemiesHits : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            PlayersCollisions _playerCollisions = collider.GetComponent<PlayersCollisions>();
            _playerCollisions.GetInjured();
        }
    }
}
