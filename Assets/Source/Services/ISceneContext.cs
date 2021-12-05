using UnityEngine;
using UnityEngine.Tilemaps;

namespace Source
{
    public interface ISceneContext
    {
        public Camera Camera { get; }
        public Tilemap Map { get; }
        public MainCanvas Canvas { get; }
    }
}