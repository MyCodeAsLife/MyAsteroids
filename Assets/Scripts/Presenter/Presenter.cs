using System;
using UnityEngine;

namespace Asteroids
{
    public abstract class Presenter : MonoBehaviour
    {
        public RootController UserInput { get; private set; }
        public bool IsPaused { get; private set; }

        public abstract event Action<Presenter> Deactivated;
        public event Action<float> Updated;

        public GameObjectType ObjectType { get; private set; }          // Позиция форматирования?

        protected virtual void Awake()
        {
            UserInput = FindFirstObjectByType<RootController>();
        }

        protected virtual void OnEnable()
        {
            UserInput.PauseMenuPressed += OnPauseMenuPresed;
        }

        protected virtual void OnDisable()
        {
            UserInput.PauseMenuPressed -= OnPauseMenuPresed;
        }

        private void Update()
        {
            Updated?.Invoke(Time.deltaTime);
        }

        public void SetGameObjectType(GameObjectType objectType) => ObjectType = objectType;
        public abstract void OnPauseSwith();

        private void OnPauseMenuPresed()
        {
            IsPaused = IsPaused ? false : true;
            OnPauseSwith();
        }
    }
}