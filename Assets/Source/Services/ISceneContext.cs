using UnityEngine.Tilemaps;

namespace Source
{
    public interface ISceneContext
    {
        public Tilemap GroundMap { get; }
        public Tilemap LandMap { get; }
    }
}