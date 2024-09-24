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

        //enemy
        int spawnCount = 1;
        int v1 = 0;

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
            TorreDefensa defensa = new TorreDefensa();
            TipoDeEstructura.Add(recolector);
            TipoDeEstructura.Add(mantenimiento);
            TipoDeEstructura.Add(defensa);

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
                EnemyAlive();

                if (PlayersStructures.Count <= 0)
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
                Console.Clear();
                Console.WriteLine($"[Turn {wave}]");
                Console.WriteLine("[Player Turn]");
                Console.WriteLine($"[Money = {playerMoney}]");
                Console.WriteLine($"[Structures = {PlayersStructures.Count}/{maxStructure}]");
                Console.WriteLine("Select a action");
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
                            Console.WriteLine("You have reached the max building you can have");
                        }
                        playerTurn = false;
                        break;

                    case "3":
                        Console.Clear();
                        //Enseñar todos los enemigos
                        if (enemies.Count <= 0)
                        {
                            Console.WriteLine("There's no enemy alive");
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
                        Console.WriteLine("You dont have enough money to build");
                        Console.ReadKey();
                    }
                    else
                    {
                        playerMoney = recolector.Build(playerMoney);
                        PlayersStructures.Add(recolector);
                        Console.WriteLine($"It have been added a {recolector.Name}");
                        Console.ReadKey();
                    }
                    break;

                case 2:
                    Maintenance_structure mantenimiento = new Maintenance_structure();
                    if (playerMoney < mantenimiento.Price)
                    {
                        Console.WriteLine("You dont have enough money to build");
                        Console.ReadKey();
                    }
                    else
                    {
                        playerMoney = mantenimiento.Build(playerMoney);
                        PlayersStructures.Add(mantenimiento);
                        Console.WriteLine($"It have been added a {mantenimiento.Name}");
                        Console.ReadKey();
                    }
                    break;

                case 3:
                    TorreDefensa defensa = new TorreDefensa();
                    if (playerMoney < defensa.Price)
                    {
                        Console.WriteLine("You dont have enough money to build");
                        Console.ReadKey();
                    }
                    else
                    {
                        playerMoney = defensa.Build(playerMoney);
                        PlayersStructures.Add(defensa);
                        Console.WriteLine($"It have been added a {defensa.Name}");
                        Console.ReadKey();
                    }
                    break;
            }
        }

        public void StructureFunction()
        {
            int increaseMaxStruc = 0;

            foreach (var structure in PlayersStructures)
            {
                if (structure is Recolector recolector)
                {
                    playerMoney = recolector.Function(playerMoney);
                }
                if (structure is Maintenance_structure maintanece)
                {
                    int ammount = PlayersStructures.OfType<Maintenance_structure>().Count();
                    //int increase =  maintanece.Function(increaseMaxStruc);
                    maxStructure = minStructure + (ammount * 2);
                }
                if (structure is TorreDefensa defender)
                {
                    if (enemies.Count() > 0)
                    {

                        var firstEnemy = enemies[0];
                        firstEnemy.RemainingHP(defender.damage);
                        if(firstEnemy.hP <= 0)
                        {
                            enemies.RemoveAt(0);
                        }
                    }
                }
            }
        }
        public void EnemyTurn()
        {
            Console.Clear();
            Console.WriteLine("Enemy Turn");
            SpawnEnemy();
            EnemyAttack();
            Console.ReadKey();
        }

        public void SpawnEnemy()
        {
            bool canSpawn = false;
            if (enemies.Count <= 0)
            {
                canSpawn = true;
                int v2 = 1;
                for (int i = 0; i < spawnCount; i++)
                {
                    int temp1;
                    temp1 = v1;
                    v1 = v2;
                    v2 = temp1 + v1;
                }
                spawnCount++;
            }
            int spawnTime = 0;
            if (canSpawn)
            {
                while (spawnTime < v1)
                {
                    Enemy newEnemy = new Enemy("goblin", 10, 8, 0);
                    enemies.Add(newEnemy);
                    spawnTime++;
                }
                Console.WriteLine($"Spawned {v1} enemies.");
                canSpawn = false;
            }
        }

        public void ShowEnemy()
        {
            int count = 0;
            foreach (Enemy enemy in enemies)
            {
                count++;
                Console.WriteLine($"{count}. {enemy.GetInfo()}- {enemy.turn} turns alive");
            }
        }

        public void EnemyAttack()
        {
            foreach (Enemy enemy in enemies)
            {
                if (enemy.turn >= 1)
                {
                    if (PlayersStructures.Exists(structure => structure is TorreDefensa))
                    {
                        enemy.EnemyAttack(PlayersStructures.OfType<TorreDefensa>().First().Hp, PlayersStructures.OfType<TorreDefensa>().First().Name);
                        PlayersStructures.OfType<TorreDefensa>().First().GetDamaged(enemy.damage);
                        if (PlayersStructures.OfType<TorreDefensa>().First().Hp <= 0)
                        {
                            PlayersStructures.Remove(PlayersStructures.OfType<TorreDefensa>().First());
                        }
                    }
                    else if (PlayersStructures.Exists(structure => structure is Maintenance_structure))
                    {
                        enemy.EnemyAttack(PlayersStructures.OfType<Maintenance_structure>().First().Hp, PlayersStructures.OfType<Maintenance_structure>().First().Name);
                        PlayersStructures.OfType<Maintenance_structure>().First().GetDamaged(enemy.damage);
                        if (PlayersStructures.OfType<Maintenance_structure>().First().Hp <= 0)
                        {
                            PlayersStructures.Remove(PlayersStructures.OfType<Maintenance_structure>().First());
                        }
                    }
                    else if (PlayersStructures.Exists(structure => structure is Recolector))
                    {
                        enemy.EnemyAttack(PlayersStructures.OfType<Recolector>().First().Hp, PlayersStructures.OfType<Recolector>().First().Name);
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

        public void EnemyAlive()
        {
            if(enemies.Count > 0)
            {
                foreach (Enemy enemys in enemies)
                {
                    if (enemys.hP > 0)
                    {
                        enemys.GetTurn();
                    }
                }
                enemies.RemoveAll(enemies => enemies.hP <=0);
            }
        }
    }
}
