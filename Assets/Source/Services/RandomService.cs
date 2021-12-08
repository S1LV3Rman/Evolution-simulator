using System;

namespace Source
{
    public sealed class RandomService : IRandomService
    {
        private readonly Random _rng;

        public RandomService(int? seed = null)
        {
            _rng = seed.HasValue ? new Random(seed.Value) : new Random();
        }
        
        public int Range(int min, int max)
        {
            return _rng.Next(min, max + 1);
        }

        public float Range(float min, float max)
        {
            return min + (max - min) * (float)_rng.NextDouble();
        }

        public int Around(int mean, int delta)
        {
            return Range(mean - delta, mean + delta);
        }

        public float Around(float mean, float delta)
        {
            return Range(mean - delta, mean + delta);
        }
    }
}