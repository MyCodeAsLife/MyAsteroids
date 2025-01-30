using System;
using UnityEngine;

namespace Asteroids
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private ShipPresenter _playerShip;

        private PresentersFactory _factory;
        private System.Random _random;

        private void Awake()
        {
            _factory = GetComponent<PresentersFactory>();
            _random = new System.Random();
        }

        private void Update()
        {

        }

        public void SetTargetToUfo(ShipPresenter ship)      // Нужно?
        {
            _playerShip = ship;
        }

        private Presenter InitEnemy(Presenter obj)
        {
            Vector2 spawnPosition = GetRandomPosition();        // Астероиды и ufo. Но! не для частей остероидов
            Vector2 directionMovement = Vector2.zero;           // Астероиды и части астероидов
            float movementSpeed = 0f;                           // Все
            float rotationSpeed = 0f;                           // Все
            float rotationDirection = 0f;                       // Астероидыи части астероидов

            if (obj.ObjectType == GameObjectType.Ufo)       // Присваивать сразу объекту а не переменным
            {
                movementSpeed = UnityEngine.Random.Range(Config.UfoMinSpeed, Config.UfoMaxSpeed);                 // Дублируется?
                rotationSpeed = UnityEngine.Random.Range(Config.UfoMinRotationSpeed, Config.UfoMaxRotationSpeed);                 // Дублируется?
                ((UfoPresenter)obj).SetAtackTarget(_playerShip);
            }
            else if (obj.ObjectType == GameObjectType.Asteroid || obj.ObjectType == GameObjectType.AsteroidPart)
            {
                movementSpeed = UnityEngine.Random.Range(Config.AsteroidMinSpeed, Config.AsteroidMaxSpeed);                 // Дублируется?
                rotationSpeed = UnityEngine.Random.Range(Config.AsteroidMinRotationSpeed, Config.AsteroidMaxRotationSpeed);                 // Дублируется?
                directionMovement = GetRandomDirectionMovement(spawnPosition);
                rotationDirection = UnityEngine.Random.Range(-1f, 1f);

                if (obj.ObjectType == GameObjectType.AsteroidPart)
                    spawnPosition = GetRandomPosition();        // Позицию для чайстей остероида, получать от самого астероида. Или сам астероид будет устанавливать им стартовую позицию?
            }

            return obj;
        }

        private Vector2 GetRandomPosition()        // Для всех кроме снарядов      
        {
            bool isVertical = _random.Next(2) == 1 ? true : false;
            int extremePosition = _random.Next(2);
            float position = (float)_random.NextDouble();
            Vector2 newPosition = new Vector2();

            newPosition.y = isVertical ? extremePosition : position;
            newPosition.x = isVertical ? position : extremePosition;

            return newPosition;
        }

        private Vector2 GetRandomDirectionMovement(Vector2 position)        // Для всех кроме снарядов
        {
            // Делать проверку, если позиция за экраном, то направление делать в конусе между углами противоположного края
            // Если позиция в пределах экрана, то направление делать полностью случайным.
            throw new NotImplementedException();
        }
    }
}