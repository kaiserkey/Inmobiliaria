using Internal;
using System;
using MySql.Data.MySqlClient;

namespace Inmobiliaria.Models;

public class RepositorioPago {
    public RepositorioPago() {
    }

    public List<Pago> GetPagos(MySqlDatabase mySqlDatabase)
{
    var pagos = new List<Pago>();
    using (var cmd = mySqlDatabase.Connection.CreateCommand() as MySqlCommand)
    {
        cmd.CommandText = @"SELECT IdPago, Monto, Fecha, Descripcion, IdContrato
                            FROM Pago";

        using (var reader = cmd.ExecuteReader())
        {
            while (reader.Read())
            {
                var pago = new Pago
                {
                    IdPago = reader.GetInt32(nameof(Pago.IdPago)),
                    Monto = reader.GetDecimal(nameof(Pago.Monto)),
                    Fecha = reader.GetDateTime(nameof(Pago.Fecha)),
                    Descripcion = reader.GetString(nameof(Pago.Descripcion)),
                    IdContrato = reader.GetInt32(nameof(Pago.IdContrato)),
                };
                pagos.Add(pago);
            }

        }
    }
    return pagos;
}

public Pago GetPago(MySqlDatabase mySqlDatabase, int id)
{
    using (var cmd = mySqlDatabase.Connection.CreateCommand() as MySqlCommand)
    {
        cmd.CommandText = @"SELECT IdPago, Monto, Fecha, Descripcion, IdContrato
                            FROM Pago
                            WHERE IdPago = @IdPago";
        cmd.Parameters.AddWithValue("@IdPago", id);

        using (var reader = cmd.ExecuteReader())
        {
            if (reader.Read())
            {
                var pago = new Pago
                {
                    IdPago = reader.GetInt32(nameof(Pago.IdPago)),
                    Monto = reader.GetDecimal(nameof(Pago.Monto)),
                    Fecha = reader.GetDateTime(nameof(Pago.Fecha)),
                    Descripcion = reader.GetString(nameof(Pago.Descripcion)),
                    IdContrato = reader.GetInt32(nameof(Pago.IdContrato)),
                };

                return pago;
            }
        }
    }
    return null;
}

public int CreatePago(MySqlDatabase mySqlDatabase, Pago CreatePago)
{
    int res = -1;
    using (var cmd = mySqlDatabase.Connection.CreateCommand() as MySqlCommand)
    {

        cmd.CommandText = @"INSERT INTO Pago (Monto, Fecha, Descripcion, IdContrato) 
                            VALUES (@Monto, @Fecha, @Descripcion, @IdContrato);
                            SELECT LAST_INSERT_ID();";

        cmd.Parameters.AddWithValue("@Monto", CreatePago.Monto);
        cmd.Parameters.AddWithValue("@Fecha", CreatePago.Fecha);
        cmd.Parameters.AddWithValue("@Descripcion", CreatePago.Descripcion);
        cmd.Parameters.AddWithValue("@IdContrato", CreatePago.IdContrato);

        res = Convert.ToInt32(cmd.ExecuteScalar());
        CreatePago.IdPago = res;
    }
    return res;
}

public int UpdatePago(MySqlDatabase mySqlDatabase, Pago pago)
{
    int res = -1;
    using (var cmd = mySqlDatabase.Connection.CreateCommand() as MySqlCommand)
    {

        cmd.CommandText = @"UPDATE Pago SET Monto = @Monto, Fecha = @Fecha, Descripcion = @Descripcion, IdContrato = @IdContrato
                            WHERE IdPago = @IdPago;";

        cmd.Parameters.AddWithValue("@IdPago", pago.IdPago);
        cmd.Parameters.AddWithValue("@Monto", pago.Monto);
        cmd.Parameters.AddWithValue("@Fecha", pago.Fecha);
        cmd.Parameters.AddWithValue("@Descripcion", pago.Descripcion);
        cmd.Parameters.AddWithValue("@IdContrato", pago.IdContrato);

        res = cmd.ExecuteNonQuery();
    }
    return res;
}



}