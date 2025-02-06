using System;
using UnityEngine;

namespace Asteroids
{
    public abstract class Presenter : MonoBehaviour
    {
        public abstract event Action<Presenter> Deactivated;

        public GameObjectType ObjectType { get; private set; }          // ������� ��������������?

        public void SetGameObjectType(GameObjectType objectType) => ObjectType = objectType;
    }
}