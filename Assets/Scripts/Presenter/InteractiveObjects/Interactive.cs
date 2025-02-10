using System;
using UnityEngine;

namespace Asteroids
{
    public class Interactive : Presenter, IDamageable
    {
        private Transformable _objectModel;
        private ViewMovement _viewMovement;
        private CapsuleCollider2D _collider;
        private int _enemyLayer;

        public override event Action<Presenter> Deactivated;
        public event Action<Interactive> Destroyed;

        public ModelMovement ModelMovement { get; private set; }

        private void Start()
        {
            _collider = GetComponent<CapsuleCollider2D>();
            _viewMovement = new ViewMovement(transform);
            _viewMovement.SetScaleWindowSize(Config.ScaleWindowSize);
            //var _canvas = GetComponentInParent<Canvas>();
            var _canvas = FindFirstObjectByType<Canvas>();
            var _displaySize = _canvas.renderingDisplaySize / _canvas.scaleFactor;
            ModelMovement.SetScreenAspectRatio(_displaySize);
            _viewMovement.SetDisplaySize(_displaySize);

            Updated += OnUpdate;
        }

        public void SetOverlapLayer(int layer) => _enemyLayer = layer;
        public void SetModel(Transformable model) => _objectModel = model;
        public void SetModelMovement(ModelMovement movement) => ModelMovement = movement;
        public void SetPosition(Vector2 position) => _objectModel.Position.Value = position;
        public void SetRotationAngle(float angle) => _objectModel.RotationAngle = angle;
        public void SetDirectionMovement(Vector2 direction) => _objectModel.DirectionMovement = direction;
        public void SetMovementSpeed(float movementSpeed) => _objectModel.MovementSpeed = movementSpeed;
        public void SetMaxMovementSpeed(float maxMovementSpeed) => _objectModel.MaxMovementSpeed = maxMovementSpeed;
        public void SetDegreesPerSecond(float degreesPerSecond) => _objectModel.DegreesPerSecond = degreesPerSecond;
        public void SetDirectionOfRotation(float directionOfRotation) => _objectModel.DirectionOfRotation = directionOfRotation;
        public Vector2 GetPosition() => _objectModel.Position.Value;
        public void SubscribeOnPositionChanged(Action<Vector2> func) => _objectModel.Position.Changed += func;

        public void TakeDamage()
        {
            Destroyed?.Invoke(this);
            Deactivated?.Invoke(this);
        }

        private void CollisionCheck()
        {
            var hit = Physics2D.OverlapCapsule(transform.position, _collider.size, _collider.direction, _objectModel.RotationAngle, 1 << _enemyLayer);

            if (hit != null)
            {
                if (hit.TryGetComponent<IDamageable>(out IDamageable obj))
                    obj.TakeDamage();

                TakeDamage();
            }
        }

        private void PositionCheck()
        {
            var position = _objectModel.Position.Value;

            if (position.x > Config.MaxBoundaryExistenceObjects ||
               position.y > Config.MaxBoundaryExistenceObjects ||
               position.x < Config.MinBoundaryExistenceObjects ||
               position.y < Config.MinBoundaryExistenceObjects)
                Deactivated?.Invoke(this);
        }

        private void OnUpdate(float deltaTime)
        {
            ModelMovement.Tick(Time.deltaTime);
            _viewMovement.Move(_objectModel.Position.Value);
            _viewMovement.Rotate(_objectModel.RotationAngle);
            CollisionCheck();
            PositionCheck();
        }

        protected override void OnPauseMenuPresed(bool isPaused)
        {
            if (isPaused)
                Updated -= OnUpdate;
            else
                Updated += OnUpdate;
        }
    }
}