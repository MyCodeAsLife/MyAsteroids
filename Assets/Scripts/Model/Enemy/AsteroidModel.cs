namespace Asteroids
{
    public class AsteroidModel : EnemyModel     // ”празднить все модел кроме Enemy?
    {
        // —тартовое положение(за пределами экрана), движение и вращение задавать при получении объекта из пула

        public AsteroidModel(int cost = Config.AsteroidCost) : base(cost) { }

        // ”ничтожение будет контролировать спавнер, провер€€ не вылетел ли астероид за границы экрана.
        // »ли зациклить астероид как и корабль игрока?
    }
}