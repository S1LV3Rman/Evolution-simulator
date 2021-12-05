using UnityEngine.Tilemaps;

namespace Source
{
    public interface IConfig
    {
        public float CameraMinSize { get; }
        public float CameraMaxSize { get; }
        public float CameraSizeScaler { get; }
        public float CameraResizeTime { get; }
        public float WorldTimeSpeed { get; }
        public int WorldSize { get; }
        
        public TileBase GrassTile { get; }
        public TileBase DirtTile { get; }
        public Tile BlankTile { get; }
        
        
        public int[] TestVariables { get; }
    }
}