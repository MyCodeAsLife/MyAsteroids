using UnityEngine;

namespace Asteroids
{
    public abstract class EnemyModel : Transformable
    {
        public readonly int Cost;

        // При своем уничтожении, свою стоимость прибавлять к обсчему счету.
        // Наверно это будет делать спавнер врагов

        public EnemyModel(Vector2 position, float rotation, int cost) : base(position, rotation)
        {
            Cost = cost;
        }
    }
}