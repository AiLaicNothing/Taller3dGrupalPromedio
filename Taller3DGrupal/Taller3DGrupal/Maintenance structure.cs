using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Taller3DGrupal
{
    internal class Maintenance_structure: Structure
    {
        private int capacidadConstruccion = 1; // Capacidad inicial de construcción
        public Maintenance_structure()
        {
            name = "Estructura de Mantenimiento";
            hp = 20;
            price = 50;
            description = "Permite construir más estructuras por turno";
        }

        public override string GetInfo()
        {
            return base.GetInfo();
        }

        public override int Build(int dineroDelJugador)
        {
            return dineroDelJugador -= precio;
        }

        // Método para realizar la función principal
        public override int Function(int MaximoDeEstructuras)
        {
            return MaximoDeEstructuras += capacidadConstruccion; // Incrementa la capacidad de construcción cada turno
        }

        // El método para recibir daño permanece igual
        public override int GetDamaged(int enemyDamage)
        {
            return base.GetDamaged(enemyDamage);
        }
    }

}
