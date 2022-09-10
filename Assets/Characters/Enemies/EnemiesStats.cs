using UnityEngine;

public class EnemiesStats : MonoBehaviour
{
    [SerializeField] private IntVariable _enemiesMaxHP;
    private int _enemyCurrentHP;
    //################################################################################################################################
    #region UNITY API
    void Start()
    {
        _enemyCurrentHP = _enemiesMaxHP.Value;
    }
    #endregion
    //################################################################################################################################
    /// <summary>
    /// Teste si les HP des Enemies sont inférieur ou égal à 0.
    /// </summary>
    /// <returns>Retourne "true" si les HP de l'Enemy tombe à 0 sinon "false".</returns>
    public bool EnemyIsDead()
    {
        return (_enemyCurrentHP <= 0);
    }
    /// <summary>
    /// Retire 1 HP aux Enemies.
    /// Retire 5 HP à la place si "isCritical" vaut "true".
    /// </summary>
    /// <param name="isCritical">(default) false : 1 HP ou true : 5 HP</param>
    public void HitEnemy(bool isCritical = false)
    {
        _enemyCurrentHP -= (isCritical) ? 5 : 1;
    }
    /// <summary>
    /// Assigne la valeur des HP des Enemies à 0.
    /// </summary>
    public void KillEnemy()
    {
        _enemyCurrentHP = 0;
    }
    //################################################################################################################################
}