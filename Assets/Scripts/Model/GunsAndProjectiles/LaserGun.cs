using UnityEngine;

namespace Asteroids
{
    internal class LaserGun : BaseGun
    {
        public LaserGun(PresentersFactory factory, ShipPresenter ship, float cooldown = Config.LaserCooldown) : base(factory, ship, cooldown) { }

        private void OnShot(Presenter projectile)
        {
            //projectile.transform.parent = Ship;
            //projectile.SetPosition(Ship.position);
            //projectile.SetMovementSpeed(0.001f);
            //projectile.SetMaxMovementSpeed(0.001f);
            projectile.SetDirection(new Vector2(-1, 1));
            projectile.gameObject.SetActive(true);
        }

        public override void Tick(float deltatime)
        {
            _time += deltatime;

            if (_isShooting && Config.DefaultGunCooldown < _time)
            {
                //Presenter bullet = _factory.GetObject("BulletPresenter");
                //OnShot(bullet);
                _time = 0f;
            }
        }
    }
}
