using System;
using System.Collections.Generic;
using Runtime.Enemy;
using UnityEngine;

namespace Runtime.Player.Components
{
    public class PlayerProjectileTrigger : MonoBehaviour
    {
        public List<GameObject> enemiesList = new List<GameObject>();

        private void OnEnable()
        {
            // var enemies = GameObject.FindGameObjectsWithTag("Enemy");
            // Debug.Log(enemies.Length);
            //
            // for (var i = 0; i < enemies.Length; i++)
            // {
            //     Debug.Log($"{enemies[i].name}: {Vector3.Distance(transform.position, enemies[i].transform.position)}");
            //     // enemiesList.Add(enemies[i]);
            //     if (Vector3.Distance(transform.position, enemies[i].transform.position) < 10f)
            //     {
            //         if (enemies[i].GetComponent<EnemyMonoBehaviour>().enemyDie) return;
            //
            //         enemiesList.Add(enemies[i]);
            //     }
            // }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {
                enemiesList.Add(other.gameObject);
            }
        }
    }
}