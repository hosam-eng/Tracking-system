using CemexProject.Models;
using System.Data;
using System.Data.SqlClient;

namespace CemexProject.Repositories
{
    public class TruckRepository : ITruckRepository
    {
        string connectionString;

        public TruckRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void updateTrucks(Truck truck)
        {
            var query = "UPDATE Truck set LastUpdateDate = @lastUpdate where ID = @id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@id", truck.ID);
                    command.Parameters.AddWithValue("@lastUpdate", truck.LastUpdateDate);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw;
                }
                finally
                {
                    connection.Close();
                }
            }
        }
        public List<Truck> GetTrucks()
        {
            List<Truck> trucks = new List<Truck>();
            Truck truck;

            var data = GetTrucksFromDb();

            foreach (DataRow row in data.Rows)
            {
                truck = new Truck
                {
                    ID = Convert.ToInt32(row["ID"]),
                    number = Convert.ToInt32(row["number"]),
                    driver_name = row["driver_name"].ToString(),
                    Type = row["Type"].ToString(),
                    Lat = Convert.ToDouble(row["Lat"]),
                    Lng = Convert.ToDouble(row["Lng"]),
                    LastUpdateDate =Convert.ToDateTime(row["LastUpdateDate"])
                };
                trucks.Add(truck);
            }

            return trucks;
        }

        public DataTable GetTrucksFromDb()
        {
            var query = "SELECT * FROM Truck";
            DataTable dataTable = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            dataTable.Load(reader);
                        }
                    }

                    return dataTable;
                }
                catch (Exception ex)
                {
                    throw;
                }
                finally
                {
                    connection.Close();
                }
            }
        }

    }
}
