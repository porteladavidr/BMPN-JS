using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace bpm_js.Controllers
{
    public class DiagramaController : Controller
    {
        private readonly string connectionString = "Server=DAVID\\SQLEXPRESS; Database=BPMN_DEMO; User Id=callback; Password=softium;";

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetFluxos()
        {
            var fluxos = new List<dynamic>();

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var query = "SELECT id, nome FROM fluxo";

                using (var command = new SqlCommand(query, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        fluxos.Add(new { id = reader.GetInt32(0), nome = reader.GetString(1) });
                    }
                }
            }

            return Json(fluxos, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetDiagram(int id)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var query = "SELECT nome, xml_conteudo FROM fluxo WHERE id = @id";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            var fluxo = new
                            {
                                nome = reader.GetString(0),
                                xml_conteudo = reader.GetString(1)
                            };
                            return Json(fluxo, JsonRequestBehavior.AllowGet);
                        }
                    }
                }
            }

            return Json(null, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SaveDiagram(int? id, string nome, string diagram)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command;

                    if (id.HasValue)
                    {
                        var query = "UPDATE fluxo SET nome = @nome, xml_conteudo = @xml_conteudo WHERE id = @id";
                        command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@id", id.Value);
                    }
                    else
                    {
                        var query = "INSERT INTO fluxo (nome, xml_conteudo) VALUES (@nome, @xml_conteudo)";
                        command = new SqlCommand(query, connection);
                    }

                    command.Parameters.AddWithValue("@nome", nome);
                    command.Parameters.AddWithValue("@xml_conteudo", diagram);
                    command.ExecuteNonQuery();
                }

                return Json(new { Status = "Diagrama salvo com sucesso no banco de dados" });
            }
            catch (Exception ex)
            {
                return Json(new { Status = "Erro ao salvar o diagrama", Message = ex.Message });
            }
        }
    }
}
