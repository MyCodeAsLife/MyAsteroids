using System;

namespace Asteroids
{
    public class AsteroidModelTest : EnemyModelTest
    {
        // Стартовое положение(за пределами экрана), движение и вращение задавать при получении объекта из пула

        public AsteroidModelTest(int cost = Config.AsteroidCost) : base(cost) { }

        // Уничтожение будет контролировать спавнер, проверяя не вылетел ли астероид за границы экрана.
        // Или зациклить астероид как и корабль игрока?
        public event Action<EnemyModelTest> Destroyed;

        public void Destroy()
        {
            Destroyed?.Invoke(this);
        }
    }
}