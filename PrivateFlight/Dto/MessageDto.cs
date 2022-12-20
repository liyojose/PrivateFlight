namespace PrivateFlight.Dto
{
    public partial class MessageDto
    {
        public Guid Id { get; set; }

        public string MessageType { get; set; }

        public string Title { get; set; }

        public string Message{ get; set; }

        public string CountryCode { get; set; }

        public string StartDate { get; set; }

        public string EndDate { get; set; }
    }
}
