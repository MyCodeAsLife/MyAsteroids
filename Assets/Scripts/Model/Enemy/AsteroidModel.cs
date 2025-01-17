using System;
using UnityEngine;

namespace Asteroids
{
    public class AsteroidModel : EnemyModel
    {
        // ��������� ���������(�� ��������� ������), �������� � �������� �������� ��� ��������� ������� �� ����

        public AsteroidModel(Vector2 position, float rotation, int cost) : base(position, rotation, cost) { }

        // ����������� ����� �������������� �������, �������� �� ������� �� �������� �� ������� ������.
        // ��� ��������� �������� ��� � ������� ������?
        public event Action<EnemyModel> Destroyed;

        public void Destroy()
        {
            Destroyed?.Invoke(this);
        }
    }
}