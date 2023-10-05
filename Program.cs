using Npgsql;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

string connectionString = "User Id=postgres;Password=lctasd!#%&(;Server=db.uioaemhkajajdgxqskwv.supabase.co;Port=5432;Database=postgres";

NpgsqlConnection connection;



app.MapGet("/", () => "Hello World!");
app.MapGet("/students", () => GetStudents());

app.Run();


List<Student> GetStudents() {
    List<Student> students = new List<Student>();

    using (connection = new NpgsqlConnection(connectionString)) {
        try {
            connection.Open();
            Console.WriteLine("Connected to PostgreSQL");

        } catch (Exception e) {
            Console.WriteLine(e.Message);
        }

        string query = "SELECT * FROM \"Student\"";
        using (var command = new NpgsqlCommand(query, connection)) {
            using (var reader = command.ExecuteReader()) {
                while (reader.Read())
                {
                    Console.WriteLine($"{reader.GetInt64(0)} {reader.GetString(1)}"); // Assuming the first column is a string
                
                    var newStudent = new Student { Id = reader.GetInt32(0), Name = reader.GetString(1)};
                    students.Add(newStudent);
                }
            }
        }
    } // end of using connection

    return students;
}