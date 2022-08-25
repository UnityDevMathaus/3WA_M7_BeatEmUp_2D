using UnityEngine;

public class Players : MonoBehaviour
{
    [SerializeField] private PlayersIdentities _playerIdentity;
    [SerializeField] private CurrentPlayer _currentPlayer;
    [SerializeField] private PlayersInputsList _inputsMode;
    private PlayersStats _playerStats;
    private IntVariable _playerHP;
    private IntVariable _playerMP;
    private IntVariable _playerLife;

    void Start()
    {
        GetComponentInChildren<PlayersInputs>().ActiveInputs(_inputsMode);
        GetComponentInChildren<PlayersRenderers>().SetPlayerSprite(_playerIdentity);
        _playerStats = GetComponentInChildren<PlayersStats>();
        _playerStats.CurrentPlayer = _currentPlayer;
        _playerStats.ResetCurrentPlayerStats();
        _playerHP = _playerStats.CurrentPlayerStats[0];
        _playerMP = _playerStats.CurrentPlayerStats[1];
        _playerLife = _playerStats.CurrentPlayerStats[2];
    }
    void Update()
    {
        DebugLife();
    }
    private void DebugLife()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1)) _playerHP.Value--;
        if (Input.GetKeyDown(KeyCode.Alpha2)) _playerMP.Value--;
        if (Input.GetKeyDown(KeyCode.Alpha3)) _playerLife.Value--;
    }
}