using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace DND_Elmir_Bekric
{
    public enum ItemType
    {
        Helmet,
        Chest,
        Leggs,
        Boots,
        Shield,
        OneHanded,
        TwoHanded,
        Bow
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                int level = 0;
                int luck = 0;
                //Eingabe
                try
                {
                    Console.Write("Enter level (1-100): ");
                    level = int.Parse(Console.ReadLine());

                    Console.Write("Enter luck (1-20): ");
                    luck = int.Parse(Console.ReadLine());
                }
                catch (FormatException e)
                {
                    Console.WriteLine($"Invalid input. Please enter a valid number: {e.Message}");
                }

                if (level > 0 && luck > 0)
                {
                    GenerateItem(level, luck);
                }
            }
        }
        static public void GenerateItem(int level, int luck)
        {
            Random random = new Random();
            Boolean continuGeneratinge = true;

            //Generiert Items solange nicht "N"
            while (continuGeneratinge)
            {
                //Item-Level ranges
                int minRange = Math.Max(level - 10 + luck, 0);
                int maxRange = level + 10;

                //Item-Typ Enum
                ItemType itemType = (ItemType)random.Next(0, 8);
                bool isArmor = itemType < ItemType.OneHanded;

                int itemStat = random.Next(minRange, maxRange);

                //Falls 20 dann neu gewürfelt als Legendäres Item
                bool isLegendary = random.Next(1, 21) == 20;

                if (isLegendary)
                {
                    itemStat = random.Next(maxRange, maxRange + 10);
                }

                //Generierung eines Rüstungs Items
                if (isArmor)
                {
                    Armor armorItem = new Armor
                    {
                        Type = itemType,
                        Defense = itemStat,
                        IsLegendary = isLegendary
                    };
                    Console.WriteLine($"Generated {(armorItem.IsLegendary ? ", Legendary" : "")}Armor item: {armorItem.Type}, Defense: {armorItem.Defense}");
                }
                //Generierung eines Waffen ITems
                else
                {
                    Weapon weaponItem = new Weapon
                    {
                        Type = itemType,
                        //Falls 2-Hand doppelter Schaden
                        Damage = itemType == ItemType.TwoHanded ? itemStat * 2 : itemStat,
                        IsLegendary = isLegendary
                    };
                    Console.WriteLine($"Generated {(weaponItem.IsLegendary ? ", Legendary" : "")}Weapon item: {weaponItem.Type},  Attack: {weaponItem.Damage}");

                }

                Console.WriteLine("Do you want to continue with these stats? Y/N");
                string continueInput = Console.ReadLine().ToUpper();
                continuGeneratinge = continueInput.Equals("Y");
            }
        }
    }
}
