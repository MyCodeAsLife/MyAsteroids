namespace Asteroids
{
    internal class ShipModelTest : Transformable
    {
        public BaseGun FirstGun { get; private set; }
        public BaseGun SecondGun { get; private set; }

        public ShipModelTest()
        {
            FirstGun = new DefaultGun();   // Пули будут братся из фабрики
            SecondGun = new LaserGun();    // Лазер тоже из фабрики?
        }
    }
}