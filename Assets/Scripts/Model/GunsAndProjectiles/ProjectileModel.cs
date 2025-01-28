namespace Asteroids
{
    internal class ProjectileModel : Transformable      // Нужен ли ProjectileModel?  Удалить
    {
        //protected float LifeTime;     // Если положение будет контролить спавнер, то время жизни ненужно
        //protected float Time;

        // Уничтожение будет контролировать спавнер, проверяя не вылетел ли объект за границы экрана.
    }
}
