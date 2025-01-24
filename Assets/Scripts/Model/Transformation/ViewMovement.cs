using UnityEngine;

namespace Asteroids
{
    public class ViewMovement
    {
        private Transform _view;
        //private float _xRatio;
        //private float _yRatio;
        private Vector2 _offsetPosition;
        //private Vector2 _displaySize;

        public Vector2 DisplaySize { get; private set; }

        public ViewMovement(Transform view)
        {
            _view = view;
        }

        public void Move(Vector2 position)
        {
            _view.localPosition = (DisplaySize * position) - _offsetPosition;
        }

        public void Rotate(float rotationAngle)
        {
            _view.rotation = Quaternion.Euler(0f, 0f, rotationAngle);
        }

        //public Vector2 SpeedCorrectionRelativeScreenSize(Vector2 position)
        //{
        //    position.x *= _xRatio;
        //    position.y *= _yRatio;
        //    return position;
        //}

        public void SetDisplaySize(Vector2 size)
        {
            DisplaySize = size;
            _offsetPosition = DisplaySize / 2 * Config.ScaleWindowSize;
            //CalculateDisplayRatio();
        }

        //private void CalculateDisplayRatio()
        //{
        //    float ratio = DisplaySize.x / DisplaySize.y;
        //    _xRatio = ratio < 1 ? ratio : 1;
        //    _yRatio = ratio > 1 ? ratio : 1;
        //}
    }
}