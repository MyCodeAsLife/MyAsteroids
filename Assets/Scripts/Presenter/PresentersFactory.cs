using System;
using UnityEngine;

namespace Asteroids
{
    public class PresentersFactory : MonoBehaviour
    {
        private readonly Vector2 DefaultPosition = new Vector2(-1000f, -1000f);

        private ObjectPool<Presenter> _ufoPool;
        private ObjectPool<Presenter> _asteroidPool;
        private ObjectPool<Presenter> _asteroidPartPool;
        private ObjectPool<Presenter> _bulletPool;
        private ObjectPool<Presenter> _laserPool;

        private void Awake()
        {
            var ufoPrefab = Resources.Load<UfoPresenter>("Prefabs/Ufo");
            _ufoPool = new ObjectPool<Presenter>(GameObjectType.Ufo, ufoPrefab, Create, Enable, Disable);

            var asteroidPrefab = Resources.Load<AsteroidPresenter>("Prefabs/Asteroid");
            _asteroidPool = new ObjectPool<Presenter>(GameObjectType.Asteroid, asteroidPrefab, Create, Enable, Disable);

            //var asteroidPartPrefab = Resources.Load<AsteroidPartPresenter>("Prefabs/AsteroidPart");
            //_asteroidPartPool = new ObjectPool<Presenter>(GameObjectType.AsteroidPart, asteroidPartPrefab, Create, Enable, Disable);

            var bulletPrefab = Resources.Load<ProjectilePresenter>("Prefabs/Bullet");
            _bulletPool = new ObjectPool<Presenter>(GameObjectType.Bullet, bulletPrefab, Create, Enable, Disable);

            var laserPrefab = Resources.Load<ProjectilePresenter>("Prefabs/Laser");
            _laserPool = new ObjectPool<Presenter>(GameObjectType.Laser, laserPrefab, Create, Enable, Disable);
        }

        public Presenter GetObject(GameObjectType type)
        {
            switch (type)
            {
                case GameObjectType.Ufo:
                    return _ufoPool.Get();

                case GameObjectType.Asteroid:
                    return _asteroidPool.Get();

                case GameObjectType.AsteroidPart:
                    return _asteroidPartPool.Get();

                case GameObjectType.Bullet:
                    return _bulletPool.Get(); ;

                case GameObjectType.Laser:
                    return _laserPool.Get();

                default:
                    throw new ArgumentException(nameof(type));
            }
        }

        private Presenter Create(GameObjectType objectType, Presenter prefab)
        {
            var obj = Instantiate<Presenter>(prefab);
            obj.transform.SetParent(transform.parent);
            obj.SetGameObjectType(objectType);

            return obj;
        }

        private void Enable(Presenter obj)
        {
            obj.Destroyed += OnDestroyed;
            obj.transform.rotation = Quaternion.identity;
            obj.transform.position = DefaultPosition;
            obj.gameObject.SetActive(true);
        }

        private void Disable(Presenter obj)
        {
            obj.Destroyed -= OnDestroyed;
            obj.gameObject.SetActive(false);
            obj.transform.parent = transform.parent;
        }

        private void OnDestroyed(Presenter obj)
        {
            obj.transform.SetParent(transform.parent);

            switch (obj.ObjectType)
            {
                case GameObjectType.Ufo:
                    _ufoPool.Return(obj);
                    break;

                case GameObjectType.Asteroid:
                    _asteroidPool.Return(obj);
                    break;

                case GameObjectType.AsteroidPart:
                    _asteroidPartPool.Return(obj);
                    break;

                case GameObjectType.Bullet:
                    _bulletPool.Return(obj);
                    break;

                case GameObjectType.Laser:
                    _laserPool.Return(obj);
                    break;

                default:
                    throw new ArgumentException(nameof(obj));
            }
        }
    }
}