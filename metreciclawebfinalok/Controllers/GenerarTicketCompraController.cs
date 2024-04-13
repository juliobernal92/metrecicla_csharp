using MetReciclaWebFinalOK.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Globalization;
using System.Net.Http.Headers;
using System.Text;

namespace MetReciclaWebFinalOK.Controllers
{
    public class GenerarTicketCompraController : Controller
    {
        static readonly string urlBase = "http://localhost:53209/VendedoresRest.svc";
        private readonly MetrecicladbContext _context;

        public GenerarTicketCompraController(MetrecicladbContext context)
        {
            _context = context;
        }

        // GET: GenerarTicketCompraController
        public ActionResult Index()
        {
            // Obtener los nombres de la tabla Chatarra desde la base de datos
            var nombresChatarra = _context.Chatarras.Select(c => new SelectListItem { Value = c.Idchatarra.ToString(), Text = c.Nombre }).ToList();

            // Crear una lista de SelectListItem para pasar a la vista
            ViewBag.NombresChatarra = nombresChatarra;

            return View();
        }

        //--------------------------------------------------------------------

        [HttpGet]
        public JsonResult ObtenerNombreChatarra(int idChatarra)
        {
            try
            {
                var chatarra = _context.Chatarras.SingleOrDefault(c => c.Idchatarra == idChatarra);

                if (chatarra != null)
                {
                    return Json(new { nombre = chatarra.Nombre });
                }
                else
                {
                    return Json(new { error = "Chatarra no encontrada" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { error = "Error al obtener el nombre de la chatarra: " + ex.Message });
            }
        }

        [HttpGet]
        public JsonResult BuscarVendedorPorId(int idVendedor)
        {
            try
            {
                // Buscar el vendedor en la base de datos por ID
                var vendedor = _context.Vendedores.SingleOrDefault(v => v.Idvendedor == idVendedor);

                if (vendedor != null)
                {
                    // Devolver los datos del vendedor si se encuentra
                    return Json(new { Nombre = vendedor.Nombre, Telefono = vendedor.Telefono, Direccion = vendedor.Direccion });
                }
                else
                {
                    // Devolver null si el vendedor no se encuentra
                    return Json(null);
                }
            }
            catch (Exception ex)
            {
                // Manejar cualquier error que pueda ocurrir durante la búsqueda
                return Json(new { error = "Error al buscar el vendedor: " + ex.Message });
            }
        }


        [HttpGet]
        public IActionResult BuscarVendedor(int idVendedor)
        {
            try
            {
                // Aquí deberías realizar la búsqueda del vendedor en tu base de datos
                // y devolver los datos encontrados o un indicador de que no existe.

                // Ejemplo: Supongamos que tienes una clase Vendedor con propiedades como Nombre, Telefono, Direccion
                var vendedor = _context.Vendedores.FirstOrDefault(v => v.Idvendedor == idVendedor);

                if (vendedor != null)
                {
                    var resultado = new
                    {
                        encontrado = true,
                        nombre = vendedor.Nombre,
                        telefono = vendedor.Telefono,
                        direccion = vendedor.Direccion
                    };

                    return Json(resultado);
                }
                else
                {
                    var resultado = new { encontrado = false };
                    return Json(resultado);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al buscar vendedor: " + ex.Message);
                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<IActionResult>CrearTicketCompra(int idVendedor, DateTime Fecha)
        {
            try
            {
                int idUsuario = 2;
                var nuevoTicketCompra = new TicketCompra
                {
                    Fecha = Fecha,
                    Idvendedor = idVendedor,
                    Idusuario = idUsuario
                };
                _context.TicketCompras.Add(nuevoTicketCompra);
                await _context.SaveChangesAsync();

                // Recupera el ID del ticket después de guardarlo en la base de datos
                int idTicketCompra = nuevoTicketCompra.Idticketcompra;

                // Almacena el idTicketCompra en TempData
                TempData["idticketcompra"] = idTicketCompra;

                // Devuelve el ID del ticket como JSON
                return Json(new { idTicketCompra });
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error al crear ticket: {ex.Message}");
                return View("Error");
            }
        }




        [HttpGet]
        public JsonResult ObtenerIdPrecioChatarra(int idChatarra)
        {
            try
            {
                var chatarra = _context.Chatarras.SingleOrDefault(c => c.Idchatarra == idChatarra);

                if (chatarra != null)
                {
                    return Json(new { id = chatarra.Idchatarra, precio = chatarra.Preciocompra });
                }
                else
                {
                    return Json(new { error = "Chatarra no encontrada" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { error = "Error al obtener ID y precio de la chatarra: " + ex.Message });
            }
        }

        //---------------------------------------------------------------------

        //AGREGAR VENDEDOR
        [HttpPost]
        public async Task<IActionResult> CrearVendedor(string Nombre, string Telefono, string Direccion, DateTime Fecha)
        {
            try
            {
                var nuevoVendedor = new Vendedor
                {
                    Nombre = Nombre,
                    Telefono = Telefono,
                    Direccion = Direccion
                };

                _context.Vendedores.Add(nuevoVendedor);
                await _context.SaveChangesAsync();

                // Recuperar el ID después de guardarlo en la base de datos
                int idVendedor = nuevoVendedor.Idvendedor;
                string Nombrenuevo = nuevoVendedor.Nombre;
                string TelNuevo = nuevoVendedor.Telefono;
                string DirecNuevo = nuevoVendedor.Direccion;
                // Asignar el ID al objeto para que se muestre en la vista
                nuevoVendedor.Idvendedor = idVendedor;
                TempData["NuevoVendedorId"] = idVendedor;
                TempData["NuevoNombre"] = Nombrenuevo;
                TempData["NuevoTel"] = TelNuevo;
                TempData["NuevoDir"] = DirecNuevo;

                ////AÑADIR TICKETCOMPRA
                int idUsuario = 2;
                try
                {
                    var nuevoTicketCompra = new TicketCompra
                    {
                        Fecha = Fecha,
                        Idvendedor = idVendedor,
                        Idusuario = idUsuario
                    };
                    _context.TicketCompras.Add(nuevoTicketCompra);
                    await _context.SaveChangesAsync();
                    int idTicketCompra = nuevoTicketCompra.Idticketcompra;
                    nuevoTicketCompra.Idticketcompra = idTicketCompra;
                    TempData["idticketcompra"] = idTicketCompra;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al generar el ticket: {ex.Message}");
                    return View("Error");
                }

                return RedirectToAction("Index", nuevoVendedor);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al crear vendedor: {ex.Message}");
                return View("Error");
            }
        }






        //AGREGAR DETALLES COMPRA

        [HttpPost]
        public async Task<IActionResult> CrearDetalleCompra(int idchatarra, int idticketcompra, decimal Cantidad, double Subtotal)
        {
            try
            {
                var nuevoDetalle = new DetallesCompra
                {
                    Idchatarra = idchatarra,
                    Idticketcompra = idticketcompra,
                    Cantidad = Cantidad,
                    Subtotal = Subtotal
                };

                _context.DetallesCompras.Add(nuevoDetalle);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error");
            }
        }


        //IMPRIMIR TICKET

        [HttpPost]
        public async Task<IActionResult> ImprimirTicket(int idticketcompra, int total)
        {
            try
            {
                var nuevoTotalCompra = new TotalCompra
                {
                    Idticketcompra = idticketcompra,
                    Total = total
                };

                _context.TotalCompras.Add(nuevoTotalCompra);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index");
            }
        }

        //FIN IMPRIMIR TICKET

        // GENERAR TXT



        [HttpPost]
        public IActionResult GenerarTicketTxt(int idticketcompra, int total, DateTime Fecha, string Nombre)
        {
            try
            {
                // Obtener los detalles del ticket, cargando la relación de navegación IdchatarraNavigation
                var detalles = _context.DetallesCompras
                    .Include(d => d.IdchatarraNavigation)
                    .Where(d => d.Idticketcompra == idticketcompra)
                    .ToList();

                // Crear el contenido del archivo TXT
                StringBuilder contenidoTxt = new StringBuilder();
                contenidoTxt.AppendLine("-------------MET RECICLA LUQUE-----------");
                contenidoTxt.AppendLine("Telefono: (0984) 749-327");
                contenidoTxt.AppendLine();
                contenidoTxt.AppendLine($"Fecha: {Fecha.ToString("dd-MM-yyyy")}");
                contenidoTxt.AppendLine($"Vendedor: {Nombre}");
                contenidoTxt.AppendLine("-----------------------------------------");
                contenidoTxt.AppendLine("CANT      DESC          PREC    SUBT");
                contenidoTxt.AppendLine("-----------------------------------------");

                // Formato de moneda para el guaraní
                CultureInfo culture = new CultureInfo("es-PY");

                // Agregar detalles
                foreach (var detalle in detalles)
                {
                    string nombreChatarra = detalle.IdchatarraNavigation != null ? detalle.IdchatarraNavigation.Nombre : "Nombre no disponible";
                    decimal precioCompra = (decimal)(detalle.IdchatarraNavigation != null ? detalle.IdchatarraNavigation.Preciocompra : 0);

                    contenidoTxt.AppendLine($"{detalle.Cantidad,-9} {nombreChatarra,-13} {precioCompra.ToString(),-8} {detalle.Subtotal.ToString(),-8}");
                }

                contenidoTxt.AppendLine("-----------------------------------------");
                contenidoTxt.AppendLine($"TOTAL:                        {total.ToString("C", culture),-8}");

                // Devolver el contenido del archivo
                var bytes = Encoding.UTF8.GetBytes(contenidoTxt.ToString());
                var stream = new MemoryStream(bytes);


                return File(stream, "text/plain", $"Ticket_{idticketcompra}.txt");
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }




        // FIN TXT

        //ACTUALIZAR TABLA
        [HttpGet]
        public ActionResult ObtenerDetallesCompra(int? idTicketCompra)
        {
            var detallesCompra = _context.DetallesCompras.Where(d => d.Idticketcompra == idTicketCompra).ToList();

            // Renderizar la vista parcial y devolverla como una cadena
            return PartialView("_TablaDetallesCompra", detallesCompra);
        }


        // GET: GenerarTicketCompraController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: GenerarTicketCompraController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: GenerarTicketCompraController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync(Vendedor vendedores)
        {
            try
            {
                string recurso = "/AgregarVendedores";
                if (ModelState.IsValid)
                {
                    using (HttpClient client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(urlBase);
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                        // Serializar objeto de entrada
                        var input = JsonConvert.SerializeObject(vendedores, Newtonsoft.Json.Formatting.Indented);
                        StringContent content = new StringContent(input, Encoding.UTF8, "application/json");

                        // Realizar la solicitud HTTP POST
                        HttpResponseMessage response = await client.PostAsync(recurso, content);
                        if (response == null)
                        {
                            Console.WriteLine($"response es null");
                        }

                        if (response.IsSuccessStatusCode)
                        {
                            // Operación exitosa, redirigir a Index
                            await _context.SaveChangesAsync();

                            return RedirectToAction("Index", "GenerarTicketCompra");
                        }
                        else
                        {
                            // Manejar error si es necesario
                            Console.WriteLine($"Error al insertar en la base de datos");
                        }
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                // Loguea la excepción para diagnóstico
                Console.WriteLine($"Error al insertar en la base de datos: {ex.Message}");

                return View();
            }
        }


        // GET: GenerarTicketCompraController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: GenerarTicketCompraController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: GenerarTicketCompraController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: GenerarTicketCompraController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
