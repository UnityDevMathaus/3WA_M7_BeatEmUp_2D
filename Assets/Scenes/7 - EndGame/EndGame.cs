using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class EndGame : MonoBehaviour
{
    //##### SERIALIZE FIELD REFERENCES ###############################################################################################
    [SerializeField] private DialogsBox _titleTextTheEndDialogsBox;
    [SerializeField] private DialogsBox _titleQuestionMark;
    [SerializeField] private DialogsBox _pressXToEndDialogsBox;
    [SerializeField] private Image _transitionImage;
    //##### SERIALIZE FIELD ARRAYS ###################################################################################################
    [SerializeField] private DialogsBox[] _scoresDialogsBox;
    [SerializeField] private IntVariable[] _allScoresAndTime;
    //##### REGIONS ##################################################################################################################
    #region UNITY API
    void Start()
    {
        InitialiseTransitionImageRenderer();
        InitializeAllScoreTextValue();
    }
    void Update()
    {
        EndGameMecanism();
        CloseEndGameSceneMecanism();
    }
    #endregion
    //################################################################################################################################
    #region FONCTIONS D'INITIALISATION
    private void InitialiseTransitionImageRenderer()
    {
        _transitionImage.enabled = true;
        _transitionImage.canvasRenderer.SetAlpha(0f);
    }
    private void InitializeAllScoreTextValue()
    {
        int index = 2;
        int scoreTotal = 0;
        int timeTotal = 0;
        foreach (IntVariable scoreThenTime in _allScoresAndTime)
        {
            if (index % 2 == 0)
            {
                scoreTotal += scoreThenTime.Value;
                _scoresDialogsBox[index].ChangeText(scoreThenTime.Value.ToString());
            }
            else
            {
                timeTotal += scoreThenTime.Value;
                _scoresDialogsBox[index].ChangeText(TimeToString(scoreThenTime.Value));
            }
            index++;
        }
        _scoresDialogsBox[index].ChangeText(scoreTotal.ToString());
        _scoresDialogsBox[++index].ChangeText(TimeToString(timeTotal));
    }
    private string TimeToString(int value)
    {
        int minutes, secondes;
        string tens;
        minutes = value / 60;
        secondes = value % 60;
        tens = (secondes < 10) ? "0" : "";
        return $"{minutes.ToString()}:{tens}{secondes.ToString()}";
    }
    #endregion
    //################################################################################################################################
    #region MECANIQUE DE LA CLASSE
    private void EndGameMecanism()
    {
        if (_titleQuestionMark.AlphaValue == 1)
        {
            ShowAllScoreDialogsBox();
        }
        else
        {
            TitleTextChangeAlpha();
        }
    }
    private void TitleTextChangeAlpha()
    {
        if (_titleTextTheEndDialogsBox.AlphaValue == 1)
        {
            _titleQuestionMark.ChangeAlpha();
            _transitionImage.CrossFadeAlpha(0.4f, 2f, false);
        }
        else
        {
            _titleTextTheEndDialogsBox.ChangeAlpha();
        }
    }
    private void ShowAllScoreDialogsBox()
    {
        foreach (DialogsBox item in _scoresDialogsBox)
        {
            item.ChangeAlpha();
        }
    }
    #endregion
    //################################################################################################################################
    #region MECANIQUE DE CHANGEMENT DE SCENE
    private void CloseEndGameSceneMecanism()
    {
        if (_pressXToEndDialogsBox.AlphaValue == 1 && Input.GetKeyDown(KeyCode.X)) SceneManager.LoadScene(8);
    }
    #endregion
    //################################################################################################################################
}