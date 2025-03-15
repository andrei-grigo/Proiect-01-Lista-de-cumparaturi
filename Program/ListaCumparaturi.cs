namespace Proiect01
{
    public class ListaCumparaturi
    {
        public List<Produs> DeCumparat;
        public List<Produs> Achizitionate;

        public ListaCumparaturi()
        {
            DeCumparat = new List<Produs>();
            Achizitionate = new List<Produs>();
        }

        public void AfiseazaLista()
        {
            Console.WriteLine("Ai de cumparat urmatoarele produse:\n");
            bool gasit = false;
            int index = 1;

            for (int i = 0; i < DeCumparat.Count; i++)
            {
                Console.WriteLine($"{index}: {DeCumparat[i].GetData()}");
                index++;
                gasit = true;
            }

            if (!gasit)
            {
                Console.WriteLine("Lista este goala! Ai cumparat toate produsele.");
            }
        }
    }
}
