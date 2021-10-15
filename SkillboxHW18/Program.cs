using Microsoft.EntityFrameworkCore;
using System;
using System.IO;

namespace SkillboxHW18
{
    class Program
    {
        static void Main(string[] args)
        {   //создаём базу данных
            string path = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\SkillboxHW18DB.mdf"));
            var random = new Random();
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseSqlServer($"Data Source=(LocalDB)\\MSSQLLocalDB; AttachDbFilename=\"{path}\";Integrated Security=True;Connect Timeout=30")
                .Options;
            using var db = new DataContext(options);
            
            db.Database.EnsureCreated();
            //наполняем базу данных героями
            for (int i = 0; i < random.Next(1, 30); i++)
            {
                db.Heroes.Add(new Hero()
                {
                    Armor = GetArmor(),
                    Wearpon = GetWearpon(),
                    Name = GetRandomName(),
                    Health = 100
                });
            }
            db.SaveChanges();
            //выводим результат в консоль
            Print(db);

        }

        /// <summary>
        /// Выводит в консоль базу данных
        /// </summary>
        /// <param name="db"></param>
        private static void Print(DataContext db)
        {
            Console.WriteLine($"{"Id",4} | {"Имя",10} | {"Доспех",10} | {"Оружие",10}\n\n");

            foreach (var hero in db.Heroes.Include(a => a.Wearpon).Include(a => a.Armor))
            {
                Console.WriteLine($"{hero.Id,4} | {hero.Name,10} | {hero.Armor.Name,10} | {hero.Wearpon.Name,10}");
            }
        }

        /// <summary>
        /// Возвращает экземляр оружия
        /// </summary>
        /// <returns></returns>
        static Wearpon GetWearpon()
        {
            Random random = new();

            return new Wearpon()
            {
                Damage = random.Next(1, 10),
                Name = GetWearponName()
            };
        }
        
        /// <summary>
        /// Возвращает экземпляр брони
        /// </summary>
        /// <returns></returns>
        static Armor GetArmor()
        {
            Random random = new();
            return new Armor()
            {
                Name = GetArmorName(),
                Defense = random.Next(1, 9)
            };
        }
        
        /// <summary>
        /// Возвращает рандомно одно из названий брони
        /// </summary>
        /// <returns></returns>
        static string GetArmorName()
        {
            Random random = new Random();

            switch (random.Next(6))
            {
                case 1: return "Chain_mail";
                case 2: return "Armor";
                case 3: return "Cloak";
                case 5: return "Jacket";
                case 6: return "Mantle";
                default: return "Nu";
            }
        }
        
        /// <summary>
        /// Возвращает рандомно одно из названий оружия
        /// </summary>
        /// <returns></returns>
        static string GetWearponName()
        {
            Random random = new Random();

            switch (random.Next(1, 7))
            {
                case 1: return "Knife";
                case 2: return "Rapier";
                case 3: return "Magic";
                case 5: return "Sword";
                case 6: return "Stick";
                default: return "Finger";
            }
        }

        /// <summary>
        /// Возвращает рандомно одно из нескольких имён 
        /// </summary>
        /// <returns></returns>
        static string GetRandomName()
        {
            Random random = new Random();

            switch (random.Next(11))
            {
                case 1: return "Matt";
                case 2: return "Rob";
                case 3: return "John";
                case 4: return "Margarett";
                case 5: return "Rose";
                case 6: return "Jim";
                case 7: return "Rocko";
                case 8: return "Polo";
                case 9: return "Mika";
                case 10: return "Elie";
                default: return "Igor";
            }
        }
    }
}
