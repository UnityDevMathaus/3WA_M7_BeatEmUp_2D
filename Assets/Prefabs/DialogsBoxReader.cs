using UnityEngine;

public class DialogsBoxReader : MonoBehaviour
{
    [SerializeField] private DialogsBox _dialogsDialogsBox;
    [SerializeField] private int _maxRepeat = 1;
    [SerializeField] private float _delayForNextText = 3f;
    private string[] _textList = { };
    //################################################################################################################################
    #region UNITY API
    private AudioSource _audioSource;
    void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }
    void Start()
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
    //################################################################################################################################
    #region Mecanique des dialogues
    private int _currentTextIndex;
    private int _currentCharIndex;
    private string _currentDialogText;
    private string _writeCurrentDialogText;
    private bool _writeNextText;
    private int _repeatLast;
    private float _timeForNextPrintChar;
    private float _timeForNextSound;
    private float _delayForPrintChar = 0.2f;
    private bool _readingIsOver; public bool ReadingIsOver { get => _readingIsOver; }
    /// <summary>
    /// 
    /// </summary>
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
    /// <summary>
    /// 
    /// </summary>
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
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    private bool IsLastText()
    {
        return _currentTextIndex == _textList.Length - 1;
    }
    /// <summary>
    /// 
    /// </summary>
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
    /// <summary>
    /// 
    /// </summary>
    private void PlaySound()
    {
        if (_currentCharIndex != 0 && _currentCharIndex % 5 == 0) _audioSource.Play();
    }
    /// <summary>
    /// 
    /// </summary>
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