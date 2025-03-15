namespace Proiect01
{
    public class Produs
    {
        public string Denumire;
        public int Cantitate;
        public bool EsteCumparat;

        public Produs(string denumire, int cantitate)
        {
            Denumire = denumire;
            EsteCumparat = false;
            Cantitate = cantitate;
        }

        public string GetData()
        {
            string status = "De cumparat";

            if (EsteCumparat)
            {
                status = "Achizitionat";
            }

            return $"{Denumire} - {Cantitate}buc. - {status}";
        }
    }
}