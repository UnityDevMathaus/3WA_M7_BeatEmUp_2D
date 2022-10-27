using UnityEngine;

public class DebugHolder : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.CompareTag("Player"))
        {
            Players p = collider.GetComponentInParent<Players>();
            p.CanHold = true;
            Debug.Log("CAN HOLD");
        }
    }
    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            Players p = collider.GetComponentInParent<Players>();
            p.CanHold = false;
            Debug.Log("CANT HOLD");
        }
    }
}