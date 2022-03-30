using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

namespace Runtime.Enemy
{
    public class EnemyMonoBehaviour : MonoBehaviour
    {
        [SerializeField] private float rigidBodySpeed;
        public NavMeshAgent navMeshAgent;
        public Transform targetTransform;
        public GameObject attachedRopeGameObject;
        [SerializeField] private GameObject agentMesh;
        [SerializeField] private CapsuleCollider capsuleCollider;
        [SerializeField] private GameObject dieParticles;
        [SerializeField] private GameObject dieSprite;
        public bool enemyDie;

        public bool agentCanMove;

        private IEnumerator _movePositionEnumerator;
        [SerializeField] private Rigidbody rigidBody;

        private void OnEnable()
        {
            // agentCanMove = true;

            navMeshAgent = GetComponent<NavMeshAgent>();
            targetTransform = GameObject.Find("Player").transform;

            _movePositionEnumerator = MovePositionEnumerator();
            rigidBody = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            if (!agentCanMove) return;

            navMeshAgent.SetDestination(targetTransform.position);
        }

        public void BeginMovePosition(Transform newTargetTransform)
        {
            navMeshAgent.enabled = false;
            agentCanMove = false;
            targetTransform = newTargetTransform;
            rigidBody.isKinematic = false;

            StartCoroutine(_movePositionEnumerator);
        }

        public void EndMovePosition()
        {
            StopCoroutine(_movePositionEnumerator);

            targetTransform = GameObject.Find("Player").transform;
            navMeshAgent.enabled = true;
            rigidBody.isKinematic = true;
            agentCanMove = true;
        }

        public void DestroyAgent()
        {
            StopCoroutine(_movePositionEnumerator);

            enemyDie = true;
            agentCanMove = false;
            navMeshAgent.enabled = false;
            rigidBody.isKinematic = true;
            capsuleCollider.isTrigger = false;
            rigidBody.constraints = RigidbodyConstraints.FreezePosition;
            rigidBody.constraints = RigidbodyConstraints.FreezeRotation;

            agentMesh.SetActive(false);
            dieParticles.SetActive(true);
            dieSprite.SetActive(true);
        }

        private IEnumerator MovePositionEnumerator()
        {
            while (true)
            {
                var direction = (targetTransform.position - transform.position).normalized;
                rigidBody.MovePosition(transform.position + direction * rigidBodySpeed * Time.fixedDeltaTime);
                yield return null;
            }
        }
    }
}