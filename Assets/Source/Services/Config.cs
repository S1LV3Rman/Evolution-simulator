using UnityEngine;
using UnityEngine.Tilemaps;

namespace Source
{
    [CreateAssetMenu(fileName = "GlobalConfig",menuName = "Config/Global Config")]
    public sealed class Config : ScriptableObject, IConfig
    {
        [Space]
        [SerializeField] private float _cameraMinSize;
        [SerializeField] private float _cameraMaxSize;
        [SerializeField] private float _cameraSizeScaler;
        [SerializeField] private float _cameraResizeTime;
        [Space]
        [SerializeField] private float _worldTimeSpeed;
        [SerializeField] private int _worldSize;
        [SerializeField] private float _worldGravitation;
        [Space]
        [SerializeField] private float _worldAbsorptionPower;
        [SerializeField] private float _passiveAbsorptionPower;
        [SerializeField] private float _motionAbsorptionPower;
        [SerializeField] private float _deathEnergyThreshold;
        [Space]
        [SerializeField] private TileBase _grassTile;
        [SerializeField] private TileBase _dirtTile;
        [SerializeField] private Tile _blankTile;
        [Space]
        [SerializeField] private int[] _testVariables;


        public float CameraMinSize => _cameraMinSize;
        public float CameraMaxSize => _cameraMaxSize;
        public float CameraSizeScaler => _cameraSizeScaler;

        public float CameraResizeTime => _cameraResizeTime;

        public float WorldTimeSpeed => _worldTimeSpeed;
        public int WorldSize => _worldSize;

        public float WorldGravitation => _worldGravitation;

        public float WorldAbsorptionPower => _worldAbsorptionPower;

        public float PassiveAbsorptionPower => _passiveAbsorptionPower;

        public float MotionAbsorptionPower => _motionAbsorptionPower;

        public float DeathEnergyThreshold => _deathEnergyThreshold;

        public TileBase GrassTile => _grassTile;
        public TileBase DirtTile => _dirtTile;
        public Tile BlankTile => _blankTile.Clone();


        public int[] TestVariables => _testVariables;
    }
}
