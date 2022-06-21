using System.Data.SqlClient;

namespace appWebAPI.DAO
{
    public class Conexion
    {
        SqlConnection cn = new SqlConnection(@"server = (local);database = Negocios2022;Trusted_Connection = True;" +
            "MultipleActiveResultSets = True;TrustServerCertificate = False;Encrypt = False ");

        public SqlConnection getcn
        {
            get { return cn; }
        }
    }
}
