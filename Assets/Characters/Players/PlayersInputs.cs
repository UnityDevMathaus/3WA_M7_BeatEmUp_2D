using UnityEngine;

public class PlayersInputs : MonoBehaviour
{
    [SerializeField] private GameObject[] _inputs;

    public void ActiveInputs(PlayersInputsList _inputMode)
    {
        _inputs[(int)_inputMode].SetActive(true);
    }
}