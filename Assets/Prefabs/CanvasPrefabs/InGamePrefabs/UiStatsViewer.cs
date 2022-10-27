using TMPro;
using UnityEngine;
public class UiStatsViewer : MonoBehaviour
{
    //##### SERIALIZE FIELD REFERENCES ###############################################################################################
    [SerializeField] private IntVariable _maxStat;
    [SerializeField] private IntVariable _currentStat;
    //##### OBJECTS ##################################################################################################################
    private TextMeshProUGUI _text;
    //##### REGIONS ##################################################################################################################
    #region UNITY API
    void Awake()
    {
        InitializeAwakeReferences();
    }
    void Update()
    {
        _text.text = _currentStat.Value.ToString();
    }
    #endregion
    //################################################################################################################################
    #region FONCTIONS D'INITIALISATION
    private void InitializeAwakeReferences()
    {
        _text = GetComponentInChildren<TextMeshProUGUI>();
        _currentStat.Value = _maxStat.Value;
    }
    #endregion
    //################################################################################################################################
}