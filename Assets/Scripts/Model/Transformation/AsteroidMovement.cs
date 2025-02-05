using System;
using UnityEngine;

namespace Asteroids
{
    public class AsteroidMovement : ModelMovement
    {
        public AsteroidMovement(Transformable model) : base(model) { }

        public override void Tick(float deltaTime)
        {
            Move(deltaTime);
            Rotate(deltaTime);
        }

        private void Move(float deltaTime)          // Такаяже как у Projectile
        {
            Vector2 nextPosition = Model.DirectionMovement + Model.DirectionMovement * (Model.MovementSpeed * deltaTime);
            nextPosition = Vector2.ClampMagnitude(nextPosition, Model.MovementSpeed);

            nextPosition = Model.Position + SpeedCorrectionRelativeScreenSize(nextPosition);
            base.Move(nextPosition);
        }

        private new void Rotate(float deltaTime)     // Такая же как и у ship
        {
            if (Model.DirectionOfRotation == 0)
                throw new InvalidOperationException(nameof(Model.DirectionOfRotation));

            float delta = Model.RotationAngle + (Model.DegreesPerSecond * deltaTime * Model.DirectionOfRotation);
            base.Rotate(delta);
        }
    }
}