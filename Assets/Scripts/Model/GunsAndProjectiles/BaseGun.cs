using System;
using UnityEngine;

namespace Asteroids
{
    internal abstract class BaseGun
    {
        private ProjectilePresenter _prefab;
        //private ProjectilePoolModel _projectiles = new();
        private bool _isShooting = false;
        private float _cooldown;
        private float _time = Config.DefaultGunCooldown;

        //public event Action<ProjectilePresenter> Shot;

        public BaseGun(float cooldown, ProjectilePresenter prefab)
        {
            _cooldown = cooldown;
            _time = cooldown;
            _prefab = prefab;
        }

        public void OnShootingStart()
        {
            _isShooting = true;
        }

        public void OnShootingCancel()
        {
            _isShooting = false;
        }

        public void Tick(float deltatime)           // Если в ship сделать эвент для тиков, то _isShooting можно убрать
        {
            _time += deltatime;

            if (_isShooting && Config.DefaultGunCooldown < _time)
            {
                Debug.Log("Shot");                                              //++++++++++++++++++++++++++

                //Instant 
                //ProjectilePresenter projectile =
                //Shot?.Invoke(projectile);

                //Shot?.Invoke(_projectiles.Get());
                _time = 0f;
            }
        }
    }
}
