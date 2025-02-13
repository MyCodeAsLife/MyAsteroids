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
            Updated += OnUpdated;
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

        private float CalculateSpeed(Vector2 lastPosition, Vector2 currentPosition, float deltaTime)
        {
            float deltaX = Mathf.Pow(currentPosition.x - lastPosition.x, 2f);
            float deltaY = Mathf.Pow(currentPosition.y - lastPosition.y, 2f);
            return Mathf.Sqrt(deltaX + deltaY) / deltaTime;
        }

        private void Move()
        {
            ((ShipModel)Model).LastPosition = Model.Position;
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

        private void OnUpdated(float deltaTime)
        {
            Rotation?.Invoke(deltaTime);
            Movement?.Invoke(Model.Forward, deltaTime);
            Move();
            _inertiaSimulator.SlowDown(deltaTime);
            ((ShipModel)Model).Speed = CalculateSpeed(((ShipModel)Model).LastPosition, Model.Position, deltaTime);
        }
    }
}