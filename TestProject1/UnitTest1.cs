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
               var response = controller.PostMachine(machine);
                Assert.IsInstanceOf<OkNegotiatedContentResult<List<Machine>>>(response);
            }

            var result = controller.GetAllMachines() as List<Machine>;
            

            
            Assert.Greater(result.Count,0);

        }
        [Test]
        public void PostMachine_MachineIsNullShouldReturnBadRequest()
        {

            Machine initialMachine = null;

            
            var response = controller.PostMachine(initialMachine);
            Assert.IsInstanceOf<BadRequestErrorMessageResult>(response);

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
        public void PutMachine_IDLessThanZero()
        {
            var machine = new Machine { Id = -1, Name = "Less than zero" };
            var result = controller.PutMachine(-1, machine);

            Assert.IsInstanceOf<BadRequestErrorMessageResult>(result);

        }

        [Test]
        public void PutMachine_MachineIsNull()
        {
            Machine machine = null;
            var result = controller.PutMachine(-1, machine);

            Assert.IsInstanceOf<BadRequestErrorMessageResult>(result);

        }

        [Test]
        public void PutMachine_NameIsEmpty()
        {
            var machine = new Machine { Id = 2, Name = "" };
            var result = controller.PutMachine(2, machine);

            Assert.IsInstanceOf<BadRequestErrorMessageResult>(result);

        }

        [Test]
        public void PutMachine_MachineNotFound()
        {
            var machine = new Machine { Id = 99, Name = "Spaceship" };
            var result = controller.PutMachine(99, machine);

            Assert.IsInstanceOf<NotFoundResult>(result);

        }





        [Test]
        public void DeleteMachine_ShouldDeleteMachine()
        {
            

            var result = controller.DeleteMachine(2) as OkResult;
            
            var machinesList = controller.GetAllMachines().ToList();
            Assert.Less(machinesList.Count,3);
            
        }

        [Test]
        public void DeleteMachine_TestinvalidID()
        {

            var result = controller.DeleteMachine(-1);
            Assert.IsInstanceOf<BadRequestErrorMessageResult>(result);

        }

        [Test]
        public void DeleteMachine_TestMachineNotFound()
        {

            var result = controller.DeleteMachine(69);
            Assert.IsInstanceOf<NotFoundResult>(result);

        }
    }
}