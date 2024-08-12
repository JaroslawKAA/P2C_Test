namespace Core.Services
{
    public interface ITickService
    {
        void Tick();
        void SetTickDelay(float tickDelay);
    }
}
