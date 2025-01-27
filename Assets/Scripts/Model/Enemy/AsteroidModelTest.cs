using System;

namespace Asteroids
{
    public class AsteroidModelTest : EnemyModelTest
    {
        // ��������� ���������(�� ��������� ������), �������� � �������� �������� ��� ��������� ������� �� ����

        public AsteroidModelTest(int cost = Config.AsteroidCost) : base(cost) { }

        // ����������� ����� �������������� �������, �������� �� ������� �� �������� �� ������� ������.
        // ��� ��������� �������� ��� � ������� ������?
        public event Action<EnemyModelTest> Destroyed;

        public void Destroy()
        {
            Destroyed?.Invoke(this);
        }
    }
}