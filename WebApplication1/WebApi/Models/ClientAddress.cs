using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ClientWork.Models
{
    public class ClientAddress
    {
        public int Id { get; set; }
        public string Address { get; set; }
       
        public int ClientId { get; set; }

        //public Client Client { get; set; }
    }
}