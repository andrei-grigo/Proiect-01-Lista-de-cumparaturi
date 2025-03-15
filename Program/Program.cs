namespace Proiect01
{
    internal class Program
    {
        public static ListaCumparaturi Lista;

        private static void Main(string[] args)
        {
            Lista = new ListaCumparaturi();
            Lista.DeCumparat.Add(new Produs("Branza", 1));
            Lista.DeCumparat.Add(new Produs("Carne", 1));
            Lista.DeCumparat.Add(new Produs("Lapte", 1));
            Lista.DeCumparat.Add(new Produs("Oua", 1));
            Lista.DeCumparat.Add(new Produs("Faina", 1));
            Lista.DeCumparat.Add(new Produs("Cafea", 1));

            Lista.DeCumparat[0].EsteCumparat = true;
            Lista.Achizitionate.Add(Lista.DeCumparat[0]);
            Lista.DeCumparat.RemoveAt(0);

            Lista.DeCumparat[2].EsteCumparat = true;
            Lista.Achizitionate.Add(Lista.DeCumparat[2]);
            Lista.DeCumparat.RemoveAt(2);

            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("===============================");
                Console.WriteLine("GESTIONARE LISTA DE CUMPARATURI");
                Console.WriteLine("===============================");
                Console.ResetColor();

                Lista.AfiseazaLista();

                Console.WriteLine("\nMENIU PRINCIPAL:");
                Console.WriteLine("1: Adauga un produs");
                Console.WriteLine("2: Sterge produs(e)");
                Console.WriteLine("3: Editeaza un produs");
                Console.WriteLine("4: Bifeaza un produs ca achizitionat");
                Console.WriteLine("5: Sortare");
                Console.WriteLine("6: Cauta un produs");
                Console.WriteLine("7: Vezi ce ai cumparat");
                Console.WriteLine("8: Vezi toate produsele");
                Console.WriteLine("0: Iesire");

                int optiune;
                bool optiuneValida;
                do
                {
                    Console.Write("\nSelecteaza o optiune: ");
                    optiuneValida = int.TryParse(Console.ReadLine(), out optiune);

                    if (!optiuneValida || optiune > 8 || optiune < 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Optiune invalida! Alege un numar corect.");
                        Console.ResetColor();
                    }
                } while (!optiuneValida || optiune > 8 || optiune < 0);

                switch (optiune)
                {
                    case 0: Console.WriteLine("Iesire... La revedere!"); return;
                    case 1: Functii.AdaugaProdus(); break;
                    case 2: Functii.StergeProdus(); break;
                    case 3: Functii.EditareProdus(); break;
                    case 4: Functii.BifeazaCumparat(); break;
                    case 6: Functii.CautaProdus(); break;
                    case 7: Functii.VeziListaCumparate(); break;
                    case 8: Functii.VeziToateProdusele(); break;
                }
            }
        }
    }
}