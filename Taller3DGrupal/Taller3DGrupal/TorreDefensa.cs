using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taller3DGrupal
{
    internal class TorreDefensa: Structure
    {
        public int damage;
        
        public TorreDefensa() 
        {
            name = "TorreDefensa";
            hp = 30;
            price = 10;
            description = $"Deal {damage} damage to one enemy";
        }

        public override string GetInfo()
        {
            return base.GetInfo();
        }

        public override int Build(int money)
        {
            return base.Build(money);
        }

        public override int Function(int value)
        {
            return base.Function(value);
        }

        public override int GetDamaged(int enemyDamage)
        {
            return base.GetDamaged(enemyDamage);
        }
    }
}
