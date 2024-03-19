namespace Paninoteca
{
    internal class Program
    {
        static void Main(string[] args)
        {
            BusinesPanino gestore = new BusinesPanino();
            if(gestore.inserisciPanino(new Panino("sobbrio", "panino zozzoso", 10.2f, true)))
                Console.WriteLine("ho inserito il panino");
            else
                Console.WriteLine("elemento gia inserito o errore generico");

            List<Panino> lista = gestore.getLista();

            foreach(Panino p in lista) 
                Console.WriteLine($"i panini sono: { p.Nome}");

            lista = gestore.getListaVegano();

            foreach (Panino p in lista)
                Console.WriteLine($"i panini vegani sono: {p.Nome}");
            
            lista = gestore.getListaVeganoBetter();

            foreach (Panino p in lista)
                Console.WriteLine($"i panini vegani sono: {p.Nome}");

            Console.WriteLine($"la count degli elemento è: {gestore.getNElementi()}");
            Console.WriteLine($"la media del prezzo degli elemento è: {gestore.getMedia()}");
        }
    }
}
