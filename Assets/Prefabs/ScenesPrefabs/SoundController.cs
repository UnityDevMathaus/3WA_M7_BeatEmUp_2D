using UnityEngine;
using UnityEngine.UI;

public class SoundController : MonoBehaviour
{
    [SerializeField] Sprite _soundOff;
    [SerializeField] Sprite _soundOn;
    [SerializeField] private BoolVariable _soundIsOn;
    //################################################################################################################################
    #region UNITY API
    Image _soundControlUI;
    void Awake()
    {
        _soundControlUI = GetComponent<Image>();
    }
    void Start()
    {
        SetSoundValue();
    }
    #endregion
    //################################################################################################################################  
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
    //################################################################################################################################  
}