using Leopotam.Ecs;
using Source.Systems;
using UnityEngine;

namespace Source
{
    public class Startup : MonoBehaviour
    {
        private EcsWorld _world;
        private EcsSystems _systems;
        
        [SerializeField] private Config _config;
        
        private void Start()
        {
            Application.targetFrameRate = 60;
            
            _world = new EcsWorld();
            _systems = new EcsSystems(_world);
#if UNITY_EDITOR
            //Leopotam.Ecs.UnityIntegration.EcsWorldObserver.Create(_world);
            //Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create(_systems);
#endif
            _systems
                .Add(new WorldGenerator())
                .Add(new TestGenerator())

                .Inject(GetComponent<ISceneContext>())
                .Inject(_config)
                .Inject(new RandomService())
                
                .Init();
        }

        private void Update()
        {
            _systems?.Run();
        }

        private void OnDestroy()
        {
            if (_systems != null)
            {
                _systems.Destroy();
                _systems = null;
                _world.Destroy();
                _world = null;
            }
        }
    }
}