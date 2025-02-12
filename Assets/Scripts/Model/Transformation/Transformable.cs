using System;
using UnityEngine;

namespace Asteroids
{
    public abstract class Transformable
    {
        private Vector2 _position = new();
        private float _directionOfRotation;             // Используется только в Ship и Asteroid
        private float _degreesPerSecond;                // Используется только в Ship и Ufo
        private float _rotationAngle;                   //Используется только в Ship и Asteroid,
        private float _movementSpeed;
        private float _maxMovementSpeed;

        public event Action<Vector2> PositionChanged;
        public event Action<float> RotationChanged;
        public event Action<float> SpeedChanged;

        //public SingleReactiveProperty<Vector2> Position = new();
        public Vector2 DirectionMovement { get; set; }          // Используется только в Asteroid, вынести в Asteroid?

        public float MovementSpeed
        {
            get
            {
                return _movementSpeed;
            }
            set
            {
                if (value > 0)
                {
                    _movementSpeed = value;
                    SpeedChanged?.Invoke(value);
                }
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

        public Vector2 Position
        {
            get
            {
                return _position;
            }

            set
            {
                _position = value;
                PositionChanged?.Invoke(value);
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
                RotationChanged?.Invoke(_rotationAngle);
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

        public Vector2 Forward => Quaternion.Euler(0, 0, _rotationAngle) * Vector3.up;
    }
}