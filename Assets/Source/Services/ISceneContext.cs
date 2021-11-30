using UnityEngine.Tilemaps;

namespace Source
{
    public interface ISceneContext
    {
        public Tilemap Map { get; }
    }
}