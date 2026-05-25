using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    class Incident
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
        public DateTime FinalDate { get; set; }
    }

    class FullIncident
    {
        public int Id { get; set; }
        public string AuthorName { get; set; }
        public string AuthorEmail { get; set; }
        public string Description { get; set; }
        public int Priority { get; set; }
        public string CategoryName { get; set; }
        public string SubdivisionName { get; set; }
        public string EmployeName { get; set; }
        public string EmployeEmail { get; set; }
        public string StatusName { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime FinalDate { get; set; }
    }

    class Employe
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string login { get; set; }
        public string password { get; set; }
    }

    class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    class Subdivision
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    class Status
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    class History
    {
        public int Id { get; set; }
        public int IncedentId { get; set; }
        public int EmployeId { get; set; }
        public string Description { get; set; }
        public string zametka { get; set; }
    }

    class toSQL
    {
        string connString = "";

        public toSQL()
        {
            GetConnString();
        }

        public string GetConnString()
        {
            return "123";
        }

        public bool SetConnString(string a)
        {
            return true;
        }

        public int UserIsTrue(string login, string password)
        {
            if(true)
            {
                return 1;
            }
            return 0;
        }
        public string GetUserFullName(int ui)
        {
            return "Веномов Веном Веномович";
        }

        public Incident[] GetAllIncident()
        {
            var list = new List<Incident>();

            return list.ToArray();
        }
        public Incident[] GetAllCategory()
        {
            var list = new List<Incident>();

            return list.ToArray();
        }

        public Incident[] GetAllSubdivision()
        {
            var list = new List<Incident>();

            return list.ToArray();
        }
        public Incident[] GetAllStatus()
        {
            var list = new List<Incident>();

            return list.ToArray();
        }

        public Incident[] GetAllFullIncident()
        {
            var list = new List<Incident>();

            return list.ToArray();
        }
    }
}
