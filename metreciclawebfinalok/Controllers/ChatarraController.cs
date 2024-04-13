using MetReciclaWebFinalOK.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace MetReciclaWebFinalOK.Controllers
{
    public class ChatarraController : Controller
    {
        static string urlBase = "http://localhost:53209/ChatarraRest.svc";

        // GET: ChatarraController
        public ActionResult Index()
        {
            List<ChatarraVMRest> listaChatarra = new List<ChatarraVMRest>();
            try
            {
                //crear una solicitud para la url
                string recurso = "/ListaChatarra";
                WebRequest request = WebRequest.Create(urlBase + recurso);

                //Obtenga la respuesta
                WebResponse response = request.GetResponse();
                string responseFromServer;

                //Obtenga el contenido devuelto por el servidor
                //El bloque de uso garantiza que la transmision se cierre automaticamente
                using (Stream dataStream = response.GetResponseStream())
                {
                    //Abra la transmision usando un StreamReader para acceder facilmente
                    StreamReader reader = new StreamReader(dataStream);
                    //Lee el contenido
                    responseFromServer = reader.ReadToEnd();
                }
                var resultado = JsonConvert.DeserializeObject<List<ChatarraVMRest>>(responseFromServer);
                listaChatarra = resultado.ToList();


            }
            catch (Exception ex)
            {
                throw new Exception("Error en la obtencion de las chatarras");
            }
            return View(listaChatarra);
        }

        // GET: ChatarraController/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                string recurso = "/ListaChatarra";
                WebRequest request = WebRequest.Create(urlBase + recurso);

                WebResponse response = request.GetResponse();
                string responseFromServer;

                using (Stream dataStream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(dataStream);
                    responseFromServer = reader.ReadToEnd();
                }

                var listaChatarra = JsonConvert.DeserializeObject<List<ChatarraVMRest>>(responseFromServer);

                // Asegúrate de obtener la chatarra específica que corresponde al ID que estás buscando
                var chatarra = listaChatarra.FirstOrDefault(c => c.Idchatarra == id);

                if (chatarra == null)
                {
                    // Manejar el caso en que no se encuentra la chatarra con el ID especificado
                    // Puedes redirigir a una vista de error o realizar alguna otra acción adecuada.
                    throw new Exception($"No se encontró la chatarra con el ID {id}");
                }

                return View(chatarra);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener detalles de la chatarra", ex);
            }
        }


        // GET: ChatarraController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ChatarraController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ChatarraVMRest chatarraVMRest)
        {
            try
            {
                string recurso = "/AgregarChatarra";
                if (ModelState.IsValid)
                {
                    // Cree una solicitud utilizando una URL que pueda enviar un post.
                    WebRequest request = WebRequest.Create(urlBase + recurso);
                    // Establezca la propiedad Método de la solicitud en POST.
                    request.Method = "POST";
                    // Establezca la propiedad ContentType de WebRequest.
                    request.ContentType = "application/json";

                    // Serializar objeto de entrada
                    var input = JsonConvert.SerializeObject(chatarraVMRest, Newtonsoft.Json.Formatting.Indented);
                    byte[] byteArray = Encoding.UTF8.GetBytes(input);

                    // Establezca la propiedad ContentLength de WebRequest.
                    request.ContentLength = byteArray.Length;

                    // Obtenga el flujo de solicitudes.
                    Stream dataStream = request.GetRequestStream();
                    // Escriba los datos en el flujo de solicitud.
                    dataStream.Write(byteArray, 0, byteArray.Length);
                    // Cierre el objeto Stream.
                    dataStream.Close();

                    //Obtenga la respuesta
                    WebResponse response = request.GetResponse();
                    string respuesta;

                    //Leemos la respuesta
                    using (var streamReader = new StreamReader(response.GetResponseStream()))
                    {
                        respuesta = streamReader.ReadToEnd();
                    }

                    return RedirectToAction(nameof(Index));
                }
            }
            catch
            {
                return View();
            }
            return View();
        }


        // GET: ChatarraController/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {
                string recurso = "/ListaChatarra";
                WebRequest request = WebRequest.Create(urlBase + recurso);

                WebResponse response = request.GetResponse();
                string responseFromServer;

                using (Stream dataStream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(dataStream);
                    responseFromServer = reader.ReadToEnd();
                }

                var listaChatarra = JsonConvert.DeserializeObject<List<ChatarraVMRest>>(responseFromServer);

                var chatarra = listaChatarra.FirstOrDefault(c => c.Idchatarra == id);

                if (chatarra == null)
                {
                    // Manejar el caso en que no se encuentra la chatarra con el ID especificado
                    // Puedes redirigir a una vista de error o realizar alguna otra acción adecuada.
                    throw new Exception($"No se encontró la chatarra con el ID {id}");
                }

                return View(chatarra);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener la chatarra para editar", ex);
            }
        }

        // POST: ChatarraController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ChatarraVMRest chatarraVMRest)
        {
            try
            {
                string recurso = $"/ActualizarChatarra";

                if (ModelState.IsValid)
                {
                    // Asegúrate de asignar el id a tu ViewModel para que se envíe correctamente en la solicitud.
                    chatarraVMRest.Idchatarra = id;

                    // Crear una solicitud utilizando una URL que pueda enviar un post.
                    WebRequest request = WebRequest.Create(urlBase + recurso);

                    // Establecer la propiedad Método de la solicitud en PUT.
                    request.Method = "PUT";
                    // Establecer la propiedad ContentType de WebRequest.
                    request.ContentType = "application/json";

                    // Serializar objeto de entrada
                    var input = JsonConvert.SerializeObject(chatarraVMRest, Newtonsoft.Json.Formatting.Indented);
                    byte[] byteArray = Encoding.UTF8.GetBytes(input);

                    // Establecer la propiedad ContentLength de WebRequest.
                    request.ContentLength = byteArray.Length;

                    // Obtener el flujo de solicitudes.
                    Stream dataStream = request.GetRequestStream();
                    // Escribir los datos en el flujo de solicitud.
                    dataStream.Write(byteArray, 0, byteArray.Length);
                    // Cerrar el objeto Stream.
                    dataStream.Close();

                    // Obtener la respuesta
                    WebResponse response = request.GetResponse();
                    string respuesta;

                    // Leemos la respuesta
                    using (var streamReader = new StreamReader(response.GetResponseStream()))
                    {
                        respuesta = streamReader.ReadToEnd();
                    }

                    // Puedes verificar la respuesta para tomar decisiones adicionales si es necesario.

                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                // Manejar errores y posiblemente redirigir a una página de error.
                throw new Exception("Error al editar la chatarra", ex);
            }

            return View(chatarraVMRest);
        }



        // GET: ChatarraController/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                string recurso = "/ListaChatarra"; // Ajusta el nombre del método en tu servicio WCF
                WebRequest request = WebRequest.Create(urlBase + recurso);

                WebResponse response = request.GetResponse();
                string responseFromServer;

                using (Stream dataStream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(dataStream);
                    responseFromServer = reader.ReadToEnd();
                }

                var listaChatarra = JsonConvert.DeserializeObject<List<ChatarraVMRest>>(responseFromServer);

                var chatarra = listaChatarra.FirstOrDefault(c => c.Idchatarra == id);

                if (chatarra == null)
                {
                    // Manejar el caso en que no se encuentra la chatarra con el ID especificado
                    // Puedes redirigir a una vista de error o realizar alguna otra acción adecuada.
                    throw new Exception($"No se encontró la chatarra con el ID {id}");
                }
                return View(chatarra);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener la chatarra para eliminar", ex);
            }
        }

        // POST: ChatarraController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, ChatarraVMRest chatarraVMRest)
        {
            try
            {
                string recurso = "/EliminarChatarra"; // Ajusta el nombre del método en tu servicio WCF

                // Crear una solicitud para la URL
                WebRequest request = WebRequest.Create(urlBase + recurso);

                // Establecer la propiedad Método de la solicitud en DELETE
                request.Method = "DELETE";
                // Establecer la propiedad ContentType de WebRequest
                request.ContentType = "application/json";

                // Puedes enviar el ViewModel en el cuerpo de la solicitud si es necesario,
                // por ejemplo, si el servicio WCF espera datos adicionales.
                // Serializar el ViewModel a JSON y escribirlo en el cuerpo de la solicitud.
                string jsonRequestBody = JsonConvert.SerializeObject(chatarraVMRest);
                byte[] byteArray = Encoding.UTF8.GetBytes(jsonRequestBody);

                using (Stream dataStream = request.GetRequestStream())
                {
                    dataStream.Write(byteArray, 0, byteArray.Length);
                }

                // Obtener la respuesta
                WebResponse response = request.GetResponse();
                string respuesta;

                // Leer la respuesta
                using (var streamReader = new StreamReader(response.GetResponseStream()))
                {
                    respuesta = streamReader.ReadToEnd();
                }

                // Puedes verificar la respuesta para tomar decisiones adicionales si es necesario.

                return RedirectToAction(nameof(Index)); // Redirigir a la página principal después de eliminar.
            }
            catch (Exception ex)
            {
                // Manejar errores y posiblemente redirigir a una página de error.
                throw new Exception("Error al eliminar la chatarra", ex);
            }
        }


    }
}
