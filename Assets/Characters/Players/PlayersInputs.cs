using UnityEngine;

public class PlayersInputs : MonoBehaviour
{
    [SerializeField] private Inputs[] _inputs;
    private Inputs _playerInputs; public Inputs PlayerInputs { get => _playerInputs; }
    public void ActiveInputs(PlayersInputsList _inputMode)
    {
        _playerInputs = _inputs[(int)_inputMode];
        _playerInputs.gameObject.SetActive(true);
    }
}