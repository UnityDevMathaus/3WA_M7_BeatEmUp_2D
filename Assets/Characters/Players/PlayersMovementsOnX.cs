using UnityEngine;

public class PlayersMovementsOnX : MonoBehaviour
{
    [SerializeField] private Players _player;
    [SerializeField] private float _playerHorizontalSpeed = 2f;

    void Update()
    {
        DebugAxeX();
    }

    private void DebugAxeX()
    {
        Vector3 vector = new Vector3(Input.GetAxisRaw("Horizontal") * Time.deltaTime * _playerHorizontalSpeed, 0);
        _player.transform.Translate(vector);
    }
}