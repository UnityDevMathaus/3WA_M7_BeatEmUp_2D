using UnityEngine;

public class PlayersHits : MonoBehaviour
{
    [SerializeField] private Players _player;
    private CircleCollider2D _playerHitsCollider;
    //################################################################################################################################
    #region UNITY API
    void Awake()
    {
        _playerHitsCollider = GetComponent<CircleCollider2D>();
    }
    void Update()
    {
        EnableHits();
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Enemies"))
        {
            BaseCollisions _enemyCollisions = collider.GetComponent<BaseCollisions>();
            _enemyCollisions.GetInjured();
        }
    }
    #endregion
    //################################################################################################################################
    /// <summary>
    /// Active ou désactive le collider du GameObject HitsCollider des Players.
    /// </summary>
    private void EnableHits()
    {
        if (_player.IsFighting && !_player.IsHolding && !_player.IsThrowing)
        {
            _playerHitsCollider.enabled = true;
        }
        else
        {
            _playerHitsCollider.enabled = false;
        }
    }
    //################################################################################################################################
}