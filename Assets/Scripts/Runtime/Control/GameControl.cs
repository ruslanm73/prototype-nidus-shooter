using System.Collections;
using Runtime.Configurations;
using Runtime.Player.Components;
using UnityEngine;

namespace Runtime.Control
{
    public interface IGameControl
    {
        bool GameControlAvailable { get; set; }
        void EnableGameControl();
        void DisableGameControl();
    }

    public class GameControl : IGameControl
    {
        private readonly IPlayerProjectile _playerProjectile;
        private readonly GameControlMouseCast _gameControlMouseCast;

        public GameControl(PlayerConfiguration playerConfiguration,MonoBehaviour monoBehaviour, IPlayerProjectile playerProjectile)
        {
            _playerProjectile = playerProjectile;
            _gameControlMouseCast = new GameControlMouseCast(playerConfiguration, monoBehaviour, playerProjectile);
        }

        public void EnableGameControl()
        {
            _gameControlMouseCast.EnableDetect();
        }

        public void DisableGameControl()
        {
            _gameControlMouseCast.DisableDetect();
        }

        public bool GameControlAvailable { get; set; }
    }

    public class GameControlMouseCast
    {
        private readonly PlayerConfiguration _playerConfiguration;
        private readonly MonoBehaviour _monoBehaviour;
        private readonly IPlayerProjectile _playerProjectile;
        private readonly IEnumerator _updateMousePositionEnumerator;

        private Vector3 worldPosition;
        private GameObject _primitive;

        public GameControlMouseCast(PlayerConfiguration playerConfiguration, MonoBehaviour monoBehaviour, IPlayerProjectile playerProjectile)
        {
            _playerConfiguration = playerConfiguration;
            _monoBehaviour = monoBehaviour;
            _playerProjectile = playerProjectile;
            _updateMousePositionEnumerator = UpdateMousePositionEnumerator();
        }

        public void EnableDetect()
        {
            DetectAvailable = true;
            _monoBehaviour.StartCoroutine(_updateMousePositionEnumerator);
        }

        public void DisableDetect()
        {
            DetectAvailable = false;
            _monoBehaviour.StopCoroutine(_updateMousePositionEnumerator);
        }

        private IEnumerator UpdateMousePositionEnumerator()
        {
            while (DetectAvailable)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                    RaycastHit hit = new RaycastHit();

                    if (Physics.Raycast(ray, out hit, 1000, _playerConfiguration.targetLayerMask))
                    {
                        Debug.Log(hit.collider.gameObject.name);

                        _playerProjectile.CreateProjectile();
                        yield return null;
                        _playerProjectile.UpdateProjectilePosition(hit.point);
                        _playerProjectile.DetachProjectile();

                        if (hit.transform.gameObject.layer == 9)
                        {
                            Debug.Log("SAW!");
                        }
                    }
                }

                yield return null;
            }
        }

        public bool DetectAvailable { get; set; }
    }
}