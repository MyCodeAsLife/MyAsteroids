using UnityEngine;
namespace Asteroids
{
    public class AsteroidMovement : Movement
    {
        private float _deltaTime;

        public AsteroidMovement(Transformable model, Vector2 displaySize) : base(model, displaySize) { }

        public override void Move(Vector2 position)     // направление? следующая позиция?
        {
            Vector2 nextPosition = Model.Position + Direction * (MovementSpeed * _deltaTime);
            nextPosition = Vector2.ClampMagnitude(nextPosition, MovementSpeed);

            nextPosition = Model.Position + SpeedCorrectionRelativeScreenSize(nextPosition);
            base.Move(nextPosition);
        }

        public override void Rotate(float deltaTime)
        {
            //Debug.Log($"Angle: {Model.RotationAngle}| RotationSpeed: {RotationSpeed}| DirectionOfRotation: {DirectionOfRotation}");         //++++++++++++++++++++++
            float angle = Model.RotationAngle + (RotationSpeed * _deltaTime * DirectionOfRotation);
            Debug.Log(angle);                               //++++++++++++++++++++++++++++++++
            base.Rotate(angle);
        }

        public override void Tick(float deltaTime)
        {
            _deltaTime = deltaTime;
            Move(Direction);               // Ненужная передача параметров
            Rotate(deltaTime);
        }
    }
}