using MachineRegistry.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MachineRegistry.Controllers
{
    public class MachineController : ApiController
    {
        static List<Machine> machines = new List<Machine>();
        //{
        //    new Machine {Id = 0, Name = "Default"},
        //    new Machine {Id = 1, Name = "Gravemaskin"},
        //    new Machine {Id = 2, Name = "Brannbil"}
        //};

        public IEnumerable<Machine> GetAllMachines()
        {
            return machines;
        }
        public IHttpActionResult GetMachine(int id)
        {
            var machine = machines.FirstOrDefault((m) => m.Id == id);
            if(machine == null)
            {
                return NotFound();
            }
            return Ok(machine);
        }     

        public IHttpActionResult PostMachine(Machine machine)
        {
            if(machine == null)
            {
                return BadRequest("machine cannot be null");
            }
            if(machines.Count() < 1)
            {
                machine.Id = 0;
            }
            else
            {
                machine.Id = machines.Last().Id + 1;
            }
            
            machines.Add(machine);

            return CreatedAtRoute("DefaultApi", new { id = machine.Id }, machine);
            //return Ok(machines);
        }

        //public void Post([FromBody] string value)
        //{

        //}
        public IHttpActionResult PutMachine(int id, Machine machine)
        {
            if (id < 0)
            {
                return BadRequest("Not a valid ID");
            }

            if (machine == null)
            {
                return BadRequest("Machine cannot be null");
            }

            var existingMachine = machines.FirstOrDefault(m => m.Id == id);

            if(existingMachine == null)
            {
                return NotFound();
            }
            existingMachine.Name = machine.Name;
            return Ok(existingMachine);
        }

        public IHttpActionResult DeleteMachine(int id)
        {
            if(id < 0)
            {
                return BadRequest("Not a valid ID");
            }
            var machine = machines.FirstOrDefault(m => m.Id == id);
            if(machine != null)
            {
                machines.Remove(machine);
                return Ok();
            }

            return NotFound();
        }
    }
}
