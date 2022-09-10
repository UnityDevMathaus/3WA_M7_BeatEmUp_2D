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
    //################################################################################################################################
    public bool PlayerIsDead()
    {
        return (_currentPlayerStats[0].Value <= 0);
    }
    /// <summary>
    /// Retire 1 HP au Player.
    /// Retire 5 HP à la place si "isCritical" vaut "true".
    /// </summary>
    /// <param name="isCritical">(default) false : 1 HP ou true : 5 HP</param>
    public void HitPlayer(bool isCritical = false)
    {
        _currentPlayerStats[0].Value -= (isCritical) ? 5 : 1;
    }
    #region MECANIQUE DE RESET
    /// <summary>
    /// Reset la valeur du nombre de vie des Players.
    /// </summary>
    private void ResetCurrentPlayerLife()
    {
        _currentPlayerStats[2].Value = _playersMaxLife.Value;
    }
    /// <summary>
    /// Reset la valeur des HP des Players.
    /// </summary>
    private void ResetCurrentPlayerHP()
    {
        _currentPlayerStats[0].Value = _playersMaxHP.Value;
    }
    /// <summary>
    /// Reset la valeur des PV des Players.
    /// </summary>
    public void ResetCurrentPlayerMP()
    {
        _currentPlayerStats[1].Value = 0;
    }
    /// <summary>
    /// Reset les statistiques des Players après un GAMEOVER.
    /// </summary>
    public void ResetCurrentPlayerStats()
    {
        ResetCurrentPlayerLevelStats();
        ResetCurrentPlayerLife();
    }
    /// <summary>
    /// Reset les statistiques des Players après avoir perdu une vie ou en changeant de Levels.
    /// </summary>
    public void ResetCurrentPlayerLevelStats()
    {
        ResetCurrentPlayerHP();
        ResetCurrentPlayerMP();
    }
    #endregion
    //################################################################################################################################
}