using UnityEngine;

namespace Asteroids
{
    public class ProjectileMovement : ModelMovement
    {
        public ProjectileMovement(Transformable model) : base(model)
        {
            Updated += Move;
        }

        private void Move(float deltaTime)          // Такаяже как у Asteroid
        {
            Vector2 nextPosition = Model.DirectionMovement + Model.DirectionMovement * (Model.MovementSpeed * deltaTime);
            nextPosition = Vector2.ClampMagnitude(nextPosition, Model.MovementSpeed);

            nextPosition = Model.Position + SpeedCorrectionRelativeScreenSize(nextPosition);
            base.Move(nextPosition);
        }
    }
}