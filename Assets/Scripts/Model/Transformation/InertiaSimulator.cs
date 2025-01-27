using UnityEngine;

namespace Asteroids
{
    internal class InertiaSimulator
    {
        private readonly float _secondToStop = 1f;

        private Transformable _model;

        public InertiaSimulator(Transformable model)
        {
            _model = model;
        }

        public Vector2 Acceleration { get; private set; }

        public void Accelerate(Vector2 direction, float deltaTime)
        {
            Acceleration += direction * (_model.MovementSpeed * deltaTime);
            Acceleration = Vector2.ClampMagnitude(Acceleration, _model.MaxMovementSpeed);
        }

        public void SlowDown(float deltaTime)
        {
            Acceleration -= Acceleration * (deltaTime / _secondToStop);
        }
    }
}
