namespace Source
{
    public interface IRandomService
    {
        int Range(int min, int max);
        float Range(float min, float max);
        int Around(int mean, int delta);
        float Around(float mean, float delta);
    }
}