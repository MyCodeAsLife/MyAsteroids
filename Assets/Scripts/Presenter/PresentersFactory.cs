using System;
using UnityEngine;

namespace Asteroids
{
    public class PresentersFactory : MonoBehaviour
    {
        private const string UfoType = "UfoPresenter";                          // Вынести в Config ?
        private const string AsteroidType = "AsteroidPresenter";
        private const string AsteroidPartType = "AsteroidPartPresenter";
        private const string BulletType = "BulletPresenter";
        private const string LaserType = "LaserPresenter";

        [SerializeField] private ShipPresenter _ufoTarget;

        private ObjectPool<Presenter> _ufoPool;
        private ObjectPool<Presenter> _asteroidPool;
        private ObjectPool<Presenter> _asteroidPartPool;
        private ObjectPool<Presenter> _bulletPool;
        private ObjectPool<Presenter> _laserPool;

        private System.Random _random;

        private void Awake()
        {
            var ufoPrefab = Resources.Load<UfoPresenter>("Prefabs/Ufo");
            _ufoPool = new ObjectPool<Presenter>(ufoPrefab, Create, Enable, Disable);

            var asteroidPrefab = Resources.Load<AsteroidPresenter>("Prefabs/Asteroid");
            _asteroidPool = new ObjectPool<Presenter>(asteroidPrefab, Create, Enable, Disable);

            //var asteroidPartSpawner = Resources.Load<AsteroidPartPresenter>("Prefabs/AsteroidPart");
            //_asteroidPartPool = new ObjectPool<Presenter>(asteroidPartSpawner, Create, Enable, Disable);

            var bulletPrefab = Resources.Load<ProjectilePresenter>("Prefabs/Bullet");
            _bulletPool = new ObjectPool<Presenter>(bulletPrefab, Create, Enable, Disable);

            var laserPrefab = Resources.Load<ProjectilePresenter>("Prefabs/Laser");
            _laserPool = new ObjectPool<Presenter>(laserPrefab, Create, Enable, Disable);
            _random = new System.Random();
        }

        public Presenter GetObject(string type)
        {
            switch (type)
            {
                case UfoType:
                    return _ufoPool.Get();

                case AsteroidType:
                    return _asteroidPool.Get();

                case AsteroidPartType:
                    return _asteroidPartPool.Get();

                case BulletType:
                    return _bulletPool.Get(); ;

                case LaserType:
                    return _laserPool.Get();

                default:
                    throw new ArgumentException(nameof(type));
            }
        }

        public void SetTargetToUfo(ShipPresenter ship)
        {
            _ufoTarget = ship;
        }

        private Presenter Create(Presenter prefab)
        {
            var obj = Instantiate<Presenter>(prefab);
            obj.transform.SetParent(transform.parent);

            return obj;
        }

        private void Enable(Presenter obj)
        {
            obj.Destroyed += OnDestroyed;
            obj.transform.rotation = Quaternion.identity;
            obj.transform.position = Vector3.zero;
            obj.gameObject.SetActive(true);
        }

        private void Disable(Presenter obj)
        {
            obj.Destroyed -= OnDestroyed;
            obj.gameObject.SetActive(false);
        }

        private void OnDestroyed(Presenter obj)
        {
            var type = obj.GetType().Name;
            obj.transform.SetParent(transform.parent);

            switch (type)
            {
                case UfoType:
                    _ufoPool.Return(obj);
                    break;

                case AsteroidType:
                    _asteroidPool.Return(obj);
                    break;

                case AsteroidPartType:
                    _asteroidPartPool.Return(obj);
                    break;

                case BulletType:
                    _bulletPool.Return(obj);
                    break;

                case LaserType:
                    _laserPool.Return(obj);
                    break;

                default:
                    throw new ArgumentException(nameof(obj));
            }
        }

        private Presenter InitEnemy(Presenter obj, string type)        // Для всех кроме снарядов, вынести в спавнер врагов?
        {
            Vector2 spawnPosition = GetRandomPosition();        // Астероиды и ufo
            Vector2 directionMovement = Vector2.zero;           // Астероиды
            float movementSpeed = 0f;                           // Все
            float rotationSpeed = 0f;                           // Астероиды и ufo

            if (type == UfoType)
            {
                movementSpeed = UnityEngine.Random.Range(Config.UfoMinSpeed * 100, Config.UfoMaxSpeed);
                rotationSpeed = UnityEngine.Random.Range(Config.UfoMinRotationSpeed, Config.UfoMaxRotationSpeed);
            }
            else if (type == AsteroidType || type == AsteroidPartType)
            {
                movementSpeed = UnityEngine.Random.Range(Config.AsteroidMinSpeed, Config.AsteroidMaxSpeed);
                rotationSpeed = UnityEngine.Random.Range(Config.AsteroidMinRotationSpeed, Config.AsteroidMaxRotationSpeed);
                directionMovement = GetRandomDirectionMovement(spawnPosition);
            }

            return obj;
        }

        private Vector2 GetRandomPosition()        // Для всех кроме снарядов, вынести в спавнер врагов?         
        {
            bool isVertical = _random.Next(2) == 1 ? true : false;
            int extremePosition = _random.Next(2);
            float position = (float)_random.NextDouble();
            Vector2 newPosition = new Vector2();

            newPosition.y = isVertical ? extremePosition : position;
            newPosition.x = isVertical ? position : extremePosition;

            return newPosition;
        }

        private Vector2 GetRandomDirectionMovement(Vector2 position)        // Для всех кроме снарядов, вынести в спавнер врагов?
        {
            throw new NotImplementedException();
        }
    }
}