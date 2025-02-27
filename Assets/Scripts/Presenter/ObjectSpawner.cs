using System;
using UnityEngine;

namespace Asteroids
{
    public class ObjectSpawner : MonoBehaviour
    {
        private const float Half = 0.5f;
        private const float Offset = 0.05f;

        private RootAudioSystem _audioSystem;
        private PresentersFactory _factory;
        private InformationPanel _informationPanel;
        private System.Random _random = new();

        private float _timerForUfoSpawn = Config.UfoSpawnInterval;
        private float _timerForAsteroidSpawn = Config.AsteroidSpawnInterval;

        private event Action Updated;

        public ShipPresenter PlayerShip { get; private set; }

        private void OnEnable()
        {
            GameState.SwitchIsPause += OnPausePress;
            OnPausePress(GameState.IsPaused);
        }

        private void OnDisable()
        {
            GameState.SwitchIsPause -= OnPausePress;
        }

        private void Start()
        {
            _audioSystem = FindFirstObjectByType<RootAudioSystem>();
            _informationPanel = FindFirstObjectByType<InformationPanel>();
            _factory = GetComponent<PresentersFactory>();

            // ��� ��� ����, ��� ������������ ��� ������� � ��������� �������������
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
            playerShip.SubscribeOnSpeedChanged(_informationPanel.OnPlayerSpeedChanged);
            playerShip.SubscribeOnRotationChanged(_informationPanel.OnPlayerRotationChanged);
            playerShip.SubscribeOnSecondGunCharged(_informationPanel.OnSecondGunCharged);
            playerShip.SubscribeOnSecondGunNumberChargesChanged(_informationPanel.OnSecondGunNumberChargesChanged);
            playerShip.SubscribeOnSecondGunMaxNumberChargesChanged(_informationPanel.OnSecondGunMaxNumberChargesChanged);
            playerShip.transform.localScale = new Vector3(Config.PlayerShipSize, Config.PlayerShipSize, 1f);
        }

        private void Update()
        {
            Updated?.Invoke();
        }

        private void OnPausePress(bool isPaused)
        {
            if (isPaused)
                Updated -= SpawnObjects;
            else
                Updated += SpawnObjects;
        }

        private void SpawnObjects()
        {
            _timerForUfoSpawn -= Time.deltaTime;
            _timerForAsteroidSpawn -= Time.deltaTime;

            if (_timerForUfoSpawn < 0)
            {
                _timerForUfoSpawn = UnityEngine.Random.Range(Config.UfoSpawnInterval * Half, Config.UfoSpawnInterval);

                Interactive obj = (Interactive)_factory.GetObject(GameObjectType.Ufo);
                obj.Deactivated += OnDeactivated;                   // ���� ����������� 1 ���, �� ���� ����� �������
                obj.Destroyed += Explode;                           // ����������� 1 ���?
                obj.Destroyed += _informationPanel.OnObjectDestroy; // ����������� 1 ���?
                InitInteractiveObject(obj);
            }

            if (_timerForAsteroidSpawn < 0)
            {
                _timerForAsteroidSpawn = UnityEngine.Random.Range(Config.AsteroidSpawnInterval * Half, Config.AsteroidSpawnInterval);

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
                movementSpeed = UnityEngine.Random.Range(Config.UfoMinSpeed, Config.UfoMaxSpeed);                 // �����������?
                obj.SetDegreesPerSecond(UnityEngine.Random.Range(Config.UfoMinRotationSpeed, Config.UfoMaxRotationSpeed));                 // �����������?
                ((UfoPresenter)obj).SetAtackTarget(PlayerShip);
                objectSize = UnityEngine.Random.Range(Config.UfoMinSize, Config.UfoMaxSize);
            }
            else if (obj.ObjectType == GameObjectType.Asteroid || obj.ObjectType == GameObjectType.AsteroidPart)
            {
                movementSpeed = UnityEngine.Random.Range(Config.AsteroidMinSpeed, Config.AsteroidMaxSpeed);                 // �����������?
                obj.SetDegreesPerSecond(UnityEngine.Random.Range(Config.AsteroidMinRotationSpeed, Config.AsteroidMaxRotationSpeed));                 // �����������?
                obj.SetDirectionMovement(GetRandomStartDirectionMovement(isVertical, spawnPosition));
                obj.SetDirectionOfRotation(UnityEngine.Random.Range(-1f, 1f));

                if (obj.ObjectType == GameObjectType.AsteroidPart)
                    objectSize = UnityEngine.Random.Range(Config.AsteroidPartMinSize, Config.AsteroidPartMaxSize);
                else
                    objectSize = UnityEngine.Random.Range(Config.AsteroidMinSize, Config.AsteroidMaxSize);
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
                int numberOfFragmets = UnityEngine.Random.Range(Config.AsteroidPartMinNumberOfFragments, Config.AsteroidPartMaxNumberOfFragments + 1);

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
            _audioSystem.PlaySoundExplosion();
            var explosion = (ExplosionPresenter)_factory.GetObject(GameObjectType.Explosion);
            explosion.Explode(obj.transform);
        }
    }
}