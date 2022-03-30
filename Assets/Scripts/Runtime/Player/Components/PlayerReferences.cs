using UnityEngine;

namespace Runtime.Player.Components
{
    public interface IPlayerReferences
    {
        Transform PlayerModelTransform { get; set; }
        Transform PlayerMeshTransform { get; set; }

        Animator PlayerAnimator { get; set; }
    }

    public class PlayerReferences : IPlayerReferences
    {
        public PlayerReferences(Transform playerTransform)
        {
            PlayerModelTransform = playerTransform.Find("PlayerModel");
            PlayerMeshTransform = PlayerModelTransform.Find("PlayerMesh");

            PlayerAnimator = PlayerMeshTransform.GetComponent<Animator>();
        }

        public Transform PlayerModelTransform { get; set; }
        public Transform PlayerMeshTransform { get; set; }
        public Animator PlayerAnimator { get; set; }
    }
}