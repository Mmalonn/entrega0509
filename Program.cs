// See https://aka.ms/new-console-template for more information
using entrega_viernes_5_09.Domain;
using entrega_viernes_5_09.Services;

Console.WriteLine("Hello, World!");

ArticleService aService = new ArticleService();
PaymentService pService = new PaymentService();
BillService bService = new BillService();

//foreach (Payment p in pService.GetPayments())
//{
//    Console.WriteLine(p);
//}
//Console.WriteLine("-------------");
//Console.WriteLine(pService.GetPayment(2));
//Payment nuevo = new Payment();
//nuevo.Id = 7;
//nuevo.Nombre = "test";
//nuevo.EstaActivo = false;
//Console.WriteLine(pService.SavePayment(nuevo));
//Console.WriteLine("-------------");

//Article nuevoArticulo1 = aService.GetArticle(1);
//DetailBill nuevoDetalle1 = new DetailBill();
//nuevoDetalle1.Articulo = nuevoArticulo1;
//nuevoDetalle1.Cantidad = 10;

//Article nuevoArticulo2 = aService.GetArticle(2);
//DetailBill nuevoDetalle2 = new DetailBill();
//nuevoDetalle2.Articulo = nuevoArticulo2;
//nuevoDetalle2.Cantidad = 20;
//Console.WriteLine(nuevoDetalle1);
//Console.WriteLine(nuevoDetalle2);

//List<DetailBill> lista = new List<DetailBill>();
//lista.Add(nuevoDetalle1);
//lista.Add(nuevoDetalle2);

//Payment pago = pService.GetPayment(1);

//Bill factura = new Bill();
//factura.Payment = pago;
//factura.Cliente = "testeador testiño2";
//factura.FacturaActiva = true;
//factura.Details = lista;

//Console.WriteLine(bService.SaveBill(factura));

//Console.WriteLine(bService.DeleteBill(1));

//foreach (Bill b in bService.GetBills())
//{
//    Console.WriteLine("La factura " + b.nroFactura + " con su cliente: " + b.Cliente + " el dia: " + b.fecha);
//}