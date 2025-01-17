using UnityEngine;

namespace Asteroids
{
    public class UfoModel : EnemyModel
    {
        // Стартовое положение(за пределами экрана), движение и вращение задавать при получении объекта из пула
        public UfoModel(Vector2 position, float rotation) : base(position, rotation, Config.UfoCost) { }
    }
}