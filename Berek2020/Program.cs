using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Berek2020
{
    class Program
    {
        static void Main(string[] args)
        {
            var list = new List<Adatszerkezet>();
            using (var fs = new FileStream("Berek2020.txt", FileMode.Open))
            {
                using (var sr = new StreamReader(fs, Encoding.UTF8))
                {
                    sr.ReadLine();
                    while (!sr.EndOfStream)
                    {
                        var sor = sr.ReadLine().Split(';');
                        var a = new Adatszerkezet();
                        a.Nev = sor[0];
                        a.Nem = sor[1];
                        a.Reszleg = sor[2];
                        a.BelepesEve = int.Parse(sor[3]);
                        a.Ber = int.Parse(sor[4]);
                        list.Add(a);
                    }
                }
            }

            //foreach (var item in list)
            //{
            //    Console.WriteLine($"{item.Nev}  {item.Nem} {item.Reszleg} {item.BelepesEve} {item.Ber}");
            //}

            //3.feladt:
            Console.WriteLine($"3.feladat: Dolgozók Száma: {list.Count()} fő");

            //4. feladat:
            Console.WriteLine($"4.feladat: Bérek átlaga: {list.Average(x => x.Ber) / 1000: 00.0} eFt"); //vagy
            Console.WriteLine($"4.feladat: Bérek átlaga: {list.Average(x => x.Ber) / 1000:f5} eFt");

            //5.feladat:
            Console.Write("5.feladat: Kérem a részleg nevét: ");
            var valasz = Console.ReadLine();

            //6.feladat:
            try
            {
                var maxber = list.Where(x => x.Reszleg == valasz).OrderByDescending(x => x.Ber).First();
                Console.WriteLine($"6.feladat: A legtöbbet kereső dolgozó a megadott részlegen:\n" +
                    $"\tNév: {maxber.Nev}\n" +
                    $"\tNem: {maxber.Nem.Length}\n" +
                    $"\tBelépés: {maxber.BelepesEve}\n" +
                    $"\tBér: {maxber.Ber:### ###} Forint");
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine("A megadott részleg nem létezik a cégnél!");
            }
            catch (Exception)
            {
                Console.WriteLine("Egyébb hiba");
            }

            //public void hatos()
            //{
            //    if (list.FindAll(x => x.Reszleg.ToLower() == valasz.ToLower()).Count() == 0)
            //    {
            //        Console.WriteLine("A megadott részleg nem létezik a cégnél!");
            //        return;
            //    }
            //    var maxber2 = list.Where(x => x.Reszleg == valasz).OrderByDescending(x => x.Ber).First();
            //    Console.WriteLine($"6.feladat: A legtöbbet kereső dolgozó a megadott részlegen:\n" +
            //                $"\tNév: {maxber2.Nev}\n" +
            //                $"\tNem: {maxber2.Nem}\n" +
            //                $"\tBelépés: {maxber2.BelepesEve}\n" +
            //                $"\tBér: {maxber2.Ber:### ###} Forint");
            //}

            //vagy de függvényben
            //if (!Dolgozok.Exists(x => x.Reszleg == reszleg))
            //{
            //    Console.WriteLine("6. feladat: A megadott részleg nem létezik a cégnél!");
            //    return;
            //}

            //7.feladat:
            list.GroupBy(x => x.Reszleg).ToList().ForEach(x => Console.WriteLine($"\t{x.Key} - {x.Count()} fő"));


            Console.ReadLine();
        }
        
    }
}
