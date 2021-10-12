using Microsoft.EntityFrameworkCore;
using System;

namespace SkillboxHW18
{
    class Program
    {
        static void Main(string[] args)
        {
            var random = new Random();
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"\\\\rttv.ru\\profile\\UserData2\\iapomelnikov\\My Documents\\SkillboxHW18DB.mdf\";Integrated Security=True;Connect Timeout=30")
                .Options;
            using var db = new DataContext(options);

            db.Database.EnsureCreated();
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
            Print(db);

        }

        private static void Print(DataContext db)
        {
            Console.WriteLine($"{"Id",4} | {"Имя",10} | {"Доспех",10} | {"Оружие",10}\n\n");

            foreach (var hero in db.Heroes.Include(a => a.Wearpon).Include(a => a.Armor))
            {
                Console.WriteLine($"{hero.Id,4} | {hero.Name,10} | {hero.Armor.Name,10} | {hero.Wearpon.Name,10}");
            }
        }

        static Wearpon GetWearpon()
        {
            Random random = new();

            return new Wearpon()
            {
                Damage = random.Next(1, 10),
                Name = GetWearponName()
            };
        }
        
        static Armor GetArmor()
        {
            Random random = new();
            return new Armor()
            {
                Name = GetArmorName(),
                Defense = random.Next(1, 9)
            };
        }
        
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
