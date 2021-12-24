namespace Source
{
    public struct Replication : IModule
    {
        public float Weight { get; set; }
        public float EnergyCost { get; set; }
        public float IncubationTime { get; set; }
        public int Amount { get; set; }
    }
}