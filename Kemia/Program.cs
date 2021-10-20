using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Kemia
{
    class Program
    {
        static void Main(string[] args)
        {
            var list = new List<Adatszerkezet>();
            using (var fs = new FileStream("felfedezesek.csv", FileMode.Open))
            {
                using (var sr = new StreamReader(fs, Encoding.UTF8))
                {
                    sr.ReadLine();
                    while (!sr.EndOfStream)
                    {
                        var sor = sr.ReadLine().Split(';');
                        var a = new Adatszerkezet();
                        a.Ev = sor[0];
                        a.Nev = sor[1];
                        a.Vegyjel = sor[2];
                        a.Rendszam = int.Parse(sor[3]);
                        a.Felfedezo = sor[4];
                        list.Add(a);

                    }
                }
            }

            //foreach (var item in list)
            //{
            //    Console.WriteLine($"{item.Ev} {item.Nev} {item.Vegyjel} {item.Rendszam} {item.Felfedezo}");
            //}

            //3.feladat:
            Console.WriteLine($"3. feladat: Elemek száma: {list.Count()}");

            //4.feladat
            var okrban = list.Where(x => x.Ev.ToLower() == "ókor");
            Console.WriteLine($"4.feladat: Felfedezések Száma az ókorban: {okrban.Count()}");

            //vagy ez így
            //Console.WriteLine($"4.feladat: Felfedezések Száma az ókorban: {list.Count(x=>x.Ev=="Ókor")}");

            //5.feladat:
            Console.Write("5.feladat: kérek egy vegyjelet: ");
            var valasz = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(valasz) || valasz.Length > 2 || !Regex.IsMatch(valasz, "^[a-zA-Z]*$"))
            {
                Console.Write("5.feladat: kérek egy vegyjelet: ");
                valasz = Console.ReadLine();
            }

            //vagy Péter...
            //Console.Write("5. Feladat: Kérek egy vegyjelet: ");
            //var vegyjel = Console.ReadLine().ToLower().Trim();
            //var valid = isExists(vegyjel);

            //while (vegyjel.Length < 1 || vegyjel.Length > 2 || !valid)
            //{
            //    Console.Write("5. Feladat: Kérek egy vegyjelet: ");
            //    vegyjel = Console.ReadLine().ToLower().Trim();
            //    valid = isExists(vegyjel);
            //}

            //6.feladat:
            Console.WriteLine("6.feladat: keresés:");
            var talalat = list.FirstOrDefault(x=>x.Vegyjel.ToLower()==valasz.ToLower());
            //var talalat2 = list.Where(x=>x.Vegyjel.Contains(valasz)).ToString();
            //var talalat3 = list.Where(x=> x.Vegyjel.ToLower() == valasz.ToLower()).Select(x=>x.Vegyjel.ToLower());

            if (!(talalat is null))
            {
                Console.WriteLine
                ($"\tAz elem vegyjele: {talalat.Vegyjel}\n" +
                $"\tAz elem neve: {talalat.Nev}\n" +
                $"\tRendszáma: {talalat.Rendszam}\n" +
                $"\tA felfedezés éve: {talalat.Ev}\n" +
                $"\tFelfedező: {talalat.Felfedezo}\n");

                //list.Where(x => x.Vegyjel.ToLower() == valasz.ToLower()).ToList().ForEach(x => Console.WriteLine
                //($"\tAz elem vegyjele: {x.Vegyjel}\n" +
                //$"\tAz elem neve: {x.Nev}\n" +
                //$"\tRendszáma: {x.Rendszam}\n" +
                //$"\tA felfedezés éve: {x.Ev}\n" +
                //$"\tFelfedező: {x.Felfedezo}\n"));
            }
            else
            {
                Console.WriteLine("Nincs ilyen elem a listában!");
            }

            //7.feladat
            var maxEv = 0;
            var evLista = list.Where(x => x.Ev != "Ókor")
                              .Select(x => int.Parse(x.Ev))                             
                              .ToList();

            for (int i = 0; i < evLista.Count()-1; i++)
            {
                var kulonbseg = evLista[i+1]-evLista[i];

                if (kulonbseg>maxEv)
                {
                    maxEv = kulonbseg;
                }
                //maxEv = kulonbseg > maxEv ? kulonbseg : maxEv
            }
            Console.WriteLine($"7. Feladat: {maxEv} év volt a leghosszabb időszak két elem felfedezése között.");

            //8.feladat:
            Console.WriteLine("8.feladat: statisztika");
            //list.GroupBy(x => x.Ev).Where(x=>x.Count()>3 && x.Key!="Ókor").ToList().ForEach(x => Console.WriteLine($"{x.Key} : {x.Count()}"));
            //evLista.GroupBy(x => x).Where(x=>x.Count()>3).ToList().ForEach(x => Console.WriteLine($"{x.Key} : {x.Count()}"));

            //Juli megoldása
            //var listcount = evLista.GroupBy(x =>x)
            //                    .Where(x => x.Count() > 3)
            //                    .Select(y => new { Element = y.Key, Counter = y.Count() })
            //                    .ToList();

            //listcount.ForEach(x => Console.WriteLine($"\t{x.Element}: {x.Counter} db"));

            //vagy Péter
           list
                .FindAll(a => a.Ev.ToLower() != "ókor")
                .GroupBy(b => b.Ev)
                .Select(c => new { Ev = c.Key, elofordulas = c.Count() })
                .Where(d => d.elofordulas > 3)
                .ToList()
                .ForEach(x => Console.WriteLine($"\t{x.Ev} : {x.elofordulas}"));



            Console.ReadLine();
        }

        //Péter...
        //private static bool isExists(string userInput)
        //{
        //    var abc = "abcdefghijklmnopqrstuvwxyz".ToCharArray();
        //    var inputArray = userInput.ToCharArray();
        //    var exists = false;
        //    foreach (var ch in inputArray)
        //    {
        //        if (!abc.Contains(ch))
        //        {
        //            exists = false;
        //            break;
        //        }
        //        else
        //        {
        //            exists = true;
        //        }
        //    }
        //    return exists;
        //}

    }
}
