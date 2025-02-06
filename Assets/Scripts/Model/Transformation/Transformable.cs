using UnityEngine;

namespace Asteroids
{
    public abstract class Transformable
    {
        private float _directionOfRotation;             // ������������ ������ � Ship � Asteroid
        private float _degreesPerSecond;                // ������������ ������ � Ship � Ufo
        private float _rotationAngle;                   //������������ ������ � Ship � Asteroid,
        private float _movementSpeed;
        private float _maxMovementSpeed;

        public SingleReactiveProperty<Vector2> Position = new();
        public Vector2 DirectionMovement { get; set; }          // ������������ ������ � Asteroid, ������� � Asteroid?
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
        }

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