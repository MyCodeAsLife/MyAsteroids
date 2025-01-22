using UnityEngine;

namespace Asteroids
{
    public class UfoMovement : Movement
    {
        private ShipPresenter _target;      // Передавать через интерфейс?
        private InertiaSimulator _inertiaSimulator;
        private Vector2 _nextPosition;

        public UfoMovement(Transformable model,/* Vector2 displaySize,*/ ShipPresenter target) : base(model/*, displaySize*/)
        {
            _target = target;
            _inertiaSimulator = new InertiaSimulator();
        }

        public override void Tick(float deltaTime)
        {
            _inertiaSimulator.Accelerate(Model.Forward, deltaTime);
            _inertiaSimulator.SlowDown(deltaTime);
            Move(_inertiaSimulator.Acceleration);
            Rotate(deltaTime);
        }

        protected override void Move(Vector2 position)
        {
            var nextPosition = Model.Position + SpeedCorrectionRelativeScreenSize(position);
            base.Move(nextPosition);
        }

        protected override void Rotate(float deltaTime)
        {
            var playerPosition = _target.GetPosition();
            float angle = Mathf.Atan2(playerPosition.y - Model.Position.y, playerPosition.x - Model.Position.x) * Mathf.Rad2Deg - 90;
            angle = Mathf.MoveTowardsAngle(Model.RotationAngle, angle, deltaTime * Model.DegreesPerSecond);
            base.Rotate(angle);
        }
    }
}