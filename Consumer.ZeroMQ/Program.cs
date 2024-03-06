// See https://aka.ms/new-console-template for more information
using Core.ZeroMQ.Extensions;
using Core.ZeroMQ.Models;
using NetMQ;
using NetMQ.Sockets;

public static partial class Program
{
    static CancellationTokenSource cts = new CancellationTokenSource();
    static int i = 0;
    static void ShowThreadCounter()
    {
        while (true)
        {
            //Console.WriteLine("Quantidade de mensagens: {0:C0} ", i);
            Console.WriteLine("Quantidade de mensagens: {0:N0}", i);
            i = 0;
            Thread.Sleep(1000);
        }
    }

    static Task taskCounter = Task.Run(() =>
    {
        ShowThreadCounter();
    }, cts.Token);

    static async Task Main(string[] args)
    {
        Console.WriteLine("Connecting to server…");

        //    taskCounter.Wait();

        //using (var rep1 = new ResponseSocket("@tcp://localhost:5001"))
        //using (var rep2 = new ResponseSocket("@tcp://localhost:5002"))
        //using (var poller = new NetMQPoller { rep1, rep2 })
        //{
        //    // these event will be raised by the Poller
        //    rep1.ReceiveReady += (s, a) =>
        //    {
        //        // receive won't block as a message is ready
        //        string msg = a.Socket.ReceiveFrameString();
        //        // send a response
        //        a.Socket.SendFrame("Response");
        //    };
        //    rep2.ReceiveReady += (s, a) =>
        //    {
        //        // receive won't block as a message is ready
        //        string msg = a.Socket.ReceiveFrameString();
        //        // send a response
        //        a.Socket.SendFrame("Response");
        //    };
        //    // start polling (on this thread)
        //    poller.Run();
        //}

        using (var subscriber = new SubscriberSocket())
        {
            subscriber.Connect("tcp://producer.zeromq:5555");
            subscriber.Subscribe("A");
            //subscriber.Subscribe("B");

            while (true)
            {
                //var topic = subscriber.ReceiveFrameString();
                var msg = subscriber.ReceiveMultipartBytes(1);//.ReceiveFrameBytes();//.ReceiveFrameString();
                var trade = msg[1].DeserializeFromByteArrayProtobuf<Trade>();
                DateTime now = DateTime.UtcNow;
                var latency = now.Ticks - trade.Send;

                //Console.WriteLine("From Publisher tipo mensagem {0}, MaxFloor {1}, account {2}, Balance {3}, Latency {4} Nanoseconds ",
                //    trade.messageType,
                //    trade.MaxFloor,
                //    trade.Account,
                //    trade.Balance, 
                //    latency);


                i++;
                Interlocked.Increment(ref i);
                //Console.WriteLine("From Publisher: {0} {1}", topic, msg);
            }
        }
    }
}
//using (var requester = new RequestSocket())
//{
//    requester.Connect("tcp://localhost:5555");

//    int requestNumber;
//    for (requestNumber = 0; requestNumber != 10; requestNumber++)
//    {
//        Console.WriteLine("Sending Hello {0}...", requestNumber);
//        requester.SendFrame("Hello");
//        string str = requester.ReceiveFrameString();
//        Console.WriteLine("Received World {0}", requestNumber);
//    }
//}