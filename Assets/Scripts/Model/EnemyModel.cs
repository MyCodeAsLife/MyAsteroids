namespace Asteroids
{
    public class EnemyModel : Transformable
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