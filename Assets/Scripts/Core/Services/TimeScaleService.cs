using UnityEngine;

public class TimeScaleService : ITimeScaleService
{
    public void SetTimeScale(float timeScale) => Time.timeScale = timeScale;
    public float GetTimeScale() => Time.timeScale;
}
