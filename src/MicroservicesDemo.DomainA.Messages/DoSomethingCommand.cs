namespace MicroservicesDemo.DomainA.Messages
{
    public class DoSomethingCommand
    {
        public string ThingToDo { get; set; }
        public string PersonDoingThing { get; set; }
    }
}