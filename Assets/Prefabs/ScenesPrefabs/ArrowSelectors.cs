using UnityEngine;
using UnityEngine.UI;

public class ArrowSelectors : MonoBehaviour
{
    [SerializeField] private IntVariable _currentCharacterSelected;   
    //################################################################################################################################
    #region UNITY API
    private Image _arrowImage;
    void Awake()
    {
        _arrowImage = GetComponent<Image>();
        _arrowImage.enabled = false;
    }
    #endregion
    //################################################################################################################################
    private int _activeValue; public int ActiveValue { get => _activeValue; set => _activeValue = value; }
    public void ActiveArrowSelectors(bool isSelected)
    {
        _arrowImage.enabled = isSelected;
    }
    //################################################################################################################################
}