using UnityEngine;

public class PlayersStats : MonoBehaviour
{
    [SerializeField] private IntVariable _playersMaxHP;
    [SerializeField] private IntVariable _playersMaxMP;
    [SerializeField] private IntVariable _playersMaxLife;

    [SerializeField] private IntVariable[] _player1Stats;
    [SerializeField] private IntVariable[] _player2Stats;

    private IntVariable[] _currentPlayerStats; public IntVariable[] CurrentPlayerStats { get => _currentPlayerStats; }
    private CurrentPlayer _currentPlayer;

    public CurrentPlayer CurrentPlayer {
        get => _currentPlayer;
        set {
            _currentPlayer = value;
            if (_currentPlayer.Equals(CurrentPlayer.P2))
            {
                _currentPlayerStats = _player2Stats;
            }
            else
            {
                _currentPlayerStats = _player1Stats;
            }
        }
    }

    private void ResetCurrentPlayerHP()
    {
        _currentPlayerStats[0].Value = _playersMaxHP.Value;  
    }
    private void ResetCurrentPlayerLife()
    {
        _currentPlayerStats[2].Value = _playersMaxLife.Value;
    }

    public void ResetCurrentPlayerMP()
    {
        _currentPlayerStats[1].Value = _playersMaxMP.Value;
    }

    public void ResetCurrentPlayerLevelStats()
    {
        ResetCurrentPlayerHP();
        ResetCurrentPlayerMP();
    }
    public void ResetCurrentPlayerStats()
    {
        ResetCurrentPlayerLevelStats();
        ResetCurrentPlayerLife();
    }


}
