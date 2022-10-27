using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class KillScreen : MonoBehaviour
{
    //##### SERIALIZE FIELD PARAMETERS ###############################################################################################
    [SerializeField] private float _backgroundTranslationSpeed = 320f;
    //##### SERIALIZE FIELD REFERENCES ###############################################################################################
    [SerializeField] private Image _transitionImage;
    [SerializeField] private Image _logoImage;
    //##### SERIALIZE FIELD ARRAYS ###################################################################################################
    [SerializeField] private RectTransform[] _backgroundsRectTransform;
    //##### TIMERS ###################################################################################################################
    private float _timeForCloseScene;
    private float _timeForNextLogoMovement;
    private float _delayForCloseScene = 10f;
    private float _delayForNextLogoMovement = 0.2f; 
    //##### OBJECTS ##################################################################################################################
    private RectTransform _currentFirstBackgroundRectTransform;
    private RectTransform _logoRectTransform;
    private Vector2 _logoInitialPosition;
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
        InitializeLogoImageRenderer();
        _timeForCloseScene = Time.time + _delayForCloseScene;
    }
    void Update()
    {
        KillScreenMecanism();
        CloseKillScreenSceneMecanism();
    }
    #endregion
    //################################################################################################################################
    #region FONCTIONS D'INITIALISATION
    private void InitializeAwakeReferences()
    {
        _logoRectTransform = _logoImage.rectTransform;
    }
    private void InitializeStartReferences()
    {
        _currentFirstBackgroundRectTransform = _backgroundsRectTransform[0];
        _logoInitialPosition = _logoRectTransform.position;
    }
    private void InitializeTransitionImageRenderer()
    {
        _transitionImage.enabled = true;
        _transitionImage.canvasRenderer.SetAlpha(0.9f);
        _transitionImage.CrossFadeAlpha(0f, 1f, false);
    }
    private void InitializeLogoImageRenderer()
    {
        _logoImage.canvasRenderer.SetAlpha(0.7f);
        _logoImage.CrossFadeAlpha(1f, 1.5f, false);
    }
    #endregion
    #region MECANIQUE DE LA CLASSE
    //##### PRIMITIVES ###############################################################################################################
    private bool _logoImageIsMoving;
    private bool _switchTheFirstBackground;
    private bool _switchBeetweenVerticalAndHorizontal;
    //################################################################################################################################
    private void KillScreenMecanism()
    {
        BackgroundsTranslation();
        TheFirstBackgroundResetPosition();
        UpdateLogoMovement();
    }
    private void BackgroundsTranslation()
    {
        Vector2 translationVector = Vector2.left * _backgroundTranslationSpeed * Time.deltaTime;
        foreach (RectTransform rectTransform in _backgroundsRectTransform)
        {
            rectTransform.Translate(translationVector);
        }
    }
    private void TheFirstBackgroundResetPosition()
    {
        if (_currentFirstBackgroundRectTransform.localPosition.x <= -320)
        {
            _switchTheFirstBackground = !_switchTheFirstBackground;
            Vector2 newPosition = _currentFirstBackgroundRectTransform.localPosition;
            newPosition.x = -_currentFirstBackgroundRectTransform.localPosition.x;
            _currentFirstBackgroundRectTransform.localPosition = newPosition;
            _currentFirstBackgroundRectTransform = _switchTheFirstBackground ? _backgroundsRectTransform[1] : _backgroundsRectTransform[0];
        }
    }
    private void UpdateLogoMovement()
    {
        if (Time.time > _timeForNextLogoMovement)
        {
            _timeForNextLogoMovement = Time.time + _delayForNextLogoMovement;
            if (!_logoImageIsMoving)
            {
                RandomizeLogoMovement();
            }
            else
            {
                _logoRectTransform.position = _logoInitialPosition;
            }
            _logoImageIsMoving = !_logoImageIsMoving;
        }
    }
    private void RandomizeLogoMovement()
    {
        int randomNumber = Random.Range(0, 10);
        switch (randomNumber)
        {
            case 2:
            case 4:
            case 6:
            case 8:
                _logoRectTransform.position = _logoInitialPosition + RandomizeAndSwitchDirection() * 2;
                break;
            default:
                _logoRectTransform.position = _logoInitialPosition;
                break;
        }
    }
    private Vector2 RandomizeAndSwitchDirection()
    {
        _switchBeetweenVerticalAndHorizontal = !_switchBeetweenVerticalAndHorizontal;
        int randomNumber = Random.Range(0, 10);
        if (randomNumber % 2 == 0)
        {
            return (_switchBeetweenVerticalAndHorizontal) ? Vector2.down : Vector2.left;
        }
        else
        {
            return (_switchBeetweenVerticalAndHorizontal) ? Vector2.up : Vector2.right;
        }
    }
    #endregion
    #region MECANIQUE DE CHANGEMENT DE SCENE
    private void CloseKillScreenSceneMecanism()
    {
        if (Time.time > _timeForCloseScene)
        {
            _transitionImage.CrossFadeAlpha(1.0f, 0.5f, false);
        }
        if (_transitionImage.canvasRenderer.GetAlpha() > 0.99f)
        {
            SceneManager.LoadScene(1);
        }
    }
    #endregion
    //################################################################################################################################
}