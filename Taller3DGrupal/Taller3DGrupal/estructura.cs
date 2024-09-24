using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taller3DGrupal
{
    internal class Structure

    {
        protected string name;
        protected string description;
        protected int price;
        protected int hp;


        public string Name
        {
            get { return name; }
        }

        public string Description
        {
            get { return description; }
        }
        public int Price
        {
            get { return price; }
        }
        public int Hp
        {
            get { return hp; }
        }
        public virtual string GetInfo()
        {
            return $"{name} - {description}- Cost:{Price}";
        }
        public virtual int Build(int money)
        {
            return money = -price;
        }
        public virtual int Function(int value)
        {
            return 0;
        }
        public virtual int GetDamaged(int enemyDamage)
        {
            return hp -= enemyDamage;
        }

    }
}
