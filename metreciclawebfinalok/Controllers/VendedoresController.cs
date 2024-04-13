using MetReciclaWebFinalOK.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WCFSoapVendedores;

namespace MetReciclaWebFinalOK.Controllers
{
    public class VendedoresController : Controller
    {
        VendedoresSoapClient clientVendedor = new VendedoresSoapClient();
        // GET: VendedoresController
        public ActionResult Index()
        {
            List<VendedorViewModel> vendedorViewModels = new List<VendedorViewModel>();
            var listaVendedores = clientVendedor.ListaVendedores();
            vendedorViewModels = (from a in listaVendedores
                                  select new VendedorViewModel
                                  {
                                      Idvendedor = a.idvendedor,
                                      Nombre = a.nombre,
                                      Telefono = a.telefono,
                                      Direccion = a.direccion
                                  }).ToList();
            return View(vendedorViewModels);
        }

        // GET: VendedoresController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: VendedoresController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: VendedoresController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(VendedorViewModel vendedorViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    VendedoresDc vendedoresDc = new VendedoresDc();
                    vendedoresDc.Nombre = vendedorViewModel.Nombre;
                    vendedoresDc.Telefono = vendedorViewModel.Telefono;
                    vendedoresDc.Direccion = vendedorViewModel.Direccion;
                    clientVendedor.AgregarVendedores(vendedoresDc);
                    return RedirectToAction(nameof(Index));
                }

            }
            catch
            {
                return View();
            }
            return View();
        }

        // GET: VendedoresController/Edit/5
        public ActionResult Edit(int id)
        {
            var vendedor = clientVendedor.ObtenerVendedores(id);

            // Mapear los datos a Vendedor View Model
            VendedorViewModel vendedorViewModel = new VendedorViewModel
            {
                Idvendedor = vendedor.Idvendedor,
                Nombre = vendedor.Nombre,
                Telefono = vendedor.Telefono,
                Direccion = vendedor.Direccion
            };

            return View(vendedorViewModel);
        }

        // POST: VendedoresController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, VendedorViewModel vendedorViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    VendedoresDc vendedorDc = new VendedoresDc();
                    vendedorDc.Idvendedor = vendedorViewModel.Idvendedor;
                    vendedorDc.Nombre = vendedorViewModel.Nombre;
                    vendedorDc.Telefono = vendedorViewModel.Telefono;
                    vendedorDc.Direccion = vendedorViewModel.Direccion;
                    clientVendedor.ActualizarVendedores(vendedorDc);
                    return RedirectToAction(nameof(Index));
                }

            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Error al procesar la solicitud: " + ex.Message;
                return View(vendedorViewModel);
            }
            return View();
        }

        // GET: VendedoresController/Delete/5
        public ActionResult Delete(int id)
        {
            var vendedor = clientVendedor.ObtenerVendedores(id);
            VendedorViewModel vendedorVM = new VendedorViewModel
            {
                Idvendedor = vendedor.Idvendedor,
                Nombre = vendedor.Nombre,
                Telefono = vendedor.Telefono,
                Direccion = vendedor.Direccion
            };
            return View(vendedorVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, VendedorViewModel vendedorViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    
                }
                var vendedor = clientVendedor.ObtenerVendedores(id);
                clientVendedor.EliminarVendedores(vendedor);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al procesar la solicitud: " + ex.Message);
                ViewBag.ErrorMessage = "Error al procesar la solicitud: " + ex.Message;
                return View(vendedorViewModel);
            }
            return View();
        }


    }
}
