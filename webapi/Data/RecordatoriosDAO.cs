using MySqlConnector;
using webapi.Model;
using webapi.Util;

namespace webapi.Data
{
    public class RecordatoriosDAO
    {
        //DESARROLLAR AQUI EL METODO ASIGNADO EN CLASE
        public ulong Editar(Recordatorios rec)
        {
            var ad = new AccesoDatos();
            using (ad)
            {
                ad.parameters.Add(new MySqlParameter("@idRecordatorios", rec.idRecordatorios));
                ad.parameters.Add(new MySqlParameter("@notitas_id", rec.notitas_id));
                ad.parameters.Add(new MySqlParameter("@fecha_recordatorio", rec.fecha_recordatorio));
                ad.sentencia = "UPDATE recordatorios " +
                                "SET fecha_recordatorio = @fecha_recordatorio" +
                                "WHERE idRecordatorios = @idRecordatorios";
                return (ulong)ad.ejecutarSentencia(TIPOEJECUCIONSQL.SENTENCIASQL);
            }
        }

        public Recordatorios GetOneById(int idRecordatorio)
        {
            var ad = new AccesoDatos();
            Recordatorios recordatorio = null;

            using (ad)
            {
                ad.parameters.Add(new MySqlParameter("@idRecordatorios", idRecordatorio));
                ad.sentencia = "SELECT * FROM Recordatorios WHERE idRecordatorios = @idRecordatorios";

                var reader = (MySqlDataReader)ad.ejecutarSentencia(TIPOEJECUCIONSQL.CONSULTA);

                while (reader.Read())
                {
                    recordatorio = new Recordatorios();
                    recordatorio.idRecordatorios = (ulong)reader.GetInt32("idRecordatorios");
                    recordatorio.notitas_id = reader.GetInt32("notitas_id");

                    if (!reader.IsDBNull(reader.GetOrdinal("fecha_recordatorio")))
                    {
                        recordatorio.fecha_recordatorio = reader.GetDateTime(reader.GetOrdinal("fecha_recordatorio"));
                    }
                    else
                    {
                        recordatorio.fecha_recordatorio = DateTime.MinValue;
                    }

                }
            }

            return recordatorio;
        }


    }
}
