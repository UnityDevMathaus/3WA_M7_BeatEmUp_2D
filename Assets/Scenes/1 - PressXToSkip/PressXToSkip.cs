using UnityEngine;
using UnityEngine.SceneManagement;
public class PressXToSkip : MonoBehaviour
{
    //##### SERIALIZE FIELD REFERENCES ###############################################################################################
    [SerializeField] private DialogsBox _skipDialogsBox;
    [SerializeField] private DialogsBox _dialogsDialogsBox;
    [SerializeField] private DialogsBoxReader _dialogBoxReader;
    [SerializeField] private Transform _cartridgeTransform;
    //##### SERIALIZE FIELD ARRAYS ###################################################################################################
    [SerializeField] private AudioClip[] _audioClips;
    [SerializeField] private GameObject[] _effects;
    //##### TIMERS ###################################################################################################################
    private float _timeForNextSound;
    private float _delayForNextSound = 0.5f;
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
    void Update()
    {
        PressXToSkipMecanism();
        ClosePressXToSkipMecanism();
    }
    #endregion
    //################################################################################################################################
    #region FONCTIONS D'INITIALISATION
    //##### PRIMITIVES ###############################################################################################################
    private int _audioClipIndex;
    //##### PRIMITIVES ARRAYS ########################################################################################################
    private string[] _dialogsList = { Dialogs.PRELOAD_TEXT0, Dialogs.PRELOAD_TEXT1,
                                   Dialogs.PRELOAD_TEXT2, Dialogs.PRELOAD_TEXT3,
                                   Dialogs.PRELOAD_TEXT4, Dialogs.PRELOAD_TEXT5,
                                   Dialogs.PRELOAD_TEXT6, Dialogs.PRELOAD_TEXT7};
    //################################################################################################################################
    private void InitializeAwakeReferences()
    {
        _audioSource = GetComponent<AudioSource>();
    }
    private void InitializeStartReferences()
    {
        _audioClipIndex = 0;
        _dialogBoxReader.SetTextList(_dialogsList);
        _dialogBoxReader.gameObject.SetActive(true);
        _skipDialogsBox.ChangeText("PRESS X TO SKIP");
    }
    #endregion
    //################################################################################################################################
    #region MECANIQUE DE LA CLASSE
    //##### PRIMITIVES ###############################################################################################################
    private bool _goToNextMenu;
    private bool _cartridgeIsLanding;
    //################################################################################################################################
    private void PressXToSkipMecanism()
    {
        if (_goToNextMenu)
        {
            MoveCartridge();
        }
        else
        {
            ReadAllDialogs();
        }
    }
    private void MoveCartridge()
    {
        Vector3 _cartridgePosition = _cartridgeTransform.position;
        _cartridgeTransform.position = Vector2.MoveTowards(_cartridgeTransform.position, new Vector2(_cartridgeTransform.position.x, 51.1f), 2f);
        if (_cartridgePosition == _cartridgeTransform.position && !_cartridgeIsLanding)
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
    private void ReadAllDialogs()
    {
        if (_skipDialogsBox.AlphaValue == 1)
        {
            PressXToSkipDialogs();
        }
        else
        {
            _skipDialogsBox.ChangeAlpha();
        }
        _dialogBoxReader.WriteNextText();
    }
    private void PressXToSkipDialogs()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            _goToNextMenu = true;
            DisableText();
        }
    }
    #endregion
    //################################################################################################################################
    #region MECANIQUE DE CHANGEMENT DE SCENE
    private void ClosePressXToSkipMecanism()
    {
        if (_goToNextMenu && !_audioSource.isPlaying && Time.time > _timeForNextSound)
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