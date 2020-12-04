using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D4
{
    class Passport
    {
        public string byr;
        public string iyr;
        public string eyr;
        public string hgt;
        public string hcl;
        public string ecl;
        public string pid;
        public string cid;

        public Passport()
        {
            byr = "";
            iyr = "";
            eyr = "";
            hgt = "";
            hcl = "";
            ecl = "";
            pid = "";
            cid = "";
        }
    }


    class Program
    {
        static private void D4a()
        {
            List<Passport> passports = new List<Passport>();
            using (StreamReader input = File.OpenText("d:\\programming\\Advent of Code\\data 2020\\D4\\input.txt"))
            {
                string line = "";
                Passport p = null;
                while ((line = input.ReadLine()) != null)
                {
                    if (p == null)
                        p = new Passport();

                    if (line.Length == 0)
                    {
                        passports.Add(p);
                        p = null;
                    }
                    else
                    {
                        string[] t1 = line.Split(' ');
                        for (int i = 0; i < t1.Length; i++)
                        {
                            string[] t2 = t1[i].Split(':');
                            switch (t2[0])
                            {
                                case "byr":
                                    p.byr = t2[1];
                                    break;
                                case "iyr":
                                    p.iyr = t2[1];
                                    break;
                                case "eyr":
                                    p.eyr = t2[1];
                                    break;
                                case "hgt":
                                    p.hgt = t2[1];
                                    break;
                                case "hcl":
                                    p.hcl = t2[1];
                                    break;
                                case "ecl":
                                    p.ecl = t2[1];
                                    break;
                                case "pid":
                                    p.pid = t2[1];
                                    break;
                                case "cid":
                                    p.cid = t2[1];
                                    break;
                            }
                        }
                    }
                }
                if (p != null)
                    passports.Add(p);
            }

            int count = passports.FindAll(p => p.byr.Length > 0 && p.ecl.Length > 0 && p.eyr.Length > 0 && p.hcl.Length > 0 && p.hgt.Length > 0 && p.iyr.Length > 0 && p.pid.Length > 0).Count();

            Console.WriteLine(count);

            Console.WriteLine("end");
            Console.ReadLine();
        }


        static private void D4b()
        {
            List<Passport> passports = new List<Passport>();
            using (StreamReader input = File.OpenText("d:\\programming\\Advent of Code\\data 2020\\D4\\input.txt"))
            {
                string line = "";
                Passport p = null;
                while ((line = input.ReadLine()) != null)
                {
                    if (p == null)
                        p = new Passport();

                    if (line.Length == 0)
                    {
                        passports.Add(p);
                        p = null;
                    }
                    else
                    {
                        string[] t1 = line.Split(' ');
                        for (int i = 0; i < t1.Length; i++)
                        {
                            string[] t2 = t1[i].Split(':');
                            switch (t2[0])
                            {
                                case "byr":
                                    p.byr = t2[1];
                                    break;
                                case "iyr":
                                    p.iyr = t2[1];
                                    break;
                                case "eyr":
                                    p.eyr = t2[1];
                                    break;
                                case "hgt":
                                    p.hgt = t2[1];
                                    break;
                                case "hcl":
                                    p.hcl = t2[1];
                                    break;
                                case "ecl":
                                    p.ecl = t2[1];
                                    break;
                                case "pid":
                                    p.pid = t2[1];
                                    break;
                                case "cid":
                                    p.cid = t2[1];
                                    break;
                            }
                        }
                    }
                }
                if (p != null)
                    passports.Add(p);
            }

            List<Passport> filtered = passports.FindAll(p => p.byr.Length > 0 && p.ecl.Length > 0 && p.eyr.Length > 0 && p.hcl.Length > 0 && p.hgt.Length > 0 && p.iyr.Length > 0 && p.pid.Length > 0);
            filtered = filtered.FindAll(p => Convert.ToInt32(p.byr) >= 1920 && Convert.ToInt32(p.byr) <= 2002);
            filtered = filtered.FindAll(p => Convert.ToInt32(p.iyr) >= 2010 && Convert.ToInt32(p.iyr) <= 2020);
            filtered = filtered.FindAll(p => Convert.ToInt32(p.eyr) >= 2020 && Convert.ToInt32(p.eyr) <= 2030);
            filtered = filtered.FindAll(p => (p.hgt.Contains("cm") && Convert.ToInt32(p.hgt.Replace("cm", "")) >= 150 && Convert.ToInt32(p.hgt.Replace("cm", "")) <= 193)
                || (p.hgt.Contains("in") && Convert.ToInt32(p.hgt.Replace("in", "")) >= 59 && Convert.ToInt32(p.hgt.Replace("in", "")) <= 76));
            filtered = filtered.FindAll(p => p.hcl.Length == 7 && p.hcl.StartsWith("#"));
            filtered = filtered.FindAll(p => p.ecl == "amb" || p.ecl == "blu" || p.ecl == "brn" || p.ecl == "gry" || p.ecl == "grn" || p.ecl == "hzl" || p.ecl == "oth");
            filtered = filtered.FindAll(p => p.pid.Length == 9);

            Console.WriteLine(filtered.Count());

            Console.WriteLine("end");
            Console.ReadLine();
        }


        static void Main(string[] args)
        {
            //D4a();

            D4b();
        }
    }
}
