using UnityEngine;

public class PlayersMovementsOnY : MonoBehaviour
{
    [SerializeField] private Players _player;
    [SerializeField] private float _playerVerticalSpeed = 2f;

    void Update()
    {
        DebugAxeY();
    }

    private void DebugAxeY()
    {
        Vector3 vector = new Vector3(0, Input.GetAxisRaw("Vertical") * Time.deltaTime * _playerVerticalSpeed);
        _player.transform.Translate(vector);
    }
}