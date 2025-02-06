namespace Asteroids
{
    internal static class Config
    {
        public const string PlayerLayerName = "Player";
        public const string EnemyLayerName = "Enemy";

        public const float PlayerShipMovementSpeed = 0.003f;
        public const float PlayerShipMaxMovementSpeed = 0.003f;
        public const float PlayerRotationSpeed = 180f;
        public const float UfoMinSpeed = 0.001f;
        public const float UfoMaxSpeed = 0.001f;
        public const float UfoMaxRotationSpeed = 300f;
        public const float UfoMinRotationSpeed = 100f;
        public const float AsteroidMaxSpeed = 0.004f;
        public const float AsteroidMinSpeed = 0.0002f;
        public const float AsteroidMaxRotationSpeed = 500f;
        public const float AsteroidMinRotationSpeed = 10f;
        public const float CooldownDefaultGun = 0.1f;
        public const float BulletSpeed = 0.01f;
        public const float CooldownLaserGun = 2f;
        public const float LaserGunChargingTime = 10f;
        public const float LaserGunShotDuration = 10f;
        public const int MaxNumberOfLaserCharges = 10;

        public const float UfoSpawnInterval = 2f;               // Реализовать
        public const float AsteroidSpawnInterval = 2f;
        public const float AsteroidPartSpawnInterval = 2f;      // Реализовать

        public const float BulletSize = 20f;
        public const float UfoMinSize = 6f;
        public const float UfoMaxSize = 10f;
        public const float AsteroidMinSize = 6.5f;
        public const float AsteroidMaxSize = 9f;
        public const float AsteroidPartMinSize = 8.5f;
        public const float AsteroidPartMaxSize = 10f;

        public const int AsteroidPartMinNumberOfFragments = 0;
        public const int AsteroidPartMaxNumberOfFragments = 4;

        public const float ScaleWindowSize = 1.03f;
        public const float MaxBoundaryExistenceObjects = 1.2f;
        public const float MinBoundaryExistenceObjects = -0.2f;

        public const int UfoCost = 10;
        public const int AsteroidCost = 5;
        public const int AsteroidParthCost = 1;
    }
}