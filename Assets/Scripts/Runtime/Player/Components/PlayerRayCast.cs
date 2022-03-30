using UnityEngine;

namespace Runtime.Player.Components
{
    public interface IPlayerRayCast
    {
        void CreateRayCast();
        RaycastHit HitResultTransform { get; set; }
    }

    public class PlayerRayCast : IPlayerRayCast
    {
        private readonly LayerMask _layerMask;
        private readonly Transform _playerTransform;

        public PlayerRayCast(LayerMask layerMask, Transform playerTransform)
        {
            _layerMask = layerMask;
            _playerTransform = playerTransform;
        }

        public void CreateRayCast()
        {
            if (Physics.Raycast(_playerTransform.position, _playerTransform.TransformDirection(Vector3.forward), out var hit, Mathf.Infinity, _layerMask))
            {
                Debug.DrawRay(_playerTransform.position, _playerTransform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            }
            else
            {
                Debug.DrawRay(_playerTransform.position, _playerTransform.TransformDirection(Vector3.forward) * 1000, Color.white);
            }

            HitResultTransform = hit;
        }

        public RaycastHit HitResultTransform { get; set; }
    }
}