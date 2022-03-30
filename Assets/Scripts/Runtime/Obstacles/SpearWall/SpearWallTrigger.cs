using UnityEngine;

namespace Runtime.Obstacles.SpearWall
{
    public class SpearWallTrigger : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {
                transform.parent.GetComponent<Animator>().SetTrigger("EnableSpearWall");
            }
        }
    }
}