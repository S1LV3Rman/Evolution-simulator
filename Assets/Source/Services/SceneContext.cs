using UnityEngine;
using UnityEngine.Tilemaps;

namespace Source
{
    public sealed class SceneContext : MonoBehaviour, ISceneContext
    {
        [SerializeField] private Tilemap _groundMap;
        [SerializeField] private Tilemap _landMap;


        public Tilemap GroundMap => _groundMap;
        public Tilemap LandMap => _landMap;
    }
}