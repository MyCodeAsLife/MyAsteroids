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

        public void SetTargetToUfo(ShipPresenter ship)      // �����?
        {
            _playerShip = ship;
        }

        private Presenter InitEnemy(Presenter obj)
        {
            Vector2 spawnPosition = GetRandomPosition();        // ��������� � ufo. ��! �� ��� ������ ����������
            Vector2 directionMovement = Vector2.zero;           // ��������� � ����� ����������
            float movementSpeed = 0f;                           // ���
            float rotationSpeed = 0f;                           // ���
            float rotationDirection = 0f;                       // ���������� ����� ����������

            if (obj.ObjectType == GameObjectType.Ufo)       // ����������� ����� ������� � �� ����������
            {
                movementSpeed = UnityEngine.Random.Range(Config.UfoMinSpeed, Config.UfoMaxSpeed);                 // �����������?
                rotationSpeed = UnityEngine.Random.Range(Config.UfoMinRotationSpeed, Config.UfoMaxRotationSpeed);                 // �����������?
                ((UfoPresenter)obj).SetAtackTarget(_playerShip);
            }
            else if (obj.ObjectType == GameObjectType.Asteroid || obj.ObjectType == GameObjectType.AsteroidPart)
            {
                movementSpeed = UnityEngine.Random.Range(Config.AsteroidMinSpeed, Config.AsteroidMaxSpeed);                 // �����������?
                rotationSpeed = UnityEngine.Random.Range(Config.AsteroidMinRotationSpeed, Config.AsteroidMaxRotationSpeed);                 // �����������?
                directionMovement = GetRandomDirectionMovement(spawnPosition);
                rotationDirection = UnityEngine.Random.Range(-1f, 1f);

                if (obj.ObjectType == GameObjectType.AsteroidPart)
                    spawnPosition = GetRandomPosition();        // ������� ��� ������� ���������, �������� �� ������ ���������. ��� ��� �������� ����� ������������� �� ��������� �������?
            }

            return obj;
        }

        private Vector2 GetRandomPosition()        // ��� ���� ����� ��������      
        {
            bool isVertical = _random.Next(2) == 1 ? true : false;
            int extremePosition = _random.Next(2);
            float position = (float)_random.NextDouble();
            Vector2 newPosition = new Vector2();

            newPosition.y = isVertical ? extremePosition : position;
            newPosition.x = isVertical ? position : extremePosition;

            return newPosition;
        }

        private Vector2 GetRandomDirectionMovement(Vector2 position)        // ��� ���� ����� ��������
        {
            // ������ ��������, ���� ������� �� �������, �� ����������� ������ � ������ ����� ������ ���������������� ����
            // ���� ������� � �������� ������, �� ����������� ������ ��������� ���������.
            throw new NotImplementedException();
        }
    }
}