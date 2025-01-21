using UnityEngine;

namespace Asteroids
{
    internal class InertiaSimulator
    {
        private readonly float _unitsPerSecond = 0.001f;
        private readonly float _maxSpeed = 0.001f;
        private readonly float _secondToStop = 1f;

        public Vector2 Acceleration { get; private set; }

        public void Accelerate(Vector2 direction, float deltaTime)
        {
            Acceleration += direction * (_unitsPerSecond * deltaTime);
            Acceleration = Vector2.ClampMagnitude(Acceleration, _maxSpeed);
        }

        public void SlowDown(float deltaTime)
        {
            Acceleration -= Acceleration * (deltaTime / _secondToStop);
        }
    }
}
