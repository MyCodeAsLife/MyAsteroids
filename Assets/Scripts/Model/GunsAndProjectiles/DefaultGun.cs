using UnityEngine;

namespace Asteroids
{
    internal class DefaultGun : BaseGun
    {
        public DefaultGun(PresentersFactory factory, ShipPresenter ship, float cooldown = Config.DefaultGunCooldown) : base(factory, ship, cooldown) { }

        public override void Tick(float deltatime)
        {
            _time += deltatime;

            if (_isShooting && Config.DefaultGunCooldown < _time)
            {
                Presenter bullet = _factory.GetObject(GameObjectType.Bullet);
                OnShot(bullet);
                _time = 0f;
            }
        }

        private void OnShot(Presenter projectile)
        {
            projectile.transform.localScale = new Vector3(20f, 20f, 20f);
            projectile.SetRotationAngle(Ship.GetAngleRotation());
            projectile.SetPosition(Ship.GetPosition());
            projectile.SetMovementSpeed(0.01f);
            projectile.SetMaxMovementSpeed(0.01f);
            Vector2 Forward = Quaternion.Euler(0, 0, Ship.GetAngleRotation()) * Vector3.up;
            projectile.SetDirection(Forward);                // Поменять модель движения? иначе без направления не движется
        }
    }
}