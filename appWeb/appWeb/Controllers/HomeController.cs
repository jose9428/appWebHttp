using appWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Diagnostics;

namespace appWeb.Controllers
{
    public class HomeController : Controller
    {
        string urlBase = "https://localhost:7004/api/Seller/";

        public async Task<List<Pais>> getPaises()
        {
            List<Pais> lista = new List<Pais>();
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(urlBase);
                HttpResponseMessage mensaje = await cliente.GetAsync("paises");
                if (mensaje.IsSuccessStatusCode)
                {
                    string respuesta = await mensaje.Content.ReadAsStringAsync();
                    lista = JsonConvert.DeserializeObject<List<Pais>>(respuesta);
                }
            }
            return lista;
        }

        public async Task<List<Seller>> getSellers()
        {
            List<Seller> lista = new List<Seller>();
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(urlBase);
                HttpResponseMessage mensaje = await cliente.GetAsync("sellers");
                if (mensaje.IsSuccessStatusCode)
                {
                    string respuesta = await mensaje.Content.ReadAsStringAsync();
                    lista = JsonConvert.DeserializeObject<List<Seller>>(respuesta);
                }
            }
            return lista;
        }

        public async Task<Seller> getSeller(string codigo)
        {
            Seller obj = null;
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(urlBase);
                HttpResponseMessage mensaje = await cliente.GetAsync($"buscar?codigo={codigo}");
                if (mensaje.IsSuccessStatusCode)
                {
                    string respuesta = await mensaje.Content.ReadAsStringAsync();
                    obj = JsonConvert.DeserializeObject<Seller>(respuesta);
                }
            }
            return obj;
        }

        public async Task<string> guardar(Seller reg)
        {
            string mensaje = "";
           
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(urlBase);

                StringContent contenido = new StringContent(
                JsonConvert.SerializeObject(reg), System.Text.Encoding.UTF8, "application/json");

                HttpResponseMessage respuesta = await cliente.PutAsync("actualizar", contenido);
                if (respuesta.IsSuccessStatusCode)
                {
                    mensaje = await respuesta.Content.ReadAsStringAsync();
                }
            }
            return mensaje;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            Task<List<Pais>> listaPaises = getPaises();
            Task<List<Seller>> listaSellers = getSellers();
            ViewBag.paises = new SelectList(await listaPaises, "idpais", "nombrepais");
            ViewBag.sellers = listaSellers.Result;

            return View(new Seller());
        }

        [HttpGet]
        public async Task<IActionResult> Create(string? codigo = null , string? mensaje = null)
        {
            Task<List<Pais>> listaPaises = getPaises();
            Task<List<Seller>> listaSellers = getSellers();
            ViewBag.paises = new SelectList(await listaPaises, "idpais", "nombrepais");
            ViewBag.sellers = listaSellers.Result;

            Seller obj = new Seller();

            if (codigo != null)
            {
                obj = await getSeller(codigo);
            }
            ViewBag.mensaje = mensaje;
            return View(obj);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Seller obj)
        {
            if (obj.codigo == null)
            {
                obj.codigo = "";
            }

            string mensaje = guardar(obj).Result;

            return RedirectToAction("Create", new { codigo = obj.codigo , mensaje = mensaje });

        }

    }
}