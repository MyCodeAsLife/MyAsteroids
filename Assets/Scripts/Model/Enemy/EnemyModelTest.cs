namespace Asteroids
{
    public abstract class EnemyModelTest : TransformableTest
    {
        public readonly int Cost;

        // ��� ����� �����������, ���� ��������� ���������� � ������� �����.
        // ������� ��� ����� ������ ������� ������

        public EnemyModelTest(int cost)
        {
            Cost = cost;
        }
    }
}