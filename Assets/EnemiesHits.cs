using UnityEngine;

public class EnemiesHits : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            Debug.Log("HITTTTTTTT");
        }
    }
}
