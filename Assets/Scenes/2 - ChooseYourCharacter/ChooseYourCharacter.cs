using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChooseYourCharacter : MonoBehaviour
{
    [SerializeField] GameObject _1P;
    [SerializeField] GameObject _2P;
    [SerializeField] Sprite _showInputs1P;
    [SerializeField] Sprite _showInputs2P;
    [SerializeField] Image _showInputsImage;
    [SerializeField] private IntVariable _currentCharacterSelectedByP1;
    [SerializeField] private IntVariable _currentCharacterSelectedByP2;
    //################################################################################################################################
    #region UNITY API
    void Awake()
    {
    }
    void Start()
    {
        _currentCharacterSelectedByP1.Value = 0;
        _currentCharacterSelectedByP2.Value = 0;
        _timeForNextPrintChar = Time.time + _delayForPrintChar;
        _currentDialogText = _textList[0];
        _currentCharIndex = 0;
        _currentTextIndex = 0;
        _writeNextText = true;
        _writeCurrentDialogText = "";
    }
    void Update()
    {
        if (_allCharacterIsSelected)
        {
            _dialogsDialogsBox.gameObject.SetActive(true);
            WriteNextText();
        } else
        {
            CharacterSelectMecanics();
        }  
    }
    #endregion
    //################################################################################################################################
    private int _P1choice = 0;
    private int _P2choice = 0;
    private int _select1PCharacter; public int Select1PCharacter { get => _select1PCharacter; set => _select1PCharacter = Mathf.Clamp(value, 0, 4); }
    private int _select2PCharacter; public int Select2PCharacter { get => _select2PCharacter; set => _select2PCharacter = Mathf.Clamp(value, 0, 4); }
    private bool _allCharacterIsSelected;


    private void CharacterSelectMecanics()
    {
        CharacterSelectModeMecanics();
        if (_isCharacterSelectMode)
        {
            if (_P1choice == 0) GetInputsInSelection(_currentCharacterSelectedByP1, KeyCode.RightArrow, KeyCode.LeftArrow);
            ExitCharacterSelectMode();
            A();
        }
        else
        {
            EnterCharacterSelectMode();
            ChangeModeSelection();
        }
    }
    //################################################################################################################################
    #region MyRegion
    private bool _isCharacterSelectMode;
    private int _selectMode = 0;
    private bool _fireX;
    private bool _fireC;
    private bool _fireV;
    private bool _fireComma;
    private bool _firePeriod;
    private void EnterCharacterSelectMode()
    {
        if (!_isCharacterSelectMode && _fireX)
        {
            _isCharacterSelectMode = true;
            _currentCharacterSelectedByP1.Value = 2;
            if (_selectMode == 1) _currentCharacterSelectedByP2.Value = 1;
        }
    }
    private void ChangeModeSelection()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            SetMode2P();
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            SetMode1P();
        }      
    }
    private void SetMode1P()
    {
        _currentCharacterSelectedByP1.Value = 0;
        _currentCharacterSelectedByP2.Value = 0;
        _selectMode = 0;
        _isCharacterSelectMode = false;
        _1P.SetActive(true);
        _2P.SetActive(false);
        ResetInputs();
    }
    private void SetMode2P()
    {
        _selectMode = 1;
        _1P.SetActive(false);
        _2P.SetActive(true);
    }
    private void ResetInputs()
    {
        _fireX = false;
        _fireC = false;
        _fireV = false;
        _fireComma = false;
        _firePeriod = false;
        _P1choice = 0;
        _P2choice = 0;
    }
    #endregion
    //################################################################################################################################
    #region Inputs
    private void CharacterSelectModeMecanics()
    {
        if (_selectMode == 1 && _isCharacterSelectMode)
        {
            GetInputs2P();
            _showInputsImage.sprite = _showInputs2P;
            if (_P2choice == 0) GetInputsInSelection(_currentCharacterSelectedByP2, KeyCode.D, KeyCode.A);
        }
        else
        {
            GetInputs1P();
            _showInputsImage.sprite = _showInputs1P;
        }
    }
    private void GetInputs1P()
    {
        _fireX = Input.GetKeyDown(KeyCode.X);
        _fireC = Input.GetKeyDown(KeyCode.C);

    }
    private void GetInputs2P()
    {
        _fireC = Input.GetKeyDown(KeyCode.C);
        _fireV = Input.GetKeyDown(KeyCode.V);
        _fireComma = Input.GetKeyDown(KeyCode.Comma);
        _firePeriod = Input.GetKeyDown(KeyCode.Period);

    }
    private void GetInputsInSelection(IntVariable value, KeyCode right, KeyCode left)
    {
        if (Input.GetKeyDown(right))
        {
            value.Value = Mathf.Clamp(value.Value + 1, 1, 5);
        }
        else if (Input.GetKeyDown(left))
        {
            value.Value = Mathf.Clamp(value.Value - 1, 1, 5);
        }
    }
    private void ExitCharacterSelectMode()
    {
        if (_isCharacterSelectMode)
        {
            bool exit = false;
            if (_selectMode == 0)
            {
                exit = _fireC && _P1choice == 0;
            }
            else
            {
                if (_fireV && _P1choice == 0 || _firePeriod && _P2choice == 0)
                {
                    exit = true;
                }
            }
            if (exit) SetMode1P();
        }
    }
    private void A()
    {
        if (_selectMode == 0)
        {
            if (_P1choice != 0 && _fireC) _P1choice = 0;
            else if (_P1choice == 0 && _fireX)
                _P1choice = (_P2choice != _currentCharacterSelectedByP1.Value) ? _currentCharacterSelectedByP1.Value : 0;
            _allCharacterIsSelected = _P1choice != 0;
        } else
        {
            if (_P1choice != 0 && _fireV) _P1choice = 0;
            else if (_P1choice == 0 && _fireC) _P1choice = _currentCharacterSelectedByP1.Value;
            if (_P2choice != 0 && _firePeriod) _P2choice = 0;
            else if (_P2choice == 0 && _fireComma)
                _P2choice = (_P1choice != _currentCharacterSelectedByP2.Value) ? _currentCharacterSelectedByP2.Value : 0;
            _allCharacterIsSelected = (_P1choice != 0 && _P2choice != 0);
        }
    }
    private void WaitManager()
    {
        if (!_allCharacterIsSelected) _waitManagerTalkTime = Time.time;
        else
        {
            if (Time.time > _waitManagerTalkTime + _delayManagerTalk)
            {
                Debug.Log("Tak");
            }
        }
    }
    #endregion
    //################################################################################################################################
    private float _delayManagerTalk = 2f;
    private float _waitManagerTalkTime;
    private int _counterManagerText = 3;
    private string[] _textList = { Dialogs.SELECT_TEXT0, Dialogs.SELECT_TEXT1, Dialogs.SELECT_TEXT2};
    private string _currentDialogText;
    private string _writeCurrentDialogText;
    private int _currentCharIndex;
    private int _currentTextIndex;
    private float _timeForNextPrintChar;
    private float _delayForPrintChar = 0.2f;
    private bool _writeNextText;
    private void WriteNextText()
    {
        if (_currentCharIndex >= _currentDialogText.Length)
        {
            _timeForNextPrintChar = Time.time + 3f;
            SelectTextToWrite();
        }
        else
        {
            //PlaySound();
            WriteNextChar();
        }
    }
    private void SelectTextToWrite()
    {
        if (!IsLastText())
        {
            _currentTextIndex++;
            _currentDialogText = _textList[_currentTextIndex];
            _writeCurrentDialogText = "";
            _currentCharIndex = 0;
        }
        else
        {
            SceneManager.LoadScene(3);
        }
    }
    private bool IsLastText()
    {
        return _currentTextIndex == _textList.Length - 1;
    }
    //private void PlaySound()
    //{
    //    if (_currentCharIndex != 0 && _currentCharIndex % 5 == 0) _audioSource.Play();
    //}
    [SerializeField] private DialogsBox _dialogsDialogsBox;
    private void WriteNextChar()
    {
        _writeCurrentDialogText += _currentDialogText[_currentCharIndex];
        _dialogsDialogsBox.ChangeText(_writeCurrentDialogText);
        _currentCharIndex++;
    }
    //################################################################################################################################
}