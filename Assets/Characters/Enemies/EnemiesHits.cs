using UnityEngine;

public class EnemiesHits : MonoBehaviour
{
    [SerializeField] private Enemies _enemy;
    private CircleCollider2D _enemyHitsCollider;
    //################################################################################################################################
    #region UNITY API
    void Awake()
    {
        _enemyHitsCollider = GetComponent<CircleCollider2D>();
    }
    void Update()
    {
        EnableHits();
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            PlayersCollisions _playerCollisions = collider.GetComponent<PlayersCollisions>();
            _playerCollisions.GetInjured();
        }
    }
    #endregion
    //################################################################################################################################
    /// <summary>
    /// Active ou désactive le collider du GameObject HitsCollider des Enemies.
    /// </summary>
    private void EnableHits()
    {
        if (_enemy.IsFighting && !_enemy.IsHolding)
        {
            _enemyHitsCollider.enabled = true;
        }
        else
        {
            _enemyHitsCollider.enabled = false;
        }
    }
    //################################################################################################################################
}