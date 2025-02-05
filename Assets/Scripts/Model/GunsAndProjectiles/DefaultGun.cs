using System;
using UnityEngine;

namespace Asteroids
{
    internal class DefaultGun : BaseGun
    {
        private PresentersFactory _factory;    // Передать через интерфейс?

        public DefaultGun(ShipPresenter ship, float cooldown = Config.CooldownDefaultGun) : base(ship, cooldown) { }

        public override void Tick(float deltatime)
        {
            Timer += deltatime;

            if (IsPressShooting && Config.CooldownDefaultGun < Timer)
                Shooting();
        }

        public void SetFactory(PresentersFactory factory) => _factory = factory;

        protected override void Shooting()
        {
            Timer = 0f;
            Interactive projectile = (Interactive)_factory.GetObject(GameObjectType.Bullet);
            projectile.transform.localScale = new Vector3(Config.BulletSize, Config.BulletSize);
            projectile.SetRotationAngle(Ship.GetAngleRotation());
            projectile.SetPosition(Ship.GetPosition());
            projectile.SetMovementSpeed(Config.BulletSpeed);
            Vector2 Forward = Quaternion.Euler(0, 0, Ship.GetAngleRotation()) * Vector3.up;
            projectile.SetDirectionMovement(Forward);
        }
    }
}