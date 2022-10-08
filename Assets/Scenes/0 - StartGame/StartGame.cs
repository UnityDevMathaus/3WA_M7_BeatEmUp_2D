using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    //################################################################################################################################
    [SerializeField] private SceneLoader _sceneLoader;
    [SerializeField] private IntVariable _currentScene;
    [SerializeField] private TextMeshProUGUI _progressValue;
    [SerializeField] private Transform _progressBarTransform;
    //################################################################################################################################
    private float _timeBeforeClickAvailable;
    private float _delayForClickAvailable = 2.1f;
    private float _realDelayForClickAvailable;
    private bool _canClick;
    private int _progress;
    private Vector3 _progressBarPosition;
    //################################################################################################################################
    #region UNITY API
    void Awake()
    {
        _currentScene.Value = SceneManager.GetActiveScene().buildIndex;
    }
    void Start()
    {
        StartWaitingTime();
        _progressBarPosition = _progressBarTransform.position;
    }
    void Update()
    {
        ClickToStart();
    }
    #endregion
    //################################################################################################################################
    private void ClickToStart()
    {
        if (_canClick)
        {
            if (Input.GetMouseButtonDown(0)) SceneManager.LoadScene(1);
        }
        else
        {
            ProgressText();
            ProgressBar();
        }
    }
    private void StartWaitingTime()
    {
        _timeBeforeClickAvailable = Time.time + _delayForClickAvailable;
        _realDelayForClickAvailable = _timeBeforeClickAvailable - Time.time;
        // discutable, mais permet d'éviter le calcul pendant l'update
        // et d'éviter un bug dans le cas où Time.time ne commence pas à 0 en faussant son écart avec _delayForClickAvailable.
    }
    private void ProgressText()
    {
        if (_progress < 100)
        {
            _progress = (int)(Time.time * 100 / _realDelayForClickAvailable);
            _progress = (_progress > 100) ? 100 : _progress;
            _progressValue.text = _progress.ToString() + '%';
        } else if (_progressValue.text == "100%")
        {
            _progressValue.text = "CLICK THE SCREEN TO BEGIN";
            _canClick = true;
        }
    }
    private void ProgressBar()
    {
        if (_progress < 100)
        {
            _progressBarTransform.position = new Vector3(_progressBarPosition.x + 2.12f * _progress, _progressBarPosition.y, 0);
        }
    }
    //################################################################################################################################
}