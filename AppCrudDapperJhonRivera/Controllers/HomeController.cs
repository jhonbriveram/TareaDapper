using AppCrudDapperJhonRivera.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Diagnostics;
using Dapper;
using System.Data.SqlClient;

namespace AppCrudDapperJhonRivera.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private string connection = "Server=BRIVERAM\\SQLEXPRESS; Database=Tienda; Integrated Security = true";

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        [HttpGet]
        public IActionResult Index()
        {
            IEnumerable<ClienteModel> lst = new List<ClienteModel>();
            using (IDbConnection conexion= new SqlConnection(connection))
            {
                conexion.Open();
                var sql = "select * from Cliente";
                lst = conexion.Query<ClienteModel>(sql);
                 
            }

            return View(lst);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add( ClienteModel model)
        {
            int result = 0;
            using (IDbConnection conexion = new SqlConnection(connection))
            {
                conexion.Open();
                var sql = "insert into Cliente(NroDocumento,Nombre,Direccion,Telefono,Observacion)"+
                    "values(@NroDocumento,@Nombre,@Direccion,@Telefono,@Observacion)";

                result = conexion.Execute(sql, model);
            }

            return RedirectToAction("index");
        }
        
        public IActionResult Edit(int id)
        {
            IEnumerable<ClienteModel> lst = new List<ClienteModel>();
            using (IDbConnection conexion = new SqlConnection(connection))
            {
                conexion.Open();
                var sql = "select * from Cliente where IdCliente ="+id;
                lst = conexion.Query<ClienteModel>(sql);
            }
            return View(lst);
        }

        [HttpGet]
        public IActionResult Delete(ClienteModel model, int id)
        {
            int result = 0;
            using (IDbConnection conexion = new SqlConnection(connection))
            {
                conexion.Open();
                var sql = "delete from Cliente where IdCliente ="+id;

                result = conexion.Execute(sql, model);
            }
            return RedirectToAction("index");
        }
        [HttpPost]
        public IActionResult Edit(ClienteModel model)
        {
            int result = 0;
            using(IDbConnection conexion = new SqlConnection(connection))
            {
                conexion.Open();
                var sql =  "Update Cliente set  NroDocumento = @NroDocumento,Nombre =@Nombre,Direccion =@Direccion,Telefono=@Telefono,Observacion=@Observacion" +
                    " where IdCliente = @IdCliente";
                result = conexion.Execute(sql, model);
            }
            return RedirectToAction("index");
        }
    }
}