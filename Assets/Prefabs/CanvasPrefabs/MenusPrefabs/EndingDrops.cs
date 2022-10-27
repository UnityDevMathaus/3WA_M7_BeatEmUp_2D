using UnityEngine;
using UnityEngine.UI;
public class EndingDrops : MonoBehaviour
{
    //##### SERIALIZE FIELD PARAMETERS ###############################################################################################
    [SerializeField] private float _delayForStart = 0f;
    [SerializeField] private float _fixYPosition = 12f;
    //##### SERIALIZE FIELD REFERENCES ###############################################################################################
    [SerializeField] private Image _drop;
    [SerializeField] private Image _shadow;
    //##### TIMERS ###################################################################################################################
    private float _timeForStart;
    private float _timeForDisapear;
    private float _timeForStartBlink;
    private float _timeForBlink;
    private float _delayForDisapear = 10f;
    private float _delayForStartBlink = 8.5f;
    private float _delayForBlink = 0.18f;
    //##### OBJECTS ##################################################################################################################
    private Vector2 _startposition;
    //##### REGIONS ##################################################################################################################
    #region UNITY API
    void Start()
    {
        InitializeStartReferences();
    }
    void Update()
    {
        EndingDropsMecanics();
    }
    #endregion
    //################################################################################################################################
    #region FONCTIONS D'INITIALISATION
    private void InitializeStartReferences()
    {
        _timeForDisapear = Time.time + _delayForDisapear + _delayForStart;
        _startposition = _drop.rectTransform.position;
        _timeForStartBlink = Time.time + _delayForStartBlink + _delayForStart;
        _timeForStart = Time.time + _delayForStart;
    }
    #endregion
    //################################################################################################################################
    #region MECANIQUE DE LA CLASSE
    private void EndingDropsMecanics()
    {
        if (Time.time > _timeForStart)
        {
            if (Time.time < _timeForDisapear)
            {
                DropDrops();
            }
            else
            {
                ResetDrops();
            }
        }
    }
    private void DropDrops()
    {
        if (_drop.rectTransform.position.y > _shadow.rectTransform.position.y + _fixYPosition)
        {
            Vector3 a = Vector2.zero;
            a.y = -350f * Time.deltaTime;
            _drop.rectTransform.Translate(a);
        }
        else
        {
            if (!_shadow.enabled) _shadow.enabled = true;
            ClearDrops();
        }
    }
    private void ClearDrops()
    {
        if (Time.time > _timeForStartBlink)
        {
            if (Time.time > _timeForBlink)
            {
                _drop.enabled = !_drop.enabled;
                _shadow.enabled = !_shadow.enabled;
                _timeForBlink = Time.time + _delayForBlink;
            }
        }
    }
    private void ResetDrops()
    {
        _timeForStart = Time.time + _delayForStart;
        _timeForDisapear = Time.time + _delayForDisapear + _delayForStart;
        _timeForStartBlink = Time.time + _delayForStartBlink + _delayForStart;
        _drop.rectTransform.position = _startposition;
        _shadow.enabled = false;
    }
    #endregion
    //################################################################################################################################
}