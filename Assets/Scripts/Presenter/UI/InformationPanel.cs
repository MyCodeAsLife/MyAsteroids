using System;
using TMPro;
using UnityEngine;

namespace Asteroids
{
    public class InformationPanel : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI _laserChargesUI;
        [SerializeField] TextMeshProUGUI _laserCooldownUI;
        [SerializeField] TextMeshProUGUI _playerScoreUI;
        [SerializeField] TextMeshProUGUI _playerPositionUI;

        private int _playerScore = 0;

        private void Start()
        {
            _laserChargesUI.text = "Test1";
            _laserCooldownUI.text = "Test2";
            _playerScoreUI.text = "Очки: " + _playerScore;
            _playerPositionUI.text = "Test4";
        }

        public void OnObjectDestroy(Presenter obj)
        {
            switch (obj.ObjectType)
            {
                case GameObjectType.Asteroid:
                    _playerScore += Config.AsteroidCost;
                    break;
                case GameObjectType.AsteroidPart:
                    _playerScore += Config.AsteroidParthCost;
                    break;
                case GameObjectType.Ufo:
                    _playerScore += Config.UfoCost;
                    break;
                default:
                    throw new ArgumentException(nameof(GameObjectType));
            }

            _playerScoreUI.text = "Очки: " + _playerScore;
        }

        public void OnPlayerPositionChanged(Vector2 position)
        {
            _playerPositionUI.text = "Координаты коробля (X : " + string.Format("{0:0.000}", position.x) + " | Y : " + string.Format("{0:0.000}", position.y) + ")";
        }

        internal void OnSecondGunCharge(float cooldown)
        {
            _laserCooldownUI.text = "Перезарядка лазера: " + string.Format("{0:0.0}", cooldown);
        }

        internal void OnSecondGunNumberChargesChange(int amount)
        {
            _laserChargesUI.text = "Выстрелов лазером: " + amount;
        }
    }
}