using System;
using UnityEngine;

namespace Asteroids
{
    internal class ShipMovementTest : ModelMovementTest
    {
        public readonly InertiaSimulatorTest _inertiaSimulator = new();

        public event Action<Vector2, float> Movement;
        public event Action<float> Rotation;

        public override void Tick(float deltaTime)
        {
            Rotation?.Invoke(deltaTime);
            Movement?.Invoke(Model.Forward, deltaTime);
            Move(_inertiaSimulator.Acceleration);
            _inertiaSimulator.SlowDown(deltaTime);
        }

        public void OnMovementStart()
        {
            Movement += _inertiaSimulator.Accelerate;
        }

        public void OnMovementCancel()
        {
            Movement -= _inertiaSimulator.Accelerate;
        }

        public void OnRotationStart(float direction)
        {
            Model.DirectionOfRotation = direction;
            Rotation += Rotate;
        }

        public void OnRotationCancel()
        {
            Rotation -= Rotate;
        }

        protected override void Move(Vector2 position)
        {
            var nextPosition = Model.Position + SpeedCorrectionRelativeScreenSize(position);
            nextPosition.x = Mathf.Repeat(nextPosition.x, Config.ScaleWindowSize);
            nextPosition.y = Mathf.Repeat(nextPosition.y, Config.ScaleWindowSize);
            base.Move(nextPosition);
        }

        protected override void Rotate(float deltaTime)
        {
            if (Model.DirectionOfRotation == 0)
                throw new InvalidOperationException(nameof(Model.DirectionOfRotation));

            float delta = (Model.DirectionOfRotation * deltaTime * Model.DegreesPerSecond) + Model.RotationAngle;
            base.Rotate(delta);
        }
    }
}