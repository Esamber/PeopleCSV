using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using Faker;
using Microsoft.EntityFrameworkCore;

namespace CSVPeople.Data
{
    public class PeopleRepository
    {
        private readonly string _connectionString;
        public PeopleRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public List<Person> GetPeople()
        {
            using var ctx = new PeopleDbContext(_connectionString);
            return ctx.People
                .ToList();
        }

        public void AddPeopleBase64(string base64File)
        {
            int commaIndex = base64File.IndexOf(',');
            string base64 = base64File.Substring(commaIndex + 1);
            byte[] fileData = Convert.FromBase64String(base64);
            List<Person> people = GetFromCsv(fileData);
            AddPeople(people);
        }

        public void AddPeople(List<Person> people)
        {
            using var ctx = new PeopleDbContext(_connectionString);
            ctx.People.AddRange(people);
            ctx.SaveChanges();
        }

        static List<Person> GetFromCsv(byte[] csvBytes)
        {
            using var memoryStream = new MemoryStream(csvBytes);
            using var reader = new StreamReader(memoryStream);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            return csv.GetRecords<Person>().ToList();
        }

        public string GeneratePeopleListCsv(int quantity)
        {
            return GetCsv(GeneratePeopleList(quantity));
        }

        public List<Person> GeneratePeopleList(int quantity)
        {
            var people = new List<Person>();
            for (int i = 0; i < quantity; i++)
            {
                var person = new Person { 
                    FirstName = Faker.Name.First(), 
                    LastName = Faker.Name.Last(), 
                    Address = Faker.Address.StreetAddress(), 
                    Age = Faker.RandomNumber.Next(20, 100) };
                person.Email = Faker.Internet.Email($"{person.FirstName} {person.LastName}");
                people.Add(person);
            }
            return people;
        }

        static string GetCsv(List<Person> ppl)
        {
            var builder = new StringBuilder();
            var stringWriter = new StringWriter(builder);

            using var csv = new CsvWriter(stringWriter, CultureInfo.InvariantCulture);
            csv.WriteRecords(ppl);

            return builder.ToString();
        }

        public void DeletePeople()
        {
            using var ctx = new PeopleDbContext(_connectionString);
            ctx.Database.ExecuteSqlInterpolated($"DELETE FROM People");
        }
    }
}
