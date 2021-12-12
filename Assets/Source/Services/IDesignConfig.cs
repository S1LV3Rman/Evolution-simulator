using UnityEngine;
using UnityEngine.Tilemaps;

namespace Source
{
    public interface IDesignConfig
    {
        public Sprite PlaySprite { get; }
        public Sprite PauseSprite { get; }
        
        public TileBase GrassTile { get; }
        public TileBase DirtTile { get; }
        public Tile BlankTile { get; }
    }
}