using System;
using UnityEngine;

namespace Runtime.Enemy
{
    public class EnemyTrigger : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.name == "ProjectileMesh")
            {
                var enemyMonoBehaviour = GetComponent<EnemyMonoBehaviour>();

                if (enemyMonoBehaviour.enemyDie) return;
                Destroy(enemyMonoBehaviour.attachedRopeGameObject);
                enemyMonoBehaviour.EndMovePosition();
            }

            if (other.name == "SawTrigger")
            {
                var enemyMonoBehaviour = GetComponent<EnemyMonoBehaviour>();

                Destroy(enemyMonoBehaviour.attachedRopeGameObject);
                enemyMonoBehaviour.DestroyAgent();
            }
        }
    }
}