namespace MicroservicesDemo.DomainA.Messages
{
    public class SomethingDoneEvent
    {
        public string ThingThatWasDone { get; set; }
        public string PersonThatDidIt { get; set; }
        public Quality Quality { get; set; }
    }
}
