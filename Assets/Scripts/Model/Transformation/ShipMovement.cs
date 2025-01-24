using System;
using UnityEngine;

namespace Asteroids
{
    internal class ShipMovement : Movement
    {
        private InertiaSimulator _inertiaSimulator;

        public event Action<Vector2, float> Movement;
        public event Action<float> Rotation;

        public ShipMovement(Transformable ship/*, Vector2 displaySize*/) : base(ship/*, displaySize*/)
        {
            _inertiaSimulator = new InertiaSimulator();
        }

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
            Model.DirectionOfRotation = direction > 0 ? 1 : -1;
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

            Model.DirectionOfRotation = Model.DirectionOfRotation > 0 ? 1 : -1;
            float delta = (Model.DirectionOfRotation * deltaTime * Model.DegreesPerSecond) + Model.RotationAngle;
            base.Rotate(delta);
        }
    }
}