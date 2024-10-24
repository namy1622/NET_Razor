using System;
using System.ComponentModel.DataAnnotations;

namespace razorwebapp_sql.Models
{
    public class Article
    {
        public int ID { get; set; }
        public string Title { get; set; }

        [DataType(DataType.Date)]
        public DateTime PublishDate { get; set; }

        public string Content {set; get;}
    }
}