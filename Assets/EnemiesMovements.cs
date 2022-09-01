using UnityEngine;

public class EnemiesMovements : MonoBehaviour
{
    [SerializeField] private Transform _parent;
    [SerializeField] private EnemiesIA _ia;

    void Update()
    {
            _parent.position = Vector2.MoveTowards(_parent.position, _ia.Direction, 0.005f);
    }
}
