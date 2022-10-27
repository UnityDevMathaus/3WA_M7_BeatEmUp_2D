using UnityEngine;
using UnityEngine.UI;
public class CharactersSelectors : MonoBehaviour
{
    //##### SERIALIZE FIELD PARAMETERS ###############################################################################################
    [SerializeField] private string _characterName = "name";
    [SerializeField] private int _currentCharacterSelector;
    //##### SERIALIZE FIELD REFERENCES ###############################################################################################
    [SerializeField] private DialogsBox _charactareName;
    [SerializeField] private Transform _characterSprite;
    [SerializeField] private ShadowCanvas _characterShadow;
    [SerializeField] private ArrowSelectors _arrowSelectorP1;
    [SerializeField] private ArrowSelectors _arrowSelectorP2;
    [SerializeField] private IntVariable _currentP1SelectedCharacter;
    [SerializeField] private IntVariable _currentP2SelectedCharacter;
    //##### TIMERS ###################################################################################################################
    private float _timeForJumpingStart;
    private float _delayForAnimationCycle = 0.125f;
    //##### REGIONS ##################################################################################################################
    #region UNITY API
    void Awake()
    {
        InitializeAwakeReferences();
    }
    void Start()
    {
        InitializeStartReferences();
    }
    void Update()
    {
        IsFocused();
        SetEnable();
        if (_isJumping)
            ResolveJumpRenderer();
    }
    #endregion
    //################################################################################################################################
    #region FONCTIONS D'INITIALISATION
    private void InitializeAwakeReferences()
    {
        _arrowSelectorP1.ActiveValue = _currentCharacterSelector;
        _arrowSelectorP2.ActiveValue = _currentCharacterSelector;
    }
    private void InitializeStartReferences()
    {
        _charactareName.ChangeText(_characterName.ToUpper());
        _charactareName.gameObject.SetActive(false);
    }
    #endregion
    //################################################################################################################################
    #region MECANIQUE DE LA CLASSE
    //##### PRIMITIVES ###############################################################################################################
    private bool _isFocusedByP1;
    private bool _isFocusedByP2;
    private bool _isSelected;
        public bool IsSelected { get => _isSelected; set => _isSelected = value; }
    private bool _isJumping;
    private float _jumpSpeedUp = 120f;
    private float _jumpSpeedDown = 150f;
    //################################################################################################################################
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
    public void DisableArrows()
    {
        _arrowSelectorP1.gameObject.SetActive(false);
        _arrowSelectorP2.gameObject.SetActive(false);
        _charactareName.ChangeText("");

    }
    public void Jump()
    {
        _isJumping = true;
        StartJumpingTime();
        _characterShadow.StartJump();
        Vector2 newVector = Vector2.zero;
        newVector.y = -9.5f;
        switch (_characterName.ToUpper())
        {
            case "CHRIS":
                newVector.y = 0;
                newVector.x = 30;
                break;
            case "BEN":
                newVector.y = 25;
                break;
            default:
                break;
        }
        transform.Translate(newVector);
        _characterShadow.GetComponent<Image>().enabled = true;
        newVector.x = 0;
        newVector.y = 9.5f;
        _characterSprite.Translate(newVector);
    }
    public void StartJumpingTime()
    {
        _timeForJumpingStart = Time.time;
    }
    private void ResolveJumpRenderer()
    {
        int _jumpStep = (int)((Time.time - _timeForJumpingStart) / _delayForAnimationCycle);
        if (_isJumping)
        {
            Vector2 newVector = Vector2.zero;
            switch (_jumpStep)
            {
                case 0:
                case 1:
                    newVector.y = Time.deltaTime * _jumpSpeedUp;
                    break;
                case 2:
                case 3:
                    newVector.y = Time.deltaTime * -_jumpSpeedDown;
                    break;
                default:
                    _isJumping = false;
                    _characterShadow.StartLand();
                    break;
            }
            _characterSprite.Translate(newVector);
        }
    }
    #endregion
    //################################################################################################################################
}