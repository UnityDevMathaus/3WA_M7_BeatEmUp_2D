using TMPro;
using UnityEngine;

public class UiStatsViewer : MonoBehaviour
{
    [SerializeField] private IntVariable _maxStat;
    [SerializeField] private IntVariable _currentStat;
    private TextMeshProUGUI _text;

    void Awake()
    {
        _text = GetComponentInChildren<TextMeshProUGUI>();
        _currentStat.Value = _maxStat.Value;
    }
    void Update()
    {
        _text.text = _currentStat.Value.ToString();
    }
}