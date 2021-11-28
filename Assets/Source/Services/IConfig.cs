using UnityEngine.Tilemaps;

namespace Source
{
    public interface IConfig
    {
        public float DefaultFrameTime { get; }
        public float WorldTimeSpeed { get; }
        public int WorldRadius { get; }
        
        public TileBase GrassTile { get; }
        public TileBase DirtTile { get; }
        public TileBase LifeTile { get; }
        
        
        public int[] TestVariables { get; }
    }
}