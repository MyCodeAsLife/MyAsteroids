using System.Collections.Generic;

namespace Asteroids
{
    internal class ProjectilePoolModel
    {
        private Queue<Projectile> _pool = new();
        private List<Projectile> _active = new();

        public ProjectilePoolModel()
        {
            Return(CreateObject());
        }

        public int NumberOfActiveObjects => _active.Count;

        public Projectile Get()
        {
            Projectile obj = _pool.Count > 0 ? _pool.Dequeue() : CreateObject();
            _active.Add(obj);
            return obj;
        }

        public void Return(Projectile obj)
        {
            _active.Remove(obj);
            _pool.Enqueue(obj);
        }

        public void ReturnAll()
        {
            foreach (Projectile obj in _active.ToArray())
                Return(obj);
        }

        private Projectile CreateObject()
        {
            return new Projectile();
        }
    }
}