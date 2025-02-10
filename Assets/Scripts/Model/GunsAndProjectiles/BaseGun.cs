using System;

namespace Asteroids
{
    internal abstract class BaseGun
    {
        protected readonly ShipPresenter Ship;
        protected readonly float FireRate;

        protected bool IsPressShooting = false;
        protected float Timer;

        public event Action Shot;

        public BaseGun(ShipPresenter ship, float cooldown)
        {
            Ship = ship;
            FireRate = cooldown;
            Timer = cooldown;
        }

        public void OnShootingStart() => IsPressShooting = true;
        public void OnShootingCancel() => IsPressShooting = false;
        protected virtual void Shooting() => Shot?.Invoke();

        public abstract void Tick(float deltatime);
    }
}
