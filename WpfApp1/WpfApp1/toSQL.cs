using System;
using System.IO;
using System.Reflection;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Remoting.Messaging;
using System.Data;
using System.Windows.Documents;

namespace WpfApp1
{
    public class Incident
    {
        public int Id { get; set; }
        public string AuthorName { get; set; }
        public string AuthorEmail { get; set; }
        public string Description { get; set; }
        public int Priority { get; set; }
        public int CategoryId { get; set; }
        public int SubdivisionId { get; set; }
        public int EmployeId { get; set; }
        public int StatusId { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ?FinalDate { get; set; }
    }

    public class IncidentFile
    {
        public int Id { get; set; }
        public int IncidentId { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public long FileSize { get; set; }
        public byte[] FileData { get; set; }
    }

    public class FullIncident
    {
        public int Id { get; set; }
        public string AuthorName { get; set; }
        public string AuthorEmail { get; set; }
        public string Description { get; set; }
        public int Priority { get; set; }
        public string CategoryName { get; set; }
        public string SubdivisionName { get; set; }
        public int EmloyeId { get; set; }
        public string EmployeName { get; set; }
        public string EmployeEmail { get; set; }
        public string StatusName { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ?FinalDate { get; set; }
        public int ?FreshDate { get; set; }
    }

    public class Employe
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string login { get; set; }
        public string password { get; set; }
    }

    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        override public string ToString() { return Name; }
    }

    public class Subdivision
    {
        public int Id { get; set; }
        public string Name { get; set; }
        override public string ToString() { return Name; }
    }

    public class Status
    {
        public int Id { get; set; }
        public string Name { get; set; }
        override public string ToString() { return Name; }
    }

    public class History
    {
        public int Id { get; set; }
        public int IncidentId { get; set; }
        public int EmployeId { get; set; }
        public string Description { get; set; }
        public string zametka { get; set; }
    }

    class toSQL
    {
        //string connectionStr = "Server=localhost;Database=IncidentManagmentSystem;Trusted_Connection=True;TrustServerCertificate=true";

        public toSQL()
        {
            GetConnString();
        }

        public string GetConnString()
        {
            string toThisPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string connectionFilePath = Path.Combine(toThisPath, "Connection.txt");

            try
            {
                string connectionStr = File.ReadAllText(connectionFilePath).Trim();
                return connectionStr;
            }
            catch {}
            return "";
        }

        public bool SetConnString(string a)
        {
            string toThisPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string connectionFilePath = Path.Combine(toThisPath, "Connection.txt");

            File.WriteAllText(connectionFilePath, a.Trim());
            return true;
        }

        public bool TestConnection(string connectionString)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    return true;
                }
            }
            catch {}

            return false;
        }

        public int UserIsTrue(string login, string password)
        {
            try
            {
                using (var connection = new SqlConnection(GetConnString()))
                {
                    connection.Open();
                    var command = new SqlCommand("select Id from Employees where Login=@login and Password=@password", connection);
                    command.Parameters.AddWithValue("@login", login);
                    command.Parameters.AddWithValue("@password", password);
                    var reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        int a = (int)reader["Id"];
                        if (a != 0)
                        {
                            return a;
                        }
                    }
                }
                return 0;
            }
            catch {}

            return 0;
        }

        

        public Incident[] GetAllIncident()
        {
            var list = new List<Incident>();

            using (var connection = new SqlConnection(GetConnString()))
            {
                connection.Open();
                var command = new SqlCommand("SELECT * FROM Incidents ORDER BY id", connection);
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(new Incident
                    {
                        Id = (int)reader["id"],
                        AuthorName = reader["AuthorName"].ToString(),
                        AuthorEmail = reader["AuthorEmail"].ToString(),
                        Description = reader["Description"].ToString(),
                        Priority = (int)reader["Priority"],
                        CategoryId = (int)reader["CategoryId"],
                        SubdivisionId = (int)reader["SubdivisionId"],
                        EmployeId = (int)reader["EmployeId"],
                        StatusId = (int)reader["StatusId"],
                        CreateDate = (DateTime)reader["CreateDate"],
                        FinalDate = (DateTime)reader["FinalDate"]
                    });
                }
            }
            return list.ToArray();
        }

        public Incident GetIncidentById(int id)
        {
            
            using (var connection = new SqlConnection(GetConnString()))
            {
                connection.Open();
                var command = new SqlCommand("SELECT * FROM Incidents where id = @id ORDER BY id", connection);
                command.Parameters.AddWithValue("@id", id);
                var reader = command.ExecuteReader();


                if (reader.Read())
                {
                    return new Incident
                    {
                        Id = (int)reader["id"],
                        AuthorName = reader["AuthorName"].ToString(),
                        AuthorEmail = reader["AuthorEmail"].ToString(),
                        Description = reader["Description"].ToString(),
                        Priority = (int)reader["Priority"],
                        CategoryId = (int)reader["CategoryId"],
                        SubdivisionId = (int)reader["SubdivisionId"],
                        EmployeId = (int)reader["EmployeId"],
                        StatusId = (int)reader["StatusId"],
                        CreateDate = (DateTime)reader["CreateDate"],
                        FinalDate = (DateTime)reader["FinalDate"]
                    };
                }

            }
            return null;
        }

        public Employe[] GetAllEmploye()
        {
            var list = new List<Employe>();

            using (var connection = new SqlConnection(GetConnString()))
            {
                connection.Open();
                var command = new SqlCommand("SELECT * FROM Employees ORDER BY id", connection);
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(new Employe
                    {
                        Id = (int)reader["id"],
                        Name = reader["Name"].ToString(),
                        Email = reader["Email"].ToString(),
                        login = reader["Login"].ToString(),
                        password = reader["Password"].ToString(),
                    });
                }
            }
            return list.ToArray();
        }

        public Employe GetEmployeById(int id)
        {

            using (var connection = new SqlConnection(GetConnString()))
            {
                connection.Open();
                var command = new SqlCommand("SELECT * FROM Employees where id = @id ORDER BY id", connection);
                command.Parameters.AddWithValue("@id", id);
                var reader = command.ExecuteReader();


                if (reader.Read())
                {
                    return new Employe
                    {
                        Id = (int)reader["id"],
                        Name = reader["Name"].ToString(),
                        Email = reader["Email"].ToString(),
                        login = reader["Login"].ToString(),
                        password = reader["Password"].ToString(),
                    };
                }

            }
            return null;
        }
        public Category[] GetAllCategory()
        {
            var list = new List<Category>();
            using (var connection = new SqlConnection(GetConnString()))
            {
                connection.Open();
                var command = new SqlCommand("select * from Categories order by Id", connection);
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(new Category
                    {
                        Id = (int)reader["Id"],
                        Name = reader["Name"].ToString()
                    });
                }
            }

            return list.ToArray();
        }
        public Category GetCategoryById(int id)
        {

            using (var connection = new SqlConnection(GetConnString()))
            {
                connection.Open();
                var command = new SqlCommand("SELECT * FROM Categories where id = @id ORDER BY id", connection);
                command.Parameters.AddWithValue("@id", id);
                var reader = command.ExecuteReader();


                if (reader.Read())
                {
                    return new Category
                    {
                        Id = (int)reader["id"],
                        Name = reader["Name"].ToString(),
                    };
                }

            }
            return null;
        }

        public Subdivision[] GetAllSubdivision()
        {
            var list = new List<Subdivision>();
            using (var connection = new SqlConnection(GetConnString()))
            {
                connection.Open();
                var command = new SqlCommand("select * from Subdivisions order by Id", connection);
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(new Subdivision
                    {
                        Id = (int)reader["Id"],
                        Name = reader["Name"].ToString()
                    });
                }
            }

            return list.ToArray();
        }
        public Subdivision GetSubdivisionById(int id)
        {

            using (var connection = new SqlConnection(GetConnString()))
            {
                connection.Open();
                var command = new SqlCommand("SELECT * FROM Subdivisions where id = @id ORDER BY id", connection);
                command.Parameters.AddWithValue("@id", id);
                var reader = command.ExecuteReader();


                if (reader.Read())
                {
                    return new Subdivision
                    {
                        Id = (int)reader["id"],
                        Name = reader["Name"].ToString(),
                    };
                }

            }
            return null;
        }
        public Status[] GetAllStatus()
        {
            var list = new List<Status>();
            using (var connection = new SqlConnection(GetConnString()))
            {
                connection.Open();
                var command = new SqlCommand("select * from Statuses order by Id", connection);
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(new Status
                    {
                        Id = (int)reader["Id"],
                        Name = reader["Name"].ToString()
                    });
                }
            }

            return list.ToArray();
        }

        public FullIncident[] GetAllFullIncident()
        {
            var list = new List<FullIncident>();

            using (var connection = new SqlConnection(GetConnString()))
            {
                connection.Open();
                var command = new SqlCommand(@"Select I.Id, I.AuthorName, I.AuthorEmail, I.Description, I.Priority, C.Name as CName, 
                    Su.Name as SuName, E.Id as EId, E.Name as EName, E.Email as EEMail, St.Name as StName, I.CreateDate, I.FinalDate,
                    DATEDIFF(DAY, GETDATE(), I.FinalDate) as FreshDate from Incidents I join Categories C on C.Id = I.CategoryId join 
                    Subdivisions Su on Su.Id = I.SubdivisionId join Employees E on E.Id = I.EmployeId join Statuses St on St.Id = 
                    I.StatusId order by I.Id", connection);
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(new FullIncident
                    {
                        Id = (int)reader["id"],
                        AuthorName = reader["AuthorName"].ToString(),
                        AuthorEmail = reader["AuthorEmail"].ToString(),
                        Description = reader["Description"].ToString(),
                        Priority = (int)reader["Priority"],
                        CategoryName = reader["CName"].ToString(),
                        SubdivisionName = reader["SuName"].ToString(),
                        EmloyeId = (int)reader["EId"],
                        EmployeName = reader["EName"].ToString(),
                        EmployeEmail = reader["EEMail"].ToString(),
                        StatusName = reader["StName"].ToString(),
                        CreateDate = (DateTime)reader["CreateDate"],
                        FinalDate = (DateTime)reader["FinalDate"],
                        FreshDate = (int)reader["FreshDate"],
                    });
                }
            }
            return list.ToArray();
        }

        public int GetNewId(string type)
        {

            string a;
            using (var connection = new SqlConnection(GetConnString()))
            {
                connection.Open();
                var command = new SqlCommand("SELECT (IDENT_CURRENT(@type) + 1) AS NextID", connection);
                command.Parameters.AddWithValue("@type", type);
                var reader = command.ExecuteReader();

                reader.Read();
                a = reader["NextID"].ToString();
                
            }
            return Convert.ToInt32(a);
        }

        public int CreateIncident(Incident a)
        {

            using (var connection = new SqlConnection(GetConnString()))
            {
                connection.Open();
                var command = new SqlCommand(@"INSERT INTO Incidents (AuthorName, AuthorEmail, Description, Priority, CategoryId, SubdivisionId, EmployeId, StatusId, CreateDate, FinalDate) VALUES
                    (@AuthorName, @AuthorEmail, @Description, @Priority, @CategoryId, @SubdivisionId, @EmployeId, @StatusId, @CreateDate, @FinalDate);
                    ", connection);

                command.Parameters.AddWithValue("@AuthorName", a.AuthorName);
                command.Parameters.AddWithValue("@AuthorEmail", a.AuthorEmail);
                command.Parameters.AddWithValue("@Description", a.Description);
                command.Parameters.AddWithValue("@Priority", a.Priority);
                command.Parameters.AddWithValue("@CategoryId", a.CategoryId);
                command.Parameters.AddWithValue("@SubdivisionId", a.SubdivisionId);
                command.Parameters.AddWithValue("@EmployeId", a.EmployeId);
                command.Parameters.AddWithValue("@StatusId", a.StatusId);
                command.Parameters.AddWithValue("@CreateDate", a.CreateDate);
                command.Parameters.AddWithValue("@FinalDate", a.FinalDate);


                return Convert.ToInt32(command.ExecuteScalar());
            }
        }

        public bool UpdateIncident(Incident a)
        {
            using (var connection = new SqlConnection(GetConnString()))
            {
                connection.Open();
                var command = new SqlCommand(@"UPDATE Incidents SET AuthorName = @AuthorName, AuthorEmail = @AuthorEmail, Description = @Description, Priority = @Priority, CategoryId = @CategoryId, 
                    SubdivisionId = @SubdivisionId, EmployeId = @EmployeId, StatusId = @StatusId, FinalDate = @FinalDate WHERE id = @Id
                    ", connection);

                command.Parameters.AddWithValue("@Id", a.Id);
                command.Parameters.AddWithValue("@AuthorName", a.AuthorName);
                command.Parameters.AddWithValue("@AuthorEmail", a.AuthorEmail);
                command.Parameters.AddWithValue("@Description", a.Description);
                command.Parameters.AddWithValue("@Priority", a.Priority);
                command.Parameters.AddWithValue("@CategoryId", a.CategoryId);
                command.Parameters.AddWithValue("@SubdivisionId", a.SubdivisionId);
                command.Parameters.AddWithValue("@EmployeId", a.EmployeId);
                command.Parameters.AddWithValue("@StatusId", a.StatusId);
                command.Parameters.AddWithValue("@FinalDate", a.FinalDate);


                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }

        public bool UpdateEmploye(Employe a)
        {
            using (var connection = new SqlConnection(GetConnString()))
            {
                connection.Open();
                var command = new SqlCommand(@"UPDATE Employees SET Name = @Name, Email = @Email, Login = @Login, Password = @Password WHERE id = @Id
                    ", connection);

                command.Parameters.AddWithValue("@Id", a.Id);
                command.Parameters.AddWithValue("@Name", a.Name);
                command.Parameters.AddWithValue("@Email", a.Email);
                command.Parameters.AddWithValue("@Login", a.login);
                command.Parameters.AddWithValue("@Password", a.password);


                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }

        public int CreateEmploye(Employe a)
        {
            using (var connection = new SqlConnection(GetConnString()))
            {
                connection.Open();
                var command = new SqlCommand(@"INSERT INTO Employees (Name, Email, Login, Password) values (@Name, @Email, @Login, @Password);
                    ", connection);

                //command.Parameters.AddWithValue("@Id", a.Id);
                command.Parameters.AddWithValue("@Name", a.Name);
                command.Parameters.AddWithValue("@Email", a.Email);
                command.Parameters.AddWithValue("@Login", a.login);
                command.Parameters.AddWithValue("@Password", a.password);

                return Convert.ToInt32(command.ExecuteScalar());
            }
        }

        public bool UpdateCategory(Category a)
        {
            using (var connection = new SqlConnection(GetConnString()))
            {
                connection.Open();
                var command = new SqlCommand(@"UPDATE Categories SET Name = @Name WHERE id = @Id
                    ", connection);

                command.Parameters.AddWithValue("@Id", a.Id);
                command.Parameters.AddWithValue("@Name", a.Name);

                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
        public int CreateCategory(Category a)
        {
            using (var connection = new SqlConnection(GetConnString()))
            {
                connection.Open();
                var command = new SqlCommand(@"INSERT INTO Categories (Name) values (@Name);
                    ", connection);

                //command.Parameters.AddWithValue("@Id", a.Id);
                command.Parameters.AddWithValue("@Name", a.Name);

                return Convert.ToInt32(command.ExecuteScalar());
            }
        }
        public bool UpdateSubdivision(Subdivision a)
        {
            using (var connection = new SqlConnection(GetConnString()))
            {
                connection.Open();
                var command = new SqlCommand(@"UPDATE Subdivisions SET Name = @Name WHERE id = @Id
                    ", connection);

                command.Parameters.AddWithValue("@Id", a.Id);
                command.Parameters.AddWithValue("@Name", a.Name);

                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
        public int CreateSubdivision(Subdivision a)
        {
            using (var connection = new SqlConnection(GetConnString()))
            {
                connection.Open();
                var command = new SqlCommand(@"INSERT INTO Subdivisions (Name) values (@Name);
                    ", connection);

                //command.Parameters.AddWithValue("@Id", a.Id);
                command.Parameters.AddWithValue("@Name", a.Name);

                return Convert.ToInt32(command.ExecuteScalar());
            }
        }

        public int LoginIsUnique(string login)
        {
            using (var connection = new SqlConnection(GetConnString()))
            {
                connection.Open();
                var command = new SqlCommand(@"select count(login) as CL from Employees where login = @login ;
                    ", connection);

                //command.Parameters.AddWithValue("@Id", a.Id);
                command.Parameters.AddWithValue("@login", login);
                var reader = command.ExecuteReader();
                reader.Read();

                return (int)reader["CL"];

            }
        }

        public bool CIWE(int Id)
        {
            using (var connection = new SqlConnection(GetConnString()))
            {
                connection.Open();
                var command = new SqlCommand(@"select count(I.id) as ref from Incidents I join Employees E on E.Id = I.EmployeId where E.Id = @Id
                    ", connection);

                //command.Parameters.AddWithValue("@Id", a.Id);
                command.Parameters.AddWithValue("@Id", Id);
                var reader = command.ExecuteReader();
                reader.Read();
                if ((int)reader["ref"] > 0)
                {
                    return true;
                }
            }

            return false;
        }

        public bool CIWC(int Id)
        {
            using (var connection = new SqlConnection(GetConnString()))
            {
                connection.Open();
                var command = new SqlCommand(@"select count(I.id) as ref from Incidents I join Categories E on E.Id = I.EmployeId where E.Id = @Id
                    ", connection);

                //command.Parameters.AddWithValue("@Id", a.Id);
                command.Parameters.AddWithValue("@Id", Id);
                var reader = command.ExecuteReader();
                reader.Read();
                if ((int)reader["ref"] > 0)
                {
                    return false;
                }
            }

            return true;
        }

        public bool CIWS(int Id)
        {
            using (var connection = new SqlConnection(GetConnString()))
            {
                connection.Open();
                var command = new SqlCommand(@"select count(I.id) as ref from Incidents I join Subdivisions E on E.Id = I.EmployeId where E.Id = @Id
                    ", connection);

                //command.Parameters.AddWithValue("@Id", a.Id);
                command.Parameters.AddWithValue("@Id", Id);
                var reader = command.ExecuteReader();
                reader.Read();
                if ((int)reader["ref"] > 0)
                {
                    return true;
                }
            }

            return false;
        }

        public bool DeleteEmploye(int id)
        {
            using (SqlConnection sqlConnection = new SqlConnection(GetConnString()))
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("delete from Employees where Id = @id", sqlConnection);
                sqlCommand.Parameters.AddWithValue("@Id", id);


                int num = sqlCommand.ExecuteNonQuery();
                return num > 0;
            }
        }

        public bool DeleteCategory(int id)
        {
            using (SqlConnection sqlConnection = new SqlConnection(GetConnString()))
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("delete from Categories where Id = @id", sqlConnection);
                sqlCommand.Parameters.AddWithValue("@Id", id);


                int num = sqlCommand.ExecuteNonQuery();
                return num > 0;
            }
        }
        public bool DeleteSubdivision(int id)
        {
            using (SqlConnection sqlConnection = new SqlConnection(GetConnString()))
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("delete from subdivisions where Id = @id", sqlConnection);
                sqlCommand.Parameters.AddWithValue("@Id", id);


                int num = sqlCommand.ExecuteNonQuery();
                return num > 0;
            }
        }

        public int CreateFile(IncidentFile a)
        {
            using (var connection = new SqlConnection(GetConnString()))
            {
                connection.Open();
                var command = new SqlCommand(@"insert into Files (incidentId, FileName, ContentType, FileSize, FileData, UploadDate) values (@incidentId, @FileName, @ContentType, @FileSize, @FileData, @UploadDate);
                    ", connection);

                //command.Parameters.AddWithValue("@Id", a.Id);
                command.Parameters.AddWithValue("@incidentId", a.IncidentId);
                command.Parameters.AddWithValue("@FileName", a.FileName);
                command.Parameters.AddWithValue("@ContentType", a.ContentType);
                command.Parameters.AddWithValue("@FileSize", a.FileSize);
                command.Parameters.AddWithValue("@FileData", SqlDbType.VarBinary).Value = a.FileData;
                command.Parameters.AddWithValue("@UploadDate", DateTime.Now);

                return Convert.ToInt32(command.ExecuteScalar());
            }
        }

        public IncidentFile[] GetAllFileById(int Id)
        {
            var list = new List<IncidentFile>();

            using (var connection = new SqlConnection(GetConnString()))
            {
                connection.Open();
                var command = new SqlCommand("select * from Files where incidentId = @Id order by Id;", connection);
                command.Parameters.AddWithValue("@Id", Id);
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(new IncidentFile
                    {
                        Id = (int)reader["id"],
                        IncidentId = (int)reader["IncidentId"],
                        FileName = reader["FileName"].ToString(),
                        ContentType = reader["ContentType"].ToString(),
                        FileSize = (long)reader["FileSize"],
                        FileData = (byte[])reader["FileData"]
                    });
                }
            }
            return list.ToArray();
        }

        public bool DeleteFile(int id)
        {
            using (SqlConnection sqlConnection = new SqlConnection(GetConnString()))
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("delete from Files where Id = @id", sqlConnection);
                sqlCommand.Parameters.AddWithValue("@Id", id);


                int num = sqlCommand.ExecuteNonQuery();
                return num > 0;
            }
        }

        public CategoryStatistic[] GetCategoryStatistic()
        {
            var list = new List<CategoryStatistic>();
            using (var connection = new SqlConnection(GetConnString()))
            {
                connection.Open();
                var command = new SqlCommand("select S.Name, count(I.StatusId) as MaxStat from Statuses S join Incidents I on I.StatusId = S.Id group by S.Name order by MaxStat", connection);
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(new CategoryStatistic
                    {
                        CategoryName = reader["Name"].ToString(),
                        Count = (int)reader["MaxStat"]
                    });
                }
            }

            return list.ToArray();
        }

        public History[] GetHistoryById(int Id)
        {
            var list = new List<History>();

            using (var connection = new SqlConnection(GetConnString()))
            {
                connection.Open();
                var command = new SqlCommand("select * from Histories where IncidentId = @id order by Id", connection);
                command.Parameters.AddWithValue("@Id", Id);
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(new History
                    {
                        Id = (int)reader["Id"],
                        IncidentId = (int)reader["IncidentId"],
                        EmployeId = (int)reader["EmployeId"],
                        Description = reader["Description"].ToString(),
                    });
                }
            }
            return list.ToArray();
        }

        public int CreateHistory(History a)
        {
            using (var connection = new SqlConnection(GetConnString()))
            {
                connection.Open();
                var command = new SqlCommand(@"insert into Histories(IncidentId, EmployeId, Description, ChangeDate) values (@IncidentId, @EmployeId, @Description, @ChangeDate);
                    ", connection);

                //command.Parameters.AddWithValue("@Id", a.Id);
                command.Parameters.AddWithValue("@IncidentId", a.IncidentId);
                command.Parameters.AddWithValue("@EmployeId", a.EmployeId);
                command.Parameters.AddWithValue("@Description", a.Description);
                command.Parameters.AddWithValue("@ChangeDate", DateTime.Now);

                return Convert.ToInt32(command.ExecuteScalar());
            }
        }

        public string GetStatusNameById(int Id)
        {
            using (var connection = new SqlConnection(GetConnString()))
            {
                connection.Open();
                var command = new SqlCommand("select Name from Statuses where Id = @Id", connection);
                command.Parameters.AddWithValue("@Id", Id);
                var reader = command.ExecuteReader();

                reader.Read();
                return reader["Name"].ToString();
                
            }
        }
    }
    public class CategoryStatistic
    {
        public string CategoryName { get; set; }
        public int Count { get; set; }
    }
}
