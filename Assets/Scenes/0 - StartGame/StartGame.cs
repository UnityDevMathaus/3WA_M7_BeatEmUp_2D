using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
public class StartGame : MonoBehaviour
{
    //##### SERIALIZE FIELD REFERENCES ###############################################################################################
    [SerializeField] private TextMeshProUGUI _progressValueTMP;
    [SerializeField] private Transform _progressBarTransform;
    [SerializeField] private BoolVariable _soundIsOn;
    //##### TIMERS ###################################################################################################################
    private float _timeBeforeClickAvailable;
    private float _delayForClickAvailable = 2.1f;
    private float _realDelayForClickAvailable;
    //##### OBJECTS ##################################################################################################################
    private Vector3 _progressBarPosition;
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
        StartGameMecanism();
        CloseStartGameSceneMecanism();
    }
    #endregion
    //################################################################################################################################
    #region FONCTIONS D'INITIALISATION
    private void InitializeAwakeReferences()
    {
        _soundIsOn.Value = true;
    }
    private void InitializeStartReferences()
    {
        StartWaitingTime();
        _progressBarPosition = _progressBarTransform.position;
    }
    private void StartWaitingTime()
    {
        _timeBeforeClickAvailable = Time.time + _delayForClickAvailable;
        _realDelayForClickAvailable = _timeBeforeClickAvailable - Time.time;
        // discutable, mais permet d'éviter le calcul pendant l'update
        // et d'éviter un bug dans le cas où Time.time ne commence pas à 0 en faussant son écart avec _delayForClickAvailable.
    }
    #endregion
    //################################################################################################################################
    #region MECANIQUE DE LA CLASSE
    //##### PRIMITIVES ###############################################################################################################
    private bool _canClick;
    private int _progressValue;
    //################################################################################################################################
    private void StartGameMecanism()
    {
        if (!_canClick)
        {
            ProgressText();
            ProgressBar();
        }
    }
    private void ProgressText()
    {
        if (_progressValue < 100)
        {
            _progressValue = (int)(Time.time * 100 / _realDelayForClickAvailable);
            _progressValue = (_progressValue > 100) ? 100 : _progressValue;
            _progressValueTMP.text = _progressValue.ToString() + '%';
        }
        else if (_progressValueTMP.text == "100%")
        {
            _progressValueTMP.text = "CLICK THE SCREEN TO BEGIN";
            _canClick = true;
        }
    }
    private void ProgressBar()
    {
        if (_progressValue < 100)
        {
            _progressBarTransform.position = new Vector3(_progressBarPosition.x + 2.12f * _progressValue, _progressBarPosition.y, 0);
        }
    }
    #endregion
    //################################################################################################################################
    #region MECANIQUE DE CHANGEMENT DE SCENE
    private void CloseStartGameSceneMecanism()
    {
        if (Input.GetMouseButtonDown(0) && _canClick) SceneManager.LoadScene(1);
    }
    #endregion
    //################################################################################################################################
}