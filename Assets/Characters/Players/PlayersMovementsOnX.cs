using UnityEngine;

public class PlayersMovementsOnX : MonoBehaviour
{
    [SerializeField] private Players _player;
    [SerializeField] private float _playerHorizontalSpeed = 2f;
    private int _horizontalVelocity;
    private CharactersRenderers _renderer;
    private CircleCollider2D _playerHitsCollider;
    private void Awake()
    {
        _renderer = _player.GetComponentInChildren<CharactersRenderers>();
    }
    void Update()
    {
        DebugAxeX();
        _horizontalVelocity = Mathf.RoundToInt(Input.GetAxisRaw("Horizontal"));
        FlipPlayersDirection();
    }

    private void DebugAxeX()
    {
        Vector3 vector = new Vector3(Input.GetAxisRaw("Horizontal") * Time.deltaTime * _playerHorizontalSpeed, 0);
        _player.transform.Translate(vector);
    }

    private void FlipPlayersDirection()
    {
        _player.ReverseRenderer(_horizontalVelocity);
    }
}