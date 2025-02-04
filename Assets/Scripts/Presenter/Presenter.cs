using System;
using UnityEngine;

namespace Asteroids
{
    public class Presenter : MonoBehaviour, IDamageable
    {
        private Transformable _objectModel;
        private ViewMovement _viewMovement;
        private CapsuleCollider2D _collider;
        private int _enemyLayer;

        public event Action<Presenter> Destroyed;
        public event Action<Presenter> Deactivated;

        public ModelMovement ModelMovement { get; private set; }
        public GameObjectType ObjectType { get; private set; }          // Позиция форматирования?

        private void Start()
        {
            _collider = GetComponent<CapsuleCollider2D>();
            _viewMovement = new ViewMovement(transform);
            _viewMovement.SetScaleWindowSize(Config.ScaleWindowSize);
            var _canvas = GetComponentInParent<Canvas>();
            var _displaySize = _canvas.renderingDisplaySize / _canvas.scaleFactor;
            ModelMovement.SetScreenAspectRatio(_displaySize);
            _viewMovement.SetDisplaySize(_displaySize);
        }

        protected void Update()
        {
            ModelMovement.Tick(Time.deltaTime);
            _viewMovement.Move(_objectModel.Position);
            _viewMovement.Rotate(_objectModel.RotationAngle);
            CollisionCheck();
            PositionCheck();          // Проверка на выход за границы игровой области
        }

        public void SetOverlapLayer(int layer) => _enemyLayer = layer;
        public void SetModel(Transformable model) => _objectModel = model;
        public void SetModelMovement(ModelMovement movement) => ModelMovement = movement;
        public void SetPosition(Vector2 position) => _objectModel.Position = position;
        public void SetRotationAngle(float angle) => _objectModel.RotationAngle = angle;
        public void SetDirectionMovement(Vector2 direction) => _objectModel.DirectionMovement = direction;
        public void SetMovementSpeed(float movementSpeed) => _objectModel.MovementSpeed = movementSpeed;
        public void SetMaxMovementSpeed(float maxMovementSpeed) => _objectModel.MaxMovementSpeed = maxMovementSpeed;
        public void SetDegreesPerSecond(float degreesPerSecond) => _objectModel.DegreesPerSecond = degreesPerSecond;
        public void SetDirectionOfRotation(float directionOfRotation) => _objectModel.DirectionOfRotation = directionOfRotation;
        public void SetGameObjectType(GameObjectType objectType) => ObjectType = objectType;
        public Vector2 GetPosition() => _objectModel.Position;

        public void TakeDamage()
        {
            Destroyed?.Invoke(this);
            Deactivated?.Invoke(this);
        }

        private void CollisionCheck()
        {
            var hit = Physics2D.OverlapCapsule(transform.position, _collider.size, _collider.direction, _objectModel.RotationAngle, 1 << _enemyLayer);

            if (hit != null && hit.TryGetComponent<IDamageable>(out IDamageable obj))       // Лазер пока что не уничтожает объекты
            {
                obj.TakeDamage();
                Deactivated?.Invoke(this);
            }
        }

        private void PositionCheck()                                // Не используется игроком и НЛО, вынести
        {
            if (_objectModel.Position.x > Config.MaxBoundaryExistenceObjects ||
               _objectModel.Position.y > Config.MaxBoundaryExistenceObjects ||
               _objectModel.Position.x < Config.MinBoundaryExistenceObjects ||
               _objectModel.Position.y < Config.MinBoundaryExistenceObjects)
                Deactivated?.Invoke(this);
        }
    }
}