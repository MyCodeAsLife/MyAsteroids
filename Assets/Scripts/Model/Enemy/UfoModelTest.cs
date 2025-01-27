namespace Asteroids
{
    public class UfoModelTest : EnemyModelTest
    {
        // Стартовое положение(за пределами экрана), движение и вращение задавать при получении объекта из пула
        public UfoModelTest(int cost = Config.UfoCost) : base(cost) { }
    }
}