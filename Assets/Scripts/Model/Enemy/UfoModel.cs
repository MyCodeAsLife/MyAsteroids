using UnityEngine;

namespace Asteroids
{
    public class UfoModel : EnemyModel
    {
        // ��������� ���������(�� ��������� ������), �������� � �������� �������� ��� ��������� ������� �� ����
        public UfoModel(Vector2 position, float rotation) : base(position, rotation, Config.UfoCost) { }
    }
}