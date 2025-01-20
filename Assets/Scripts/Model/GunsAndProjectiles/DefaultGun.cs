namespace Asteroids
{
    internal class DefaultGun : BaseGun
    {
        public DefaultGun(ProjectilePresenter prefab, float cooldown = Config.DefaultGunCooldown) : base(cooldown, prefab) { }
    }
}