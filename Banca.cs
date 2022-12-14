//Oggi cerchiamo di migliorare un pochino l’approccio nella costruzione di un codice ad oggetti, ben suddiviso tra diverse responsabilità.
//Il program.cs si occuperà quindi di gestire tutti i console.writeline/readline mentre le classi di dominio del nostro progetto devono occuparsi
//solo della logica applicativa e fare dei controlli dei dati quando questi violano qualche logica.
//Esempio, quando un cliente non può chiedere più prestiti? quale entità potrebbe occuparsi di questo controllo?
//Non dimentichiamoci però che abbiamo aggiunto la keyword static, quale parametro di quale entità potrebbe essere trasformato in STATIC così come abbiamo visto negli esempi?
//Consegna:
//Sviluppare un’applicazione orientata agli oggetti per gestire i prestiti che una banca concede ai propri clienti.
//La banca è caratterizzata da
//un nome
//un insieme di clienti (una lista)
//un insieme di prestiti concessi ai clienti (una lista)
//I clienti sono caratterizzati da
//nome,
//cognome,
//codice fiscale
//stipendio
//I prestiti sono caratterizzati da
//ID
//intestatario del prestito (il cliente),
//un ammontare,
//una rata,
//una data inizio,
//una data fine.
//Per la banca deve essere possibile:
//aggiungere, modificare e ricercare un cliente.
//aggiungere un prestito.
//effettuare delle ricerche sui prestiti concessi ad un cliente dato il codice fiscale
//sapere, dato il codice fiscale di un cliente, l’ammontare totale dei prestiti concessi.
//sapere, dato il codice fiscale di un cliente, quante rate rimangono da pagare alla data odierna.
//Per i clienti e per i prestiti si vuole stampare un prospetto riassuntivo con tutti i dati che li caratterizzano in un formato di tipo stringa a piacere.
//Bonus:
//visualizzare per ogni cliente, la situazione dei suoi prestiti in formato tabellare.




public class Banca
{
    public string Nome { get; set; }
    List<Cliente> Clienti { get; set; }
    List<Prestito> Prestiti { get; set; }

    public Banca(string Nome)
    {
        Nome = Nome;
        Clienti = new List<Cliente>();
        Prestiti = new List<Prestito>();
        Cliente Cliente1 = new Cliente("Gloria", "Gherardi", "GHRGLR4235263755", 2500);
        Cliente Cliente2 = new Cliente("Giulio", "Verdi", "GLIVRD4235263755", 1800);
        Clienti.Add(Cliente1);
        Clienti.Add(Cliente2);

        var dateNow = DateOnly.FromDateTime(DateTime.Now);
        Prestito Prestito1 = new Prestito(20000, 1, dateNow, Cliente1);
        Prestito Prestito2 = new Prestito(30000, 10, dateNow, Cliente2);
        Prestito Prestito3 = new Prestito(20000, 1, dateNow, Cliente1);
        Prestiti.Add(Prestito3);
        Prestiti.Add(Prestito2);
        Prestiti.Add(Prestito1);

    }


    public bool AggiungiCliente(string nome, string cognome, string codiceFiscale, int stipendio)
    {

        if (
            nome == null || nome == "" ||
            cognome == null || cognome == "" ||
            codiceFiscale == null || codiceFiscale == "" ||
            stipendio < 0
            )
        {
            return false;
        }

        Cliente esistente = RicercaCliente(codiceFiscale);

        //se il cliente esiste l'istanza sarà diversa dal null
        if (esistente != null)
            return false;

        Cliente cliente = new Cliente(nome, cognome, codiceFiscale, stipendio);
        Clienti.Add(cliente);

        return true;
    }

    public Cliente RicercaCliente(string codiceFiscale)
    {

        foreach (Cliente cliente in Clienti)
        {
            if (cliente.CodiceFiscale == codiceFiscale)
                return cliente;
        }

        return null;
    }
    public Cliente ModificaCliente(string nome, string cognome, string codiceFiscale, int stipendio, string nuovoCF)
    {
        Cliente cliente = RicercaCliente(codiceFiscale);

        if (cliente.CodiceFiscale != "")
        {
            if (codiceFiscale == nuovoCF)
                cliente.CodiceFiscale = codiceFiscale;
            else
                cliente.CodiceFiscale = nuovoCF;

            cliente.Stipendio = stipendio;
            cliente.Nome = nome;
            cliente.Cognome = cognome;

            return cliente;
        }
        else
        {
            return null;

        }


    }
    public List<Prestito> RicercaPrestito(string codiceFiscale)
    {
        List<Prestito> trovati = new List<Prestito>();

        foreach (Prestito item in Prestiti)
        {
            if (item.Intestatario.CodiceFiscale == codiceFiscale)
            {
                trovati.Add(item);
            }
        }
        return trovati;

    }

    public int AmmontareTotalePrestitiCliente(string codiceFiscale)
    {

        List<Prestito> prestitiUtente = RicercaPrestito(codiceFiscale);

        return prestitiUtente.Count;
    }

    public int RateMancantiCliente(string codiceFiscale)
    {
        int rateMancanti = 0;
        List<Prestito> prestiti = RicercaPrestito(codiceFiscale);
        foreach (Prestito item in prestiti)
        {
            DateTime oggi = DateOnly.FromDateTime(DateTime.Now).ToDateTime(TimeOnly.Parse("10:00 PM"));
            DateTime fine = item.Fine.ToDateTime(TimeOnly.Parse("10:00 PM"));
            rateMancanti = fine.Subtract(oggi).Days / 30;

            item.RateMancanti();
        }
        return rateMancanti;

    }

    public void AggiungiPrestito(Prestito nuovoPrestito)
    {
        Prestiti.Add(nuovoPrestito);
        Console.WriteLine("{1}: {0}", nuovoPrestito, nuovoPrestito.GetType().ToString());
    }

}

