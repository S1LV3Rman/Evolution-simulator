using UnityEngine;
using UnityEngine.Tilemaps;

namespace Source
{
    [CreateAssetMenu(fileName = "GlobalConfig",menuName = "Config/Global Config")]
    public sealed class Config : ScriptableObject, IConfig
    {
        [SerializeField] private float _cameraMinSize;
        [SerializeField] private float _cameraMaxSize;
        [SerializeField] private float _cameraSizeScaler;
        [SerializeField] private float _cameraResizeTime;
        
        [SerializeField] private float _worldTimeSpeed;
        [SerializeField] private int _worldSize;

        [SerializeField] private TileBase _grassTile;
        [SerializeField] private TileBase _dirtTile;
        [SerializeField] private TileBase _lifeTile;
        
        
        [SerializeField] private int[] _testVariables;


        public float CameraMinSize => _cameraMinSize;
        public float CameraMaxSize => _cameraMaxSize;
        public float CameraSizeScaler => _cameraSizeScaler;

        public float CameraResizeTime => _cameraResizeTime;

        public float WorldTimeSpeed => _worldTimeSpeed;
        public int WorldSize => _worldSize;

        public TileBase GrassTile => _grassTile;
        public TileBase DirtTile => _dirtTile;
        public TileBase LifeTile => _lifeTile;


        public int[] TestVariables => _testVariables;
    }
}
