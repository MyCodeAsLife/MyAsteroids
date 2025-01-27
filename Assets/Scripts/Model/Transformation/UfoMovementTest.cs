using UnityEngine;

namespace Asteroids
{
    public class UfoMovementTest : ModelMovementTest
    {
        private ShipPresenterTest _target;      // Передавать через интерфейс?
        private InertiaSimulatorTest _inertiaSimulator;
        private Vector2 _nextPosition;

        public UfoMovementTest(TransformableTest model, ShipPresenterTest target) : base(model)
        {
            _target = target;
            _inertiaSimulator = new InertiaSimulatorTest(model);
        }

        public override void Tick(float deltaTime)
        {
            _inertiaSimulator.Accelerate(Model.Forward, deltaTime);
            _inertiaSimulator.SlowDown(deltaTime);
            Move(_inertiaSimulator.Acceleration);
            Rotate(deltaTime);
        }

        private new void Move(Vector2 position)
        {
            var nextPosition = Model.Position + SpeedCorrectionRelativeScreenSize(position);
            base.Move(nextPosition);
        }

        private new void Rotate(float deltaTime)
        {
            var playerPosition = _target.GetPosition();
            float delta = Mathf.Atan2(playerPosition.y - Model.Position.y, playerPosition.x - Model.Position.x) * Mathf.Rad2Deg - 90;
            delta = Mathf.MoveTowardsAngle(Model.RotationAngle, delta, deltaTime * Model.DegreesPerSecond);
            base.Rotate(delta);
        }
    }
}