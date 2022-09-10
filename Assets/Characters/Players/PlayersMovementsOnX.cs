using UnityEngine;

public class PlayersMovementsOnX : MonoBehaviour
{
    [SerializeField] private Players _player;
    [SerializeField] private float _playerHorizontalSpeed = 2f;
    private int _horizontalVelocity; public int HorizontalVelocity { get => _horizontalVelocity; set => _horizontalVelocity = value; }
    void Update()
    {
        DebugAxeX();
        _horizontalVelocity = Mathf.RoundToInt(Input.GetAxisRaw("Horizontal"));
    }

    private void DebugAxeX()
    {
        Vector3 vector = new Vector3(Input.GetAxisRaw("Horizontal") * Time.deltaTime * _playerHorizontalSpeed, 0);
        _player.transform.Translate(vector);
    }

}