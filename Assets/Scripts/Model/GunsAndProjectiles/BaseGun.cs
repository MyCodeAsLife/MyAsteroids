using System;

namespace Asteroids
{
    internal abstract class BaseGun
    {
        private ProjectilePoolModel _projectiles = new();
        private bool _isShooting = false;
        private float _time = Config.DefaultGunCooldown;

        public event Action<Projectile> Shot;

        public void OnShootingCancel()
        {
            _isShooting = false;
        }

        public void OnShootingStart()
        {
            _isShooting = true;
        }

        public void Tick(float deltatime)           // Если в ship сделать эвент для тиков, то _isShooting можно убрать
        {
            _time += deltatime;

            if (_isShooting && Config.DefaultGunCooldown < _time)
            {
                Shot?.Invoke(_projectiles.Get());
                _time = 0f;
            }
        }
    }
}
