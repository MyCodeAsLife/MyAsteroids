namespace Asteroids
{
    public abstract class EnemyModelTest : TransformableTest
    {
        public readonly int Cost;

        // При своем уничтожении, свою стоимость прибавлять к обсчему счету.
        // Наверно это будет делать спавнер врагов

        public EnemyModelTest(int cost)
        {
            Cost = cost;
        }
    }
}