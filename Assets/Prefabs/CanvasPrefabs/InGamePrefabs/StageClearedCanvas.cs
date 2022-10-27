using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class StageClearedCanvas : MonoBehaviour
{
    //##### SERIALIZE FIELD REFERENCES ###############################################################################################
    [SerializeField] private Transform _stageScoreTransform;
    [SerializeField] private Transform _completeTimeTransform;
    [SerializeField] private IntVariable _stageScore;
    [SerializeField] private IntVariable _completeTime;
    [SerializeField] private Image _backgroundRenderer;
    [SerializeField] private TextMeshProUGUI _pressAnyKeyText;
    //##### TIMERS ###################################################################################################################
    private float _timeForTimer;
    private float _timerDelay = 1.5f;
    private float _blinkTimerDelay = 0.5f;
    //##### OBJECTS ##################################################################################################################
    private Vector2 _translationStageScore;
    private Vector2 _translationCompleteTime;
    private Color _backgroundAlphaColor;
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
        StageClearedCanvasMecanism();
    }
    #endregion
    //################################################################################################################################
    #region FONCTIONS D'INITIALISATION
    private void InitializeAwakeReferences()
    {
        _completeTime.Value = (int)Time.time; //Capture le temps de completion lorsque le canvas est activé.
    }
    private void InitializeStartReferences()
    {
        _translationStageScore = _stageScoreTransform.localPosition;
        _translationCompleteTime = _completeTimeTransform.localPosition;
        _translationStageScore.x = 0;
        _translationCompleteTime.x = 0;
        _backgroundAlphaColor = _backgroundRenderer.color;
        _backgroundAlphaColor.a = 0.97f;
        _timeForTimer = Time.time + _timerDelay;
    }
    #endregion
    //################################################################################################################################
    #region MECANIQUE DE LA CLASSE
    //##### PRIMITIVES ###############################################################################################################
    private bool _translationEnded;
    private bool _canPressKeyToEnd;
    private bool _pressAnyKey; public bool PressAnyKey { get => _pressAnyKey; }
    //################################################################################################################################
    private void StageClearedCanvasMecanism()
    {
        PressAnyKeyToContinue();
        ShowStageClearedAnimations();
    }
    /// <summary>
    /// Assigne la propriété PressAnyKey si le canvas est complètement chargé.
    /// </summary>
    private void PressAnyKeyToContinue()
    {
        if (_canPressKeyToEnd) _pressAnyKey = Input.anyKeyDown;
    }
    /// <summary>
    /// Applique les transitions et le background puis à interval régulier,
    /// définie un timer et désactive le texte "Press ... to continue".
    /// </summary>
    private void ShowStageClearedAnimations()
    {
        if (_pressAnyKeyText.gameObject.activeSelf && Time.time > _timeForTimer)
        {
            _timeForTimer = Time.time + _blinkTimerDelay;
            _pressAnyKeyText.gameObject.SetActive(false);
        }
        else
        {
            SetBackgroundAlphaColor();
            ApplyTranslations();
            Canvas.ForceUpdateCanvases();
        }
    }
    /// <summary>
    /// Applique la couleur alpha au canvas.
    /// </summary>
    private void SetBackgroundAlphaColor()
    {
        if (_backgroundAlphaColor.a - _backgroundRenderer.color.a > 0.001f)
        {
            _backgroundRenderer.color = Color.Lerp(_backgroundRenderer.color, _backgroundAlphaColor, 0.02f);
        }
    }
    /// <summary>
    /// Applique les transitions du Stage Score et du Complete Time
    /// et affiche les valeurs du Stage Score et du Complete Time.
    /// Dévérouille ensuite la possibilité d'appuyer sur une touche pour continuer puis à interval régulier,
    /// définie un timer et active le texte "Press ... to continue".
    /// </summary>
    private void ApplyTranslations()
    {
        if (!_translationEnded)
        {
            ApplyScoreTransformTranslation();
            ApplyCompleteTimeTranslation();
            _translationEnded = _stageScoreTransform.localPosition.x == 0 && _completeTimeTransform.localPosition.x == 0;
            if (_translationEnded)
            {
                ShowStageScore();
                ShowCompleteTime();
            }
            _timeForTimer = Time.time + _timerDelay;
        }
        else if (Time.time > _timeForTimer)
        {
            _timeForTimer = Time.time + _blinkTimerDelay;
            _pressAnyKeyText.gameObject.SetActive(true);
            if (!_canPressKeyToEnd) _canPressKeyToEnd = true;
        }
    }
    /// <summary>
    /// Applique la transition du Stage Score.
    /// </summary>
    private void ApplyScoreTransformTranslation()
    {
        _stageScoreTransform.localPosition = Vector2.MoveTowards(_stageScoreTransform.localPosition, _translationStageScore, 1f);
    }
    /// <summary>
    /// Apllique la transition du Complete Time.
    /// </summary>
    private void ApplyCompleteTimeTranslation()
    {
        _completeTimeTransform.localPosition = Vector2.MoveTowards(_completeTimeTransform.localPosition, _translationCompleteTime, 1f);
    }
    /// <summary>
    /// Récupère et affiche la valeur du Stage Score.
    /// </summary>
    private void ShowStageScore()
    {
        TextMeshProUGUI[] a = _stageScoreTransform.gameObject.GetComponentsInChildren<TextMeshProUGUI>(true);
        a[0].gameObject.SetActive(true);
        a[1].gameObject.SetActive(true);
        a[1].text = _stageScore.Value.ToString();
    }
    /// <summary>
    /// Récupère et affiche la valeur du Complete Time.
    /// </summary>
    private void ShowCompleteTime()
    {
        TextMeshProUGUI[] a = _completeTimeTransform.gameObject.GetComponentsInChildren<TextMeshProUGUI>(true);
        a[0].gameObject.SetActive(true);
        a[1].gameObject.SetActive(true);
        a[1].text = _completeTime.Value.ToString();
    }
    #endregion
    //################################################################################################################################
}