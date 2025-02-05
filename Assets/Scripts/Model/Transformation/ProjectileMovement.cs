using UnityEngine;

namespace Asteroids
{
    public class ProjectileMovement : ModelMovement
    {
        public ProjectileMovement(Transformable model) : base(model) { }

        public override void Tick(float deltaTime)
        {
            Move(deltaTime);
        }

        private void Move(float deltaTime)          // Такаяже как у Asteroid
        {
            Vector2 nextPosition = Model.DirectionMovement + Model.DirectionMovement * (Model.MovementSpeed * deltaTime);
            nextPosition = Vector2.ClampMagnitude(nextPosition, Model.MovementSpeed);

            nextPosition = Model.Position.Value + SpeedCorrectionRelativeScreenSize(nextPosition);
            base.Move(nextPosition);
        }
    }
}