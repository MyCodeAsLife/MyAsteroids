using UnityEngine;

namespace Asteroids
{
    internal abstract class BaseGun
    {
        protected readonly ShipPresenter Ship;

        protected PresentersFactory _factory;    // Передать через интерфейс?
        protected bool _isShooting = false;
        protected float _cooldown;
        protected float _time = Config.DefaultGunCooldown;

        //public virtual event Action<Presenter> Shot;

        public BaseGun(PresentersFactory factory, ShipPresenter ship, float cooldown)
        {
            _factory = factory;
            _cooldown = cooldown;
            _time = cooldown;
            Ship = ship;
        }

        public void OnShootingStart()      // Убрать, сделать через ивент
        {
            _isShooting = true;
        }

        public void OnShootingCancel()
        {
            _isShooting = false;
        }

        public virtual void Tick(float deltatime)           // Если в ship сделать эвент для тиков, то _isShooting можно убрать
        {
            Debug.Log("Tick");
        }
    }
}
