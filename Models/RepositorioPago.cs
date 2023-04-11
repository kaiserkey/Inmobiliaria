using Internal;
using System;
using MySql.Data.MySqlClient;

namespace Inmobiliaria.Models;

public class RepositorioPago
{
    public RepositorioPago()
    {
    }

    public List<Pago> GetPagos(MySqlDatabase mySqlDatabase)
    {
        var pagos = new List<Pago>();
        using (var cmd = mySqlDatabase.Connection.CreateCommand() as MySqlCommand)
        {
            cmd.CommandText = @"SELECT IdPago, Monto, Fecha, NumeroPago, IdContrato
                            FROM Pago";

            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    var pago = new Pago
                    {
                        IdPago = reader.GetInt32(nameof(Pago.IdPago)),
                        Monto = reader.GetDecimal(nameof(Pago.Monto)),
                        NumeroPago = reader.GetString(nameof(Pago.NumeroPago)),
                        Fecha = reader.GetDateTime(nameof(Pago.Fecha)),
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
            cmd.CommandText = @"SELECT IdPago, Monto, Fecha, NumeroPago, IdContrato
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
                        NumeroPago = reader.GetString(nameof(Pago.NumeroPago)),
                        Fecha = reader.GetDateTime(nameof(Pago.Fecha)),
                        IdContrato = reader.GetInt32(nameof(Pago.IdContrato)),
                    };

                    return pago;
                }
            }
        }
        return null;
    }

    public int createPago(MySqlDatabase mySqlDatabase, Pago createPago)
    {
        int res = -1;
        using (var cmd = mySqlDatabase.Connection.CreateCommand() as MySqlCommand)
        {

            cmd.CommandText = @"INSERT INTO Pago (Monto, Fecha, NumeroPago, IdContrato) 
                            VALUES (@Monto, @Fecha, @IdContrato, @NumeroPago);
                            SELECT LAST_INSERT_ID();";

            cmd.Parameters.AddWithValue("@Monto", createPago.Monto);
            cmd.Parameters.AddWithValue("@NumeroPago", createPago.NumeroPago);
            cmd.Parameters.AddWithValue("@Fecha", createPago.Fecha);
            cmd.Parameters.AddWithValue("@IdContrato", createPago.IdContrato);

            res = Convert.ToInt32(cmd.ExecuteScalar());
            createPago.IdPago = res;
        }
        return res;
    }

    public int UpdatePago(MySqlDatabase mySqlDatabase, Pago pago)
    {
        int res = -1;
        using (var cmd = mySqlDatabase.Connection.CreateCommand() as MySqlCommand)
        {

            cmd.CommandText = @"UPDATE Pago SET Monto = @Monto, Fecha = @Fecha, IdContrato = @IdContrato, NumeroPago = @NumeroPago
                            WHERE IdPago = @IdPago;";

            cmd.Parameters.AddWithValue("@IdPago", pago.IdPago);
            cmd.Parameters.AddWithValue("@Monto", pago.Monto);
            cmd.Parameters.AddWithValue("@NumeroPago", pago.NumeroPago);
            cmd.Parameters.AddWithValue("@Fecha", pago.Fecha);
            cmd.Parameters.AddWithValue("@IdContrato", pago.IdContrato);

            res = cmd.ExecuteNonQuery();
        }
        return res;
    }

    public int DeletePago(MySqlDatabase mySqlDatabase, int id)
    {
        int res = -1;
        using (var cmd = mySqlDatabase.Connection.CreateCommand() as MySqlCommand)
        {

            cmd.CommandText = @"DELETE FROM Pago WHERE IdPago = @IdPago;";

            cmd.Parameters.AddWithValue("@IdPago", id);

            res = cmd.ExecuteNonQuery();
        }
        return res;
    }

}