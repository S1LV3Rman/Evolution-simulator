namespace Source
{
    public struct Life
    {
        public string Name { get; set; }
        public string Parent { get; set; }
        public float Energy { get; set; }
        public IForm Form { get; set; }
    }
}