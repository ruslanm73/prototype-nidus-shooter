using System.Collections;
using Dreamteck.Splines;
using Runtime.Configurations;
using UnityEngine;

namespace Runtime.Player.Components
{
    public interface IPlayerControl
    {
    }

    public class PlayerControl : IPlayerControl
    {
        private readonly PlayerConfiguration _playerConfiguration;
        private readonly PlayerMonoBehaviour _playerMonoBehaviour;
        private readonly UltimateJoystick _ultimateJoystick;
        private readonly GameObject _playerGameObject;
        private readonly Animator _playerAnimator;
        private IPlayerRayCast _playerRayCast;
        private IPlayerProjectile _playerProjectile;

        private IEnumerator _updatePlayerTransformEnumerator;

        public PlayerControl(PlayerConfiguration playerConfiguration,
            PlayerMonoBehaviour playerPlayerMonoBehaviour,
            UltimateJoystick ultimateJoystick,
            GameObject playerGameObject,
            Animator playerAnimator)
        {
            _playerConfiguration = playerConfiguration;
            _playerMonoBehaviour = playerPlayerMonoBehaviour;
            _ultimateJoystick = ultimateJoystick;
            _playerGameObject = playerGameObject;
            _playerAnimator = playerAnimator;

            // _ultimateJoystick.OnPointerDownCallback += OnPointerJoystickDown;
            // _ultimateJoystick.OnPointerUpCallback += OnPointerJoystickUp;
        }

        private void OnPointerJoystickDown()
        {
            PlayerMovementAvailable = true;

            _playerRayCast = new PlayerRayCast(_playerConfiguration.targetLayerMask, _playerGameObject.transform);
            _playerProjectile = new PlayerProjectile(_playerConfiguration, _playerMonoBehaviour);

            _updatePlayerTransformEnumerator = PlayerMovementEnumerator();
            _playerMonoBehaviour.StartCoroutine(_updatePlayerTransformEnumerator);

            _playerGameObject.GetComponent<SplineFollower>().followSpeed = 0f;
        }

        private void OnPointerJoystickUp()
        {
            PlayerMovementAvailable = false;
            // _playerAnimator.SetFloat("PlayerSpeed", default);

            _playerMonoBehaviour.StopCoroutine(_updatePlayerTransformEnumerator);

            _playerProjectile.DisableRangeZoneSprite();
            _playerProjectile.FilchEnemy();
            _playerProjectile.DetachProjectile();

            DisablePlayerRayCast();

            _playerGameObject.GetComponent<SplineFollower>().followSpeed = 3f;
        }

        private void EnablePlayerRayCast()
        {
            var playerModel = _playerGameObject.transform.Find("PlayerModel");
            var playerRayCastRender = playerModel.transform.Find("RayCastRender").GetComponent<LineRenderer>();
            playerRayCastRender.enabled = true;

            playerRayCastRender.SetPosition(0, _playerGameObject.transform.position);
            playerRayCastRender.SetPosition(1, _playerRayCast.HitResultTransform.point);
        }

        private void DisablePlayerRayCast()
        {
            var playerModel = _playerGameObject.transform.Find("PlayerModel");
            var playerRayCastRender = playerModel.transform.Find("RayCastRender").GetComponent<LineRenderer>();
            playerRayCastRender.enabled = false;
        }

        private IEnumerator PlayerMovementEnumerator()
        {
            while (PlayerMovementAvailable)
            {
                var verticalAxis = _ultimateJoystick.GetVerticalAxis();
                var horizontalAxis = _ultimateJoystick.GetHorizontalAxis();
                var actualPlayerSpeed = _ultimateJoystick.GetDistance() * PlayerMaxSpeed;
                var transformEulerAngles =
                    new Vector3(0, Mathf.Atan2(horizontalAxis, verticalAxis) * 180 / Mathf.PI, 0);

                _playerGameObject.transform.eulerAngles = transformEulerAngles;
                // _playerGameObject.transform.Translate(_playerGameObject.transform.TransformDirection(Vector3.forward) * actualPlayerSpeed, Space.World);

                // _playerAnimator.SetFloat("PlayerSpeed", _ultimateJoystick.GetDistance());

                // var targetGameObject = _playerRayCast.CreateRayCast().gameObject;

                _playerRayCast.CreateRayCast();

                if (_playerRayCast.HitResultTransform.transform != null)
                {
                    _playerProjectile.CreateProjectile();

                    _playerProjectile.UpdateProjectilePosition(_playerRayCast.HitResultTransform.point);

                    EnablePlayerRayCast();
                }
                else
                {
                    _playerProjectile.DestroyProjectile();

                    DisablePlayerRayCast();
                }

                yield return null;
            }
        }

        public bool PlayerMovementAvailable { get; set; }
        public float PlayerMaxSpeed { get; set; } = 0.02f;
    }
}