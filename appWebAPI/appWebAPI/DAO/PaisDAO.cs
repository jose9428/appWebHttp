using appWebAPI.Models;
using System.Data;
using System.Data.SqlClient;

namespace appWebAPI.DAO
{
    public class PaisDAO
    {
        public IEnumerable<Pais> listado()
        {
            List<Pais> lista = new List<Pais>();

            using (SqlConnection cn = new Conexion().getcn)
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("exec usp_paises", cn);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    lista.Add(new Pais()
                    {
                        idpais = dr.GetString(0),
                        nombrepais = dr.GetString(1)

                    });
                }
            }
            return lista;
        }

    }
}
