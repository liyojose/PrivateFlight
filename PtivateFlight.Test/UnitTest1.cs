
using Moq;
using PrivateFlight.Data;
using PrivateFlight.Repo;
using System;
using System.Threading.Tasks;
using Xunit;

namespace PtivateFlight.Test
{
    public class MessageRepositoryTests
    {
        [Fact]
        public async Task Test_Scenario_1()
        {
            // Arrange
            var messageRepository = new InMemoeryMessageRepository();
            var countryCode = "US";
            var departureDate = new DateTime(2022, 12, 25, 00, 00, 00);

            var messsagesList = new List<Message>();
            var m1 = new Message
            {
                Id = Guid.NewGuid(),
                Type = "A",
                CountryCode= "US",
                Title = "Happy Journey",
                Message1 =  "Happy Journey",
                StartDate = new DateTime(2022, 01, 01, 00, 00, 01)
            };
            var m2 = new Message
            {
                Id = Guid.NewGuid(),
                Type = "A",
                CountryCode= "US",
                Title = "Welcome {Name}",
                Message1 =  "Welcome {Name}",
                StartDate = new DateTime(2022, 01, 02, 0, 0, 0)
            };
            messsagesList.Add(m1);
            messsagesList.Add(m2);
            messageRepository.PrepareTestData(messsagesList);
            // Act
            var result = await messageRepository.GetMessage(countryCode, departureDate);

            // Assert
            Assert.Equal("US", result.CountryCode);
            Assert.Equal("Welcome {Name}", result.Title);
            Assert.Equal("A", result.MessageType);
            Assert.Equal(new DateTime(2022, 01, 02, 0, 0, 0), Convert.ToDateTime(result.StartDate));
        }

        [Fact]
        public async Task Test_Scenario_2()
        {
            // Arrange
            var messageRepository = new InMemoeryMessageRepository();
            var countryCode = "US";
            var departureDate = new DateTime(2022, 02, 03, 00, 00, 00);

            var messsagesList = new List<Message>();
            var m1 = new Message
            {
                Id = Guid.NewGuid(),
                Type = "A",
                CountryCode= "US",
                Title = "Happy Journey",
                Message1 =  "Happy Journey",
                StartDate = new DateTime(2022, 01, 01, 00, 00, 01)
            };
            var m2 = new Message
            {
                Id = Guid.NewGuid(),
                Type = "B",
                CountryCode= "US",
                Title = "Happy Chritmas",
                Message1 =  "Happy Chritmas",
                StartDate = new DateTime(2022, 01, 01, 00, 00, 00),
                EndDate = new DateTime(2022, 12, 25, 23, 59, 59)
            };
            var m11 = new Message
            {
                Id = Guid.NewGuid(),
                Type = "A",
                CountryCode= "US",
                Title = "Title 1",
                Message1 =  "Message 1",
                StartDate = new DateTime(2022, 02, 01, 00, 00, 01)
            };
            var m22 = new Message
            {
                Id = Guid.NewGuid(),
                Type = "B",
                CountryCode= "US",
                Title = "Title 2",
                Message1 =  "Message 2",
                StartDate = new DateTime(2022, 02, 01, 00, 00, 00),
                EndDate = new DateTime(2022, 12, 25, 23, 59, 59)
            };
            messsagesList.Add(m1);
            messsagesList.Add(m2);
            messsagesList.Add(m11);
            messsagesList.Add(m22);
            messageRepository.PrepareTestData(messsagesList);
            // Act
            var result = await messageRepository.GetMessage(countryCode, departureDate);

            // Assert
            Assert.Equal("US", result.CountryCode);
            Assert.Equal("Title 2", result.Title);
            Assert.Equal("B", result.MessageType);
            Assert.Equal(new DateTime(2022, 02, 01, 00, 00, 00), Convert.ToDateTime(result.StartDate));
        }


        [Fact]
        public async Task Test_Scenario_3()
        {
            // Arrange
            var messageRepository = new InMemoeryMessageRepository();
            var countryCode = "US";
            var departureDate = new DateTime(2022, 12, 25, 00, 00, 00);

            var messsagesList = new List<Message>();
            var m1 = new Message
            {
                Id = Guid.NewGuid(),
                Type = "A",
                CountryCode= "AAA",
                Title = "Happy Journey",
                Message1 =  "Happy Journey",
                StartDate = new DateTime(2022, 01, 31, 00, 00, 01)
            };
            var m2 = new Message
            {
                Id = Guid.NewGuid(),
                Type = "B",
                CountryCode= "US",
                Title = "Welcome {Name}",
                Message1 =  "Welcome {Name}",
                StartDate = new DateTime(2022, 01, 02, 0, 0, 0),
                EndDate = new DateTime(2022, 02, 02, 0, 0, 0)
            };
            messsagesList.Add(m1);
            messsagesList.Add(m2);
            messageRepository.PrepareTestData(messsagesList);
            // Act
            var result = await messageRepository.GetMessage(countryCode, departureDate);

            // Assert
            Assert.Equal("AAA", result.CountryCode);
            Assert.Equal("Happy Journey", result.Title);
            Assert.Equal("A", result.MessageType);
            Assert.Equal(new DateTime(2022, 01, 31, 00, 00, 01), Convert.ToDateTime(result.StartDate));
        }


        [Fact]
        public async Task Test_Scenario_GetAll()
        {
            // Arrange
            var messageRepository = new InMemoeryMessageRepository();
            var countryCode = "US";

            var messsagesList = new List<Message>();
            var m1 = new Message
            {
                Id = Guid.NewGuid(),
                Type = "A",
                CountryCode= "AAA",
                Title = "Happy Journey",
                Message1 =  "Happy Journey",
                StartDate = new DateTime(2022, 01, 31, 00, 00, 01)
            };
            var m2 = new Message
            {
                Id = Guid.NewGuid(),
                Type = "B",
                CountryCode= "US",
                Title = "Welcome {Name}",
                Message1 =  "Welcome {Name}",
                StartDate = new DateTime(2022, 01, 02, 0, 0, 0),
                EndDate = new DateTime(2022, 02, 02, 0, 0, 0)
            };
            messsagesList.Add(m1);
            messsagesList.Add(m2);
            messageRepository.PrepareTestData(messsagesList);
            // Act
            var result = await messageRepository.GetAllMessage(countryCode);

            // Assert

            Assert.Equal(2, result.Count);

            Assert.Equal("AAA", result[0].CountryCode);
            Assert.Equal("Happy Journey", result[0].Title);
            Assert.Equal("A", result[0].MessageType);
            Assert.Equal(new DateTime(2022, 01, 31, 00, 00, 01), Convert.ToDateTime(result[0].StartDate));
            Assert.Equal(new DateTime(9999, 12, 31, 00, 00, 00), Convert.ToDateTime(result[0].EndDate));

            Assert.Equal("US", result[1].CountryCode);
            Assert.Equal("Welcome {Name}", result[1].Title);
            Assert.Equal("B", result[1].MessageType);
            Assert.Equal(new DateTime(2022, 01, 02, 0, 0, 0), Convert.ToDateTime(result[1].StartDate));
            Assert.Equal(new DateTime(2022, 02, 02, 0, 0, 0), Convert.ToDateTime(result[1].EndDate));
        }
    }
}
