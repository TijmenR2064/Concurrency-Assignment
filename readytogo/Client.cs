namespace readytogo;


internal class Client
{
    // do you need to add variables here?
    // add the variables you need for concurrency here

     private static readonly Semaphore _semaphore = new(
      initialCount: 0,
      maximumCount: 1);

    // do not add more variables after this comment.
    private readonly int id = 0;

    public Client(int id) // you can add more parameters if you need
    {
        this.id = id;
    }


    internal void DoWork()    // this method is not working properly
    {   // feel free to change the code in this method if needed but not the signature
    
        // each client will take a random range nap
        Thread.Sleep(new Random().Next(100, 500)); // do not remove this line
        // each client will place an order
        Order o = new();

        //place the order
        Program._semaphore.WaitOne();
        Program.orders.AddFirst(o);  // do not remove this line
        Program._semaphore.Release();
        // for each request of the client the cooks will prepare the order

        Console.WriteLine("C: Order placed by {0}", id); // do not remove this line

        //wait for the order to be ready (the cook is slow, so go take a nap)
        Thread.Sleep(new Random().Next(100, 500));  // do not remove this line
        // each client will go to the pick the oder when ready in the pickup location
        // each client will pickup the order and terminate

        _semaphore.WaitOne();
        Program.pickups.RemoveFirst(); // do not remove this line
        _semaphore.Release();
        //order pickedup
        
        

        Console.WriteLine("C: Order pickedup by {0}", id); // do not remove this line
    }
}