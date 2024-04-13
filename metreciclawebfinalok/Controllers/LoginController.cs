using MetReciclaWebFinalOK.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace MetReciclaWebFinalOK.Controllers
{
    public class LoginController : Controller
    {
        private readonly MetrecicladbContext _context;

        public LoginController(MetrecicladbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View(new LoginModel()); // Puedes pasar un modelo vacío o inicializarlo según tus necesidades
        }


        [HttpPost]
        public IActionResult HandleLogin(LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                // Verifica las credenciales en la base de datos
                var usuario = _context.Usuarios.FirstOrDefault(u => u.Cedula == loginModel.Cedula && u.Contraseña == loginModel.Contraseña);

                if (usuario != null)
                {
                    // Credenciales correctas, redirige a la página principal
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    // Credenciales incorrectas, muestra un mensaje de alerta
                    ModelState.AddModelError(string.Empty, "Cédula o contraseña incorrectas");
                    return View("Index", loginModel);
                }
            }

            // Si el modelo no es válido, vuelve a mostrar el formulario de inicio de sesión
            return View("Index", loginModel);
        }


        // GET: ChatarraController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LoginController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LoginModel loginModel)
        {
           
            try
            {
                if (ModelState.IsValid)
                {
                    Empleado empleado = new Empleado();
                    empleado.Cedula = (int)loginModel.Cedula;
                    empleado.Nombre = loginModel.Nombre;
                    empleado.Apellido = loginModel.Apellido;
                    empleado.Direccion = loginModel.Direccion;
                    empleado.Telefono = loginModel.Telefono;
                    _context.Empleados.Add(empleado);
                    await _context.SaveChangesAsync();
                    int cedulanuevo = empleado.Cedula;

                    Usuario usuario = new Usuario();
                    usuario.Cedula = cedulanuevo;
                    usuario.Contraseña = loginModel.Contraseña;
                    _context.Usuarios.Add(usuario);
                    await _context.SaveChangesAsync();

                    return RedirectToAction("Index", "Login");
                }

            }
            catch
            {
                return View();
            }
            return View(loginModel);
        }



    }
}
