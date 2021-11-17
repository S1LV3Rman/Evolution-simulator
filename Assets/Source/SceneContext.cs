using UnityEngine;
using UnityEngine.Tilemaps;

namespace Source
{
    public sealed class SceneContext : MonoBehaviour, ISceneContext
    {
        [SerializeField] private Tilemap _groundMap;


        public Tilemap GroundMap => _groundMap;
    }
}