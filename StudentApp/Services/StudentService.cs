using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using StudentApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentApp.Services
{
    public class StudentService
    {
        private readonly  IMongoCollection<Student> students;
        private readonly  IMongoCollection<Department> departments;

        public  StudentService(IConfiguration config)
        {
            MongoClient client = new MongoClient(config.GetConnectionString("StudentDb"));
            IMongoDatabase database = client.GetDatabase("StudentDb");
            students = database.GetCollection<Student>("students");
            departments = database.GetCollection<Department>("Department");
        }

        public List<Student> Get()
        {
            return students.Find(student => true).ToList();
        }
       /* public List<Student> Get2()
        {
            return students.Aggregate()
                .Lookup(
                    foreignCollection: departments,
                    localField: e => e.Dep_Id,
                    foreignField: f => f.Id,
                    @as: ()

                )
                .As<BsonDocument>().ToList();
        }*/

        public List<Department> GetDep()
        {
            return departments.Find(department => true).ToList();
        }

        public  Student Get(string id)
        {
            return students.Find(student => student.Id == id).FirstOrDefault();
        }

        public Student Create(Student student)
        {
            students.InsertOne(student);
            return student;
        }

        public  void Update(string id, Student studentIn)
        {
            students.ReplaceOne(student => student.Id == id, studentIn);
        }

        public void Remove(Student studentIn)
        {
            students.DeleteOne(student => student.Id == studentIn.Id);
        }

        public void Remove(string id)
        {
            students.DeleteOne(student => student.Id == id);
        }
    }
}
