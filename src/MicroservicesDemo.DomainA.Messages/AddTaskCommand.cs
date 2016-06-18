namespace MicroservicesDemo.DomainA.Messages
{
    public class AddTaskCommand
    {
        public string Name { get; set; }
        public TaskServerity Severity { get; set; }

        public enum TaskServerity
        {
            Low,
            Medium,
            High
        }
    }
}
