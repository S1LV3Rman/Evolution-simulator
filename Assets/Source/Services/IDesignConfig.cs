using UnityEngine;
using UnityEngine.Tilemaps;

namespace Source
{
    public interface IDesignConfig
    {
        public TileBase GrassTile { get; }
        public TileBase DirtTile { get; }
        public Tile BlankTile { get; }
    }
}