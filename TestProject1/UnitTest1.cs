using NUnit.Framework;
using MachineRegistry.Controllers;
using System.Linq;
using MachineRegistry.Models;
using System.Web.Http.Results;
using System.Collections.Generic;

namespace TestProject1
{
    public class Tests
    {
        MachineController controller = new MachineController();

        [Test]
        public void PostMachine_ShouldCreateMachinesAndReturnOK()
        {

            var initialMachines = new List<Machine>()
            {
                new Machine { Id = 0, Name = "Train" },
                new Machine { Id = 2, Name = "Digger" },
                new Machine { Id = 3, Name = "Firetruck"}
            };

            foreach (var machine in initialMachines)
            {
                controller.PostMachine(machine);
            }

            var result = controller.GetAllMachines() as List<Machine>;


            
            Assert.AreEqual(3, result.Count);
            Assert.AreEqual("Train", result[0].Name);
            Assert.AreEqual("Digger", result[1].Name);
            Assert.AreEqual("Firetruck", result[2].Name);

        }

        [Test]
        public void PutMachine_ShouldUpdateMachine()
        {


            var machine = new Machine { Id = 2, Name = "Ambulanse" };
            var result = controller.PutMachine(2, machine);


            var updatedResult = controller.GetMachine(2) as OkNegotiatedContentResult<Machine>;
            Assert.IsNotNull(result);
            Assert.AreEqual("Ambulanse", updatedResult.Content.Name);

        }

       

       

       

        [Test]
        public void DeleteMachine_ShouldDeleteMachine()
        {
            

            var result = controller.DeleteMachine(2) as OkResult;
            
            var machinesList = controller.GetAllMachines().ToList();
            Assert.Less(machinesList.Count,3);
            
        }
    }
}