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

        //[SerializeField] private UfoPresenter _ufoPrefab;
        //[SerializeField] private AsteroidPresenter _asteroidPrefab;
        //[SerializeField] private AsteroidPartPresenter _asteroidPartPrefab;
        //[SerializeField] private ProjectilePresenter _bulletPrefab;
        //[SerializeField] private ProjectilePresenter _laserPrefab;
        private ObjectPool<Presenter> _ufoPool;
        private ObjectPool<Presenter> _asteroidPool;
        private ObjectPool<Presenter> _asteroidPartPool;
        private ObjectPool<Presenter> _bulletPool;
        private ObjectPool<Presenter> _laserPool;

        //private PresentersSpawner _ufoSpawner;
        //private PresentersSpawner _asteroidSpawner;
        //private PresentersSpawner _asteroidPartSpawner;
        //private PresentersSpawner _bulletSpawner;
        //private PresentersSpawner _laserSpawner;
        private ShipPresenter _ufoTarget;
        [SerializeField] private Canvas _canvas;

        private void Awake()
        {
            var ufoPrefab = Resources.Load<UfoPresenter>("Prefabs/Ufo");
            _ufoPool = new ObjectPool<Presenter>(ufoPrefab, Create, Enable, Disable);
            //_ufoSpawner = new PresentersSpawner(ufoPrefab, CreateObject, transform.parent);

            var asteroidPrefab = Resources.Load<AsteroidPresenter>("Prefabs/Asteroid");
            _asteroidPool = new ObjectPool<Presenter>(asteroidPrefab, Create, Enable, Disable);
            //_asteroidSpawner = new PresentersSpawner(asteroidPrefab, CreateObject, transform.parent);

            //var asteroidPartSpawner = Resources.Load<AsteroidPartPresenter>("Prefabs/AsteroidPart");
            //_asteroidPartPool = new ObjectPool<Presenter>(asteroidPartSpawner, Create, Enable, Disable);
            //_asteroidPartSpawner = new PresentersSpawner(_asteroidPartPrefab, CreateObject, transform.parent);

            var bulletPrefab = Resources.Load<ProjectilePresenter>("Prefabs/Bullet");
            _bulletPool = new ObjectPool<Presenter>(bulletPrefab, Create, Enable, Disable);
            //_bulletSpawner = new PresentersSpawner(bulletPrefab, CreateObject, transform.parent);

            var laserPrefab = Resources.Load<ProjectilePresenter>("Prefabs/Laser");
            _laserPool = new ObjectPool<Presenter>(laserPrefab, Create, Enable, Disable);
            //_laserSpawner = new PresentersSpawner(laserPrefab, CreateObject, transform.parent);
        }

        public Presenter GetObject(string type)
        {
            switch (type)
            {
                case UfoType:
                    //return GetUfo();
                    return _ufoPool.Get();

                case AsteroidType:
                    //return GetAsteroid();
                    return _asteroidPool.Get();

                case AsteroidPartType:
                    //return GetAsteroidPart();
                    return _asteroidPartPool.Get();

                case BulletType:
                    //return GetBullet();
                    return _bulletPool.Get(); ;

                case LaserType:
                    //return GetLaser();
                    return _laserPool.Get();

                default:
                    throw new ArgumentException(nameof(type));
            }
        }

        public void SetTargetToUfo(ShipPresenter ship)
        {
            _ufoTarget = ship;
        }

        //private Presenter GetLaser()
        //{
        //    return _laserSpawner.GetObject();
        //}

        //private Presenter GetBullet()
        //{
        //    return _bulletSpawner.GetObject();
        //}

        //private Presenter GetAsteroidPart()
        //{
        //    return _asteroidPartSpawner.GetObject();
        //}

        //private Presenter GetAsteroid()
        //{
        //    return _asteroidSpawner.GetObject();
        //}

        //private Presenter GetUfo()
        //{
        //    return _ufoSpawner.GetObject();
        //}

        //private Presenter CreateObject(Presenter prefab)
        //{
        //    return Instantiate<Presenter>(prefab);
        //}

        private Presenter Create(Presenter prefab)
        {
            //var obj = _createEnemy(prefab);
            var obj = Instantiate<Presenter>(prefab);
            obj.transform.SetParent(transform.parent);

            return obj;
        }

        private void Enable(Presenter obj)       // Перенести инициализацию в фабрику(операции с transform)?
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

            //Collected?.Invoke();
        }
    }
}