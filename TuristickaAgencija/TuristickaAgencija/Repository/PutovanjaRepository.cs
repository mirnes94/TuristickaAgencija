using Microsoft.AspNetCore.SignalR;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TuristickaAgencija.EF;
using TuristickaAgencija.EntityModels;
using TuristickaAgencija.ViewModel;

namespace TuristickaAgencija.Repository
{
    public class PutovanjaRepository : IPutovanjaRepository
    {

       
        private readonly IHubContext<SignalServer> _context;
        string connectionString = "";

        public PutovanjaRepository(IConfiguration configuration, IHubContext<SignalServer> context)
        {
            connectionString = configuration.GetConnectionString("cs1");
            _context = context;
            
        }

      

        public List<Putovanja> GetPutovanja()
        {
            using(SqlConnection conn=new SqlConnection(connectionString))
            {
                var putovanja = new List<Putovanja>();

                conn.Open();

                SqlDependency.Start(connectionString);
                string commandText = "Select * from Putovanja";
               

               

                SqlCommand cmd = new SqlCommand(commandText, conn);
               
                SqlDependency dependency = new SqlDependency(cmd);

                dependency.OnChange += new OnChangeEventHandler(dbChangeNotification);
                
                var reader = cmd.ExecuteReader();
                
                while (reader.Read())
                {

                    var putovanje = new Putovanja
                    {
                        PutovanjaId = Convert.ToInt32(reader["PutovanjaId"]),
                        NazivPutovanja = reader["NazivPutovanja"].ToString(),
                        OpisPutovanja = reader["OpisPutovanja"].ToString(),
                        CijenaPutovanja = (float)Convert.ToDouble(reader["CijenaPutovanja"]),
                        DatumPolaska = Convert.ToDateTime(reader["DatumPolaska"]),
                        DatumDolaska = Convert.ToDateTime(reader["DatumDolaska"]),
                        BrojPutnika =Convert.ToInt32(reader["BrojPutnika"]),
                     
                    };

                    putovanja.Add(putovanje);
                }
               
                return putovanja;
            }
        }

        private void dbChangeNotification(object sender, SqlNotificationEventArgs e)
        {
            _context.Clients.All.SendAsync("refreshPutovanja");
          
        }
        }
}
