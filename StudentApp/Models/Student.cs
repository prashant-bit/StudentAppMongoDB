using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StudentApp.Models
{
    public class Student
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Name")]
        [Required]
        public string Name { get; set; }

        [BsonElement("Usn")]
        [Required]
        public string Usn{ get; set; }

        [BsonElement("Email")]
        [Required]
        public string Email { get; set; }

     
        [BsonElement("ImageUrl")]
        [Display(Name = "Photo")]
        [DataType(DataType.ImageUrl)]
        [Required]
        public string ImageUrl { get; set; }

        [BsonElement("Subjects")]
        public string Subject { get; set; }

        [BsonElement("State")]
        public string State { get; set; }

        [BsonElement("Contact_Number")]
        public string Contact_Number { get; set; }

        [BsonElement("Dep_Id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Dep_Id { get; set; }

        [BsonElement("Dep_Name")]
        public string Dep_Name { get; set; }

    }

}
