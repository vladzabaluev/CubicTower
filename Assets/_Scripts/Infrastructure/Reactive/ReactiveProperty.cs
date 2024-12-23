using System;

namespace _Scripts.Infrastructure.Reactive
{
    public class ReactiveProperty<T>
    {
        public event Action<T> OnValueChanged;
        private T _value;

        public T Value
        {
            get => _value;
            set
            {
                _value = value;
                OnValueChanged?.Invoke(_value);
            }
        }
    }
}