using Leopotam.Ecs;
using UnityEngine;

namespace Source
{
    public class Startup : MonoBehaviour
    {
        private EcsWorld _world;
        private EcsSystems _systems;
        
        [SerializeField] private Config _config;
        [SerializeField] private DesignConfig _designConfig;
        
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
                .Add(new LifeGenerator())
                
                .Add(new InputSender())
                .Add(new ButtonsSender())
                .Add(new SubscribersMaintainer())
                
                .Add(new WorldViewController())
                
                .Add(new WorldTime())
                
                .Add(new CameraController())
                .Add(new ClockController())
                .Add(new SimulationPlayerController())
                
                //Life logic
                .Add(new EnergyAbsorber())
                
                .Add(new LifeMotionPrediction())
                
                .Add(new LifeMotionRealization())
                
                .Add(new LifeMaintainer())
                
                //.Add(new TestSystem())

                .Inject(GetComponent<ISceneContext>())
                .Inject(_config)
                .Inject(_designConfig)
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