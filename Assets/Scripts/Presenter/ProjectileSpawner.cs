using System;
using UnityEngine;

namespace Asteroids
{
    public class ProjectileSpawner
    {
        private readonly Func<ProjectileView, ProjectileView> _createEnemy;
        private readonly ProjectileView _prefab;
        private readonly Transform _parent;

        private ObjectPool<ProjectileView> _pool;

        public event Action Collected;

        public ProjectileSpawner(ProjectileView prefab, Func<ProjectileView, ProjectileView> createFunc, Transform parent)
        {
            _prefab = prefab;
            _createEnemy = createFunc;
            _parent = parent;
            _pool = new ObjectPool<ProjectileView>(_prefab, Create, Enable, Disable);
        }

        ~ProjectileSpawner()
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

        private ProjectileView Create(ProjectileView prefab)
        {
            var obj = _createEnemy(prefab);
            obj.transform.SetParent(_parent);

            return obj;
        }

        private void Enable(ProjectileView obj)
        {
            obj.gameObject.SetActive(true);
            //obj.Harvest += OnResourceHarvest;
            obj.transform.rotation = Quaternion.identity;
            obj.transform.position = Vector3.zero;
        }

        private void Disable(ProjectileView obj)
        {
            //obj.Harvest -= OnResourceHarvest;
            obj.gameObject.SetActive(false);
        }

        private void OnResourceHarvest(ProjectileView resource)
        {
            resource.transform.SetParent(_parent);
            _pool.Return(resource);
            Collected?.Invoke();
        }
    }
}