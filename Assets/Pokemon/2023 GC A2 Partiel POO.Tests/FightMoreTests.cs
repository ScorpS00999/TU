using _2023_GC_A2_Partiel_POO.Level_2;
using NUnit.Framework;
using System;

namespace _2023_GC_A2_Partiel_POO.Tests.Level_2
{
    public class FightMoreTests
    {
        // Tu as probablement remarqué qu'il y a encore beaucoup de code qui n'a pas été testé ...
        // À présent c'est à toi de créer des features et les TU sur le reste du projet

        // Ce que tu peux ajouter:
        // - Ajouter davantage de sécurité sur les tests apportés
            // - un heal ne régénère pas plus que les HP Max
            // - si on abaisse les HPMax les HP courant doivent suivre si c'est au dessus de la nouvelle valeur
        // - ajouter un equipement qui rend les attaques prioritaires puis l'enlever et voir que l'attaque n'est plus prioritaire etc)
        // - Le support des status (sleep et burn) qui font des effets à la fin du tour et/ou empeche le pkmn d'agir
            // - Gérer la notion de force/faiblesse avec les différentes attaques à disposition (skills.cs)
        // - Cumuler les force/faiblesses en ajoutant un type pour l'équipement qui rendrait plus sensible/résistant à un type
            // - L'utilisation d'objets : Potion, SuperPotion, Vitess+, Attack+ etc.
        // - Gérer les PP (limite du nombre d'utilisation) d'une attaque,
        // si on selectionne une attack qui a 0 PP on inflige 0

        // Choisis ce que tu veux ajouter comme feature et fait en au max.
        // Les nouveaux TU doivent être dans ce fichier.
        // Modifiant des features il est possible que certaines valeurs
        // des TU précédentes ne matchent plus, tu as le droit de réadapter les valeurs
        // de ces anciens TU pour ta nouvelle situation.



        [Test]
        public void HealCharacter()
        {
            var c = new Character(100, 50, 30, 20, TYPE.NORMAL);
            var potion = new PotionHeal(10);
            var punch = new Punch();

            c.ReceiveAttack(punch); // hp : 100 => 60
            c.ReceiveHeal(potion);
            Assert.That(c.CurrentHealth, Is.EqualTo(70));
        }

        [Test]
        public void BlockHealth()
        {
            var c = new Character(100, 50, 30, 20, TYPE.NORMAL);
            var potion = new PotionHeal(60);
            var punch = new Punch();

            c.ReceiveAttack(punch); // hp : 100 => 60
            c.ReceiveHeal(potion);
            Assert.That(c.CurrentHealth, Is.EqualTo(100));
        }

        [Test]
        public void PotionMalusOnHealth()
        {
            var c = new Character(100, 50, 30, 20, TYPE.NORMAL);
            var potion = new PotionMalus(10);
        
            c.ReceiveMalusHealth(potion);
            Assert.That(c.CurrentHealth, Is.EqualTo(90));
            Assert.That(c.MaxHealth, Is.EqualTo(90));
        }

        [Test]
        public void FightWithType()
        {
            Character a = new Character(100, 50, 30, 20, TYPE.WATER);
            Character b = new Character(100, 50, 30, 20, TYPE.GRASS);
            Fight f = new Fight(a, b);
            Punch p = new Punch();

            // Both uses punch
            f.ExecuteTurn(p, p);

            Assert.That(a.IsAlive, Is.EqualTo(true));
            Assert.That(b.IsAlive, Is.EqualTo(true));
            Assert.That(f.IsFightFinished, Is.EqualTo(false));
            Assert.That(b.CurrentHealth > a.CurrentHealth, Is.EqualTo(true));
        }

        [Test]
        public void AllPotionBonus()
        {
            Character a = new Character(90, 40, 30, 20, TYPE.NORMAL);
            var punch = new Punch();

            a.ReceiveAttack(punch); // hp : 90 => 50

            var potionHeal = new PotionBonus(10, TYPEPOTION.HEALTH);
            var potionAttack = new PotionBonus(10, TYPEPOTION.ATTACK);
            var potionDefense = new PotionBonus(10, TYPEPOTION.DEFENSE);
            var potionSpeed = new PotionBonus(10, TYPEPOTION.SPEED);

            a.ReceivePotion(potionHeal);
            a.ReceivePotion(potionAttack);
            a.ReceivePotion(potionDefense);
            a.ReceivePotion(potionSpeed);

            Assert.That(a.CurrentHealth, Is.EqualTo(60));
            Assert.That(a.Attack, Is.EqualTo(50));
            Assert.That(a.Defense, Is.EqualTo(40));
            Assert.That(a.Speed, Is.EqualTo(30));
        }

        [Test]
        public void ShieldWithAttack()
        {
            Character a = new Character(100, 40, 30, 20, TYPE.NORMAL);
            var shield = new Shield(100, TYPE.NORMAL);
            var punch = new Punch();

            a.EquipShield(shield);

            a.ReceiveAttack(punch);

            Assert.That(a.CurrentHealth, Is.EqualTo(100));
            Assert.That(shield.CurrentHealth, Is.EqualTo(30));
        }

        [Test]
        public void ShieldBroken()
        {
            Character a = new Character(100, 40, 30, 20, TYPE.NORMAL);
            var shield = new Shield(40, TYPE.NORMAL);
            var punch = new Punch();

            a.EquipShield(shield);
            a.ReceiveAttack(punch);

            Assert.That(a.CurrentHealth, Is.EqualTo(100));
            Assert.That(shield.CurrentHealth, Is.EqualTo(0));
            Assert.That(shield.IsNotBroken, Is.EqualTo(false));

            Assert.Throws<ArgumentNullException>(() =>
            {
                a.EquipShield(null);
            });

            a.ReceiveAttack(punch);
            Assert.That(a.CurrentHealth, Is.EqualTo(60));
        }

    }
}
