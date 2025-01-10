
using System;

namespace _2023_GC_A2_Partiel_POO.Level_2
{
    public class Fight
    {
        public Fight(Character character1, Character character2)
        {
            Character1 = character1;
            Character2 = character2;

            if (Character1 == null || Character2 == null)
            {
                throw new ArgumentNullException();
            }
        }

        public Character Character1 { get; }
        public Character Character2 { get; }
        /// <summary>
        /// Est-ce la condition de victoire/défaite a été rencontré ?
        /// </summary>
        public bool IsFightFinished => !Character1.IsAlive || !Character2.IsAlive;

        float multiplicateur;

        /// <summary>
        /// Jouer l'enchainement des attaques. Attention à bien gérer l'ordre des attaques par apport à la speed des personnages
        /// </summary>
        /// <param name="skillFromCharacter1">L'attaque selectionné par le joueur 1</param>
        /// <param name="skillFromCharacter2">L'attaque selectionné par le joueur 2</param>
        /// <exception cref="ArgumentNullException">si une des deux attaques est null</exception>
        public void ExecuteTurn(Skill skillFromCharacter1, Skill skillFromCharacter2)
        {
            if (Character2.IsAlive && Character1.IsAlive)
            {
                if (Character2.Speed > Character1.Speed)
                {
                    //if (Character1.CurrentShield != null)
                    //{
                    //    multiplicateur = TypeResolver.GetFactor(Character2.CurrentShield.TypeSHield, Character1.BaseType);
                    //    Character1.ReceiveAttack(skillFromCharacter2, multiplicateur);
                    //}

                    multiplicateur = TypeResolver.GetFactor(Character2.BaseType, Character1.BaseType);
                    Character1.ReceiveAttack(skillFromCharacter2, multiplicateur);


                    if (Character1.IsAlive)
                    {
                        multiplicateur = TypeResolver.GetFactor(Character1.BaseType, Character2.BaseType);
                        Character2.ReceiveAttack(skillFromCharacter1, multiplicateur);
                    }
                }
                else
                {
                    multiplicateur = TypeResolver.GetFactor(Character1.BaseType, Character2.BaseType);
                    Character2.ReceiveAttack(skillFromCharacter1, multiplicateur);


                    if (Character2.IsAlive)
                    {
                        multiplicateur = TypeResolver.GetFactor(Character2.BaseType, Character1.BaseType);
                        Character1.ReceiveAttack(skillFromCharacter2, multiplicateur);
                    }
                }
            }

            //Character1.CurrentStatus
        }

    }
}
