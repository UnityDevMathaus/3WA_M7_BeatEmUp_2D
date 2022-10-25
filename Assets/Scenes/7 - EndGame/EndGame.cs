using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    [SerializeField] private SceneLoader _sceneLoader;
    [SerializeField] private IntVariable _currentScene;
    [SerializeField] private DialogsBox _skipDialogsBox;
    [SerializeField] private DialogsBox _skipDialogs;

    void Start()
    {
        _skipDialogsBox.ChangeText("THE END");
        _skipDialogs.ChangeText("?");
    }

    void Update()
    {
        if (_skipDialogs.AlphaValue == 1)
        {

        }
        else
        {
            if (_skipDialogsBox.AlphaValue == 1)
            {
                _skipDialogs.ChangeAlpha();
            }
            else
            {
                _skipDialogsBox.ChangeAlpha();
            }  
        }
    }
}