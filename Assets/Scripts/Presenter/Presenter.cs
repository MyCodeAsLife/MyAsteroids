using System;
using UnityEngine;

namespace Asteroids
{
    public class Presenter : MonoBehaviour
    {
        private Transformable _objectModel;
        private ViewMovement _viewMovement;
        private int _enemyLayer;

        public event Action<Presenter> Destroyed;

        public ModelMovement ModelMovement { get; private set; }
        public GameObjectType ObjectType { get; private set; }          // Позиция форматирования?

        private void Start()
        {
            _viewMovement = new ViewMovement(transform);
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
        public void SetDirection(Vector2 direction) => _objectModel.Direction = direction;
        public void SetMovementSpeed(float movementSpeed) => _objectModel.MovementSpeed = movementSpeed;
        public void SetMaxMovementSpeed(float maxMovementSpeed) => _objectModel.MaxMovementSpeed = maxMovementSpeed;
        public void SetDegreesPerSecond(float degreesPerSecond) => _objectModel.DegreesPerSecond = degreesPerSecond;
        public void SetDirectionOfRotation(float directionOfRotation) => _objectModel.DirectionOfRotation = directionOfRotation;
        public void SetGameObjectType(GameObjectType objectType) => ObjectType = objectType;

        public void CollisionCheck()
        {
            var collider = GetComponent<CapsuleCollider2D>();                   // Получить 1 раз а не каждый кадр
            var hit = Physics2D.OverlapCapsule(transform.position, collider.size, collider.direction, _objectModel.RotationAngle, 1 << _enemyLayer);

            if (hit != null)
            {
                Debug.Log(hit.gameObject.name);

                Destroyed?.Invoke(this);
            }
        }

        public void PositioCheck()
        {
            if (_objectModel.Position.x > Config.BoundaryExistenceObjects ||
               _objectModel.Position.y > Config.BoundaryExistenceObjects ||
               _objectModel.Position.x < 0 || _objectModel.Position.y < 0)
                Destroyed?.Invoke(this);
        }
    }
}