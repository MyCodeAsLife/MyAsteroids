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

        //public virtual event Action<Presenter> Shot;          // Переделать стельбу на ивент

        public BaseGun(PresentersFactory factory, ShipPresenter ship, float cooldown)       // Переделать (вынести фабрику и шип?)
        {
            _factory = factory;                 // Вынести в DefaultGun
            _cooldown = cooldown;
            _time = cooldown;
            Ship = ship;                        // Тоже вынести?
        }

        public void OnShootingStart()      // Убрать, сделать через ивент
        {
            _isShooting = true;
        }

        public void OnShootingCancel()
        {
            _isShooting = false;
        }

        public virtual void Tick(float deltatime)       // Переделать в абстрактный?
        {
            Debug.Log("Tick");
        }
    }
}
