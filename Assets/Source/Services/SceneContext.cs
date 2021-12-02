using UnityEngine;
using UnityEngine.Tilemaps;

namespace Source
{
    public sealed class SceneContext : MonoBehaviour, ISceneContext
    {
        [SerializeField] private Tilemap _groundMap;
        [SerializeField] private Camera _camera;


        public Camera Camera => _camera;

        public Tilemap Map => _groundMap;
    }
}