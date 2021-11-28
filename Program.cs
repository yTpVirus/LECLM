using System;
using System.Collections.Generic;
using System.Reflection;
using System.IO;

namespace LECLM
{
    class Program
    {
        private static List<string> maps = new List<string>();
        static void Main(string[] args)
        {
            var path = Assembly.GetEntryAssembly().GetName().CodeBase;
            var s = Path.GetDirectoryName(path).Replace("net5.0", "");
            Console.WriteLine($"Caminho Encontrado: {s}");
            Console.WriteLine($"Confirme Pressionando Enter..");
            Console.ReadLine();
            //StreamReader rd = new StreamReader();
            var t = Directory.GetFiles(Directory.GetParent(Directory.GetCurrentDirectory()).FullName, "*", SearchOption.TopDirectoryOnly);
            //
            Console.WriteLine($"Numero de Arquivos Encontrados: {t.Length}");
            Console.WriteLine($"Confirme o Escaneamento Pressionando Enter..");
            //Break
            Console.ReadLine();
            maps.Clear();
            DirectoryInfo raiz = Directory.GetParent(t[0]);
            Console.WriteLine($"Caminho Raiz: {raiz.FullName}");
            for (int i = 0; i < t.Length; i++)
            {
                var filename = t[i].Replace(raiz.FullName, "").Remove(0, 1);
                var FullDirec = t[i];
                if (filename.Contains(".map"))
                {
                    if(filename != "List.txt")
                    {
                        Console.WriteLine($"-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-");
                        maps.Add(filename);
                        LineWrite(FullDirec, "SetMinZ(0.1);", filename);
                    }
                }
            }
            //Cria o Arquivo List.txt;
            string listd = $"{raiz.FullName}/List.txt";
            var fls = File.Create(listd);
            fls.Close();
            Console.WriteLine("");
            Console.WriteLine("===============++++++++++++++++++++++++++++++++++++==================");
            Console.WriteLine("");
            //
            for (int i = 0; i < maps.Count; i++)
            {
                LineWrite(listd,maps[i],"List.txt");
            }
            Console.WriteLine("Processo Finalizado Com Sucesso!");
            Console.ReadLine();
        }

        static void LineWrite(string path, string text,string Filename)
        {
            var hold = File.ReadAllText(path);
            if (Filename == "List.txt")
            {
                if (Filename != text) 
                {
                    File.AppendAllText(path, text + Environment.NewLine);
                    Console.WriteLine($"Setando Mapa {text} No {Filename}");
                    return;
                } 
            }
            if (!hold.Contains(text)) 
            {
                File.AppendAllText(path, text + Environment.NewLine);
                Console.WriteLine($"Setando SetMinZ no Arquivo {Filename}");
                return; 
            }
            Console.WriteLine($"Pulando Arquivo {Filename}! (SetMinZ Encontrado)");
        }
    }
}
