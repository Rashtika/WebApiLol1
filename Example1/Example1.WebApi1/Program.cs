using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.AspNetCore.Builder;

namespace ConsoleApp1
{
    public class Program
    {
        private static Dictionary<int, Type> itemTypeMap = new Dictionary<int, Type>();
        private static Dictionary<int, Action<Champion>> actionMap = new Dictionary<int, Action<Champion>>();

        static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();

            //List<Champion> list = new List<Champion>();
            Champion championOne = new Champion("Bara");


            actionMap[0] = EquipAction;
            actionMap[1] = UnEquipAction;
            actionMap[2] = PrintList;

            itemTypeMap[0] = typeof(SupermenoveGache);
            itemTypeMap[1] = typeof(Shilterica);
            itemTypeMap[2] = typeof(BucketHelmet);
            itemTypeMap[3] = typeof(AmuletOfLost);

            bool quit = false;

            while (!quit)
            {
                quit = Menu(championOne);
            }

        }

        private static void EquipAction(Champion champion)
        {
            Console.WriteLine($"Available items:\n" +
                $"press\n" +
                $"0 - SupermenoveGache\n" +
                $"1 - Shilterica\n" +
                $"2 - BucketHelmet\n" +
                $"3 - AmuletOfLost");

            string option = Console.ReadLine();

            if (int.TryParse(option, out int result) && itemTypeMap.TryGetValue(result, out Type type))
            {
                Item item = (Item)Activator.CreateInstance(type);
                champion.Equip(item);
            }
        }

        private static void UnEquipAction(Champion champion)
        {
            Console.WriteLine($"Available items:\n" +
                $"press\n");
            Console.WriteLine(champion.Inventory.getDescription());

            string option = Console.ReadLine();

            if (int.TryParse(option, out int result))
            {
                champion.UnEquip(champion.Inventory.Items[result]);
            }
        }

        public static bool Menu(Champion champion)
        {
            Console.WriteLine($"Available actions:\n" +
                $"press\n" +
                $"0 - to add new item of choice\n" +
                $"1 - to remove an item of choice\n" +
                $"2 - to print list of items\n" +
                $"3 - to quit");

            string option = Console.ReadLine();
            if (int.TryParse(option, out int result))
            {
                if (actionMap.TryGetValue(result, out Action<Champion> action))
                {
                    action(champion);
                }
                else if (result == 3)
                {
                    return true;
                }
            }
            return false;
        }

        public static void PrintList(Champion champion)
        {
            Console.WriteLine(champion.Inventory.getDescription());
        }

    }
}
