using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ZeroMQ.Models;

[ProtoContract]
public class Trade
{
    [ProtoMember(1)]
    public string messageType { get; set; } = string.Empty;
    
    [ProtoMember(2)]
    public int MaxFloor { get; set; }

    [ProtoMember(3)]
    public int Account { get; set; }
    [ProtoMember(4)]
    public int Balance { get; set; }

    public Trade() { }

    public Trade(string messageType, int maxFloor, int account, int balance)
    {
        this.messageType = messageType;
        MaxFloor = maxFloor;
        Account = account;
        Balance = balance;
    }
}
