namespace Source
{
    public interface IConfig
    {
        public float CameraMinSize { get; }
        public float CameraMaxSize { get; }
        public float CameraSizeScaler { get; }
        public float CameraResizeTime { get; }
        
        public float WorldStartSpeed { get; }
        public int WorldSize { get; }
        public float WorldGravitation { get; }
        
        public float WorldAbsorptionPower { get; }
        public float PassiveAbsorptionPower { get; }
        public float MotionAbsorptionPower { get; }
        public float DeathEnergyThreshold { get; }
        
        
        public int[] TestVariables { get; }
    }
}