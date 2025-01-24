using UnityEngine;

namespace Asteroids
{
    public abstract class ModelMovementTest
    {
        protected TransformableTest Model;              // ���������� ��������� ����� ��������?

        private float _xRatio;          // ������������� �������� ������������ ����������� ������ ������
        private float _yRatio;          // ������������� �������� ������������ ����������� ������ ������

        public abstract void Tick(float deltaTime);

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

        public void SetModel(TransformableTest model) => Model = model;
        protected virtual void Move(Vector2 position) => Model.Position = position;
        protected virtual void Rotate(float delta) => Model.RotationAngle = delta;
    }
}