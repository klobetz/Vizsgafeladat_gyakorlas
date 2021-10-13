using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pilotak
{
    class Program
    {
        static void Main(string[] args)
        {
            //gyakorlás dátum megjelenítés
            //var datum = DateTime.Now;
            //Console.WriteLine(datum.ToString("yyyy. MMMM dd. dddd"));

            //var datum2 = DateTime.UtcNow;
            //Console.WriteLine(datum2.ToShortDateString());

            //var datum3 = DateTime.Today.Year;
            //Console.WriteLine(datum3);

            var list = new List<adatszerkezet>();
            using (var fs = new FileStream("pilotak.csv", FileMode.Open))
            {
                using (var sr = new StreamReader(fs,Encoding.UTF8))
                {
                    sr.ReadLine();
                    while (!sr.EndOfStream)
                    {
                        var sor = sr.ReadLine().Split(';');
                        var a = new adatszerkezet();
                        a.Nev = sor[0];
                        a.Szulido = DateTime.Parse(sor[1]); //vagy Convert.ToDateTime(sor[1]);
                        a.Nemzetiseg = sor[2];

                        if (!string.IsNullOrEmpty(sor[3]))
                        {
                            a.Rajtszam = int.Parse(sor[3]); // vagy Convert.ToInt32(sor[3])
                        }

                        //vagy 1.
                        //int.TryParse(sor[3], out int szam);
                        //a.Rajtszam = szam;

                        //vagy 2.
                        //a.Rajtszam = !string.IsNullOrEmpty(sor[3]) ? 0 : int.Parse(sor[3])

                        //vagy 3. 
                        //a.Rajtszam = int.TryParse(sor[3], out int szam) ? szam : 0;

                        //vagy 4. 
                        //if (int.TryParse(sor[3], out int szam))
                        //{
                        //    a.Rajtszam = szam;
                        //}
                        //else
                        //{
                        //    a.Rajtszam = 0;
                        //}

                        list.Add(a);
                    }
                }
                //list.ForEach(x => Console.WriteLine($"{x.Nev} {x.Szulido.ToShortDateString()} {x.Nemzetiseg} {x.Rajtszam}"));

                //3.feladat
                Console.WriteLine($"3.feladat: {list.Count()}");

                //4.feladat
                Console.WriteLine($"4.feladat: {list.Last().Nev}");

                //5.feladat:
                Console.WriteLine("5.feladat:");
                list.Where(x=>x.Szulido < Convert.ToDateTime("1901.01.01."))
                    .ToList()
                    .ForEach(x => Console.WriteLine($"\t{x.Nev} ({x.Szulido.ToShortDateString()})"));

                //6.feladat
                
                var nemzetiseg = list.Where(x => x.Rajtszam != 0)
                                     .OrderBy(x => x.Rajtszam)
                                     .First().Nemzetiseg;
                Console.WriteLine($"6.feladat: {nemzetiseg}");


                var nemzetiseg2 = list.FindAll(x => x.Rajtszam > 0)
                                     .OrderBy(x => x.Rajtszam)
                                     .First().Nemzetiseg;
                Console.WriteLine($"6.feladat: {nemzetiseg2}");


                var nemzetiseg3 = list.Where(x => x.Rajtszam > 0)
                                     .Min(x=>x.Rajtszam);
                list.Where(x => x.Rajtszam == nemzetiseg3).ToList().ForEach(x => Console.WriteLine($"6.feladat: {x.Nemzetiseg}"));


                var megold1 = list.FirstOrDefault(x=>x.Rajtszam == nemzetiseg3);
                Console.WriteLine($"6.feladat: {megold1.Nemzetiseg}");

                //7.feladat
                var rajtszamoslista = list.Where(x=>x.Rajtszam!=0).ToList();
                rajtszamoslista.GroupBy(x => x.Rajtszam)
                    .Where(x=>x.Count()>1)
                    .Select(x=>x.Key)
                    .ToList()
                    .ForEach(x => Console.Write($"{x}, "));
                
                Console.WriteLine();
                list.Where(x => x.Rajtszam != 0)
                .GroupBy(x => x.Rajtszam)
                    .Where(x => x.Count() > 1)
                    .Select(x => x.Key)
                    .ToList()
                    .ForEach(x => Console.Write($"{x}, "));
                
                Console.WriteLine();
                list.GroupBy(x=>x.Rajtszam)
                    .Where(x=>x.Key != 0 && x.Count()>1)
                    .ToList()
                    .ForEach(x => Console.Write($"{x.Key}, "));
                
            }

            Console.ReadLine();
        }
        
    }
}
