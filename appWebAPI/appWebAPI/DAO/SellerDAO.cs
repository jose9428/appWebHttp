using appWebAPI.Models;
using System.Data;
using System.Data.SqlClient;

namespace appWebAPI.DAO
{
    public class SellerDAO
    {
        public IEnumerable<Seller> listado()
        {
            List<Seller> lista = new List<Seller>();
            using (SqlConnection cn = new Conexion().getcn)
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("exec usp_sellers", cn);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    lista.Add(new Seller()
                    {
                        codigo = dr.GetString(0),
                        nombre = dr.GetString(1),
                        direccion = dr.GetString(2),
                        idpais = dr.GetString(3),
                        email = dr.GetString(4),
                    });
                }
            }

            return lista;
        }
       


        public Seller buscar(string codigo)
        {
            return listado().FirstOrDefault(x => x.codigo == codigo);
        }

        public string agregar(Seller reg)
        {
            string mensaje = string.Empty;

            using (SqlConnection cn = new Conexion().getcn)
            {
                cn.Open();
                try
                {
                    SqlCommand cmd = new SqlCommand("usp_sellers_inserta", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@nom", reg.nombre);
                    cmd.Parameters.AddWithValue("@dir", reg.direccion);
                    cmd.Parameters.AddWithValue("@pais", reg.idpais);
                    cmd.Parameters.AddWithValue("@email", reg.email);
                    int rs = cmd.ExecuteNonQuery();

                    mensaje = $"{rs} Registro añadido";
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                finally
                {
                    cn.Close();
                }
            }

            return mensaje;
        }

        public string Actualizar(Seller reg)
        {
            string mensaje = "";

            using (SqlConnection cn = new Conexion().getcn)
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("usp_seller_actualiza", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@cod", reg.codigo);
                    cmd.Parameters.AddWithValue("@nom", reg.nombre);
                    cmd.Parameters.AddWithValue("@dir", reg.direccion);
                    cmd.Parameters.AddWithValue("@idpais", reg.idpais);
                    cmd.Parameters.AddWithValue("@email", reg.email);
                    cn.Open();
                    cmd.ExecuteNonQuery();
                    mensaje = "Seller Actualizado";
                }
                catch (Exception ex)
                {
                    mensaje = ex.Message;
                }
                finally
                {
                    cn.Close();
                }
            }

            return mensaje;
        }

        public string Elimina(string codigo)
        {
            string mensaje = "";

            using (SqlConnection cn = new Conexion().getcn)
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("usp_sellers_elimina", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@cod", codigo);
                    cn.Open();
                    cmd.ExecuteNonQuery();
                    mensaje = "Seller eliminado";
                }
                catch (Exception ex)
                {
                    mensaje = ex.Message;
                }
                finally
                {
                    cn.Close();
                }
            }

            return mensaje;

        }
    }
}
