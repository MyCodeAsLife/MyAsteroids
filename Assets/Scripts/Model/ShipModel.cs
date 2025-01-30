namespace Asteroids
{
    internal class ShipModel : Transformable
    {
        public BaseGun FirstGun { get; private set; }
        public BaseGun SecondGun { get; private set; }

        public ShipModel(PresentersFactory factory, ShipPresenter ship, LaserPresenter laser)
        {
            FirstGun = new DefaultGun(factory, ship);
            SecondGun = new LaserGun(ship, laser);
        }
    }
}