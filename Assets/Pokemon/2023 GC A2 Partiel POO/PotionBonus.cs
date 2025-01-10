
namespace _2023_GC_A2_Partiel_POO.Level_2
{
    public enum TYPEPOTION { HEALTH, ATTACK, DEFENSE, SPEED }

    public class PotionBonus
    {
        public PotionBonus(int power, TYPEPOTION type)
        {
            Power = power;
            TypePotion = type;
        }

        public int Power { get; protected set; }

        public TYPEPOTION TypePotion { get; protected set; }

    }
}
