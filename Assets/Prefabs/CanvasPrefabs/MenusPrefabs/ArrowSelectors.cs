using UnityEngine;
using UnityEngine.UI;
public class ArrowSelectors : MonoBehaviour
{
    //##### SERIALIZE FIELD REFERENCES ###############################################################################################
    [SerializeField] private IntVariable _currentCharacterSelected;
    //##### OBJECTS ##################################################################################################################
    private Image _arrowImage;
    //##### REGIONS ##################################################################################################################
    #region UNITY API
    void Awake()
    {
        InitializeAwakeReferences();
    }
    #endregion
    //################################################################################################################################
    #region FONCTIONS D'INITIALISATION
    private void InitializeAwakeReferences()
    {
        _arrowImage = GetComponent<Image>();
        _arrowImage.enabled = false;
    }
    #endregion
    //################################################################################################################################
    #region MECANIQUE DE LA CLASSE
    //##### PRIMITIVES ###############################################################################################################
    private int _activeValue;
        public int ActiveValue { get => _activeValue; set => _activeValue = value; }
    //################################################################################################################################
    public void ActiveArrowSelectors(bool isSelected)
    {
        _arrowImage.enabled = isSelected;
    }
    #endregion
    //################################################################################################################################
}