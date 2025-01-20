using UnityEngine;

namespace Asteroids
{
    internal class ShipModel : Transformable
    {
        public BaseGun FirstGun { get; private set; }
        public BaseGun SecondGun { get; private set; }

        public ShipModel(Vector2 position, float rotationAngle, ProjectilePresenter prefab) : base(position, rotationAngle)
        {
            FirstGun = new DefaultGun(prefab);   // Передать пулл с проджектайлами или создать его внутри?
            SecondGun = new LaserGun(prefab);
        }
    }
}