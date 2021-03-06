using System;
using System.Collections.Generic;
using static System.Configuration.ConfigurationManager;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Dapper;
using DapperRelational.Entities;

namespace DapperRelational
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            using (var connection=new SqlConnection(ConnectionStrings["MyConnString"].ConnectionString))
            {
                var sql = @"select Capitals.CapitalId,Capitals.Name,Capitals.CountryId
                        , Countries.CountryId,Countries.Name
                        from Capitals
                        inner join Countries
                        on Capitals.CountryId=Countries.CountryId";
                var capitals = connection.Query<Capital, Country, Capital>(sql,
                    (capital, country) =>
                    {
                        capital.Country = country;
                        return capital;
                    }, splitOn: "CountryId").ToList();

                mydatagrid.ItemsSource = capitals;
            }

        }
    }
}
