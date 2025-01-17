using UnityEngine;

namespace Asteroids
{
    public abstract class EnemyModel : Transformable
    {
        public readonly int Cost;

        // ��� ����� �����������, ���� ��������� ���������� � ������� �����.
        // ������� ��� ����� ������ ������� ������

        public EnemyModel(Vector2 position, float rotation, int cost) : base(position, rotation)
        {
            Cost = cost;
        }
    }
}