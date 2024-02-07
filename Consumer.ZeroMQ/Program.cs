// See https://aka.ms/new-console-template for more information
using Core.ZeroMQ.Extensions;
using Core.ZeroMQ.Models;
using NetMQ;
using NetMQ.Sockets;

Console.WriteLine("Hello, World!");
Console.WriteLine("Connecting to hello world server…");

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
    subscriber.Connect("tcp://localhost:5555");
    subscriber.Subscribe("A");
    subscriber.Subscribe("B");

    while (true)
    {
        //var topic = subscriber.ReceiveFrameString();
        try
        {
            var msg = subscriber.ReceiveMultipartBytes(1);//.ReceiveFrameBytes();//.ReceiveFrameString();

            var trade = msg[1].DeserializeFromByteArrayProtobuf<Trade>();
            Console.WriteLine("From Publisher tipo mensagem {0}, MaxFloor {1}, account {2}, Balance {3} ",
                trade.messageType,
                trade.MaxFloor,
                trade.Account,
                trade.Balance);
        }catch(Exception ex)
        {

        }
        //Console.WriteLine("From Publisher: {0} {1}", topic, msg);
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