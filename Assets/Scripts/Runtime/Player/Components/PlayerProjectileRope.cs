using Runtime.Configurations;
using Runtime.Enemy;
using UnityEngine;
using UnityEngine.Animations;

namespace Runtime.Player.Components
{
    public interface IPlayerProjectileRope
    {
        void CreateRope();
        void AttachRope(Transform targetTransform);
    }

    public class PlayerProjectileRope : IPlayerProjectileRope
    {
        private readonly PlayerConfiguration _playerConfiguration;
        private readonly Transform _spawnTransform;
        private GameObject _ropeGameObject;

        public PlayerProjectileRope(PlayerConfiguration playerConfiguration, Transform spawnTransform)
        {
            _playerConfiguration = playerConfiguration;
            _spawnTransform = spawnTransform;
        }

        public void CreateRope()
        {
            var ropePrefab = _playerConfiguration.ropeGameObject;
            _ropeGameObject = Object.Instantiate(ropePrefab, _spawnTransform);
        }

        public void AttachRope(Transform targetTransform)
        {
            targetTransform.GetComponent<EnemyMonoBehaviour>().attachedRopeGameObject = _ropeGameObject;
            // targetTransform.GetComponent<EnemyMonoBehaviour>().navMeshAgent.enabled = false;
            targetTransform.GetComponent<EnemyMonoBehaviour>().agentCanMove = false;

            var ropeRigidBody = _ropeGameObject.transform.Find("RopeRB");
            var positionConstraint = ropeRigidBody.GetComponent<PositionConstraint>();
            ropeRigidBody.transform.parent = targetTransform;
            ropeRigidBody.transform.localPosition = new Vector3(0, 0.5f, 0);
        }
    }
}