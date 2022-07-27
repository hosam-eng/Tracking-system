using CemexProject.Hubs;
using CemexProject.Models;
using TableDependency.SqlClient;

namespace CemexProject.SqlTableDependancy
{
    public class SqlTruckDependancy
    {
        SqlTableDependency<Truck> SqlDependency;
        TruckHub truckHub;

        public SqlTruckDependancy(TruckHub truckHub)
        {
            this.truckHub = truckHub;
        }

        public void SqlTableDependency()
        {
            string connectionString = "data source=Hosam;initial catalog=Cemex;integrated security=True;";
            SqlDependency = new SqlTableDependency<Truck>(connectionString,includeOldValues:true);
            SqlDependency.OnChanged += TableDependency_OnChanged;
            SqlDependency.OnError += TableDependency_OnError;
            SqlDependency.Start();
        }

        private void TableDependency_OnChanged(object sender, TableDependency.SqlClient.Base.EventArgs.RecordChangedEventArgs<Truck> e)
        {
            if (e.ChangeType != TableDependency.SqlClient.Base.Enums.ChangeType.None)
            {
                truckHub.SendTruck(e.EntityOldValues,e.Entity);
            }
        }

        private void TableDependency_OnError(object sender, TableDependency.SqlClient.Base.EventArgs.ErrorEventArgs e)
        {
            Console.WriteLine($"{nameof(Truck)} SqlTableDependency error: {e.Error.Message}");
        }
    }
}

