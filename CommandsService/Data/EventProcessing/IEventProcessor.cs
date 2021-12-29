namespace CommandsService.EventProcessors
{
    public interface IEventProcessor
    {
        void ProcessEvent(string message);
    }
}