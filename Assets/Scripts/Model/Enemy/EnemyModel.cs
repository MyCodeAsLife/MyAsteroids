namespace Asteroids
{
    public abstract class EnemyModel : Transformable
    {
        public readonly int Cost;

        // ��� ����� �����������, ���� ��������� ���������� � ������� �����.
        // ������� ��� ����� ������ ������� ������

        public EnemyModel(int cost)
        {
            Cost = cost;
        }
    }
}