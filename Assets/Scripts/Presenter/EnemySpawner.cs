using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

namespace Asteroids
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private ShipPresenter _playerShip;

        private PresentersFactory _factory;
        private System.Random _random;

        private float _timer;

        private void Start()
        {
            _factory = GetComponent<PresentersFactory>();
            _random = new System.Random();
        }

        private void Update()
        {
            _timer += Time.deltaTime;

            if (_timer > Config.AsteroidSpawnInterval)
            {
                _timer = 0;

                var obj = _factory.GetObject(GameObjectType.AsteroidPart);              // �����
                InitEnemy(obj);
            }
        }

        private Presenter InitEnemy(Presenter obj)
        {
            bool isVertical = _random.Next(2) == 1 ? true : false;  // ���� ��������� ������� �� ��������� ��� �����������?
            Vector2 spawnPosition = GetRandomStartPosition(isVertical);        // ��������� � ufo. ��! �� ��� ������ ����������
            Vector2 directionMovement = Vector2.zero;           // ��������� � ����� ����������
            float movementSpeed = 0f;                           // ���
            float rotationSpeed = 0f;                           // ���
            float directionOfRotation = 0f;                       // ���������� ����� ����������
            float objectSize = 0f;

            if (obj.ObjectType == GameObjectType.Ufo)       // ����������� ����� ������� � �� ����������
            {
                movementSpeed = Random.Range(Config.UfoMinSpeed, Config.UfoMaxSpeed);                 // �����������?
                rotationSpeed = Random.Range(Config.UfoMinRotationSpeed, Config.UfoMaxRotationSpeed);                 // �����������?
                ((UfoPresenter)obj).SetAtackTarget(_playerShip);
                objectSize = Random.Range(Config.UfoMinSize, Config.UfoMaxSize);
            }
            else if (obj.ObjectType == GameObjectType.Asteroid || obj.ObjectType == GameObjectType.AsteroidPart)
            {
                movementSpeed = UnityEngine.Random.Range(Config.AsteroidMinSpeed, Config.AsteroidMaxSpeed);                 // �����������?
                rotationSpeed = UnityEngine.Random.Range(Config.AsteroidMinRotationSpeed, Config.AsteroidMaxRotationSpeed);                 // �����������?
                directionMovement = GetRandomStartDirectionMovement(isVertical, spawnPosition);
                directionOfRotation = UnityEngine.Random.Range(-1f, 1f);

                if (obj.ObjectType == GameObjectType.AsteroidPart)
                    objectSize = Random.Range(Config.AsteroidPartMinSize, Config.AsteroidPartMaxSize);
                else
                    objectSize = Random.Range(Config.AsteroidMinSize, Config.AsteroidMaxSize);
                //if (obj.ObjectType == GameObjectType.AsteroidPart)
                //    spawnPosition = GetRandomPosition(_random.Next(2) == 1 ? true : false);        // ������� ��� ������� ���������, �������� �� ������ ���������. ��� ��� �������� ����� ������������� �� ��������� �������?
            }

            obj.SetPosition(spawnPosition);
            obj.SetDirectionMovement(directionMovement);
            obj.SetMovementSpeed(movementSpeed);
            obj.SetDegreesPerSecond(rotationSpeed);
            obj.SetDirectionOfRotation(directionOfRotation);
            obj.transform.localScale = new Vector3(objectSize, objectSize, 1f);

            return obj;
        }

        private Vector2 GetRandomStartPosition(bool isVertical)
        {
            int extremePosition = _random.Next(2);
            float position = (float)_random.NextDouble();
            Vector2 newPosition = new Vector2();

            newPosition.y = isVertical ? extremePosition : position;
            newPosition.x = isVertical ? position : extremePosition;

            return newPosition;
        }

        private Vector2 GetRandomOppositePosition(bool isVertical, Vector2 position)
        {
            const float Half = 0.5f;
            float newPosition = (float)_random.NextDouble();

            if (isVertical)
            {
                position.y = position.y < Half ? 1 : 0;
                position.x = newPosition;
            }
            else
            {
                position.x = position.x < Half ? 1 : 0;
                position.y = newPosition;
            }

            return position;
        }

        private Vector2 GetRandomStartDirectionMovement(bool isVertical, Vector2 position)        // ��� ����������
        {
            // ������ ��������, ���� ������� �� �������, �� ����������� ������ � ������ ����� ������ ���������������� ����
            // ���� ������� � �������� ������, �� ����������� ������ ��������� ���������.
            Vector2 oppositePosition = GetRandomOppositePosition(isVertical, position);
            float delta = Mathf.Atan2(oppositePosition.y - position.y, oppositePosition.x - position.x) * Mathf.Rad2Deg - 90;
            Vector2 directionMovement = Quaternion.Euler(0, 0, delta) * Vector3.up;
            return directionMovement;
        }

        private void OnAsteroidDestoyed(Presenter obj)                  // ��������� �� ������ ���������
        {
            if (obj.ObjectType == GameObjectType.Asteroid)
            {
                // ��������� ���� ������ ���������� �� ����� ����������� ��������
                int numberOfFragmets = Random.Range(Config.AsteroidPartMinNumberOfFragments, Config.AsteroidPartMaxNumberOfFragments + 1);

                //for (int i = 0; i < numberOfFragmets; i++)
                //    _fa
            }
        }
    }
}