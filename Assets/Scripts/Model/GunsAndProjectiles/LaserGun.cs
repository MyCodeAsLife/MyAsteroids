namespace Asteroids
{
    internal class LaserGun : BaseGun
    {
        public LaserGun(ProjectilePresenter prefab, float cooldown = Config.LaserCooldown) : base(cooldown, prefab) { }
    }
}
