using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPFSemana04
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>


    public partial class MainWindow : Window
    {
        SqlConnection connection = new SqlConnection(@"Server=DESKTOP-KPV6S6K\SQLEXPRESS;DataBase=School;User Id=userTest;Password=123456");
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            List<Person> people = new List<Person>();

            connection.Open();
            SqlCommand command = new SqlCommand("BuscarPor", connection);

            command.CommandType = CommandType.StoredProcedure;

            SqlParameter parameter = new SqlParameter();
            parameter.SqlDbType = SqlDbType.VarChar;
            parameter.Size = 50;
            parameter.Value = "";
            parameter.Value = txtFirstName.Text.Trim();
            parameter.ParameterName = "@FirstName";

            SqlParameter parameter2 = new SqlParameter();
            parameter2.SqlDbType = SqlDbType.VarChar;
            parameter2.Size = 50;
            parameter.Value = "";
            parameter2.Value = txtLastName.Text.Trim();
            parameter2.ParameterName = "@LastName";


            command.Parameters.Add(parameter);
            command.Parameters.Add(parameter2);


            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Person person = new Person
                {
                    PersonId = reader["PersonId"] != DBNull.Value ? (int)reader["PersonId"] : 0,
                    FirstName = reader["FirstName"] != DBNull.Value ? (string)reader["FirstName"] : string.Empty,
                    LastName = reader["LastName"] != DBNull.Value ? (string)reader["LastName"] : string.Empty,
                    EnrollmentDate = reader["EnrollmentDate"] != DBNull.Value ? (DateTime)reader["EnrollmentDate"] : default,


                };

                people.Add(person);

            }
            connection.Close();
            dgvPeople.ItemsSource = people;

        }
    }
}
