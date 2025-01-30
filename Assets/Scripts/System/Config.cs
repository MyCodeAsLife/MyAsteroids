namespace Asteroids
{
    internal static class Config
    {
        public const string PlayerLayerName = "Player";
        public const string EnemyLayerName = "Enemy";

        public const float PlayerShipMovementSpeed = 0.003f;
        public const float PlayerShipMaxMovementSpeed = 0.003f;
        public const float PlayerRotationSpeed = 180f;
        public const float UfoMinSpeed = 0.003f;
        public const float UfoMaxSpeed = 0.0003f;
        public const float UfoMaxRotationSpeed = 300f;
        public const float UfoMinRotationSpeed = 30f;
        public const float AsteroidMaxSpeed = 0.004f;
        public const float AsteroidMinSpeed = 0.0002f;
        public const float AsteroidMaxRotationSpeed = 500f;
        public const float AsteroidMinRotationSpeed = 10f;
        public const float CooldownDefaultGun = 0.3f;
        public const float BulletSpeed = 0.01f;
        public const float CooldownLaserGun = 2f;
        public const float LaserGunChargingTime = 10f;
        public const float LaserGunShotDuration = 0.5f;
        public const int MaxNumberOfLaserCharges = 2;

        public const float BulletScale = 20f;
        public const float UfoScale = 8f;                       // Реализовать
        public const float AsteroidScale = 8f;                  // Реализовать
        public const float AsteroidPartScale = 8f;              // Реализовать

        public const float ScaleWindowSize = 1.02f;
        public const float BoundaryExistenceObjects = 1.04f;

        public const int UfoCost = 10;
        public const int AsteroidCost = 5;
        public const int AsteroidParthCost = 1;
    }
}