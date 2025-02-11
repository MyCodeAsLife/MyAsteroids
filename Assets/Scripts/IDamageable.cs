using System;

namespace Asteroids
{
    public interface IDamageable
    {
        public event Action<Interactive> Destroyed;

        public void TakeDamage();
    }
}