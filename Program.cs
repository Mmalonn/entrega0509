// See https://aka.ms/new-console-template for more information
using entrega_viernes_5_09.Domain;
using entrega_viernes_5_09.Services;

Console.WriteLine("Hello, World!");

ArticleService aService = new ArticleService();
PaymentService pService = new PaymentService();

foreach(Payment p in pService.GetPayments())
{
    Console.WriteLine(p);
}
Console.WriteLine("Hola");
Console.WriteLine(pService.GetPayment(2));

Payment nuevo = new Payment();
nuevo.Id = 7;
nuevo.Nombre = "test";
nuevo.EstaActivo = false;
Console.WriteLine(pService.SavePayment(nuevo));
