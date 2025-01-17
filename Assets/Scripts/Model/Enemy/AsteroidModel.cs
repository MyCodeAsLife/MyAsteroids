using System;
using UnityEngine;

namespace Asteroids
{
    public class AsteroidModel : EnemyModel
    {
        // Стартовое положение(за пределами экрана), движение и вращение задавать при получении объекта из пула

        public AsteroidModel(Vector2 position, float rotation, int cost) : base(position, rotation, cost) { }

        // Уничтожение будет контролировать спавнер, проверяя не вылетел ли астероид за границы экрана.
        // Или зациклить астероид как и корабль игрока?
        public event Action<EnemyModel> Destroyed;

        public void Destroy()
        {
            Destroyed?.Invoke(this);
        }
    }
}