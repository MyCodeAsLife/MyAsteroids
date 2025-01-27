namespace Asteroids
{
    internal class ShipModel : Transformable
    {
        public BaseGun FirstGun { get; private set; }
        public BaseGun SecondGun { get; private set; }

        public ShipModel()
        {
            FirstGun = new DefaultGun();   // Передать пулл с проджектайлами или создать его внутри?
            SecondGun = new LaserGun();
        }
    }
}