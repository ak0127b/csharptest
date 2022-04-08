using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    [Serializable]
    internal abstract class Contragent : IComparable<Contragent>
    {
        public int ID { get; set; }
        public int INN { get; set; }
        public int BIN { get; set; }
        public string AvtorSoz { get; set; }
        public DateTime DateCreate { get; set; }
        public DateTime DateChange { get; set; }
        public string AvtorIzm { get; set; }
        public abstract void Sortirovka();

        public Contragent(int id, string name)
        {
            ID = id;
            AvtorSoz = name;
        }

        public int CompareTo(Contragent other)
        {
            var res = INN.CompareTo(other.INN);
            if (res == 0)
                res = AvtorSoz.CompareTo(other.AvtorSoz);

            return res;
        }

        public override string ToString()
        {
            return " " + AvtorSoz;
        }
    }

    [Serializable]
    internal class FizLico : Contragent
    {
        public string LastName { get; set; }
        public string Name { get; set; }
        public string MiddleName { get; set; }

        public FizLico(int id, string Name, string MiddleName) : base (id, Name)
        {
           
        }

        public override void Sortirovka()
        {
            
        }
    }

    [Serializable]
    internal class YrLico : Contragent
    {
        public string Names { get; set; }
        public YrLico(int id, string Names) : base(id, Names)
        {
        }

        public override void Sortirovka()
        {

        }
    }

    internal class Program
    {
        private static void Main(string[] args)
        {

            var fizlico = new List<Contragent>();
            fizlico.Add(new FizLico(1, "Шерхан", "Сыздыков"));
            fizlico.Add(new FizLico(2, "Айнура", "Куанышбекова"));
            fizlico.Add(new FizLico(3, "Ерхан", "Мауяев"));
            foreach (Contragent yrlico in fizlico)
                yrlico.Sortirovka();

            Console.WriteLine("--------------------------------------------");
            Console.WriteLine("Запись в файл ...");
            using (var fs = new FileStream("C:\\temp.bin", FileMode.Create))
                new BinaryFormatter().Serialize(fs, fizlico);

            Console.WriteLine("--------------------------------------------");
            Console.WriteLine("Чтение из файла ...");
            using (var fs = new FileStream("C:\\temp.bin", FileMode.Open))
                try
                {
                    fizlico = (List<Contragent>)new BinaryFormatter().Deserialize(fs);
                }
                catch (Exception)
                {
                    Console.WriteLine("Incorrect file format or file does not exist");
                    Console.ReadLine();
                    return;
                }

            Console.WriteLine("--------------------------------------------");
            Console.WriteLine("Сортировка:");

            fizlico.Sort();

            foreach (Contragent yrlico in fizlico)
                Console.WriteLine(yrlico);

            Console.ReadKey();
        }
    }
}
