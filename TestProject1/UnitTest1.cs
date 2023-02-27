using NUnit.Framework;
using MachineRegistry.Controllers;
using System.Linq;
using MachineRegistry.Models;
using System.Web.Http.Results;
using System.Collections.Generic;
using System.Web.Http;

namespace TestProject1
{
    public class Tests
    {
        MachineController controller = new MachineController();

        [Test]
        public void GetMachines_ShouldReturnEmpty()
        {
            var result = controller.GetAllMachines() as List<Machine>;
            Assert.AreEqual(0, result.Count());
            Assert.IsNull(result.FirstOrDefault());
        }
        
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
        public void PutMachine_ShouldNotUpdateMachine()
        {
            var initialMachines = new List<Machine>()
            {
                new Machine { Id = 0, Name = "Train" },
                new Machine { Id = 1, Name = "Digger" },
                new Machine { Id = 2, Name = "Firetruck"},
                new Machine { Id = 3, Name = "Airplane"}
            };

            foreach (var machine in initialMachines)
            {
                controller.PostMachine(machine);
            }


            var invalidMachine = new Machine { Id = 5, Name = "Invalid ID on update" };
            var result = controller.PutMachine(5, invalidMachine);

            Assert.IsInstanceOf<NotFoundResult>(result);

            invalidMachine = new Machine { Id = 3, Name = "" };
            result = controller.PutMachine(3, invalidMachine);

            Assert.IsInstanceOf<BadRequestErrorMessageResult>(result);

            var updateResult = controller.GetMachine(3) as OkNegotiatedContentResult<Machine>;


            Assert.IsNotEmpty(updateResult.Content.Name);
            Assert.IsInstanceOf<BadRequestErrorMessageResult>(result);


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