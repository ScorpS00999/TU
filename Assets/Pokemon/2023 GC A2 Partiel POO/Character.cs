using System;

namespace _2023_GC_A2_Partiel_POO.Level_2
{
    /// <summary>
    /// Définition d'un personnage
    /// </summary>
    public class Character
    {
        /// <summary>
        /// Stat de base, HP
        /// </summary>
        int _baseHealth;
        /// <summary>
        /// Stat de base, ATK
        /// </summary>
        int _baseAttack;
        /// <summary>
        /// Stat de base, DEF
        /// </summary>
        int _baseDefense;
        /// <summary>
        /// Stat de base, SPE
        /// </summary>
        int _baseSpeed;
        /// <summary>
        /// Type de base
        /// </summary>
        TYPE _baseType;

        public Character(int baseHealth, int baseAttack, int baseDefense, int baseSpeed, TYPE baseType)
        {
            _baseHealth = baseHealth;
            _baseAttack = baseAttack;
            _baseDefense = baseDefense;
            _baseSpeed = baseSpeed;
            _baseType = baseType;

            CurrentHealth = MaxHealth;
        }
        /// <summary>
        /// HP actuel du personnage
        /// </summary>
        public int CurrentHealth { get; private set; }
        public TYPE BaseType { get => _baseType; }
        /// <summary>
        /// HPMax, prendre en compte base et equipement potentiel
        /// </summary>
        public int MaxHealth
        {
            get
            {
                if (CurrentEquipment != null)
                {
                    return _baseHealth + CurrentEquipment.BonusHealth;
                }
                else
                {
                    return _baseHealth;
                }
            }
        }
        /// <summary>
        /// ATK, prendre en compte base et equipement potentiel
        /// </summary>
        public int Attack
        {
            get
            {
                if (CurrentEquipment != null)
                {
                    return _baseAttack + CurrentEquipment.BonusAttack;
                }
                else
                {
                    return _baseAttack;
                }
            }
        }
        /// <summary>
        /// DEF, prendre en compte base et equipement potentiel
        /// </summary>
        public int Defense
        {
            get
            {
                if (CurrentEquipment != null)
                {
                    return _baseDefense + CurrentEquipment.BonusDefense;
                }
                else
                {
                    return _baseDefense;
                }
            }
        }
        /// <summary>
        /// SPE, prendre en compte base et equipement potentiel
        /// </summary>
        public int Speed
        {
            get
            {
                if (CurrentEquipment != null)
                {
                    return _baseSpeed + CurrentEquipment.BonusSpeed;
                }
                else
                {
                    return _baseSpeed;
                }
            }
        }
        /// <summary>
        /// Equipement unique du personnage
        /// </summary>
        public Equipment CurrentEquipment { get; private set; }

        public Shield CurrentShield { get; private set; }
        /// <summary>
        /// null si pas de status
        /// </summary>
        public StatusEffect CurrentStatus { get; private set; }

        public bool IsAlive => CurrentHealth > 0;

        //public bool IsFristToAttack => false;


        /// <summary>
        /// Application d'un skill contre le personnage
        /// On pourrait potentiellement avoir besoin de connaitre le personnage attaquant,
        /// Vous pouvez adapter au besoin
        /// </summary>
        /// <param name="s">skill attaquant</param>
        /// <exception cref="NotImplementedException"></exception>
        public void ReceiveAttack(Skill s)
        {
            if (CurrentShield != null)
            {
                CurrentShield.ReceiveAttack(s.Power);
                if (!CurrentShield.IsNotBroken)
                {
                    UnequipShield();
                }

            }
            else
            {
                CurrentHealth -= (s.Power - Defense);
                if (CurrentHealth < 0)
                {
                    CurrentHealth = 0;
                }
            }

        }

        public void ReceiveAttack(Skill s, float multi)
        {
            if (CurrentShield != null)
            {
                CurrentShield.ReceiveAttack((int)((float)s.Power * multi));
                if (!CurrentShield.IsNotBroken)
                {
                    UnequipShield();
                }
            }
            else
            {
                CurrentHealth -= (int)(((float)s.Power * multi) - Defense);
                if (CurrentHealth < 0)
                {
                    CurrentHealth = 0;
                }
            }
        }
        /// <summary>
        /// Equipe un objet au personnage
        /// </summary>
        /// <param name="newEquipment">equipement a appliquer</param>
        /// <exception cref="ArgumentNullException">Si equipement est null</exception>
        public void Equip(Equipment newEquipment)
        {
            if (newEquipment == null)
            {
                throw new ArgumentNullException();
            }
            else
            {
                CurrentEquipment = newEquipment;
            }
        }


        public void EquipShield(Shield shield)
        {
            if (shield == null)
            {
                throw new ArgumentNullException();
            }
            else
            {
                CurrentShield = shield;
            }
        }

        public void UnequipShield()
        {
            CurrentShield = null;
        }
        /// <summary>
        /// Desequipe l'objet en cours au personnage
        /// </summary>
        public void Unequip()
        {
            CurrentEquipment = null;
        }

        public void ReceiveHeal(PotionHeal potion)
        {
            ReceiveHealPotion(potion.Heal);
        }

        public void ReceiveMalusHealth(PotionMalus potion)
        {
            _baseHealth -= potion.MalusHealth;
            if (CurrentHealth > MaxHealth)
            {
                CurrentHealth = MaxHealth;
            }
        }

        public void ReceivePotion(PotionBonus potion)
        {
            if (potion.TypePotion == TYPEPOTION.HEALTH)
            {
                ReceiveHealPotion(potion.Power);
            }
            else if (potion.TypePotion == TYPEPOTION.ATTACK)
            {
                _baseAttack += potion.Power;
            }
            else if (potion.TypePotion == TYPEPOTION.DEFENSE)
            {
                _baseDefense += potion.Power;
            }
            else if (potion.TypePotion == TYPEPOTION.SPEED)
            {
                _baseSpeed += potion.Power;
            }
        }

        public void ReceiveHealPotion(int heal)
        {
            if (IsAlive)
            {
                CurrentHealth += heal;

                if (CurrentHealth > MaxHealth)
                {
                    CurrentHealth = MaxHealth;
                }
            }
        }
    }

}
