namespace Asteroids
{
    public class UfoModel : EnemyModel
    {
        // ��������� ���������(�� ��������� ������), �������� � �������� �������� ��� ��������� ������� �� ����
        public UfoModel(int cost = Config.UfoCost) : base(cost) { }
    }
}