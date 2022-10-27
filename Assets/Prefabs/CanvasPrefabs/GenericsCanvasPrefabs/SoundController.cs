using UnityEngine;
using UnityEngine.UI;
public class SoundController : MonoBehaviour
{
    //##### SERIALIZE FIELD REFERENCES ###############################################################################################
    [SerializeField] private Sprite _soundOff;
    [SerializeField] private Sprite _soundOn;
    [SerializeField] private BoolVariable _soundIsOn;
    //##### OBJECTS ##################################################################################################################
    private Image _soundControlUI;
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
    #endregion
    //################################################################################################################################
    #region FONCTIONS D'INITIALISATION
    private void InitializeAwakeReferences()
    {
        _soundControlUI = GetComponent<Image>();
    }
    private void InitializeStartReferences()
    {
        SetSoundValue();
    }
    #endregion
    //################################################################################################################################
    #region MECANIQUE DE LA CLASSE
    private void SetSoundValue()
    {
        if (_soundIsOn.Value)
        {
            AudioListener.volume = 1;
            _soundControlUI.sprite = _soundOn;
        }
        else
        {
            AudioListener.volume = 0;
            _soundControlUI.sprite = _soundOff;
        }
    }
    public void ToggleSound()
    {
        _soundIsOn.Value = !_soundIsOn.Value;
        SetSoundValue();
    }
    #endregion
    //################################################################################################################################
}