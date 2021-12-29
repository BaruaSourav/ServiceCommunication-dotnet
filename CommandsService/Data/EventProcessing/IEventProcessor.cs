namespace CommandsService.EventsProcessor
{
    public interface IEventProcessor
    {
        void ProcessEvent(string message);
    }
}