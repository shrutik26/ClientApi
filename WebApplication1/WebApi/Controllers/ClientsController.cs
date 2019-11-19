﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using ClientWork.Models;
using System.Web.Routing;

namespace ClientWork.Controllers
{
    public class ClientsController : ApiController
    {
        private ClientDBContext db = new ClientDBContext();

        // GET: api/Clients
        [Route("enterData/")]
        public IQueryable<Client> GetClients()
        {
            int data = 5;
            return db.Clients.Include(s => s.clientAddresses).Take(data);
        }


        [Route("enterData/{FName}/{LName}")]
        [ResponseType(typeof(Client))]
        public IHttpActionResult GetClientByBothName(string FName, string LName)
        {


            var clients = db.Clients.Include(data => data.clientAddresses).Where(details => details.FirstName == FName && details.LastName == LName).ToList();

            if (clients == null)
            {
                return NotFound();
            }

            return Ok(clients);
        }

        [Route("enterData/{Name}")]
        [ResponseType(typeof(Client))]
        public IHttpActionResult GetClientByName(string Name)
        {


            var clients = db.Clients.Include(data => data.clientAddresses).Where(details => details.FirstName == Name || details.LastName == Name).ToList();

            if (clients == null)
            {
                return NotFound();
            }

            return Ok(clients);
        }



        /*   // GET: api/Clients/5
           [ResponseType(typeof(Client))]
           public IHttpActionResult GetClient(int id)
           {
               Client client = db.Clients.Find(id);
               if (client == null)
               {
                   return NotFound();
               }

               return Ok(client);
           }


           // GET: api/Clients?client
           [ResponseType(typeof(Client))]
           [EnableCors(origins: "*", headers: "*", methods: "*")]
           public IHttpActionResult GetClientInformation(String FName,String LName)
           {

               IList<Client> clients = null;

               clients = db.Clients.Include(x => x.clientAddresses).Where(s => s.FirstName == FName || s.LastName == LName).ToList();
               if (clients == null)
               {
                   return NotFound();
               }

               return Ok(clients);
           }



            // GET: api/Clients/Vaibhav
           [ResponseType(typeof(Client))]
           [EnableCors(origins: "*", headers: "*", methods: "*")]
           public IHttpActionResult GetClientInformation(Client client)
           {

               IList<Client> clients = null;

               clients = db.Clients.Include(x => x.clientAddresses).Where(s => s.FirstName == client.FirstName || s.LastName == client.LastName).ToList();
               if (clients == null)
               {
                   return NotFound();
               }

               return Ok(clients);
           } */




        // PUT: api/Clients/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutClient(int id, Client client)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != client.ClientId)
            {
                return BadRequest();
            }

            db.Entry(client).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClientExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Clients
        [ResponseType(typeof(Client))]
        public IHttpActionResult PostClient(Client client)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Clients.Add(client);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = client.ClientId }, client);
        }

        // DELETE: api/Clients/5
        [ResponseType(typeof(Client))]
        public IHttpActionResult DeleteClient(int id)
        {
            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return NotFound();
            }

            db.Clients.Remove(client);
            db.SaveChanges();

            return Ok(client);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ClientExists(int id)
        {
            return db.Clients.Count(e => e.ClientId == id) > 0;
        }


    }
}