using UnityEngine;

namespace Asteroids
{
    public class ObjectSpawner : MonoBehaviour
    {
        private const float Half = 0.5f;
        private const float Offset = 0.05f;

        public ShipPresenter PlayerShip { get; private set; }

        private PresentersFactory _factory;
        private InformationPanel _informationPanel;
        private System.Random _random;

        private float _timer;
        private float _spawnInterval;

        private void Start()
        {
            _factory = GetComponent<PresentersFactory>();
            _informationPanel = GetComponent<InformationPanel>();
            _random = new System.Random();
            _spawnInterval = Random.Range(1f, Config.AsteroidSpawnInterval);

            // ��� ��� ����, ��� ������������
            var prefab = Resources.Load<ShipPresenter>("Prefabs/Player");
            var playerShip = Instantiate<ShipPresenter>(prefab);
            PlayerShip = playerShip;
            playerShip.transform.SetParent(transform.parent);
            var center = new Vector2(0.5f, 0.5f);
            var startPosition = center * Config.ScaleWindowSize;

            playerShip.SetPresentersFactory(_factory);
            playerShip.SetPosition(startPosition);
            playerShip.SetDegreesPerSecond(Config.PlayerRotationSpeed);
            playerShip.SetMovementSpeed(Config.PlayerShipMovementSpeed);
            playerShip.SetMaxMovementSpeed(Config.PlayerShipMaxMovementSpeed);
            playerShip.SubscribeOnPositionChanged(_informationPanel.OnPlayerPositionChanged);
        }

        private void Update()
        {
            _timer += Time.deltaTime;

            if (_timer > _spawnInterval)
            {
                _timer = 0;
                _spawnInterval = Random.Range(1f, Config.AsteroidSpawnInterval);

                Interactive obj = (Interactive)_factory.GetObject(GameObjectType.Asteroid);
                obj.Deactivated += OnDeactivated;                   // ���� ����������� 1 ���, �� ���� ����� �������
                obj.Destroyed += OnAsteroidDestroyed;               // ����������� 1 ���?
                obj.Destroyed += Explode;                           // ����������� 1 ���?
                obj.Destroyed += _informationPanel.OnObjectDestroy; // ����������� 1 ���?
                InitInteractiveObject(obj);
            }
        }

        private Interactive InitInteractiveObject(Interactive obj)      // ��������� �������������
        {
            bool isVertical = _random.NextDouble() > Half ? true : false;       // ���� ��������� ������� �� ��������� ��� �����������?
            Vector2 spawnPosition = GetRandomStartPosition(isVertical);         // ��������� � ufo. ��! �� ��� ������ ����������, ����� ���������� ����� ��� ������������ ��� �������� ����������� ��������
            Vector2 directionMovement = Vector2.zero;                           // ��������� � ����� ����������
            float movementSpeed = 0f;                                           // ���
            float objectSize = 0f;

            if (obj.ObjectType == GameObjectType.Ufo)
            {
                movementSpeed = Random.Range(Config.UfoMinSpeed, Config.UfoMaxSpeed);                 // �����������?
                obj.SetDegreesPerSecond(Random.Range(Config.UfoMinRotationSpeed, Config.UfoMaxRotationSpeed));                 // �����������?
                ((UfoPresenter)obj).SetAtackTarget(PlayerShip);
                objectSize = Random.Range(Config.UfoMinSize, Config.UfoMaxSize);
            }
            else if (obj.ObjectType == GameObjectType.Asteroid || obj.ObjectType == GameObjectType.AsteroidPart)
            {
                movementSpeed = Random.Range(Config.AsteroidMinSpeed, Config.AsteroidMaxSpeed);                 // �����������?
                obj.SetDegreesPerSecond(Random.Range(Config.AsteroidMinRotationSpeed, Config.AsteroidMaxRotationSpeed));                 // �����������?
                obj.SetDirectionMovement(GetRandomStartDirectionMovement(isVertical, spawnPosition));
                obj.SetDirectionOfRotation(Random.Range(-1f, 1f));

                if (obj.ObjectType == GameObjectType.AsteroidPart)
                    objectSize = Random.Range(Config.AsteroidPartMinSize, Config.AsteroidPartMaxSize);
                else
                    objectSize = Random.Range(Config.AsteroidMinSize, Config.AsteroidMaxSize);
            }

            obj.SetPosition(spawnPosition);
            obj.SetMovementSpeed(movementSpeed);
            obj.SetMaxMovementSpeed(movementSpeed);
            obj.transform.localScale = new Vector3(objectSize, objectSize, 1f);

            return obj;
        }

        private Vector2 GetRandomStartPosition(bool isVertical)      // ��������� �������������
        {
            float extremePosition = _random.Next(2) < 1 ? (0 - Offset) : (Config.ScaleWindowSize + Offset);
            float position = (float)_random.NextDouble();
            Vector2 newPosition = new Vector2();

            newPosition.y = isVertical ? extremePosition : position;
            newPosition.x = isVertical ? position : extremePosition;

            return newPosition;
        }

        private Vector2 GetRandomOppositePosition(bool isVertical, Vector2 position)      // ��������� �������������
        {
            float newPosition = (float)_random.NextDouble();

            if (isVertical)
            {
                position.y = position.y < Half ? Offset : (Config.ScaleWindowSize - Offset);
                position.x = newPosition;
            }
            else
            {
                position.x = position.x < Half ? (Config.ScaleWindowSize - Offset) : Offset;
                position.y = newPosition;
            }

            return position;
        }

        private Vector2 GetRandomStartDirectionMovement(bool isVertical, Vector2 position)      // ��������� �������������
        {
            Vector2 oppositePosition = GetRandomOppositePosition(isVertical, position);
            float delta = Mathf.Atan2(oppositePosition.y - position.y, oppositePosition.x - position.x) * Mathf.Rad2Deg - 90;
            Vector2 directionMovement = Quaternion.Euler(0, 0, delta) * Vector3.up;
            return directionMovement;
        }

        private void OnDeactivated(Presenter obj)
        {
            obj.Deactivated -= OnDeactivated;

            if (obj.TryGetComponent<Interactive>(out Interactive interactiveObject))
            {
                interactiveObject.Destroyed -= Explode;
                interactiveObject.Destroyed -= _informationPanel.OnObjectDestroy;

                if (obj.ObjectType == GameObjectType.Asteroid)
                    interactiveObject.Destroyed -= OnAsteroidDestroyed;
            }
        }

        private void OnAsteroidDestroyed(Interactive obj)      // ��������� �� �������� ������� � �� ������ �����������     // ��������� �������������
        {
            if (obj.ObjectType == GameObjectType.Asteroid)          // ���� ����� ������������ ������ ��������, �� �������� �������
            {
                int numberOfFragmets = Random.Range(Config.AsteroidPartMinNumberOfFragments, Config.AsteroidPartMaxNumberOfFragments + 1);

                for (int i = 0; i < numberOfFragmets; i++)
                {
                    Interactive part = (Interactive)_factory.GetObject(GameObjectType.AsteroidPart);
                    InitInteractiveObject(part);
                    part.SetPosition(obj.GetPosition());
                    part.Deactivated += OnDeactivated;
                    part.Destroyed += Explode;
                    part.Destroyed += _informationPanel.OnObjectDestroy; // ����������� 1 ���?
                }
            }
        }

        private void Explode(Interactive obj)
        {
            var explosion = (ExplosionPresenter)_factory.GetObject(GameObjectType.Explosion);
            explosion.Explode(obj.transform);
        }
    }
}