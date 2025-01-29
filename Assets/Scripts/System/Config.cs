public static class Config
{
    public const string PlayerLayer = "Player";
    public const string EnemyLayer = "Enemy";

    public const float PlayerShipSpeed = 0.003f;
    public const float PlayerRotationSpeed = 180f;
    public const float UfoMinSpeed = 0.0005f;
    public const float UfoMaxSpeed = 0.003f;
    public const float UfoMaxRotationSpeed = 300f;
    public const float UfoMinRotationSpeed = 30f;
    public const float AsteroidMaxSpeed = 0.005f;
    public const float AsteroidMinSpeed = 0.002f;
    public const float AsteroidMaxRotationSpeed = 500f;
    public const float AsteroidMinRotationSpeed = 10f;
    public const float LaserGunCooldown = 3f;
    public const float DefaultGunCooldown = 0.3f;
    public const float BulletSpeed = 0.01f;

    public const float BulletScale = 20f;
    //public const float LaserScaleX = 26f;
    //public const float LaserScaleY = 5000f;
    public const float UfoScale = 8f;
    public const float AsteroidScale = 8f;
    public const float AsteroidPartScale = 8f;

    public const float ScaleWindowSize = 1.02f;
    public const float BoundaryExistenceObjects = 1.04f;

    public const int UfoCost = 10;
    public const int AsteroidCost = 5;
    public const int AsteroidParthCost = 1;
}
