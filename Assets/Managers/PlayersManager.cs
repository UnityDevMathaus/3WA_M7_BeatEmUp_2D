using UnityEngine;

public class PlayersManager : MonoBehaviour
{
    [SerializeField] CurrentPlayer _currentPlayerMode;
    [SerializeField] private Players _p1; public Players P1 { get => _p1; }
    [SerializeField] private Players _p2; public Players P2 { get => _p2; }

    private void Start()
    {
        if (!_currentPlayerMode.Equals(CurrentPlayer.P2))
        {
            //KillPlayer(_p2);
        }
    }

    public void KillPlayer(Players player)
    {
        player.PlayerHP.Value = 0;
        player.PlayerMP.Value = 0;
        player.PlayerLife.Value = 0;
        Destroy(player.gameObject);
    }

    public bool AllPlayersAreDead()
    {
        return (_p1.PlayerLife.Value == 0 && _p1.PlayerLife.Value == 0);
    }
}