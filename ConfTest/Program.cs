using System;
using System.IO;
using LibConf;

namespace ConfTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = Conf.CreateConfig(ConfigType.YAML, Directory.GetCurrentDirectory(), "confTest");

            config.SetInt("int", 69);
            config.SetInt("section", "int", 420);
            config.SetInt("Fancy Section", "int", 3);

            Console.WriteLine($"no section: {config.GetInt("int")}");
            Console.WriteLine($"section: {config.GetInt("section", "int")}");
            Console.ReadLine();
        }
    }
}
