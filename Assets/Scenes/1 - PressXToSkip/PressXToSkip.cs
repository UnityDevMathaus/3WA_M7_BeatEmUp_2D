using UnityEngine;
using UnityEngine.SceneManagement;

public class PressXToSkip : MonoBehaviour
{
    //################################################################################################################################
    [SerializeField] private SceneLoader _sceneLoader;
    [SerializeField] private IntVariable _currentScene;
    [SerializeField] private DialogsBox _skipDialogsBox;
    [SerializeField] private DialogsBox _dialogsDialogsBox;
    [SerializeField] private AudioClip[] _audioClips;
    [SerializeField] Transform _cartridgeTransform;
    [SerializeField] private GameObject[] _effects;
    //################################################################################################################################
    private string[] _textList = { Dialogs.PRELOAD_TEXT0, Dialogs.PRELOAD_TEXT1,
                                   Dialogs.PRELOAD_TEXT2, Dialogs.PRELOAD_TEXT3,
                                   Dialogs.PRELOAD_TEXT4, Dialogs.PRELOAD_TEXT5,
                                   Dialogs.PRELOAD_TEXT6, Dialogs.PRELOAD_TEXT7};
    //################################################################################################################################
    #region UNITY API
    private AudioSource _audioSource;
    void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _currentScene.Value = SceneManager.GetActiveScene().buildIndex;
    }
    void Start()
    {
        _timeForNextSound = Time.time + _delayForNextSound;
        _timeForNextPrintChar = Time.time + _delayForPrintChar;
        _currentDialogText = _textList[0];
        _currentCharIndex = 0;
        _currentTextIndex = 0;
        _repeat = 0;
        _writeNextText = true;
        _writeCurrentDialogText = "";
        _audioClipIndex = 0;
    }
    void Update()
    {
        GoToNextMenu();
    }
    #endregion
    //################################################################################################################################
    #region Mecanique des dialogues
    private string _currentDialogText;
    private string _writeCurrentDialogText;
    private int _currentCharIndex;
    private int _currentTextIndex;
    private float _timeForNextPrintChar;
    private float _delayForPrintChar = 0.2f;
    private bool _writeNextText;
    private int _repeat;
    private void WriteNextText()
    {
        if (_currentCharIndex >= _currentDialogText.Length)
        {
            _timeForNextPrintChar = Time.time + 3f;
            SelectTextToWrite();
        }
        else
        {
            PlaySound();
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
            ContinueToWrite();
        }
    }
    private bool IsLastText()
    {
        return _currentTextIndex == _textList.Length - 1;
    }
    private void ContinueToWrite()
    {
        _writeNextText = (_repeat == 4) ? false : true;
        if (_writeNextText)
        {
            _repeat++;
            _writeCurrentDialogText = "";
            _currentCharIndex = 0;         
            if (_repeat == 4) _currentDialogText = " ";// L'espace est important !
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
    #endregion
    //################################################################################################################################
    #region Mecanique du menu
    private bool _goToNextMenu;
    private bool _cartridgeIsLanding;
    private float _timeForNextSound;
    private float _delayForNextSound = 0.5f;
    private int _audioClipIndex;
    private void GoToNextMenu()
    {
        if (_goToNextMenu)
        {
            ChangeMenu();
        }
        else
        {
            MenuMecanics();
        }
    }
    private void ChangeMenu()
    {
        MoveCartridge();
        CloseMenuMecanics();
    }
    private void MenuMecanics()
    {
        if (_skipDialogsBox.AlphaValue == 1)
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                _goToNextMenu = true;
                DisableText();
            }
        }
        else
        {
            _skipDialogsBox.ChangeAlpha();
        }
        if (!_audioSource.isPlaying && _writeNextText)
        {
            if (Time.time > _timeForNextPrintChar)
            {
                WriteNextText();
            }
        }
    }
    private void MoveCartridge()
    {
        Vector3 last = _cartridgeTransform.position;
        _cartridgeTransform.position = Vector2.MoveTowards(_cartridgeTransform.position, new Vector2(_cartridgeTransform.position.x, 51.1f), 2f);
        if (last == _cartridgeTransform.position && !_cartridgeIsLanding)
        {
            _cartridgeIsLanding = true;
            _effects[0].SetActive(true);
            _effects[1].SetActive(true);
            _effects[2].SetActive(true);
            _effects[3].SetActive(true);
            _effects[4].SetActive(true);
            _effects[5].SetActive(true);
            _effects[6].SetActive(true);
            _effects[7].SetActive(true);
            _effects[8].SetActive(true);
            _effects[9].SetActive(true);
        }
    }
    private void CloseMenuMecanics()
    {
        if (!_audioSource.isPlaying && Time.time > _timeForNextSound)
        {
            _timeForNextSound = Time.time + _delayForNextSound;
            if (_audioClipIndex < _audioClips.Length)
            {
                _audioSource.clip = _audioClips[_audioClipIndex];
                _audioSource.Play();
                _audioClipIndex++;
            }
            else
            {
                SceneManager.LoadScene(2);
            }
        }
    }
    private void DisableText()
    {
        _skipDialogsBox.gameObject.SetActive(false);
        _dialogsDialogsBox.gameObject.SetActive(false);
    }
    #endregion
    //################################################################################################################################
}