namespace ENotes.Entities
{
    public class Quiz
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Option1 { get; set; }
        public string Option2 { get; set; }
        public string Option3 { get; set; }
        public string Option4 { get; set; }
        public string Answer{ get; set; }
        public string Subject_Id { get; set; }
    }
}
