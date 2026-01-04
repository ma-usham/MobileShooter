using VContainer;
using VContainer.Unity;
using UnityEngine;
using Darkmatter.Core;
using Darkmatter.Presentation;
using Darkmatter.Domain;
using System.Collections.Generic;

namespace Darkmatter.App
{
    public class GameLifetimeScope : LifetimeScope
    {

        [SerializeField] private InputReaderSO inputReader;
        [SerializeField] private PlayerMotor playerMotor;
        [SerializeField] private PlayerAnimController playerAnim;
        [SerializeField] private PlayerConfigSO playerConfig;
        [SerializeField] private CameraConfigSO cameraConfig;
        [SerializeField] private CameraService camService;
        [SerializeField] private GunWeapon gunWeapon;
        [SerializeField] private PlayerAimTargetProvider TargetProvider;

        [SerializeField] private AudioService audioService;

        [Header("UI Settings")]
        [SerializeField] private GameScreenView gameScreenView;


        [Header("Factory parameters")]
        [SerializeField] private Transform playerTransform;
        [SerializeField] private GameObject fatZombie;
        [SerializeField] private GameObject SlimZombie;
        [SerializeField] private List<Transform> patrolPoints;

        [SerializeField] private EnemiesSpawnner spawnner;
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<PlayerController>(Lifetime.Scoped);

            builder.RegisterComponent<IPlayerAnim>(playerAnim);
            builder.RegisterComponent<IInputReader>(inputReader);
            builder.RegisterComponent<IPlayerPawn>(playerMotor);
            builder.RegisterComponent<ITargetProvider>(TargetProvider);

            builder.RegisterComponent(playerConfig);
            builder.RegisterComponent(cameraConfig);

            builder.RegisterComponent<ICameraService>(camService);
            builder.RegisterComponent<IReloadableWeapon>(gunWeapon);
            builder.RegisterComponent(spawnner);

            builder.RegisterComponent<IAudioService>(audioService);

            builder.Register<PlayerStateMachine>(Lifetime.Scoped);
            builder.Register<IEnemyFactory>(c =>
                  new EnemyFactory(
                      playerTransform,
                      patrolPoints,
                      fatZombie,
                      SlimZombie,
                      c.Resolve<IObjectResolver>()), // <-- inject resolver properly
                  Lifetime.Scoped);

            builder.Register<GameScreenController>(Lifetime.Scoped).WithParameter(gameScreenView).As<IGameScreenController>();



        }
    }
}
