using System;
using UnityEngine;

namespace Asteroids
{
    internal class ShipMovementTest : MovementTest
    {
        private InertiaSimulator _inertiaSimulator = new();

        public event Action<Vector2, float> Movement;
        public event Action<float> Rotation;

        //public ShipMovementTest(Transformable ship) : base(ship)
        //{
        //    _inertiaSimulator = new InertiaSimulator();
        //}

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
            Model.Direction = direction > 0 ? 1 : -1;
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
            if (Model.Direction == 0)
                throw new InvalidOperationException(nameof(Model.Direction));

            Model.Direction = Model.Direction > 0 ? 1 : -1;
            float delta = (Model.Direction * deltaTime * Model.DegreesPerSecond) + Model.RotationAngle;
            base.Rotate(delta);
        }
    }
}