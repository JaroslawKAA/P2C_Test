namespace Core.Services
{
    public interface ITimeScaleService
    {
        void SetTimeScale(float timeScale);
        float GetTimeScale();
        void IncreaseTimeScale(float timeScaleAdd);
        void DecreaseTimeScale(float timeScaleSubtract);
    }
}
