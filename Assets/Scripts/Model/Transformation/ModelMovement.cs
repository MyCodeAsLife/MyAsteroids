using System;
using UnityEngine;

namespace Asteroids
{
    public abstract class ModelMovement
    {
        protected Transformable Model;              // Возвращать интерфейс через свойство?

        private float _xRatio;          // Корректировка скорости относительно соотношения сторон экрана
        private float _yRatio;          // Корректировка скорости относительно соотношения сторон экрана

        public event Action<float> Updated;

        public ModelMovement(Transformable model)
        {
            Model = model;
        }

        public virtual void Tick(float deltaTime)
        {
            Updated?.Invoke(deltaTime);
        }

        public Vector2 SpeedCorrectionRelativeScreenSize(Vector2 position)
        {
            position.x *= _xRatio;
            position.y *= _yRatio;
            return position;
        }

        public void SetScreenAspectRatio(Vector2 displaySize)
        {
            float ratio = displaySize.x / displaySize.y;
            _xRatio = ratio < 1 ? ratio : 1;
            _yRatio = ratio > 1 ? ratio : 1;
        }

        public void SetModel(Transformable model) => Model = model;
        protected void Move(Vector2 position) => Model.Position = position;
        protected void Rotate(float delta) => Model.RotationAngle = delta;
    }
}