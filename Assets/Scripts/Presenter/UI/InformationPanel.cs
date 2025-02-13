using System;
using TMPro;
using UnityEngine;

namespace Asteroids
{
    public class InformationPanel : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI _playerPositionUI;
        [SerializeField] TextMeshProUGUI _playerRotationUI;
        [SerializeField] TextMeshProUGUI _playerSpeedUI;
        [SerializeField] TextMeshProUGUI _laserChargesUI;
        [SerializeField] TextMeshProUGUI _laserCooldownUI;

        private int _laserCharges = Config.MaxNumberOfLaserCharges;
        private int _laserMaxCharges = Config.MaxNumberOfLaserCharges;

        public int PlayerScore { get; private set; } = 0;           // Вынести в игрока, ей не место в информационной панели?

        private void Start()
        {
            Init();
        }

        public void OnObjectDestroy(Presenter obj)
        {
            switch (obj.ObjectType)
            {
                case GameObjectType.Asteroid:
                    PlayerScore += Config.AsteroidCost;
                    break;
                case GameObjectType.AsteroidPart:
                    PlayerScore += Config.AsteroidParthCost;
                    break;
                case GameObjectType.Ufo:
                    PlayerScore += Config.UfoCost;
                    break;
                default:
                    throw new ArgumentException(nameof(GameObjectType));
            }
        }

        public void OnPlayerSpeedChanged(float speed)
        {
            _playerSpeedUI.text = "Speed: " + string.Format("{0:0.00}", speed);
        }

        public void OnPlayerPositionChanged(Vector2 position)
        {
            _playerPositionUI.text = "Position (X : " + string.Format("{0:0.00}", position.x) + " | Y : " + string.Format("{0:0.00}", position.y) + ")";
        }

        public void OnPlayerRotationChanged(float angle)
        {
            _playerRotationUI.text = "Rotation: " + string.Format("{0:0.0}", angle) + '°';
        }

        public void OnSecondGunNumberChargesChanged(int amount)       // Нужно получать от корабля текущее кол-во зарядов и максимальное
        {
            _laserCharges = amount;
            ShowNumberOfLaserCharges();
        }

        public void OnSecondGunMaxNumberChargesChanged(int amount)
        {
            _laserMaxCharges = amount;
            ShowNumberOfLaserCharges();
        }

        public void OnSecondGunCharged(float cooldown)
        {
            _laserCooldownUI.text = "Cooldown: " + string.Format("{0:0.0}", cooldown);
        }

        private void ShowNumberOfLaserCharges()
        {
            _laserChargesUI.text = "Lasers: " + _laserCharges + '/' + _laserMaxCharges;
        }

        private void Init()
        {
            OnSecondGunNumberChargesChanged(Config.MaxNumberOfLaserCharges);
            OnPlayerPositionChanged(Vector2.zero);
            OnPlayerRotationChanged(0f);
            OnPlayerSpeedChanged(0f);
            OnSecondGunCharged(0f);
        }
    }
}