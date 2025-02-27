using UnityEngine;

namespace Asteroids
{
    public class UfoMovement : ModelMovement
    {
        private ShipPresenter _target;      // ���������� ����� ���������?
        private InertiaSimulator _inertiaSimulator;

        public UfoMovement(Transformable model) : base(model)
        {
            _inertiaSimulator = new InertiaSimulator(model);
            Updated += OnUpdated;
        }

        public void SetTarget(ShipPresenter target) => _target = target;

        private void Move()
        {
            var nextPosition = Model.Position + SpeedCorrectionRelativeScreenSize(_inertiaSimulator.Acceleration);
            base.Move(nextPosition);
        }

        private new void Rotate(float deltaTime)
        {
            var playerPosition = _target.GetPosition();
            float delta = Mathf.Atan2(playerPosition.y - Model.Position.y, playerPosition.x - Model.Position.x) * Mathf.Rad2Deg - 90;       // ���������� Model.Position
            delta = Mathf.MoveTowardsAngle(Model.RotationAngle, delta, deltaTime * Model.DegreesPerSecond);
            base.Rotate(delta);
        }

        private void OnUpdated(float deltaTime)
        {
            _inertiaSimulator.Accelerate(Model.Forward, deltaTime);
            _inertiaSimulator.SlowDown(deltaTime);
            Move();
            Rotate(deltaTime);
        }
    }
}