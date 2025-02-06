using System;

namespace Asteroids
{
    public class SingleReactiveProperty<T>
    {
        private T _value;

        public event Action<T> Changed;

        public T Value
        {
            get { return _value; }
            set
            {
                _value = value;
                Changed?.Invoke(_value);
            }
        }
    }
}