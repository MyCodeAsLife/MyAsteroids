using System;

namespace Asteroids
{
    internal abstract class BaseGun
    {
        protected readonly ShipPresenter Ship;
        protected readonly float FireRate;

        protected bool IsPressShooting = false;
        protected float Timer;

        public abstract event Action Shot;

        public BaseGun(ShipPresenter ship, float cooldown)
        {
            Ship = ship;
            FireRate = cooldown;
            Timer = cooldown;
        }

        public void OnShootingStart() => IsPressShooting = true;
        public void OnShootingCancel() => IsPressShooting = false;
        public abstract void Tick(float deltatime);
        protected abstract void Shooting();
    }
}
