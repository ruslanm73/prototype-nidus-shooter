using Runtime.Configurations;
using Runtime.Player.Components;
using UnityEngine;

namespace Runtime.Player
{
    public interface IPlayerView
    {
    }

    public class PlayerView : IPlayerView
    {
        private IPlayerReferences _playerReferences;
        private PlayerMonoBehaviour _playerMonoBehaviour;
        private IPlayerRayCast _playerRayCast;
        private IPlayerProjectile _playerProjectile;
        private IPlayerControl _playerControl;

        public PlayerView(PlayerConfiguration playerConfiguration, UltimateJoystick ultimateJoystick, GameObject playerGameObject)
        {
            _playerReferences = new PlayerReferences(playerGameObject.transform);

            _playerMonoBehaviour = playerGameObject.GetComponent<PlayerMonoBehaviour>();
            _playerControl = new PlayerControl(playerConfiguration, _playerMonoBehaviour, ultimateJoystick, playerGameObject, _playerReferences.PlayerAnimator);
        }
    }
}