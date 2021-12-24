namespace Source
{
    public struct Movement : IModule
    {
        public float Weight { get; set; }
        public int MaxSpeed  { get; set; }
        public int Speed  { get; set; }
        public int Acceleration  { get; set; }
        public float EnergySpentPerMass  { get; set; }
    }
}