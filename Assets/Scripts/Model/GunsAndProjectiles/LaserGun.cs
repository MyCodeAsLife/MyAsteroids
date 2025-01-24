namespace Asteroids
{
    internal class LaserGun : BaseGun
    {
        public LaserGun(float cooldown = Config.LaserCooldown) : base(cooldown) { }
    }
}
