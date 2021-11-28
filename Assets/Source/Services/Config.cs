using UnityEngine;
using UnityEngine.Tilemaps;

namespace Source
{
    [CreateAssetMenu(fileName = "GlobalConfig",menuName = "Config/Global Config")]
    public sealed class Config : ScriptableObject, IConfig
    {
        [SerializeField] private float _defaultFrameTime;
        [SerializeField] private float _worldTimeSpeed;
        [SerializeField] private int _worldRadius;

        [SerializeField] private TileBase _grassTile;
        [SerializeField] private TileBase _dirtTile;
        [SerializeField] private TileBase _lifeTile;
        
        
        [SerializeField] private int[] _testVariables;


        public float DefaultFrameTime => _defaultFrameTime;
        public float WorldTimeSpeed => _worldTimeSpeed;
        public int WorldRadius => _worldRadius;

        public TileBase GrassTile => _grassTile;
        public TileBase DirtTile => _dirtTile;
        public TileBase LifeTile => _lifeTile;


        public int[] TestVariables => _testVariables;
    }
}
