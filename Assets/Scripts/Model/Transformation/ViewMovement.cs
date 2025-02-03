using UnityEngine;

namespace Asteroids
{
    public class ViewMovement
    {
        private const float Half = 0.5f;

        private Transform _view;
        private Vector2 _displaySize;
        private Vector2 _offsetPosition;
        private float _scaleWindowSize;

        public ViewMovement(Transform view)
        {
            _view = view;
        }

        public void SetScaleWindowSize(float scale) => _scaleWindowSize = scale;

        public void Move(Vector2 position)
        {
            _view.localPosition = (_displaySize * position) - _offsetPosition;
        }

        public void Rotate(float rotationAngle)
        {
            _view.rotation = Quaternion.Euler(0f, 0f, rotationAngle);
        }

        public void SetDisplaySize(Vector2 size)
        {
            _displaySize = size;
            _offsetPosition = _displaySize * Half * _scaleWindowSize;
        }
    }
}