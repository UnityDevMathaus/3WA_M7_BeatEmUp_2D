using UnityEngine;
public class DialogsBoxReader : MonoBehaviour
{
    //##### SERIALIZE FIELD PARAMETERS ###############################################################################################
    [SerializeField] private int _maxRepeat = 1;
    [SerializeField] private float _delayForNextText = 3f;
    //##### SERIALIZE FIELD REFERENCES ###############################################################################################
    [SerializeField] private DialogsBox _dialogsDialogsBox;
    //##### TIMERS ###################################################################################################################
    private float _timeForNextPrintChar;
    private float _timeForNextSound;
    private float _delayForPrintChar = 0.2f;
    //##### OBJECTS ##################################################################################################################
    private AudioSource _audioSource;
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
    #endregion
    //################################################################################################################################
    #region FONCTIONS D'INITIALISATION
    private void InitializeAwakeReferences()
    {
        _audioSource = GetComponent<AudioSource>();
    }
    private void InitializeStartReferences()
    {
        _timeForNextSound = Time.time + _delayForPrintChar;
        _timeForNextPrintChar = Time.time + _delayForPrintChar;
        _currentDialogText = _textList[0];
        _currentCharIndex = 0;
        _currentTextIndex = 0;
        _repeatLast = 0;
        _writeNextText = true;
        _writeCurrentDialogText = "";
    }
    #endregion
    //################################################################################################################################
    #region MECANIQUE DE LA CLASSE
    //##### PRIMITIVES ###############################################################################################################
    private bool _writeNextText;
    private bool _readingIsOver;
        public bool ReadingIsOver { get => _readingIsOver; }
    private int _currentTextIndex;
    private int _currentCharIndex;
    private int _repeatLast;
    private string _currentDialogText;
    private string _writeCurrentDialogText;
    //##### PRIMITIVES ARRAYS ########################################################################################################
    private string[] _textList = { };
    //################################################################################################################################
    public void WriteNextText()
    {
        if (!_audioSource.isPlaying && _writeNextText)
        {
            if (Time.time > _timeForNextPrintChar)
            {
                if (_currentCharIndex >= _currentDialogText.Length)
                {
                    _timeForNextPrintChar = Time.time + _delayForNextText;
                    SelectTextToWrite();
                }
                else
                {
                    PlaySound();
                    WriteNextChar();
                }
            }
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
            ContinueToWrite();
        }
    }
    private bool IsLastText()
    {
        return _currentTextIndex == _textList.Length - 1;
    }
    private void ContinueToWrite()
    {
        _writeNextText = (_repeatLast == _maxRepeat) ? false : true;
        if (_writeNextText)
        {
            _repeatLast++;
            _writeCurrentDialogText = "";
            _currentCharIndex = 0;
            if (_repeatLast == _maxRepeat)
            {
                _currentDialogText = " ";// L'espace est important !
                _readingIsOver = true;
            }
        }
    }
    private void PlaySound()
    {
        if (_currentCharIndex != 0 && _currentCharIndex % 5 == 0) _audioSource.Play();
    }
    private void WriteNextChar()
    {
        _writeCurrentDialogText += _currentDialogText[_currentCharIndex];
        _dialogsDialogsBox.ChangeText(_writeCurrentDialogText);
        _currentCharIndex++;
    }
    public void SetTextList(string[] dialogs)
    {
        _textList = dialogs;
    }
    #endregion
    //################################################################################################################################
}