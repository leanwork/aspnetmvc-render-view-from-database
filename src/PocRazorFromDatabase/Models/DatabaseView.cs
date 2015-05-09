using MongoRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PocRazorFromDatabase.Models
{
    [CollectionName("views")]
    public class DatabaseView : Entity
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public string Content { get; set; }
    }
}