using UnityEngine;

public class EnemiesDetectors : MonoBehaviour
{
    [SerializeField] EnemiesIA _enemyIA;
    //################################################################################################################################
    #region UNITY API
    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.CompareTag("Player"))
        {
            _enemyIA.HasReachATarget = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collider2D)
    {
        if (collider2D.CompareTag("Player"))
        {
            _enemyIA.HasReachATarget = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collider2D)
    {
        if (collider2D.CompareTag("Player"))
        {
            _enemyIA.HasReachATarget = false;
        }
    }
    #endregion
    //################################################################################################################################
}