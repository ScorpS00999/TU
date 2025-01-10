
namespace _2023_GC_A2_Partiel_POO.Level_2
{
    public class Shield
    {

        TYPE _baseType;

        public Shield(int health, TYPE type)
        {
            CurrentHealth = health;
            _baseType = type;
        }

        public bool IsNotBroken => CurrentHealth > 0;

        public int CurrentHealth { get; private set; }

        public TYPE TypeSHield { get { return _baseType; } }

        public void ReceiveAttack(int attack)
        {
            CurrentHealth -= attack;
            if (CurrentHealth < 0)
            {
                CurrentHealth = 0;
            }
        }

    }
}
