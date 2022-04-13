using StackExchange.Redis;
using System.Data;

string connectionString = "20.225.241.127";
var redis = ConnectionMultiplexer.Connect(connectionString);
var sub = redis.GetSubscriber();
var db = redis.GetDatabase();

sub.Subscribe("perguntas").OnMessage(m =>
{
    var membros = m.ToString().Split(":");
    var pergunta = membros[2].Replace("Quanto é", "").Replace("?","");

    var x = new DataTable();
    var rsp = x.Compute(pergunta, "");
    
    var id = membros[1];

    Console.WriteLine(m);
    db.HashSet(id, "AMAE", "Essa pergunta tah mais facil ainda. A resposta eh " + rsp.ToString());
});

Console.ReadLine();
