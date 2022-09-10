using UnityEngine;

public class EnemiesStats : MonoBehaviour
{
    [SerializeField] private IntVariable _enemiesMaxHP;
    private int _enemyCurrentHP;

    public bool EnemyIsDead()
    {
        return (_enemyCurrentHP <= 0);
    }

}