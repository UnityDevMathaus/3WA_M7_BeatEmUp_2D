using UnityEngine;

public class Inputs : MonoBehaviour
{
    [SerializeField] private KeyCode _jumpKey;
    [SerializeField] private KeyCode _attackpKey;
    [SerializeField] private KeyCode[] _movementsKeys;
    private KeyCode _lastDirectionKey; public KeyCode LastDirectionKey { get => _lastDirectionKey; }
    private bool _fireJump; public bool FireJump { get => _fireJump; }
    private bool _fireAttack; public bool FireAttack { get => _fireAttack; }
    private bool _fireMove; public bool FireMove { get => _fireMove; }
    private bool _whileMove; public bool WhileMove { get => _whileMove; }

    private bool _canJump; public bool CanJump { get => _canJump; set => _canJump = value; }
    private bool _canAttack; public bool CanAttack { get => _canAttack; set => _canAttack = value; }
    private bool _canMove; public bool CanMove { get => _canMove; set => _canMove = value; }

    private void PressJump()
    {
        _fireJump = (_canJump) ? Input.GetKeyDown(_jumpKey) : false;
    }
    private void PressAttack()
    {
        _fireAttack = (_canAttack) ? Input.GetKeyDown(_attackpKey) : false;
    }
    private void PressMovementsKeys()
    {
        if (_canMove)
        {
            _fireMove = CheckMovementsKeys();
            _whileMove = CheckMovementsKeys(true);
        } else
        {
            _fireMove = false;
            _whileMove = false;
        }
    }
    private bool CheckMovementsKeys(bool isDown = false)
    {
        foreach (KeyCode keyCode in _movementsKeys)
        {
            bool fireKey = (isDown) ? Input.GetKey(keyCode) : Input.GetKeyDown(keyCode);
            if (fireKey)
            {
                if (!isDown) _lastDirectionKey = keyCode;
                return true;
            }
        }
        return false;
    }

    private void ListenInputs()
    {
        PressJump();
        PressAttack();
        PressMovementsKeys();
    }

    void Update()
    {
        ListenInputs();
    }
}