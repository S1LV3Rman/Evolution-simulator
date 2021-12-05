using UnityEngine.Tilemaps;

namespace Source
{
    public interface ICell
    {
        public TileBase Tile { get; set; }
    }
}