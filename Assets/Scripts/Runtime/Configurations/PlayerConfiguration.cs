using UnityEngine;

namespace Runtime.Configurations
{
    [CreateAssetMenu(fileName = "PlayerConfigurations", menuName = "Player Configurations", order = 0)]
    public class PlayerConfiguration : ScriptableObject
    {
        [Header("Projectile")] public GameObject projectileGameObject;
        public LayerMask targetLayerMask;
        public LayerMask selfLayerMask;
        public Sprite playerProjectileZoneSprite;
        public GameObject ropeGameObject;
        public float magnetForce;
    }
}