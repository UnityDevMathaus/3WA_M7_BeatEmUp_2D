using TMPro;
using UnityEngine;
public class DialogsBox : MonoBehaviour
{
    //##### SERIALIZE FIELD PARAMETERS ###############################################################################################
    [SerializeField] private float _alphaIncrement = 0.2f;
    [SerializeField] private float _delayForShowText = 0f;
    [SerializeField] private float _delayForNextAlphaValueAtStart = 2f;
    //##### SERIALIZE FIELD ARRAYS ###################################################################################################
    [SerializeField] private TextMeshProUGUI[] _allTextMesh;
    //##### TIMERS ###################################################################################################################  
    private float _timeForShowText;
    private float _timeForNextAlphaValue;
    private float _delayForNextAlphaValue = 0.1f;
    //##### REGIONS ##################################################################################################################
    #region UNITY API
    void Awake()
    {
        InitializeAwakeReferences();
    }
    void Start()
    {
        InitializeStartReferences();
        _timeForShowText = Time.time + _delayForShowText;
    }
    void Update()
    {
        DialogsBoxMecanism();
    }
    #endregion
    //################################################################################################################################
    #region FONCTIONS D'INITIALISATION
    private void InitializeAwakeReferences()
    {
        _currentText = "";
        _newText = _allTextMesh[0].text;
    }
    private void InitializeStartReferences()
    {
        foreach (TextMeshProUGUI textMesh in _allTextMesh)
        {
            textMesh.text = _newText;
            textMesh.enabled = false;
        }
        _isEnable = false;
        _alphaValue = 0f;
    }
    #endregion
    //################################################################################################################################
    #region MECANIQUE DE LA CLASSE
    //##### PRIMITIVES ###############################################################################################################
    private string _currentText;
    private string _newText;
    private float _alphaValue;
        public float AlphaValue { get => _alphaValue; }
    private bool _isEnable;
        public bool IsEnable { get => _isEnable; }
    //################################################################################################################################
    private void DialogsBoxMecanism()
    {
        if (_isEnable)
        {
            UpdateText();
        }
        else if (Time.time > _timeForShowText)
        {
            ActiveTMP();
        }
    }
    private void UpdateText()
    {
        if (!_currentText.Equals(_newText))
        {
            foreach (TextMeshProUGUI textMesh in _allTextMesh)
            {
                textMesh.text = _newText;
            }
            _currentText = _newText;
        }
    }
    private void ActiveTMP()
    {
        foreach (TextMeshProUGUI textMesh in _allTextMesh)
        {
            textMesh.enabled = true;
        }
        _isEnable = true;
        _timeForNextAlphaValue = Time.time + _delayForNextAlphaValueAtStart;
    }
    public void ChangeText(string newText)
    {
        _newText = newText;
    }
    public void ChangeAlpha()
    {
        if (_isEnable && Time.time > _timeForNextAlphaValue)
        {
            _alphaValue += _alphaIncrement;
            _alphaValue = Mathf.Clamp(_alphaValue, 0f, 1f);
            foreach (TextMeshProUGUI textMesh in _allTextMesh)
            {
                textMesh.alpha = _alphaValue;
            }
            _timeForNextAlphaValue = Time.time + _delayForNextAlphaValue;
        }
    }
    #endregion
    //################################################################################################################################
}