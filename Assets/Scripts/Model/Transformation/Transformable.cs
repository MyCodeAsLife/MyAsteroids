using UnityEngine;

namespace Asteroids
{
    public abstract class Transformable
    {
        private float _directionOfRotation;

        public Vector2 Position { get; private set; }
        public Vector2 Forward => Quaternion.Euler(0, 0, RotationAngle) * Vector3.up;
        public float DegreesPerSecond { get; private set; }
        public float RotationAngle { get; private set; }
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

        public Transformable(Vector2 position, float rotation)
        {
            Position = position;
            RotationAngle = rotation;
            DegreesPerSecond = 180;         // Скорость вращения
        }

        public void Rotate(float delta)
        {
            //RotationAngle = Mathf.Repeat(RotationAngle + delta, 360);
            RotationAngle = Mathf.Repeat(delta, 360);
        }

        public virtual void Move(Vector2 position)
        {
            Position = position;
        }
    }
}