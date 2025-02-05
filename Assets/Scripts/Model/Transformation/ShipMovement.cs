using System;
using UnityEngine;

namespace Asteroids
{
    internal class ShipMovement : ModelMovement
    {
        public readonly InertiaSimulator _inertiaSimulator;

        public event Action<Vector2, float> Movement;
        public event Action<float> Rotation;

        public ShipMovement(Transformable model) : base(model)
        {
            _inertiaSimulator = new InertiaSimulator(model);
        }

        public override void Tick(float deltaTime)
        {
            Rotation?.Invoke(deltaTime);
            Movement?.Invoke(Model.Forward, deltaTime);
            Move();
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

        private void Move()
        {
            var nextPosition = Model.Position + SpeedCorrectionRelativeScreenSize(_inertiaSimulator.Acceleration);
            nextPosition.x = Mathf.Repeat(nextPosition.x, Config.ScaleWindowSize);
            nextPosition.y = Mathf.Repeat(nextPosition.y, Config.ScaleWindowSize);
            base.Move(nextPosition);
        }

        private new void Rotate(float deltaTime)     // Такая же как и у asteroid, опустить в ModelMovement?
        {
            if (Model.DirectionOfRotation == 0)
                throw new InvalidOperationException(nameof(Model.DirectionOfRotation));

            float delta = Model.RotationAngle + (Model.DegreesPerSecond * deltaTime * Model.DirectionOfRotation);
            base.Rotate(delta);
        }
    }
}