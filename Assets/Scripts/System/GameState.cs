namespace Asteroids
{
    internal static class GameState
    {
        public static SingleReactiveProperty<bool> IsPaused = new();
    }
}