using UnityEngine;

namespace Asteroids
{
    public abstract class Movement
    {
        protected readonly Transformable Model;

        private readonly float _xRatio;
        private readonly float _yRatio;

        public float MovementSpeed { get; private set; }
        public float RotationSpeed { get; private set; }
        public Vector2 Direction { get; private set; }
        public float DirectionOfRotation { get; private set; }

        public Movement(Transformable model, Vector2 displaySize)
        {
            Model = model;
            float ratio = displaySize.x / displaySize.y;
            _xRatio = ratio < 1 ? ratio : 1;
            _yRatio = ratio > 1 ? ratio : 1;
        }

        public abstract void Tick(float deltaTime);

        public virtual void Move(Vector2 position)
        {
            Model.Move(position);
        }

        public virtual void Rotate(float delta)
        {
            Model.Rotate(delta);
        }

        public Vector2 SpeedCorrectionRelativeScreenSize(Vector2 position)
        {
            position.x *= _xRatio;
            position.y *= _yRatio;
            return position;
        }

        public void SetMovementSpeed(float speed)
        {
            MovementSpeed = speed;
        }

        public void SetRotationSpeed(float speed)
        {
            RotationSpeed = speed;
        }

        public void SetDirectionOfMovement(Vector2 direction)
        {
            Direction = direction;
        }

        public void SetDirectionOfRotation(float direction)     // ship тоже получает направление вращения, подкорректировать его
        {
            DirectionOfRotation = direction;
        }
    }
}