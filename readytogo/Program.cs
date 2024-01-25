
using System.Data;


namespace readytogo;
class Program
{
    
    // you MUST fill in your anme(s) and student number(s) here
    private static readonly string studentname1 = "Tijmen Rietveld";
    private static readonly string studentnum1 = "0993398";
    private static readonly string studentname2 = "";
    private static readonly string studentnum2 = "";

    // variables for concurrency?
    // add the variables you need for concurrency here in case of need

     public static Semaphore _semaphoreCook = new(
      initialCount: 0,
      maximumCount: 1);

      public static Semaphore _semaphoreClient = new(
      initialCount: 0,
      maximumCount: 1);

     
    
      

    // do not add more variables after this comment.
    // feel free to change the values of the variables below to test your code
    private static readonly int total_clients = 10; // this needs to be the same as the number of cooks
    private static int total_coocks = 10; // this needs to be the same as the number of clients


    public static int GetTotalCooks()
    {
        return total_coocks;
    }
    // do not change the code below
    public static LinkedList<Order> orders = new();
    public static LinkedList<Order> pickups = new();

    private static readonly Client[] clients = new Client[total_clients];
    private static readonly Cook[] cooks = new Cook[total_coocks];

    static void Main() //this method is not working properly
    {
        Console.WriteLine("Hello, we are starting our new pickup restaurant!");
        // the following code will create the clients and cooks DO NOT CHANGE THIS CODE
        // create many threads as clients,
        CreateClients();
        // create many coocks that can prepare only one dish per time
        CreateCooks();
        // each cook thread will start
        StartCooks();
        // each client thead will start 
        StartClients();
        // DO NOT CHANGE THE CODE ABOVE
        // use the space below to add your code if needed



        // DO NOT CHANGE THE CODE BELOW
        // print the number of orders placed and the number of orders consumed left in the lists
        Console.WriteLine("Orders left to work: " + orders.Count);
        Console.WriteLine("Orders left and not picked up: " + pickups.Count);
        // the lists should be empty
        Console.WriteLine("Name: " + studentname1 + " Student number: " + studentnum1);
        Console.WriteLine("Name: " + studentname2 + " Student number: " + studentnum2);
        //Console.WriteLine("Press any key to exit");
        //Console.ReadKey(); // this lines can be used to stop the program from exiting
    }

    private static void StartCooks() // this method is not working properly
    {   // feel free to change the code in this method if needed
        for (int i = 0; i < cooks.Length; i++)
        {

            Thread myThread = new Thread(new ThreadStart(cooks[i].DoWork));
            myThread.IsBackground=true;
            myThread.Start();
        }
    }

    private static void StartClients() // this method is not working properly
    {   // feel free to change the code in this method if needed
        Thread[] clientThreads = new Thread[clients.Length];
        for (int i = 0; i < clients.Length; i++)
        {
            clientThreads[i] = new Thread(new ThreadStart(clients[i].DoWork));
            clientThreads[i].IsBackground = true;
            clientThreads[i].Start();
            //myThread.Join();
             
        }

        // Wait for all client threads to finish
        foreach (var thread in clientThreads)
        {
            thread.Join();
        }
    }

    private static void CreateCooks()
    {   // feel free to change the code in this method if needed but not the signature
        for (int i = 0; i < total_clients; i++)
        {
            cooks[i] = new Cook(i); // Properly initialize Cook instance with required arguments
        }
    }

    private static void CreateClients()
    {   // feel free to change the code in this method if needed but not the signature
        for (int i = 0; i < total_clients; i++)
        {
            clients[i] = new Client(i); // Properly initialize Client instance with required arguments
        }
    }
}

public class Order  //do not change this class
{
    private bool ready;

    public Order()
    {
        ready = false;
    }

    public void Done()
    {
        ready = true;

    }
    public bool isReady()
    {
        return ready;
    }
}
