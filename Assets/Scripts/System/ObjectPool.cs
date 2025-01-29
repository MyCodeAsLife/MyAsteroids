namespace Asteroids
{
    using System;
    using System.Collections.Generic;

    public class ObjectPool<T>
    {
        private readonly T _environments;
        private readonly GameObjectType _objectType;

        private readonly Func<GameObjectType, T, T> _createObject;
        private readonly Action<T> _disableObject;
        private readonly Action<T> _enableObject;

        private Queue<T> _pool = new();
        private List<T> _active = new();

        public ObjectPool(GameObjectType objectType, T environments, Func<GameObjectType, T, T> createObject, Action<T> enableObject, Action<T> disableObject)
        {
            _objectType = objectType;
            _environments = environments;
            _createObject = createObject;
            _enableObject = enableObject;
            _disableObject = disableObject;

            Return(_createObject(_objectType, _environments));
        }

        public int ActiveResourcesCount => _active.Count;

        public T Get()
        {
            T obj = _pool.Count > 0 ? _pool.Dequeue() : _createObject(_objectType, _environments);
            _enableObject(obj);
            _active.Add(obj);
            return obj;
        }

        public void Return(T obj)
        {
            _disableObject(obj);
            _active.Remove(obj);
            _pool.Enqueue(obj);
        }

        public void ReturnAll()
        {
            foreach (T obj in _active.ToArray())
                Return(obj);
        }
    }
}