using UnityEngine;

namespace Asteroids
{
    public abstract class Transformable
    {
        public readonly float DegreesPerSecond = 180;               // Получать в конструкторе

        public float Direction { get; set; }
        public Vector2 Position { get; private set; }
        public float RotationAngle { get; private set; }
        public Vector2 Forward => Quaternion.Euler(0, 0, RotationAngle) * Vector3.up;

        public Transformable(Vector2 position, float rotation)
        {
            Position = position;
            RotationAngle = rotation;
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