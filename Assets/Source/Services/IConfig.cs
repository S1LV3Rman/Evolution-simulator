using UnityEngine.Tilemaps;

namespace Source
{
    public interface IConfig
    {
        public int WorldRadius { get; }
        
        public TileBase GrassTile { get; }
        public TileBase DirtTile { get; }
        
        
        public int[] TestVariables { get; }
    }
}