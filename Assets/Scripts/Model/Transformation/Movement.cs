using UnityEngine;

namespace Asteroids
{
    public abstract class Movement
    {
        protected readonly Transformable Model;

        private float _xRatio;
        private float _yRatio;

        public float MovementSpeed { get; private set; }
        public float RotationSpeed { get; private set; }
        public Vector2 Direction { get; private set; }
        public float DirectionOfRotation { get; private set; }
        public Vector2 DisplaySize { get; private set; }

        public Movement(Transformable model/*, Vector2 displaySize*/)
        {
            Model = model;
        }

        public abstract void Tick(float deltaTime);

        public Vector2 SpeedCorrectionRelativeScreenSize(Vector2 position)
        {
            position.x *= _xRatio;
            position.y *= _yRatio;
            return position;
        }

        public void SetDisplaySize(Vector2 size)
        {
            DisplaySize = size;
            CalculateDisplayRatio();
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

        protected virtual void Move(Vector2 position)
        {
            Model.Move(position);
        }

        protected virtual void Rotate(float delta)
        {
            Model.Rotate(delta);
        }

        private void CalculateDisplayRatio()
        {
            float ratio = DisplaySize.x / DisplaySize.y;
            _xRatio = ratio < 1 ? ratio : 1;
            _yRatio = ratio > 1 ? ratio : 1;
        }
    }
}