using UnityEngine;

public class SpriteMethodCaller : MonoBehaviour
{
    public EnemyController enemyController;

    public void CallShootMethod()
    {
        enemyController.Shoot();
    }
}
