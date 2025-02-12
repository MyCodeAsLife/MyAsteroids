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

        public int PlayerScore { get; private set; } = 0;           // Вынести в игрока, ей не место в информационной панели

        private void Start()
        {
            _laserChargesUI.text = "Test1";
            _laserCooldownUI.text = "Test2";
            //_playerScoreUI.text = "Очки: " + PlayerScore;
            _playerPositionUI.text = "Test4";
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

            //_playerScoreUI.text = "Очки: " + PlayerScore;
        }

        public void OnPlayerSpeedChange(float speed)
        {
            _playerSpeedUI.text = "Speed: " + string.Format("{0:0.0}", speed);
        }

        public void OnPlayerPositionChanged(Vector2 position)
        {
            _playerPositionUI.text = "Position (X : " + string.Format("{0:0.000}", position.x) + " | Y : " + string.Format("{0:0.000}", position.y) + ")";
        }

        public void OnPlayerRotationChange(float angle)
        {
            _playerRotationUI.text = "Rotation: " + string.Format("{0:0.0}", angle);            // Добавить знак градуса
        }

        public void OnSecondGunCharge(float cooldown)
        {
            _laserCooldownUI.text = "Перезарядка лазера: " + string.Format("{0:0.0}", cooldown);
        }

        public void OnSecondGunNumberChargesChange(int amount)
        {
            _laserChargesUI.text = "Выстрелов лазером: " + amount;
        }
    }
}