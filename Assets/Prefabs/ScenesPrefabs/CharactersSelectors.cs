using TMPro;
using UnityEngine;

public class CharactersSelectors : MonoBehaviour
{
    [SerializeField] private string _characterName = "name";
    [SerializeField] private int _currentCharacterSelector;
    [SerializeField] private ArrowSelectors _arrowSelectorP1;
    [SerializeField] private ArrowSelectors _arrowSelectorP2;
    [SerializeField] private IntVariable _currentP1SelectedCharacter;
    [SerializeField] private IntVariable _currentP2SelectedCharacter;
    //################################################################################################################################
    #region UNITY API
    DialogsBox _charactareName;
    void Awake()
    {
        _charactareName = GetComponentInChildren<DialogsBox>();
        _arrowSelectorP1.ActiveValue = _currentCharacterSelector;
        _arrowSelectorP2.ActiveValue = _currentCharacterSelector;
    }
    private void Start()
    {
        _charactareName.ChangeText(_characterName.ToUpper());
        _charactareName.gameObject.SetActive(false);
    }
    void Update()
    {
        IsFocused();
        SetEnable();
    }
    #endregion
    //################################################################################################################################
    private bool _isFocusedByP1;
    private bool _isFocusedByP2;
    private bool _isSelected; public bool IsSelected { get => _isSelected; set => _isSelected = value; }
    private void IsFocused()
    {
        _isFocusedByP1 = (_currentCharacterSelector == _currentP1SelectedCharacter.Value);
        _isFocusedByP2 = (_currentCharacterSelector == _currentP2SelectedCharacter.Value);
    }
    private void SetEnable()
    {
        _charactareName.gameObject.SetActive(_isFocusedByP1 || _isFocusedByP2);    
        _arrowSelectorP1.ActiveArrowSelectors(_isFocusedByP1);
        _arrowSelectorP2.ActiveArrowSelectors(_isFocusedByP2);
    }
    //################################################################################################################################
}