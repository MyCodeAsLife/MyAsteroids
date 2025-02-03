using System;
using UnityEngine;

namespace Asteroids
{
    internal class DefaultGun : BaseGun
    {
        private readonly PresentersFactory _factory;    // Передать через интерфейс?

        public override event Action Shot;

        public DefaultGun(PresentersFactory factory, ShipPresenter ship, float cooldown = Config.CooldownDefaultGun) : base(ship, cooldown)
        {
            _factory = factory;
        }

        public override void Tick(float deltatime)
        {
            Timer += deltatime;

            if (IsPressShooting && Config.CooldownDefaultGun < Timer)
                Shooting();
        }

        protected override void Shooting()
        {
            Shot?.Invoke();
            Timer = 0f;
            Presenter projectile = _factory.GetObject(GameObjectType.Bullet);
            projectile.transform.localScale = new Vector3(Config.BulletSize, Config.BulletSize);
            projectile.SetRotationAngle(Ship.GetAngleRotation());
            projectile.SetPosition(Ship.GetPosition());
            projectile.SetMovementSpeed(Config.BulletSpeed);
            Vector2 Forward = Quaternion.Euler(0, 0, Ship.GetAngleRotation()) * Vector3.up;
            projectile.SetDirectionMovement(Forward);
        }
    }
}