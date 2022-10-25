using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class KillScreen : MonoBehaviour
{
    [SerializeField] RectTransform[] _images;
    [SerializeField] RectTransform _logo;
    [SerializeField] float _moveSpeed = 320f;
    RectTransform _currentFirst;
    bool switc;
    private float _timeForNextSound;
    private float _delayForNextSound = 0.2f;
    private float _timeForClose;
    private float _delayForClose = 10f;
    bool _isMoving;
    bool _isVert;
    Vector2 initi;
    [SerializeField] private Image _canvas;
    void Start()
    {
        _currentFirst = _images[0];
        initi = _logo.position;
        _canvas.enabled = true;
        _canvas.canvasRenderer.SetAlpha(0f);
        _timeForClose = Time.time + _delayForClose;
    }
    void Update()
    {
        Vector2 trans = Vector2.left * _moveSpeed * Time.deltaTime;
        foreach (RectTransform image in _images)
        {
            image.Translate(trans);
        }
        if (_currentFirst.localPosition.x <= -320)
        {
            switc = !switc;
            Vector2 a = _currentFirst.localPosition;
            a.x = -_currentFirst.localPosition.x;
            _currentFirst.localPosition = a;
            _currentFirst = (switc) ? _images[1] : _images[0];
        }
        if (Time.time > _timeForNextSound)
        {
            _timeForNextSound = Time.time + _delayForNextSound;
            if (!_isMoving)
            {
                int rand = Random.Range(0, 10);
                switch (rand)
                {
                    case 2:
                        _logo.position = initi + IsVertical() * 2;
                        break;
                    case 4:
                        _logo.position = initi + IsVertical() * 2;
                        break;
                    case 6:
                        _logo.position = initi + IsVertical() * 2;
                        break;
                    case 8:
                        _logo.position = initi + IsVertical() * 2;
                        break;
                    default:
                        _logo.position = initi;
                        break;
                }

            } else
            {
                _logo.position = initi;
            }
            _isMoving = !_isMoving;
        }
        if (Time.time > _timeForClose)
        {
            _canvas.CrossFadeAlpha(1.0f, 0.5f, false);
        }
        if (_canvas.canvasRenderer.GetAlpha() > 0.99f)
        {
            SceneManager.LoadScene(1);
        }

    }
    private Vector2 IsVertical()
    {
        _isVert = !_isVert;
        int rand = Random.Range(0, 10);
        if (rand%2 == 0)
        {
            return (_isVert) ? Vector2.down : Vector2.left;
        } else
        {
            return (_isVert) ? Vector2.up : Vector2.right;
        }
        
    }
}
