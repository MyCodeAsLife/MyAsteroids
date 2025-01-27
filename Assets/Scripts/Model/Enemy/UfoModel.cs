namespace Asteroids
{
    public class UfoModel : EnemyModel
    {
        // Стартовое положение(за пределами экрана), движение и вращение задавать при получении объекта из пула
        public UfoModel(int cost = Config.UfoCost) : base(cost) { }
    }
}