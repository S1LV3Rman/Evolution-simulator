using UnityEngine;

namespace Source
{
    public struct CubeForm : IForm
    {
        public float Size { get; private set; }
        public float Area { get; private set; }
        public float Volume { get; private set; }
        
        
        public static CubeForm FromVolume(float volume)
        {
            return new CubeForm
            {
                Size = Mathf.Pow(volume, 1f/3f),
                Area = Mathf.Pow(volume, 2f/3f),
                Volume = volume,
            };
        }

        public static CubeForm FromArea(float area)
        {
            return new CubeForm
            {
                Size = Mathf.Sqrt(area),
                Area = area,
                Volume = Mathf.Pow(area, 3f/2f),
            };
        }

        public static CubeForm FromSize(float size)
        {
            return new CubeForm
            {
                Size = size,
                Area = size * size,
                Volume = size * size * size
            };
        }
    }
}