namespace MicroservicesDemo.DomainA.Messages
{
    public class AddTaskCommand
    {
        public string Name { get; set; }
        public TaskSeverity Severity { get; set; }

        public enum TaskSeverity
        {
            Low,
            Medium,
            High
        }
    }
}
