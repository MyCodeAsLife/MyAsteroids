namespace Asteroids
{
    public class AsteroidModel : EnemyModel     // ���������� ��� ����� ����� Enemy?
    {
        // ��������� ���������(�� ��������� ������), �������� � �������� �������� ��� ��������� ������� �� ����

        public AsteroidModel(int cost = Config.AsteroidCost) : base(cost) { }

        // ����������� ����� �������������� �������, �������� �� ������� �� �������� �� ������� ������.
        // ��� ��������� �������� ��� � ������� ������?
    }
}