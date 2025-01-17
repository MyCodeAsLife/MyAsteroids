using System;
using UnityEngine;

namespace Asteroids
{
    internal class Projectile
    {
        private Movement _mover;
        private float _lifeTime;
        private float _time;

        public event Action<Projectile> Destroed;

        public void Initialize(Transform transform) // Получать трансформ от View пулла снарядов
        {
            if (transform == null)
                throw new NullReferenceException(nameof(transform));

            //_mover = new Movement(transform);
        }

        public void SetLifeTime(float lifeTime)
        {
            _lifeTime = lifeTime;
        }

        public void SetSpeed(float speed)
        {
            //_mover.SetSpeed(speed);
        }

        public void Tick(float deltaTime)
        {
            _time += deltaTime;

            if (_lifeTime < _time)
                Destroy();
        }

        private void Destroy()
        {
            _lifeTime = 0;
            _time = 0;
            Destroed?.Invoke(this);
        }
    }
}
