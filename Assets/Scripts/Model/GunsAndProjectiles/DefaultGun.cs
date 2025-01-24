namespace Asteroids
{
    internal class DefaultGun : BaseGun
    {
        public DefaultGun(float cooldown = Config.DefaultGunCooldown) : base(cooldown) { }
    }
}