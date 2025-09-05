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
Payment nuevoPago = new Payment();
nuevoPago.Nombre = "pago test";
nuevoPago.EstaActivo = true;
//Console.WriteLine(pService.SavePayment(nuevo));
//Console.WriteLine("-------------");

Article art1 = aService.GetArticle(1);
DetailBill nuevoDetalle1 = new DetailBill();
nuevoDetalle1.Articulo = art1;
nuevoDetalle1.Cantidad = 10;

Article art2 = aService.GetArticle(1);
DetailBill nuevoDetalle2 = new DetailBill();
nuevoDetalle2.Articulo = art2;
nuevoDetalle2.Cantidad = 20;
//Console.WriteLine(nuevoDetalle1);
//Console.WriteLine(nuevoDetalle2);

Article art3 = aService.GetArticle(1);
DetailBill nuevoDetalle3 = new DetailBill();
nuevoDetalle3.Articulo = art3;
nuevoDetalle3.Cantidad = 100;

List<DetailBill> lista = new List<DetailBill>();
lista.Add(nuevoDetalle1);
lista.Add(nuevoDetalle2);
lista.Add(nuevoDetalle3);

Payment pag1 = pService.GetPayment(1);

Bill nuevaFactura = new Bill();
nuevaFactura.Payment = pag1;
nuevaFactura.Cliente = "factura test";
nuevaFactura.FacturaActiva = true;
nuevaFactura.Details = lista;

Article nuevoArticulo = new Article();
nuevoArticulo.Nombre = "test";
nuevoArticulo.PrecioUnitario = 1m;
nuevoArticulo.EstaActivo = true;

//Console.WriteLine(bService.SaveBill(factura));

//Console.WriteLine(bService.DeleteBill(1));

//foreach (Bill b in bService.GetBills())
//{
//    Console.WriteLine(b);
//}

//Console.WriteLine(bService.GetBill(4));






bool seguirPreguntandoPrincipal = true;

while (seguirPreguntandoPrincipal)
{
    Console.WriteLine("Decida con que tabla quiere interactuar, 1 para articulos, 2 para facturas, 3 para formas de pago o 0 para salir");

    if (int.TryParse(Console.ReadLine(), out int opcionPrincipal))
    {
        switch (opcionPrincipal)
        {
            case 1:
                Console.WriteLine("Seleccionó 1: Artículos");
                bool seguirEnSubMenuArticulos = true;

                while (seguirEnSubMenuArticulos)
                {
                    Console.WriteLine("Menú Artículos: 1 para alta, 2 para baja, 3 para consulta general, 4 para consulta por id, 0 para volver");

                    if (int.TryParse(Console.ReadLine(), out int opcionArticulos))
                    {
                        switch (opcionArticulos)
                        {
                            case 1:
                                Console.WriteLine("Seleccionó 1: alta de artículos. Existe un modelo de prueba de articulo para insertar. Inserte 1 para aceptar, 0 para cancelar");
                                if(int.TryParse(Console.ReadLine(), out int aceptar))
                                {
                                    if (aceptar == 1)
                                    {
                                        aService.SaveArticle(nuevoArticulo);
                                        Console.WriteLine("articulo insertado");
                                    }
                                    else
                                    {
                                        Console.WriteLine("articulo no insertado");
                                    }
                                }
                                break;
                            case 2:
                                Console.WriteLine("Seleccionó 2: baja de artículos. Ingrese el codigo del articulo a eliminar");
                                if (int.TryParse(Console.ReadLine(), out int idArticulo1))
                                {
                                    if (aService.DeleteArticle(idArticulo1))
                                    {
                                        Console.WriteLine("Articulo eliminado: ");
                                    }
                                    else
                                    {
                                        Console.WriteLine("No existe");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Entrada no válida.");
                                }
                                break;
                            case 3:
                                Console.WriteLine("Seleccionó 3: consulta general de artículos");
                                foreach (Article a in aService.GetArticles())
                                {
                                    Console.WriteLine(a);
                                }
                                break;
                            case 4:
                                Console.WriteLine("Seleccionó 4: consulta de artículos por id, ingrese el código");
                                if (int.TryParse(Console.ReadLine(), out int idArticulo))
                                {
                                    Article articulo = aService.GetArticle(idArticulo);
                                    if (articulo != null)
                                    {
                                        Console.WriteLine("Eligió el artículo: " + articulo);
                                    }
                                    else
                                    {
                                        Console.WriteLine("No existe");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Entrada no válida.");
                                }
                                break;
                            case 0:
                                Console.WriteLine("Volviendo al menú principal...");
                                seguirEnSubMenuArticulos = false;
                                break;
                            default:
                                Console.WriteLine("Opción no válida. Por favor, intente de nuevo.");
                                break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Entrada no válida. Por favor, ingrese un número.");
                    }
                }
                break;

            case 2:
                Console.WriteLine("Seleccionó 2: Facturas");
                bool seguirEnSubMenuFacturas = true;
                while (seguirEnSubMenuFacturas)
                {
                    Console.WriteLine("Menú Facturas: 1 para alta, 2 para baja, 3 para consulta general, 4 para consulta por id, 0 para volver");

                    if (int.TryParse(Console.ReadLine(), out int opcionFacturas))
                    {
                        switch (opcionFacturas)
                        {
                            case 1:
                                Console.WriteLine("Seleccionó 1: alta de facturas. Existe un modelo de prueba de factura para insertar. Inserte 1 para aceptar, 0 para cancelar");
                                if (int.TryParse(Console.ReadLine(), out int aceptar))
                                {
                                    if (aceptar == 1)
                                    {
                                        bService.SaveBill(nuevaFactura);
                                        Console.WriteLine("factura insertada");
                                    }
                                    else
                                    {
                                        Console.WriteLine("factura no insertada");
                                    }
                                }
                                break;
                            case 2:
                                Console.WriteLine("Seleccionó 2: baja de facturas. Ingrese el numero de factura a eliminar");
                                if (int.TryParse(Console.ReadLine(), out int idFactura1))
                                {
                                    if (bService.DeleteBill(idFactura1))
                                    {
                                        Console.WriteLine("Factura eliminada");
                                    }
                                    else
                                    {
                                        Console.WriteLine("No existe");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Entrada no válida.");
                                }
                                break;
                            case 3:
                                Console.WriteLine("Seleccionó 3: consulta general de facturas");
                                foreach (Bill b in bService.GetBills())
                                {
                                    Console.WriteLine(b);
                                }
                                break;
                            case 4:
                                Console.WriteLine("Seleccionó 4: consulta de facturas por id, ingrese el código");
                                if (int.TryParse(Console.ReadLine(), out int idFactura))
                                {
                                    Bill factura = bService.GetBill(idFactura);
                                    if (factura != null)
                                    {
                                        Console.WriteLine("Eligió la factura: " + factura);
                                    }
                                    else
                                    {
                                        Console.WriteLine("No existe");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Entrada no válida.");
                                }
                                break;
                            case 0:
                                Console.WriteLine("Volviendo al menú principal...");
                                seguirEnSubMenuFacturas = false;
                                break;
                            default:
                                Console.WriteLine("Opción no válida. Por favor, intente de nuevo.");
                                break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Entrada no válida. Por favor, ingrese un número.");
                    }
                }
                break;

            case 3:
                Console.WriteLine("Seleccionó 3: Formas de pago");
                bool seguirEnSubMenuPagos = true;
                while (seguirEnSubMenuPagos)
                {
                    Console.WriteLine("Menú Formas de pago: 1 para alta, 2 para baja, 3 para consulta general, 4 para consulta por id, 0 para volver");

                    if (int.TryParse(Console.ReadLine(), out int opcionPagos))
                    {
                        switch (opcionPagos)
                        {
                            case 1:
                                Console.WriteLine("Seleccionó 1: alta de formas de pago. Existe un modelo de prueba de forma de pago para insertar. Inserte 1 para aceptar, 0 para cancelar");
                                if (int.TryParse(Console.ReadLine(), out int aceptar))
                                {
                                    if (aceptar == 1)
                                    {
                                        pService.SavePayment(nuevoPago);
                                        Console.WriteLine("forma de pago insertada");
                                    }
                                    else
                                    {
                                        Console.WriteLine("forma de pago no insertada");
                                    }
                                }
                                break;
                            case 2:
                                Console.WriteLine("Seleccionó 2: baja de formas de pago. Ingrese el codigo de la forma de pago a eliminar");
                                if (int.TryParse(Console.ReadLine(), out int idPayment1))
                                {
                                    if (pService.DeletePayment(idPayment1))
                                    {
                                        Console.WriteLine("Forma de pago eliminada");
                                    }
                                    else
                                    {
                                        Console.WriteLine("No existe");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Entrada no válida.");
                                }
                                break;
                            case 3:
                                Console.WriteLine("Seleccionó 3: consulta general de formas de pago");
                                foreach (Payment p in pService.GetPayments())
                                {
                                    Console.WriteLine(p);
                                }
                                break;
                            case 4:
                                Console.WriteLine("Seleccionó 4: consulta de formas de pago por id, ingrese el código");
                                if (int.TryParse(Console.ReadLine(), out int idPayment))
                                {
                                    Payment pago = pService.GetPayment(idPayment);
                                    if (pago != null)
                                    {
                                        Console.WriteLine("Eligió la forma de pago: " + pago);
                                    }
                                    else
                                    {
                                        Console.WriteLine("No existe");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Entrada no válida.");
                                }
                                break;
                            case 0:
                                Console.WriteLine("Volviendo al menú principal...");
                                seguirEnSubMenuPagos = false;
                                break;
                            default:
                                Console.WriteLine("Opción no válida. Por favor, intente de nuevo.");
                                break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Entrada no válida. Por favor, ingrese un número.");
                    }
                }
                break;

            case 0:
                Console.WriteLine("Saliendo del programa...");
                seguirPreguntandoPrincipal = false;
                break;

            default:
                Console.WriteLine("Opción no válida. Por favor, intente de nuevo.");
                break;
        }
    }
    else
    {
        Console.WriteLine("Entrada no válida. Por favor, ingrese un número.");
    }
}

