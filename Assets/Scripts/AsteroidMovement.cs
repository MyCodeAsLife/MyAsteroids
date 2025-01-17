using UnityEngine;

namespace Asteroids
{
    public class AsteroidMovement : Movement
    {
        public AsteroidMovement(EnemyModel model, Vector2 displaySize) : base(model, displaySize) { }

        public override void Move(Vector2 position)
        {
            //transform.position = Vector3.MoveTowards(transform.position, _player.position, _speed * Time.deltaTime);

            base.Move(position);
        }

        public override void Rotate(float deltaTime)
        {
            //if (_model.Direction == 0)
            //    throw new InvalidOperationException(nameof(_model.Direction));

            //_model.Direction = _model.Direction > 0 ? 1 : -1;
            //float delta = (_model.Direction * deltaTime * _model.DegreesPerSecond);
            //_model.Rotate(delta);
        }

        public override void Tick(float deltaTime)
        {
            throw new System.NotImplementedException();
        }
    }
}