using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Source
{
    public struct WorldMap
    {
        public Dictionary<MapCoord, TileBase> Value;
    }
}