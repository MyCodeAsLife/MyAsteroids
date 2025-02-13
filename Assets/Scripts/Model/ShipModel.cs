using System;
using UnityEngine;

namespace Asteroids
{
    internal class ShipModel : Transformable
    {
        public Vector2 LastPosition = Vector2.zero;               // Используется только в Ship

        private float _speed;

        public event Action<float> SpeedChanged;

        public BaseGun FirstGun { get; private set; }
        public BaseGun SecondGun { get; private set; }

        public float Speed                                      // Позиция форматирования?????
        {
            get
            {
                return _speed;
            }

            set
            {
                if (value > 0 && value != _speed)
                {
                    _speed = value;
                    SpeedChanged?.Invoke(value);
                }
            }
        }

        public ShipModel(ShipPresenter ship, LaserPresenter laser)  // Попробовать лазер вынести в отдельный префаб и инстанциировать
        {
            FirstGun = new DefaultGun(ship);
            SecondGun = new LaserGun(ship, laser);
        }
    }
}