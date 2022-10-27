using UnityEngine;
public class ShadowCanvas : MonoBehaviour
{
    //##### SERIALIZE FIELD REFERENCES ###############################################################################################   
    [SerializeField] private AnimationsEffects _jumpEffect;
    [SerializeField] private AnimationsEffects _landEffect;
    //##### OBJECTS ##################################################################################################################
    private Animator _animator;
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
        _animator = GetComponent<Animator>();
    }
    #endregion
    //################################################################################################################################
    #region MECANIQUE DE LA CLASSE
    public void StartJump()
    {
        _animator.SetTrigger("OnJump");
        _jumpEffect.gameObject.SetActive(true);
    }
    public void StartLand()
    {
        _landEffect.gameObject.SetActive(true);
    }
    #endregion
    //################################################################################################################################
}