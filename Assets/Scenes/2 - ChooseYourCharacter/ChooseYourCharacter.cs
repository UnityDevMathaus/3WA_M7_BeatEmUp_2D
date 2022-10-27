using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class ChooseYourCharacter : MonoBehaviour
{
    //##### SERIALIZE FIELD PARAMETERS ###############################################################################################
    [SerializeField] private GameObject _1P;
    [SerializeField] private GameObject _2P;
    [SerializeField] private GameObject _addons;
    [SerializeField] private Sprite _showInputs1P;
    [SerializeField] private Sprite _showInputs2P;
    [SerializeField] private Image _transitionImage;
    [SerializeField] private Image _showInputsImage;
    [SerializeField] private Animator _managerAnimator;
    [SerializeField] private IntVariable _currentCharacterSelectedByP1;
    [SerializeField] private IntVariable _currentCharacterSelectedByP2;
    [SerializeField] private DialogsBoxReader _dialogBoxReader;
    [SerializeField] private SoundManager _soundManager;
    //##### SERIALIZE FIELD REFERENCES ###############################################################################################
    [SerializeField] private AudioClip[] _nextMusics;
    [SerializeField] private AudioClip[] _sfxSounds;
    [SerializeField] private CharactersSelectors[] _characters;
    //##### TIMERS ###################################################################################################################
    private float _startGameTimer;
    private float _startMusicTimer;
    private float _delayStartGame = 0.5f;
    private float _delayStartMusicTimer = 0.2f;
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
        InitializeTransitionImageRenderer();
    }
    void Update()
    {
        ChooseYourCharacterMecanism();
        CloseChooseYourCharacterSceneMecanism();
    }
    #endregion
    //################################################################################################################################
    #region FONCTIONS D'INITIALISATION
    //##### PRIMITIVES ARRAYS ########################################################################################################
    private string[] _textList = { Dialogs.SELECT_TEXT0, Dialogs.SELECT_TEXT1, Dialogs.SELECT_TEXT2 };
    //################################################################################################################################
    private void InitializeAwakeReferences()
    {
        _audioSource = GetComponent<AudioSource>();
    }
    private void InitializeStartReferences()
    {
        _addons.SetActive(true);
        _currentCharacterSelectedByP1.Value = 0;
        _currentCharacterSelectedByP2.Value = 0;
        _dialogBoxReader.SetTextList(_textList);
    }
    private void InitializeTransitionImageRenderer()
    {
        _transitionImage.enabled = true;
        _transitionImage.canvasRenderer.SetAlpha(0f);
    }
    #endregion
    //################################################################################################################################
    #region MECANIQUE D'INPUTS
    //##### PRIMITIVES ###############################################################################################################
    private bool _fireX;
    private bool _fireC;
    private bool _fireV;
    private bool _fireComma;
    private bool _firePeriod;
    private int _selectMode = 0;
    //################################################################################################################################
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
            _soundManager.PlayClip(_sfxSounds[0]);
        }
        else if (Input.GetKeyDown(left))
        {
            value.Value = Mathf.Clamp(value.Value - 1, 1, 5);
            _soundManager.PlayClip(_sfxSounds[0]);
        }
    }
    #endregion
    //################################################################################################################################
    #region MECANIQUE DE LA CLASSE
    //##### PRIMITIVES ###############################################################################################################
    private bool _allCharacterIsSelected;
    private bool _isInCharacterSelection;
    private int _P1choice = 0;
    private int _P2choice = 0;
    private int _select1PCharacter;
        public int Select1PCharacter { get => _select1PCharacter; set => _select1PCharacter = Mathf.Clamp(value, 0, 4); }
    private int _select2PCharacter;
        public int Select2PCharacter { get => _select2PCharacter; set => _select2PCharacter = Mathf.Clamp(value, 0, 4); }
    //################################################################################################################################
    private void ChooseYourCharacterMecanism()
    {
        if (_allCharacterIsSelected)
        {
            _showInputsImage.enabled = false;
            LaunchGamePrelude();
        }
        else
        {
            CharacterSelectMecanics();
            _startGameTimer = Time.time;
        }
    }
    private void CharacterSelectMecanics()
    {
        CharacterSelectModeMecanics();
        if (_isInCharacterSelection)
        {
            if (_P1choice == 0) GetInputsInSelection(_currentCharacterSelectedByP1, KeyCode.RightArrow, KeyCode.LeftArrow);
            ExitCharacterSelectMode();
            SelectACharacter();
        }
        else
        {
            EnterCharacterSelectMode();
            ChangePlayerMode();
        }
    }
    private void CharacterSelectModeMecanics()
    {
        if (_selectMode == 1 && _isInCharacterSelection)
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
    private void EnterCharacterSelectMode()
    {
        if (!_isInCharacterSelection && _fireX)
        {
            _soundManager.PlayClip(_sfxSounds[1]);
            _isInCharacterSelection = true;
            _currentCharacterSelectedByP1.Value = 2;
            if (_selectMode == 1) _currentCharacterSelectedByP2.Value = 1;
        }
    }
    private void ExitCharacterSelectMode()
    {
        if (_isInCharacterSelection)
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
    private void SelectACharacter()
    {
        if (_selectMode == 0)
        {
            SelectACharacterIn1PMode();
        }
        else
        {
            SelectACharacterIn2PMode();
        }
        DoWhenCharacterAreSelected();
    }
    private void SelectACharacterIn1PMode()
    {
        if (_P1choice != 0 && _fireC) _P1choice = 0;
        else if (_P1choice == 0 && _fireX)
        {
            _P1choice = _currentCharacterSelectedByP1.Value;
            _soundManager.PlayClip(_sfxSounds[1]);
        }
        _allCharacterIsSelected = _P1choice != 0;
    }
    private void SelectACharacterIn2PMode()
    {
        if (_P1choice != 0 && _fireV) _P1choice = 0;
        else if (_P1choice == 0 && _fireC)
        {
            SelectIfValidP1Character();
        }
        if (_P2choice != 0 && _firePeriod) _P2choice = 0;
        else if (_P2choice == 0 && _fireComma)
        {
            SelectIfValidP2Character();
        }
        _allCharacterIsSelected = (_P1choice != 0 && _P2choice != 0);
    }
    private void SelectIfValidP1Character()
    {
        if (_P2choice != _currentCharacterSelectedByP1.Value)
        {
            _P1choice = _currentCharacterSelectedByP1.Value;
            _soundManager.PlayClip(_sfxSounds[1]);
        }
        else
        {
            _P1choice = 0;
            _soundManager.PlayClip(_sfxSounds[2]);
        }
    }
    private void SelectIfValidP2Character()
    {
        if (_P1choice != _currentCharacterSelectedByP2.Value)
        {
            _P2choice = _currentCharacterSelectedByP2.Value;
            _soundManager.PlayClip(_sfxSounds[1]);
        }
        else
        {
            _P2choice = 0;
            _soundManager.PlayClip(_sfxSounds[2]);
        }
    }
    private void DoWhenCharacterAreSelected()
    {
        if (_allCharacterIsSelected)
        {
            foreach (CharactersSelectors item in _characters)
            {
                item.DisableArrows();
            }
        }
    }
    private void ChangePlayerMode()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            SetMode2P();
            _soundManager.PlayClip(_sfxSounds[0]);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            SetMode1P();
            _soundManager.PlayClip(_sfxSounds[0]);
        }
    }
    private void SetMode1P()
    {
        _currentCharacterSelectedByP1.Value = 0;
        _currentCharacterSelectedByP2.Value = 0;
        _selectMode = 0;
        _isInCharacterSelection = false;
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
    #region MECANIQUE DE CHANGEMENT DE SCENE
    //##### PRIMITIVES ###############################################################################################################
    private bool _loadTheGame;
    private bool _startPrelude;
    private bool _changeMusic;
    //################################################################################################################################
    private void CloseChooseYourCharacterSceneMecanism()
    {
        if (_loadTheGame) SceneManager.LoadScene(7);
    }
    private void LaunchGamePrelude()
    {
        if (Time.time > _startGameTimer + _delayStartGame)
        {
            if (_startPrelude && !_soundManager.IsPlaying)
            {
                CloseMenu();
            }
            else
            {
                OpenTheDoor();
            }
        }
    }
    private void CloseMenu()
    {
        _loadTheGame = true;
        
    }
    private void OpenTheDoor()
    {
        ChangeMusic();
        if (_dialogBoxReader.isActiveAndEnabled)
        {
            _dialogBoxReader.WriteNextText();
            if (_dialogBoxReader.ReadingIsOver && !_startPrelude)
            {
                _soundManager.PlayClip(_sfxSounds[3]);
                _transitionImage.CrossFadeAlpha(1.0f, _sfxSounds[3].length, false);
                _startPrelude = true;
            }
        }
        else
        {
            _dialogBoxReader.gameObject.SetActive(true);
        }
    }
    private void ChangeMusic()
    {
        if (_startGameTimer == 0)
        {
            _startGameTimer = Time.time + _delayStartMusicTimer;
        }
        else if (Time.time > _startGameTimer)
        {
            StopMusicLoopAndPlayDoorSfx();
        }
        PlayNewMusicAfterDoorSfx();
    }
    private void StopMusicLoopAndPlayDoorSfx()
    {
        if (_changeMusic != _allCharacterIsSelected)
        {
            _audioSource.clip = _nextMusics[0];
            _audioSource.Play();
            _audioSource.loop = false;
            _changeMusic = _allCharacterIsSelected;
            _startGameTimer = Time.time + _delayStartMusicTimer;
            _managerAnimator.SetTrigger("OnSelectCharacter");
        }
    }
    private void PlayNewMusicAfterDoorSfx()
    {
        if (!_audioSource.isPlaying)
        {
            PlayCharacterAnimation(_currentCharacterSelectedByP1);
            PlayCharacterAnimation(_currentCharacterSelectedByP2);
            _audioSource.clip = _nextMusics[1];
            _audioSource.Play();
        }
    }
    private void PlayCharacterAnimation(IntVariable selectedCharacter)
    {
        int value = selectedCharacter.Value;
        if (value != 0)
        {
            CharactersSelectors character = _characters[value - 1];
            Animator animator = character.GetComponentInChildren<Animator>();
            character.Jump();
            animator.SetFloat("CharacterChoice", selectedCharacter.Value);
            animator.SetTrigger("OnCharacterSelect");
        }
    }
    #endregion
    //################################################################################################################################
}