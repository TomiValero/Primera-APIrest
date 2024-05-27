using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Http;
using APIRest.Models;


namespace APIRest.Controllers
{
    public class ProductoController : ApiController
    {
        static Dictionary<int, Producto> productos = new Dictionary<int, Producto>();


        //GET   api/Producto
        public IEnumerable<Producto> Get()
        {
            return new List<Producto>(productos.Values);  //Trae todos los productos del diccionario(ForEach mas avanzado para Diccionario)
        }

        //GET   api/Producto/5
        public Producto Get(int id)
        {
            Producto encontrado;
            productos.TryGetValue(id, out encontrado); //Intenta encotrar el producto con ese id y lo guarda en la variable encontrado
            return encontrado;
        }


        //POST api/Producto
        public bool Post([FromBody] Producto producto) //Para insentar de recibe un producto en formato json entences [FromBody] la convierte en formato objeto C# (Serializar o Deserializar)
        {
            Producto encontrado;
            productos.TryGetValue(producto.IdProducto, out encontrado);
            if(encontrado == null)
            {
                productos.Add(producto.IdProducto, producto);
                return true;
            }
            else
            {
                return false;
            }
        }
        // PUT api/Producto/5
        public IHttpActionResult Put(int id, [FromBody] Producto producto)  // Para actualizar se recibe un producto en formato JSON
        {
            if (producto == null)
            {
                return BadRequest("Producto no puede ser nulo.");
            }

            Producto encontrado;
            if (!productos.TryGetValue(id, out encontrado))
            {
                return NotFound();  // Retorna 404 si el producto con ese Id no existe
            }

            productos[id] = producto;  // Actualiza el producto en el diccionario
            return StatusCode(System.Net.HttpStatusCode.NoContent);  // Retorna 204 No Content para indicar que la actualización fue exitosa
        }


        //DELETE api/Producto/1
        public bool Delete(int id)
        {
            return productos.Remove(id);
        }
    }
}

