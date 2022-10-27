using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Credits : MonoBehaviour
{
    //##### SERIALIZE FIELD REFERENCES ###############################################################################################
    [SerializeField] private Image _transitionImage;
    //##### SERIALIZE FIELD ARRAYS ###################################################################################################
    [SerializeField] private DialogsBox[] _creditsTextsDialogsBox;
    //##### TIMERS ###################################################################################################################
    private float _timeForCloseScene;
    private float _delayForCloseScene = 10f;
    //##### REGIONS ##################################################################################################################
    #region UNITY API
    void Start()
    {
        InitialiseTransitionImageRenderer();
        _timeForCloseScene = Time.time + _delayForCloseScene;
    }
    void Update()
    {
        CreditsMecanism();
        CloseCreditsSceneMecanism();
    }
    #endregion
    //################################################################################################################################
    #region FONCTIONS D'INITIALISATION
    private void InitialiseTransitionImageRenderer()
    {
        _transitionImage.enabled = true;
        _transitionImage.canvasRenderer.SetAlpha(0f);
    }
    #endregion
    //################################################################################################################################
    #region MECANIQUE DE LA CLASSE
    private void CreditsMecanism()
    {
        if (Time.time > _timeForCloseScene)
        {
            _transitionImage.CrossFadeAlpha(1.0f, 0.25f, false);
        }
        else
        {
            foreach (DialogsBox item in _creditsTextsDialogsBox)
            {
                item.ChangeAlpha();
            }
        }
    }
    #endregion
    //################################################################################################################################
    #region MECANIQUE DE CHANGEMENT DE SCENE
    private void CloseCreditsSceneMecanism()
    {
        if (_transitionImage.canvasRenderer.GetAlpha() > 0.99f) SceneManager.LoadScene(9);
    }
    #endregion
    //################################################################################################################################
}