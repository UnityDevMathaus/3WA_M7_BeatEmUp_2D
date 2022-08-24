using UnityEngine;
using UnityEngine.Events;

public class Collectibles : MonoBehaviour
{
    [SerializeField] UnityEvent<Players> _onTriggerEnter;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            _onTriggerEnter.Invoke(collider.GetComponent<Players>());
        }
    }

    public void AddScore(Players player)
    {

    }

    public void AddLife(Players player)
    {

    }
}