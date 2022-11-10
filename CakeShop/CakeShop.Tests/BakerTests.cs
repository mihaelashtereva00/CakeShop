using AutoMapper;
using CakeShop.BL.MediatRCommandHandlers.BakerHandlers;
using CakeShop.DL.Interfaces;
using CakeShop.Models.MediatRCommands.BakerCommands;
using CakeShop.Models.Models.ModelsSqlDB;
using CakeShop.Models.Models.Requests;
using Microsoft.Extensions.Logging;
using Moq;
using System.Net;

namespace CakeShop.Tests
{
    public class BakerTests
    {
        private IList<Baker> _bakers = new List<Baker>()
        {
            new Baker()
            {
            Id = 1,
            Age = 23,
            DateOfBirth = DateTime.Now.AddYears(-23),
            Name = "Stoyan",
            Specialty = "Baker"
            },
            new Baker()
            {
            Id = 2,
            Age = 31,
            DateOfBirth = DateTime.Now.AddYears(-31),
            Name = "Mariya",
            Specialty = "Decorator"
            },
            new Baker()
            {
            Id = 3,
            Age = 27,
            DateOfBirth = DateTime.Now.AddYears(-27),
            Name = "Boryana",
            Specialty = "Batter maker"
            }
        };

        private readonly Mock<IMapper> _mapper;
        private readonly Mock<ILogger<GetAllBakersHandler>> _loggerMockBakerGetAll;
        private readonly Mock<ILogger<AddBakerHandler>> _loggerMockAddBaker;
        private readonly Mock<ILogger<UpdateBakerHandler>> _loggerMockUpdateBaker;
        private readonly Mock<ILogger<DeleteBakerHandler>> _loggerMockDeleteBaker;
        private readonly Mock<ILogger<GetByIdBakerHandler>> _loggerMockGetByIdBaker;
        private readonly Mock<IBakerRepository> _bakerRepo;

        public BakerTests()
        {
            _mapper = new Mock<IMapper>();
            _bakerRepo = new Mock<IBakerRepository>();
            _loggerMockBakerGetAll = new Mock<ILogger<GetAllBakersHandler>>();
            _loggerMockAddBaker = new Mock<ILogger<AddBakerHandler>>();
            _loggerMockUpdateBaker = new Mock<ILogger<UpdateBakerHandler>>();
            _loggerMockDeleteBaker = new Mock<ILogger<DeleteBakerHandler>>();
            _loggerMockGetByIdBaker = new Mock<ILogger<GetByIdBakerHandler>>();
        }

        [Fact]
        public async void Get_All_Bakers()
        {
            //setup
            var expectedCount = 3;

            _bakerRepo.Setup(x => x.GetAllBakers()).ReturnsAsync(_bakers);
            var getAllBakers = new GetAllBakersCommand();
            var getAllBakersHandler = new GetAllBakersHandler(_bakerRepo.Object, _mapper.Object, _loggerMockBakerGetAll.Object);

            //inject
            var result = await getAllBakersHandler.Handle(getAllBakers, new System.Threading.CancellationToken());

            //assert
            Assert.Equal(expectedCount, result.Bakers.Count());
        }
        
        [Fact]
        public async void Get_All_Bakers_Bad_Request()
        {
            //setup
            var bakersEmpty = new List<Baker>();
            _bakerRepo.Setup(x => x.GetAllBakers()).ReturnsAsync(bakersEmpty);
            var getAllBakers = new GetAllBakersCommand();
            var getAllBakersHandler = new GetAllBakersHandler(_bakerRepo.Object, _mapper.Object, _loggerMockBakerGetAll.Object);

            //inject
            var result = await getAllBakersHandler.Handle(getAllBakers, new System.Threading.CancellationToken());

            //assert
            Assert.NotNull(result);
            Assert.Equal(HttpStatusCode.BadRequest, result.HttpStatusCode);
            Assert.Equal("There are no bakers added", result.Message);
        }
        
        [Fact]
        public async void Get_By_Id_Baker()
        {
            //setup
            var id = 3;

            _bakerRepo.Setup(x => x.GetBakertById(id)).ReturnsAsync(_bakers.FirstOrDefault(b => b.Id == id));

            var getByIdBaker = new GetBakereByIdCommand(id);
            var getByIdBakerHandler = new GetByIdBakerHandler(_bakerRepo.Object, _mapper.Object, _loggerMockGetByIdBaker.Object);

            //inject
            var result = await getByIdBakerHandler.Handle(getByIdBaker, new System.Threading.CancellationToken());

            //assert
            Assert.NotNull(result);
            Assert.Equal(HttpStatusCode.OK, result.HttpStatusCode);
            Assert.Equal("Successfully got baker", result.Message);
            Assert.Equal("Boryana", result.Baker.Name);
        }    
        
        [Fact]
        public async void Get_By_Id_Baker_Not_Found()
        {
            //setup
            var id = 4;

            _bakerRepo.Setup(x => x.GetBakertById(id)).ReturnsAsync(_bakers.FirstOrDefault(b => b.Id == id));

            var getByIdBaker = new GetBakereByIdCommand(id);
            var getByIdBakerHandler = new GetByIdBakerHandler(_bakerRepo.Object, _mapper.Object, _loggerMockGetByIdBaker.Object);

            //inject
            var result = await getByIdBakerHandler.Handle(getByIdBaker, new System.Threading.CancellationToken());

            //assert
            Assert.NotNull(result);
            Assert.Equal(HttpStatusCode.BadRequest, result.HttpStatusCode);
            Assert.Equal("The baker does not exist", result.Message);
        }

        [Fact]
        public async void Add_Baker()
        {
            //setup
            var expectedId = 4;

            var request = new BakerRequest()
            {
                Age = 36,
                DateOfBirth = DateTime.Now.AddYears(-36),
                Name = "Teodora",
                Specialty = "Decorator"
            };

            _bakerRepo.Setup(x => x.AddBaker(It.IsAny<Baker>()))
                                .Callback(() =>
                                {
                                    _bakers.Add(new Baker()
                                    {
                                        Id = expectedId,
                                        Age = request.Age,
                                        DateOfBirth = request.DateOfBirth,
                                        Name = request.Name,
                                        Specialty = request.Specialty
                                    });
                                })!.ReturnsAsync(() => _bakers.FirstOrDefault(x => x.Id == expectedId));

            //inject
            var addBaker = new AddBakerCommand(request);
            var addBakerHandler = new AddBakerHandler(_bakerRepo.Object, _mapper.Object, _loggerMockAddBaker.Object);

            var result = await addBakerHandler.Handle(addBaker, new System.Threading.CancellationToken());

            //assert

            Assert.NotNull(result);
            Assert.Equal(HttpStatusCode.OK, result.HttpStatusCode);
            Assert.Equal("Successfully added baker", result.Message);
            Assert.Equal(expectedId, result.Baker.Id);
        }

        [Fact]
        public async void Add_Baker_Bad_Request()
        {
            //setup
            var expectedId = 4;

            var request = new BakerRequest()
            {
                Age = 17,
                DateOfBirth = DateTime.Now.AddYears(-17),
                Name = "Stoyan",
                Specialty = "Decorator"
            };

            _bakerRepo.Setup(x => x.GetBakertByName(request.Name)).ReturnsAsync(_bakers.FirstOrDefault(b => b.Name == request.Name));
            _bakerRepo.Setup(x => x.AddBaker(It.IsAny<Baker>()))
                                .Callback(() =>
                                {
                                    _bakers.Add(new Baker()
                                    {
                                        Id = expectedId,
                                        Age = request.Age,
                                        DateOfBirth = request.DateOfBirth,
                                        Name = request.Name,
                                        Specialty = request.Specialty
                                    });
                                })!.ReturnsAsync(() => _bakers.FirstOrDefault(x => x.Id == expectedId));

            //inject
            var addBaker = new AddBakerCommand(request);
            var addBakerHandler = new AddBakerHandler(_bakerRepo.Object, _mapper.Object, _loggerMockAddBaker.Object);

            var result = await addBakerHandler.Handle(addBaker, new System.Threading.CancellationToken());

            //assert
            Assert.NotNull(result);
            Assert.Equal(HttpStatusCode.BadRequest, result.HttpStatusCode);
            Assert.Equal("Baker with that name already exist", result.Message);
        }

        [Fact]
        public async void Update_Baker()
        {
            //setup
            var bakerRequest = new UpdateBakerRequest()
            {
                Id = 1,
                Age = 21,
                DateOfBirth = DateTime.Now.AddYears(-21),
                Name = "Mariya Yordanova",
                Specialty = "Decorator"
            };

            _bakerRepo.Setup(x => x.GetBakertById(bakerRequest.Id)).ReturnsAsync(_bakers.FirstOrDefault(x => x.Id == bakerRequest.Id));

            //inject
            var updateBakerCommand = new UpdateBakerCommand(bakerRequest);
            var updateBakerHandler = new UpdateBakerHandler(_bakerRepo.Object, _mapper.Object, _loggerMockUpdateBaker.Object);

            var result = await updateBakerHandler.Handle(updateBakerCommand, new System.Threading.CancellationToken());

            //assert
            Assert.NotNull(result);
            Assert.Equal(HttpStatusCode.OK, result.HttpStatusCode);
            Assert.Equal("Successfully updated baker", result.Message);
        }

        [Fact]
        public async void Update_Baker_Not_Found()
        {
            //setup
            var bakerRequest = new UpdateBakerRequest()
            {
                Id = 8,
                Age = 21,
                DateOfBirth = DateTime.Now.AddYears(-21),
                Name = "Mariya Yordanova",
                Specialty = "Decorator"
            };

            _bakerRepo.Setup(x => x.GetBakertById(bakerRequest.Id)).ReturnsAsync(_bakers.FirstOrDefault(x => x.Id == bakerRequest.Id));

            //inject
            var updateBakerCommand = new UpdateBakerCommand(bakerRequest);
            var updateBakerHandler = new UpdateBakerHandler(_bakerRepo.Object, _mapper.Object, _loggerMockUpdateBaker.Object);

            var result = await updateBakerHandler.Handle(updateBakerCommand, new System.Threading.CancellationToken());

            //assert
            Assert.NotNull(result);
            Assert.Equal(HttpStatusCode.BadRequest, result.HttpStatusCode);
            Assert.Equal("The baker does not exist", result.Message);
        }

        [Fact]
        public async void Delete_Baker()
        {
            //setup
            var requestedId = 1;

            var bakerToRemove = new Baker()
            {
                Id = requestedId,
                Age = 23,
                DateOfBirth = DateTime.Now.AddYears(-23),
                Name = "Stoyan",
                Specialty = "Baker"
            };
;
            _bakerRepo.Setup(x => x.GetBakertById(requestedId)).ReturnsAsync(_bakers.FirstOrDefault(x => x.Id == requestedId));

            _bakerRepo.Setup(x => x.DeleteBaker(requestedId))
                     .Callback(() =>
                     {
                         _bakers.Remove(bakerToRemove);
                     }).ReturnsAsync(() => bakerToRemove);

            //inject
            var deleteBakerCommand = new DeleteBakerCommand(requestedId);
            var deleteBakerHandler = new DeleteBakerHandler(_bakerRepo.Object, _mapper.Object, _loggerMockDeleteBaker.Object);

            var result = await deleteBakerHandler.Handle(deleteBakerCommand, new System.Threading.CancellationToken());

            //assert
            Assert.NotNull(result);
            Assert.Equal(HttpStatusCode.OK, result.HttpStatusCode);
            Assert.Equal("Successfully deleted baker", result.Message);
            Assert.Equal(requestedId, result.Baker.Id);
        }
        
        [Fact]
        public async void Delete_Baker_Not_Found()
        {
            //setup
            var requestedId = 7;

            var bakerToRemove = new Baker()
            {
                Id = requestedId,
                Age = 23,
                DateOfBirth = DateTime.Now.AddYears(-23),
                Name = "Stoyan",
                Specialty = "Baker"
            };
;
            _bakerRepo.Setup(x => x.GetBakertById(requestedId)).ReturnsAsync(_bakers.FirstOrDefault(x => x.Id == requestedId));

            _bakerRepo.Setup(x => x.DeleteBaker(requestedId))
                     .Callback(() =>
                     {
                         _bakers.Remove(bakerToRemove);
                     }).ReturnsAsync(() => bakerToRemove);

            //inject
            var deleteBakerCommand = new DeleteBakerCommand(requestedId);
            var deleteBakerHandler = new DeleteBakerHandler(_bakerRepo.Object, _mapper.Object, _loggerMockDeleteBaker.Object);

            var result = await deleteBakerHandler.Handle(deleteBakerCommand, new System.Threading.CancellationToken());

            //assert
            Assert.NotNull(result);
            Assert.Equal(HttpStatusCode.BadRequest, result.HttpStatusCode);
            Assert.Equal("Could not find baker with that Id", result.Message);
        }
    }
}