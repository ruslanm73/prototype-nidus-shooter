using System.Collections;
using Runtime.Configurations;
using Runtime.Enemy;
using UnityEngine;

namespace Runtime.Player.Components
{
    public interface IPlayerProjectile
    {
        GameObject RootProjectileGameObject { get; set; }
        void CreateProjectile();
        void DetachProjectile();
        void DestroyProjectile();
        void UpdateProjectilePosition(Vector3 newPosition);
        void FilchEnemy();
        void DisableRangeZoneSprite();
    }

    public class PlayerProjectile : IPlayerProjectile
    {
        private readonly PlayerConfiguration _playerConfiguration;
        private readonly MonoBehaviour _playerMonoBehaviour;

        private GameObject _rangeZoneGameObject;
        private SpriteRenderer _rangeZoneSprite;
        private Collider _meshCollider;
        private Collider _sphereCollider;
        private Rigidbody _rangeZoneRigidBody;
        private PlayerProjectileTrigger _playerProjectileTrigger;

        public PlayerProjectile(PlayerConfiguration playerConfiguration, MonoBehaviour playerMonoBehaviour)
        {
            _playerConfiguration = playerConfiguration;
            _playerMonoBehaviour = playerMonoBehaviour;
        }

        public void CreateProjectile()
        {
            if (RootProjectileGameObject != null) return;

            RootProjectileGameObject = Object.Instantiate(_playerConfiguration.projectileGameObject);

            var projectileMesh = RootProjectileGameObject.transform.Find("ProjectileMesh");
            _meshCollider = projectileMesh.GetComponent<Collider>();
            _meshCollider.enabled = false;

            _rangeZoneGameObject = RootProjectileGameObject.transform.Find("ProjectileRangeZone").gameObject;

            _playerProjectileTrigger = _rangeZoneGameObject.GetComponent<PlayerProjectileTrigger>();
        }

        public void DetachProjectile()
        {
            _meshCollider.enabled = true;

            for (var i = 0; i < _playerProjectileTrigger.enemiesList.Count; i++)
            {
                var newRope = new PlayerProjectileRope(_playerConfiguration, RootProjectileGameObject.transform);

                newRope.CreateRope();

                newRope.AttachRope(_playerProjectileTrigger.enemiesList[i].transform);
                
                _playerProjectileTrigger.enemiesList[i].GetComponent<EnemyMonoBehaviour>().BeginMovePosition(RootProjectileGameObject.transform);
            }

            RootProjectileGameObject = default;
        }

        public void DestroyProjectile()
        {
            Object.Destroy(RootProjectileGameObject);
        }

        public void UpdateProjectilePosition(Vector3 newPosition)
        {
            RootProjectileGameObject.transform.position = newPosition;
        }

        public void FilchEnemy()
        {
        }

        public void DisableRangeZoneSprite()
        {
            _rangeZoneSprite.enabled = false;
        }

        public GameObject RootProjectileGameObject { get; set; }
    }
}