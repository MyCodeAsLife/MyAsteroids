using UnityEngine;

namespace Asteroids
{
    public abstract class TransformableTest
    {
        private float _directionOfRotation;
        private float _degreesPerSecond;
        private float _rotationAngle;
        private float _movementSpeed;
        private float _maxMovementSpeed;

        public Vector2 Position { get; set; }
        public Vector2 Direction { get; set; }          // Используется только в Asteroid
        public Vector2 Forward => Quaternion.Euler(0, 0, _rotationAngle) * Vector3.up;

        public float MovementSpeed
        {
            get
            {
                return _movementSpeed;
            }
            set
            {
                if (value > 0)
                    _movementSpeed = value;
            }
        }                   // Перенесено из модели

        public float MaxMovementSpeed
        {
            get
            {
                return _maxMovementSpeed;
            }
            set
            {
                if (value > 0)
                    _maxMovementSpeed = value;
            }
        }

        public float RotationAngle
        {
            get
            {
                return _rotationAngle;
            }
            set
            {
                _rotationAngle = Mathf.Repeat(value, 360);
            }
        }

        public float DegreesPerSecond
        {
            get
            {
                return _degreesPerSecond;
            }
            set
            {
                if (value > 0)
                    _degreesPerSecond = value;
            }
        }

        public float DirectionOfRotation
        {
            get
            {
                return _directionOfRotation;
            }
            set
            {
                _directionOfRotation = value > 0 ? 1 : -1;
            }
        }
    }
}