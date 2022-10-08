using TMPro;
using UnityEngine;

public class DialogsBox : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] _allTextMesh;
    private string _currentText;
    private string _newText;
    private float _alphaValue; public float AlphaValue { get => _alphaValue; }
    private float _alphaIncrement = 0.2f;
    private float _timeForNextAlphaValue;
    private float _delayForNextAlphaValueAtStart = 2f;
    private float _delayForNextAlphaValue = 0.1f;
    private void Start()
    {
        _currentText = "";
        _newText = "";
        _alphaValue = 0f;
        _timeForNextAlphaValue = Time.time + _delayForNextAlphaValueAtStart;
    }
    void Update()
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

    public void ChangeText(string newText)
    {
        _newText = newText;
    }

    public void ChangeAlpha()
    {
        if (Time.time > _timeForNextAlphaValue)
        {
            _timeForNextAlphaValue = Time.time + _delayForNextAlphaValue;
            foreach (TextMeshProUGUI textMesh in _allTextMesh)
            {
                textMesh.alpha = _alphaValue;
            }
            _alphaValue += _alphaIncrement;
            _alphaValue = Mathf.Clamp(_alphaValue, 0f, 1f);
        }

    }
}
