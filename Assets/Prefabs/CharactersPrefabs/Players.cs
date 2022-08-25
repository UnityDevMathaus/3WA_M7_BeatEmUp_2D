using UnityEngine;

public class Players : MonoBehaviour
{
    [SerializeField] private PlayersIdentities _playerIdentity;
    [SerializeField] private PlayersInputsList _inputsMode;



    void Start()
    {
        GetComponentInChildren<PlayersInputs>().ActiveInputs(_inputsMode);
        GetComponentInChildren<PlayersRenderers>().SetPlayerSprite(_playerIdentity);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
