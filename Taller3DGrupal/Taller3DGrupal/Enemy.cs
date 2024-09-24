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
        private int hP;
        private int damage;
        private int turn;

        public Enemy(string nm, int hp, int dmg, int trn)
        {
            this.name = nm;
            this.hP = hp;
            this.damage = dmg;
            this.turn = trn;
        }

        public void EnemyAttack(int strtr)
        {
            strtr -= damage;
            Console.WriteLine($"The enemy {name} has dealt {damage} damage to {strtr}.");
        }
        
        public void RemainingHP(int damageDefStructure)
        {
            hP -= damageDefStructure;
            if ( hP > 0)
            {
                Console.WriteLine($"The enemy {name} has taken {damageDefStructure} damage.");
            }
            else if ( hP <= 0)
            {
                Console.WriteLine($"The enemy {name} has been defeated.");
            }
        }

        public void GetTurn(int turn)
        {
            turn++;
        }
        
    }
}
