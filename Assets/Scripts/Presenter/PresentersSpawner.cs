using System;
using UnityEngine;

namespace Asteroids
{
    public class PresentersSpawner              // Не используется, удалить
    {
        private readonly Func<Presenter, Presenter> _createEnemy;
        private readonly Presenter _prefab;
        private readonly Transform _parent;

        private ObjectPool<Presenter> _pool;

        //public event Action Collected;

        public PresentersSpawner(Presenter prefab, Func<Presenter, Presenter> createFunc, Transform parent)
        {
            _prefab = prefab;
            _createEnemy = createFunc;
            _parent = parent;
            _pool = new ObjectPool<Presenter>(_prefab, Create, Enable, Disable);
        }

        ~PresentersSpawner()
        {
            _pool.ReturnAll();
        }

        //public int NumberOfActiveResources => _pool.ActiveResourcesCount;

        //public void Spawn(Vector3 spawnPosition)      // Перенести инициализацию в фабрику?
        //{
        //    var resource = _pool.Get();
        //    resource.transform.position = spawnPosition;
        //    resource.gameObject.SetActive(true);
        //}

        public Presenter GetObject()
        {
            return _pool.Get();
        }

        private Presenter Create(Presenter prefab)
        {
            var obj = _createEnemy(prefab);
            obj.transform.SetParent(_parent);

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
            obj.transform.SetParent(_parent);
            _pool.Return(obj);
            //Collected?.Invoke();
        }
    }
}