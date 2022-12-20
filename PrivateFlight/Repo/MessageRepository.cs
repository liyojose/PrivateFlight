

using Microsoft.EntityFrameworkCore;
using PrivateFlight.Data;
using PrivateFlight.Dto;
using System;

namespace PrivateFlight.Repo
{
    public class InMemoeryMessageRepository : IMessageRepository
    {

        public InMemoeryMessageRepository()
        {
            //using (var context = new InMemoeryPrivateFlightContext())
            //{
            //    var messsagesList = new List<Message>
            //    {
            //    new Message
            //    {
            //        Id = Guid.NewGuid(),
            //        Type = "B",
            //        CountryCode = "US",
            //        Title = "Happy Chritmas",
            //        Message1 =  "Happy Chritmas",
            //        StartDate = new DateTime(2022,12,25,0,0,0),
            //        EndDate = new DateTime(2022,12,25,23,59,59)
            //    },
            //    new Message
            //    {
            //        Id = Guid.NewGuid(),
            //        Type = "B",
            //        CountryCode= "US",
            //        Title = "Happy Chritmas",
            //        Message1 =  "Happy Chritmas",
            //        StartDate = new DateTime(2022,12,25,0,0,0),
            //       EndDate = new DateTime(2022,12,25,23,59,59)
            //    },
            //    new Message
            //    {
            //        Id = Guid.NewGuid(),
            //        Type = "B",
            //        CountryCode = "US",
            //        Title = "Happy Holidays",
            //        Message1 =  "Happy Holidays",
            //        StartDate = new DateTime(2022,12,25,0,0,0),
            //       EndDate = new DateTime(2022,12,25,23,59,59)
            //    },
            //    new Message
            //    {
            //        Id = Guid.NewGuid(),
            //        Type = "B",
            //        CountryCode= "US",
            //        Title = "Happy New Year",
            //        Message1 =  "Happy New Year",
            //        StartDate = new DateTime(2022,01,01,0,0,0),
            //        EndDate = new DateTime(2022,01,01,23,59,59)
            //    },
            //    new Message
            //    {
            //        Id = Guid.NewGuid(),
            //        Type = "A",
            //        CountryCode= "US",
            //        Title = "Happy Journey",
            //        Message1 =  "Happy Journey",
            //        StartDate = new DateTime(2022,01,01,00,00,01)
            //    },
            //    new Message
            //    {
            //        Id = Guid.NewGuid(),
            //        Type = "A",
            //        CountryCode= "AAA",
            //        Title = "Welcome {Name}",
            //        Message1 =  "Welcome {Name}",
            //        StartDate = new DateTime(2022,01,02,0,0,0)
            //    }
            //    };
            //    context.Messages.AddRange(messsagesList);
            //    context.SaveChanges();
            //}
        }

        public void PrepareTestData(string scneario)
        {
            var messsagesList = new List<Message>();
            using (var context = new InMemoeryPrivateFlightContext())
            {
                // context.Database.ExecuteSqlRaw("TRUNCATE TABLE [Message]");
                switch (scneario)
                {
                   case "Scenario 1":
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
                        break;
                    case "Scenario 2":
                         m1 = new Message
                        {
                            Id = Guid.NewGuid(),
                            Type = "A",
                            CountryCode= "US",
                            Title = "Happy Journey",
                            Message1 =  "Happy Journey",
                            StartDate = new DateTime(2022, 01, 01, 00, 00, 01)
                        };
                         m2 = new Message
                        {
                            Id = Guid.NewGuid(),
                            Type = "B",
                            CountryCode= "US",
                            Title = "Happy Chritmas",
                            Message1 =  "Happy Chritmas",
                            StartDate = new DateTime(2022, 01, 01, 00, 00, 00),
                            EndDate = new DateTime(2022, 12, 25, 23, 59, 59)
                        };
                        messsagesList.Add(m1);
                        messsagesList.Add(m2);
                        break;
                    case "Scenario 3":
                        m1 = new Message
                        {
                            Id = Guid.NewGuid(),
                            Type = "B",
                            CountryCode= "AAA",
                            Title = "Happy Journey",
                            Message1 =  "Happy Journey",
                            StartDate = new DateTime(2022, 01, 01, 00, 00, 01),
                            EndDate = new DateTime(2022, 12, 25, 23, 59, 59)
                        };
                        m2 = new Message
                        {
                            Id = Guid.NewGuid(),
                            Type = "B",
                            CountryCode= "US",
                            Title = "Happy Chritmas",
                            Message1 =  "Happy Chritmas",
                            StartDate = new DateTime(2022, 01, 01, 00, 00, 00),
                            EndDate = new DateTime(2022, 12, 25, 23, 59, 59)
                        };
                        messsagesList.Add(m1);
                        messsagesList.Add(m2);
                        break;
                }
                context.Messages.AddRange(messsagesList);
                context.SaveChanges();
            }
        }

        public void PrepareTestData(List<Message> records)
        {
            using (var context = new InMemoeryPrivateFlightContext())
            {
                var rows = from o in context.Messages
                           select o;
                foreach (var row in rows)
                {
                    context.Messages.Remove(row);
                }
                context.SaveChanges();
                //context.Database.ExecuteSqlRaw("TRUNCATE TABLE [Message]");
                context.Messages.AddRange(records);
                context.SaveChanges();
            }
        }
        public async Task<MessageDto> GetMessage(string countrycode, DateTime departuredate)
        {
            using (var db = new InMemoeryPrivateFlightContext())
            {
                var msg = await db.Messages.Where(x => x.CountryCode == countrycode && ((x.Type == "A" && x.StartDate <= departuredate) || (x.Type == "B" && x.StartDate <= departuredate &&  x.EndDate >= departuredate))).OrderByDescending(e => e.Type).ThenByDescending(e => e.StartDate).Select(d => new MessageDto
                {
                    CountryCode = d.CountryCode,
                    MessageType = d.Type,
                    Title = d.Title,
                    Message = d.Message1,
                    StartDate = d.StartDate.ToString("dd-MMM-yyy H:mm:ss"),
                    EndDate = (d.EndDate??new DateTime(9999, 12, 31)).ToString("dd-MMM-yyy H:mm:ss")
                }).FirstOrDefaultAsync();

                if (msg == null)
                {
                    msg= await db.Messages.Where(x => x.CountryCode == "AAA" && ((x.Type == "A" && x.StartDate <= departuredate) || (x.Type == "B" && x.StartDate <= departuredate &&  x.EndDate >= departuredate))).OrderByDescending(e => e.Type).ThenByDescending(e => e.StartDate).Select(d => new MessageDto
                    {
                        CountryCode = d.CountryCode,
                        MessageType = d.Type,
                        Title = d.Title,
                        Message = d.Message1,
                        StartDate = d.StartDate.ToString("dd-MMM-yyy H:mm:ss"),
                        EndDate = (d.EndDate??new DateTime(9999, 12, 31)).ToString("dd-MMM-yyy H:mm:ss")
                    }).FirstOrDefaultAsync();
                }
                return msg;
            }

        }
        public async Task<List<MessageDto>> GetAllMessage(string countrycode)
        {
            using (var db = new InMemoeryPrivateFlightContext())
            {
                return await db.Messages.Where(x => x.CountryCode == countrycode || x.CountryCode == "AAA").Select(d => new MessageDto
                {
                    CountryCode = d.Message1,
                    MessageType = d.Type,
                    Title = d.Title,
                    Message = d.Message1,
                    StartDate = d.StartDate.ToString("dd-MMM-yyy H:mm:ss"),
                    EndDate = (d.EndDate??new DateTime(9999, 12, 31)).ToString("dd-MMM-yyy H:mm:ss")
                }).ToListAsync();
            }
        }
    }
    public class MessageRepository : IMessageRepository
    {
        private readonly PrivateFlightContext db;

        public MessageRepository(PrivateFlightContext db)
        {
            this.db = db;
        }
        public async Task<MessageDto> GetMessage(string countrycode, DateTime departuredate)
        {

            var msg = await db.Messages.Where(x => x.CountryCode == countrycode && ((x.Type == "A" && x.StartDate <= departuredate) || (x.Type == "B" && x.StartDate <= departuredate &&  x.EndDate >= departuredate))).OrderByDescending(e => e.Type).ThenBy(e => e.StartDate).Select(d => new MessageDto
            {
                CountryCode = d.Message1,
                MessageType = d.Type,
                Title = d.Title,
                Message = d.Message1,
                StartDate = d.StartDate.ToString("dd-MMM-yyy H:mm:ss"),
                EndDate = (d.EndDate??new DateTime(9999, 12, 31)).ToString("dd-MMM-yyy H:mm:ss")
            }).FirstOrDefaultAsync();

            if (msg == null)
            {
                msg= await db.Messages.Where(x => x.CountryCode == "AAA" && ((x.Type == "A" && x.StartDate <= departuredate) || (x.Type == "B" && x.StartDate <= departuredate &&  x.EndDate >= departuredate))).OrderByDescending(e => e.Type).ThenBy(e => e.StartDate).Select(d => new MessageDto
                {
                    CountryCode = d.Message1,
                    MessageType = d.Type,
                    Title = d.Title,
                    Message = d.Message1,
                    StartDate = d.StartDate.ToString("dd-MMM-yyy H:mm:ss"),
                    EndDate = (d.EndDate??new DateTime(9999, 12, 31)).ToString("dd-MMM-yyy H:mm:ss")
                }).FirstOrDefaultAsync();
            }
            return msg;
        }
        public async Task<List<MessageDto>> GetAllMessage(string countrycode)
        {
            return await db.Messages.Where(x => x.CountryCode == countrycode || x.CountryCode == "AAA").Select(d => new MessageDto
            {
                CountryCode = d.Message1,
                MessageType = d.Type,
                Title = d.Title,
                Message = d.Message1,
                StartDate = d.StartDate.ToString("dd-MMM-yyy H:mm:ss"),
                EndDate = (d.EndDate??new DateTime(9999, 12, 31)).ToString("dd-MMM-yyy H:mm:ss")
            }).ToListAsync();
        }
    }
    public interface IMessageRepository
    {
        Task<List<MessageDto>> GetAllMessage(string countrycode);
        Task<MessageDto> GetMessage(string countrycode, DateTime departuredate);
    }
}
