using UnityEngine;
using UnityEngine.Tilemaps;

namespace Source
{
    [CreateAssetMenu(fileName = "DesignConfig",menuName = "Config/Design Config")]
    public class DesignConfig : ScriptableObject, IDesignConfig
    {
        [Header("Tiles")]
        [SerializeField] private TileBase _grassTile;
        [SerializeField] private TileBase _dirtTile;
        [SerializeField] private Tile _blankTile;

        public TileBase GrassTile => _grassTile;
        public TileBase DirtTile => _dirtTile;
        public Tile BlankTile => _blankTile.Clone();
    }
}