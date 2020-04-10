using ECSFromScratch.SystemGroups;
using ECSFromScratch.Systems;
using Unity.Entities;
using UnityEngine;

namespace ECSFromScratch
{
    public class AlphaScript : MonoBehaviour
    {
        private MainComponentSystemGroup system;

        // Start is called before the first frame update
        void Start()
        {
            var world = new World("Alpha");
            var mainSystemGroup = world.GetOrCreateSystem<MainComponentSystemGroup>();
            mainSystemGroup.AddSystemToUpdateList(world.GetOrCreateSystem<EnemyCountDebuggerSystem>());
            mainSystemGroup.AddSystemToUpdateList(world.GetOrCreateSystem<EnemyDestroyerSystem>());
            mainSystemGroup.AddSystemToUpdateList(world.GetOrCreateSystem<EnemySpawnerSystem>());
            mainSystemGroup.AddSystemToUpdateList(world.GetOrCreateSystem<GlobalPoisonSystem>());
            mainSystemGroup.SortSystemUpdateList();
            system = mainSystemGroup;
        }

        // Update is called once per frame
        void Update()
        {
            system.Update();
        }
    }
}