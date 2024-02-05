// See https://aka.ms/new-console-template for more information
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
        publisher
           .SendMoreFrame("A") // Topic
           .SendFrame(i.ToString()); // Message

        i++;

        if ((i % 1000) == 0 )
        //spin.SpinOnce(1);
        Thread.Sleep(1);
    }
}