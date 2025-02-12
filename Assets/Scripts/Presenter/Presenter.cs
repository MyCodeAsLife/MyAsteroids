using System;
using UnityEngine;

namespace Asteroids
{
    public abstract class Presenter : MonoBehaviour
    {
        public RootController UserInput { get; private set; }

        public abstract event Action<Presenter> Deactivated;
        public event Action<float> Updated;

        public GameObjectType ObjectType { get; private set; }          // ������� ��������������?

        protected virtual void Awake()
        {
            UserInput = FindFirstObjectByType<RootController>();
        }

        protected virtual void OnEnable()
        {
            GameState.SwitchPause += OnPauseMenuPresed;
        }

        protected virtual void OnDisable()
        {
            GameState.SwitchPause -= OnPauseMenuPresed;
        }

        private void Update()
        {
            Updated?.Invoke(Time.deltaTime);
        }

        protected virtual void OnPauseMenuPresed(bool value) { }       // ������� � Interactive? ������ ��� ������������ ������ ���
        public void SetGameObjectType(GameObjectType objectType) => ObjectType = objectType;

    }
}