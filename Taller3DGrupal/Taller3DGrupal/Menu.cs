using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taller3DGrupal
{
    internal class Menu
    {
        bool playerTurn = true;
        public int playerMoney = 50;
        public int maxStructure = 4;

        private List<Structure> TipoDeEstructura;
        private List<Structure> PlayersStructures;

        public Menu()
        {
          TipoDeEstructura = new List<Structure>();
            PlayersStructures = new List<Structure>();
        }
        public void Execute()
        {
            StructureType();


            StartGame();
        }
        public void StructureType()//estructuras existentes
        {
        
            Recolector recolector = new Recolector();   
            TipoDeEstructura.Add( recolector );
        
        }

        public void StartGame()
        {
            int wave = 1;
            bool continueWave = true;
            while (continueWave)
            {
                Console.Clear();
                Console.WriteLine($"[Wave-{wave}]");
                PlayerTurn();
                EnemyTurn();
                wave++;
                playerTurn = true;
            }
        }
        public void PlayerTurn()
        {
            playerTurn = true;

            while (playerTurn)
            {
                Console.WriteLine("[Player Turn]");
                Console.WriteLine($"[Money = {playerMoney}]");
                Console.WriteLine($"[MaxStructure = {maxStructure}]");
                Console.WriteLine("Selecciona una acción");
                Console.WriteLine("1. Show your structures");
                Console.WriteLine("2. Create structures");
                Console.WriteLine("3. Show enemies");
                Console.WriteLine("4. Pass turn");

                int option = int.Parse(Console.ReadLine());
                switch (option)
                {
                    case 1:
                        Console.Clear();
                        //Enseñar estructuras del jugador
                        ShowPlayerStructures();
                        Console.ReadKey();
                        break;

                    case 2:
                        Console.Clear();
                        // Crear estructura
                        // crea un if (checkea si la cantidad de estructuras del jugador es igual al maximo de estrucura) si es mayor dile que no puede crear mas, sino le dejas crear nomas
                        if (PlayersStructures.Count <= maxStructure)
                        {
                            CreateStructure();

                        }
                        else
                        {
                            Console.WriteLine(" no puedes seguir construllendo");
                        }
                        playerTurn = false;
                        break;

                    case 3:
                        Console.Clear();
                        //Enseñar todos los enemigos
                        Console.ReadKey();
                        break;

                    case 4:
                        playerTurn = false;
                        break;
                }

            }
        }
        public void ShowPlayerStructures()
        {
            int count = 0;  

            Console.WriteLine("[This is a list of structures you have]");
            //Show list;
            foreach (Structure structure in PlayersStructures)
            {
              count ++;
              Console.WriteLine($"{count}. {structure.Name}  ");

            }
        }

        public void CreateStructure()
        {
            Console.WriteLine("Choose a structure to build");



            int count = 0;
            //Enseñar opciones de structuras
            foreach (Structure structure in TipoDeEstructura)
            {
                count++;
                Console.WriteLine($"{count}. {structure.GetInfo()}  ");

            }

            int option = int.Parse (Console.ReadLine());
            switch (option)
            {
                case 1:
                    //if- checkea si tiene suficiente dinero, si no tiene le dices le avisas,sino agregas a la lista y descuentas el dinero
                    Recolector recolector = new Recolector();
                    if (playerMoney < recolector.Price)
                    {
                        Console.WriteLine("No tienes suficiente dinero");

                    }
                    else
                    {
                        recolector.Build(playerMoney);
                        PlayersStructures.Add (recolector);
                        Console.WriteLine($"se añadió un {recolector.Name}");
                        Console.ReadKey ();
                    }
                    break;

                case 2:
                    break;

                case 3:
                    break;
            }
        }

        public void EnemyTurn()
        {

        }
    }
}
