namespace Asteroids
{
    internal class ShipModel : Transformable
    {
        public BaseGun FirstGun { get; private set; }
        public BaseGun SecondGun { get; private set; }

        public ShipModel(ShipPresenter ship, LaserPresenter laser)  // Попробовать лазер вынести в отдельный префаб и инстанциировать
        {
            FirstGun = new DefaultGun(ship);
            SecondGun = new LaserGun(ship, laser);
        }
    }
}