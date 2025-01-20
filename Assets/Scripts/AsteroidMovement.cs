using UnityEngine;
namespace Asteroids
{
    public class AsteroidMovement : Movement
    {
        private float _deltaTime;

        public AsteroidMovement(Transformable model, Vector2 displaySize) : base(model, displaySize) { }

        public override void Tick(float deltaTime)
        {
            _deltaTime = deltaTime;
            Move();               // Ненужная передача параметров
            Rotate(deltaTime);
        }

        private void Move()     // направление? следующая позиция?
        {
            Vector2 nextPosition = Model.Position + Direction * (MovementSpeed * _deltaTime);
            nextPosition = Vector2.ClampMagnitude(nextPosition, MovementSpeed);

            nextPosition = Model.Position + SpeedCorrectionRelativeScreenSize(nextPosition);
            base.Move(nextPosition);
        }

        protected override void Rotate(float deltaTime)
        {
            float angle = Model.RotationAngle + (RotationSpeed * _deltaTime * DirectionOfRotation);
            base.Rotate(angle);
        }
    }
}