using System;
using Runtime.Configurations;
using Runtime.Control;
using Runtime.Player;
using Runtime.Player.Components;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Runtime.Managers
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private PlayerConfiguration playerConfiguration;

        private void Awake()
        {
            IPlayerProjectile playerProjectile = new PlayerProjectile(playerConfiguration, this);
            IGameControl gameControl = new GameControl(playerConfiguration, this, playerProjectile);
            gameControl.EnableGameControl();

            var ultimateJoystick = GameObject.Find("MainJoystick").GetComponent<UltimateJoystick>();
            var playerGameObject = GameObject.Find("Player");

            IPlayerView playerView = new PlayerView(playerConfiguration, ultimateJoystick, playerGameObject);
        }

        private void Update()
        {
            if (Input.GetKey(KeyCode.Space))
            {
                SceneManager.LoadScene(0);
            }
        }
    }
}