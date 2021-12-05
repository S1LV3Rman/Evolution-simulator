using UnityEngine.Tilemaps;

namespace Source
{
    public struct LifeTile
    {
        public Tile Appearance;
        public Tile Trail;

        public void SetTile(Tile tile)
        {
            Appearance = tile;
            Trail = tile.Clone();
                
            var color = tile.color;
            color.a *= 0.25f;
            Trail.color = color;
        }
    }
}