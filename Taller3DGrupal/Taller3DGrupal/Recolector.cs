using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taller3DGrupal
{
    internal class Recolector: Structure
    {
        private int MoneyGeneration = 18;
        public Recolector()
        {
            name = "Recolector";
        hp = 20;
        price = 15;
            description = " generates 10$ per turn";
        }
        public override string GetInfo()
        {
            return base.GetInfo();
               }
        public override int Build(int money)
        {
            return money =- price;
        }
        public override int Function(int money)
        {
            return money += MoneyGeneration;
        }
        public override int GetDamaged(int enemydamage)
        { 
        return base.GetDamaged(enemydamage);    
        }
    }
        }
    

