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
        public int minStructure = 4;
        public int maxStructure = 4;
        int wave;

        private List<Structure> TipoDeEstructura;
        private List<Structure> PlayersStructures;

        private List<Enemy> enemies;

        public Menu()
        {
            TipoDeEstructura = new List<Structure>();
            PlayersStructures = new List<Structure>();
            enemies = new List<Enemy>();
        }
        public void Execute()
        {
            StructureType();
            StartGame();
        }
        public void StructureType()//estructuras existentes
        {

            Recolector recolector = new Recolector();
            Maintenance_structure mantenimiento = new Maintenance_structure();
            TipoDeEstructura.Add(recolector);
            TipoDeEstructura.Add(mantenimiento);

        }

        public void StartGame()
        {
            wave = 1;
            bool continueWave = true;
            while (continueWave)
            {
                Console.Clear();
                Console.WriteLine($"[Wave-{wave}]");
                StructureFunction();
                PlayerTurn();
                EnemyTurn();
                if(PlayersStructures.Count <= 0)
                {
                    Console.WriteLine("[You lose]");
                    Console.WriteLine($"You have been alive for {wave} turns");
                    Console.ReadKey();
                    continueWave = false;
                }
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
                Console.WriteLine($"[Structures = {maxStructure}/{PlayersStructures.Count}]");
                Console.WriteLine("Selecciona una acción");
                Console.WriteLine("1. Show your structures");
                Console.WriteLine("2. Create structures");
                Console.WriteLine("3. Show enemies");
                Console.WriteLine("4. Pass turn");

                string option = Console.ReadLine();
                switch (option)
                {
                    case "1":
                        Console.Clear();
                        //Enseñar estructuras del jugador
                        ShowPlayerStructures();
                        Console.ReadKey();
                        break;

                    case "2":
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

                    case "3":
                        Console.Clear();
                        //Enseñar todos los enemigos
                        if (enemies.Count <= 0)
                        {
                            Console.WriteLine("There's no enemy in game");
                        }
                        else
                        {
                            ShowEnemy();
                        }                        
                        Console.ReadKey();
                        break;

                    case "4":
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
                count++;
                Console.WriteLine($"{count}. {structure.Name} - HP:{structure.Hp} ");
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

            int option = int.Parse(Console.ReadLine());
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
                        playerMoney= recolector.Build(playerMoney);
                        PlayersStructures.Add(recolector);
                        Console.WriteLine($"se añadió un {recolector.Name}");
                        Console.ReadKey();
                    }
                    break;

                case 2:
                    Maintenance_structure mantenimiento = new Maintenance_structure();
                    if (playerMoney < mantenimiento.Price)
                    {
                        Console.WriteLine("No tienes suficiente dinero");
                    }
                    else
                    {
                        playerMoney = mantenimiento.Build(playerMoney);
                        PlayersStructures.Add(mantenimiento);
                        Console.WriteLine($"se añadio un {mantenimiento.Name}");
                        Console.ReadKey();
                    }
                    break;

                case 3:
                    break;
            }
        }

        public void StructureFunction()
        {
            foreach (var structure in PlayersStructures)
            {
                if(structure  is Recolector recolector)
                {
                    playerMoney = recolector.Function(playerMoney);
                }
                if (structure is Maintenance_structure maintanece)
                {
                    int ammount = PlayersStructures.OfType<Maintenance_structure>().Count();
                    maxStructure = minStructure + ammount;
                }
            }
        }
        public void EnemyTurn()
        {
            Console.WriteLine("Enemy Turn");
            SpawnEnemy();
            EnemyAttack();
            Console.ReadKey();
        }

        public void SpawnEnemy()
        {
            Enemy newEnemy=new Enemy("name",10,5,0);
            if (enemies.Count <= 0)
            {
                int spawnCount = 1;
                int v1 = 0;
                int v2 = 1;
                for (int i = 0; i < spawnCount; i++)
                {
                    int temp1;
                    temp1 = v1;
                    v1 = v2;
                    v2 = temp1 + v1;
                }
                spawnCount++;

                bool canSpawn = true;
                int spawnTime = 0;
                while (canSpawn)
                {
                    if (spawnTime < v1)
                    {
                        spawnTime++;
                        enemies.Add(newEnemy);
                    }
                    canSpawn = false;
                }
                Console.WriteLine($"Spawn {v1} enemies.");
            }            
        }

        public void ShowEnemy()
        {
            int count = 0;
            foreach (Enemy enemy in enemies)
            {
                count++;
                Console.WriteLine($"{count}. {enemy.GetInfo()}");
            }
        }

        public void EnemyAttack()
        {
            Console.Clear();
            foreach (Enemy enemy in enemies)
            {
                if(PlayersStructures.Exists(structure=> structure is Maintenance_structure))
                {
                    enemy.EnemyAttack(PlayersStructures.OfType<Maintenance_structure>().First().Hp);
                    PlayersStructures.OfType<Maintenance_structure>().First().GetDamaged(enemy.damage);
                    if (PlayersStructures.OfType<Maintenance_structure>().First().Hp <= 0)
                    {
                        PlayersStructures.Remove(PlayersStructures.OfType<Maintenance_structure>().First());
                    }
                }
                else if (PlayersStructures.Exists(structure => structure is Recolector))
                {
                    enemy.EnemyAttack(PlayersStructures.OfType<Recolector>().First().Hp);
                    PlayersStructures.OfType<Recolector>().First().GetDamaged(enemy.damage);
                    if (PlayersStructures.OfType<Recolector>().First().Hp <= 0)
                    {
                        PlayersStructures.Remove(PlayersStructures.OfType<Recolector>().First());
                    }
                }
                else
                {
                    Console.WriteLine("No hay estructuras");
                }
            }
        }
    }
}
