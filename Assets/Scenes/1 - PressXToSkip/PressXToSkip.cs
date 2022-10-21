using UnityEngine;
using UnityEngine.SceneManagement;

public class PressXToSkip : MonoBehaviour
{
    //################################################################################################################################
    [SerializeField] private SceneLoader _sceneLoader;
    [SerializeField] private IntVariable _currentScene;
    [SerializeField] private DialogsBox _skipDialogsBox;
    [SerializeField] private DialogsBox _dialogsDialogsBox;
    [SerializeField] DialogsBoxReader _dialogBoxReader;
    [SerializeField] Transform _cartridgeTransform;
    [SerializeField] private AudioClip[] _audioClips;
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
        _audioClipIndex = 0;
        _dialogBoxReader.SetTextList(_textList);
        _dialogBoxReader.gameObject.SetActive(true);
        _skipDialogsBox.ChangeText("PRESS X TO SKIP");
    }
    void Update()
    {
        GoToNextMenu();
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
        _dialogBoxReader.WriteNextText();
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