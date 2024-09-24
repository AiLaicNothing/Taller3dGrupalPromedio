using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taller3DGrupal
{
    internal class Enemy
    {
        private string name;
        public int hP;
        public int damage;
        public int turn;

        public Enemy(string nm, int hp, int dmg, int trn)
        {
            this.name = nm;
            this.hP = hp;
            this.damage = dmg;
            this.turn = trn;
        }

        public void EnemyAttack(int strtrHP, string structure)
        {
            strtrHP -= damage;
            Console.WriteLine($"The enemy {name} has dealt {damage} damage to {structure}.");
        }
        
        public void RemainingHP(int damageDefStructure)
        {
            this.hP  -= damageDefStructure;
            if ( this.hP > 0)
            {
                Console.WriteLine($"The enemy {name} has taken {damageDefStructure} damage.");
            }
            else if ( this.hP <= 0)
            {
                Console.WriteLine($"The enemy {name} has been defeated.");
            }
        }

        public void GetTurn()
        {
            turn++;
        }

        public string GetInfo()
        {
            return $"{name}: Health:{hP}, Damage:{damage}.";
        }
        
    }
}
