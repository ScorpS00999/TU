
namespace _2023_GC_A2_Partiel_POO.Level_2
{
    /// <summary>
    /// Défintion simple d'un équipement apportant des boost de stats
    /// </summary>
    public class Equipment
    {
        //int _bonusHealth;
        //int _bonusAttack;
        //int _bonusDefense;
        //int _bonusSpeed;

        public Equipment(int bonusHealth, int bonusAttack, int bonusDefense, int bonusSpeed)
        {
            //_bonusHealth = bonusHealth;
            //_bonusAttack = bonusAttack;
            //_bonusDefense = bonusDefense;
            //_bonusSpeed = bonusSpeed;

            BonusHealth = bonusHealth;
            BonusAttack = bonusAttack;
            BonusDefense = bonusDefense;
            BonusSpeed = bonusSpeed;
        }

        public int BonusHealth { get; protected set; }
        public int BonusAttack { get; protected set; }
        public int BonusDefense { get; protected set; }
        public int BonusSpeed { get; protected set; }

    }
}
