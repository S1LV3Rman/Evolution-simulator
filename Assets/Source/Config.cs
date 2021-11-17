using UnityEngine;
using UnityEngine.Tilemaps;

namespace Source
{
    [CreateAssetMenu(fileName = "GlobalConfig",menuName = "Config/Global Config")]
    public sealed class Config : ScriptableObject, IConfig
    {
        [SerializeField] private int _worldRadius = 15;

        [SerializeField] private TileBase _grassTile;
        [SerializeField] private TileBase _dirtTile;
        

        public int WorldRadius => _worldRadius;

        public TileBase GrassTile => _grassTile;
        public TileBase DirtTile => _dirtTile;
    }
}