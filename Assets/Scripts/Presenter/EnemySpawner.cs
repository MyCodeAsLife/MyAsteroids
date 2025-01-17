using System;
using UnityEngine;

namespace Asteroids
{
    public class EnemySpawner : MonoBehaviour
    {
        private readonly Func<UfoPresenter, UfoPresenter> _createEnemy;
        private readonly UfoPresenter _prefab;
        private readonly Transform _parent;

        private ObjectPool<UfoPresenter> _pool;

        public event Action Collected;

        public EnemySpawner(UfoPresenter prefab, Func<UfoPresenter, UfoPresenter> createFunc, Transform parent)
        {
            _prefab = prefab;
            _createEnemy = createFunc;
            _parent = parent;
            _pool = new ObjectPool<UfoPresenter>(_prefab, Create, Enable, Disable);
        }

        ~EnemySpawner()
        {
            RemoveResourcesFromMap();
        }

        public int NumberOfActiveResources => _pool.ActiveResourcesCount;

        public void RemoveResourcesFromMap()
        {
            _pool.ReturnAll();
        }

        public void Spawn(Vector3 spawnPosition, int id)
        {
            var resource = _pool.Get();
            //resource.SetID(id);
            resource.transform.position = spawnPosition;
            resource.gameObject.SetActive(true);
        }

        private UfoPresenter Create(UfoPresenter prefab)
        {
            var obj = _createEnemy(prefab);
            obj.transform.SetParent(_parent);

            return obj;
        }

        private void Enable(UfoPresenter obj)
        {
            obj.gameObject.SetActive(true);
            //obj.Harvest += OnResourceHarvest;
            obj.transform.rotation = Quaternion.identity;
            obj.transform.position = Vector3.zero;
        }

        private void Disable(UfoPresenter obj)
        {
            //obj.Harvest -= OnResourceHarvest;
            obj.gameObject.SetActive(false);
        }

        private void OnResourceHarvest(UfoPresenter resource)
        {
            resource.transform.SetParent(_parent);
            _pool.Return(resource);
            Collected?.Invoke();
        }
    }
}