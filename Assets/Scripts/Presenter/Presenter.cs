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

        public ModelMovement ModelMovement { get; private set; }
        public GameObjectType ObjectType { get; private set; }          // Позиция форматирования?

        private void Start()
        {
            _collider = GetComponent<CapsuleCollider2D>();
            _viewMovement = new ViewMovement(transform);

            if (ObjectType == GameObjectType.Ufo || ObjectType == GameObjectType.Asteroid || ObjectType == GameObjectType.AsteroidPart)
                _viewMovement.SetScaleWindowSize(Config.BoundaryExistenceObjects);
            else
                _viewMovement.SetScaleWindowSize(Config.PlayerExistenceLimit);

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
            PositioCheck();
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

        public virtual void CollisionCheck()
        {
            var hit = Physics2D.OverlapCapsule(transform.position, _collider.size, _collider.direction, _objectModel.RotationAngle, 1 << _enemyLayer);

            if (hit != null)
            {
                hit.GetComponent<IDamageable>().TakeDamage();
                Destroyed?.Invoke(this);
            }
        }

        public void TakeDamage()
        {
            Destroyed?.Invoke(this);
        }

        private void PositioCheck()
        {
            if (_objectModel.Position.x > Config.BoundaryExistenceObjects ||
               _objectModel.Position.y > Config.BoundaryExistenceObjects ||
               _objectModel.Position.x < 0 || _objectModel.Position.y < 0)
                Destroyed?.Invoke(this);
        }
    }
}