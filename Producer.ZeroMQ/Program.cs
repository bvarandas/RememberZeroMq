// See https://aka.ms/new-console-template for more information
using Core.ZeroMQ.Extensions;
using Core.ZeroMQ.Models;
using NetMQ;
using NetMQ.Sockets;

Console.WriteLine("Hello, World!");

//using (var responder = new ResponseSocket())
//{
//    responder.Bind("tcp://*:5555");

//    while (true)
//    {
//        string str = responder.ReceiveFrameString();
//        Console.WriteLine("Received Hello");
//        Thread.Sleep(1000);  //  Do some 'work'
//        responder.SendFrame("World");
//    }
//}

//using (var rep = new ResponseSocket("@tcp://*:5002"))
//{
//    // process requests, forever...
//    while (true)
//    {
//        // receive a request message
//        var msg = rep.ReceiveFrameString();
//        // send a canned response
//        rep.SendFrame("Response");
//    }
//}

using (var publisher = new PublisherSocket())
{
    publisher.Bind("tcp://*:5555");
    int i = 0;
    SpinWait spin = new SpinWait();
    while (true)
    {
        //publisher
        //   .SendMoreFrame("A") // Topic
        //   .SendFrame(i.ToString()); // Message

        var trade = new Trade("2", 100+i, 521452, 150+i);
        var message = trade.SerializeToByteArrayProtobuf<Trade>();
        
        publisher
            .SendMoreFrame("A")
            .SendMultipartBytes(message);

        publisher
            .SendMoreFrame("B")
            .SendMultipartBytes(message);

        i++;

        if ((i % 10000) == 0 )
        //spin.SpinOnce(1);
        Thread.Sleep(1);
    }
}