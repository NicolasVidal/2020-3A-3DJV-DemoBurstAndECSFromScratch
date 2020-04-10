using ECSFromScratch.Components;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

namespace ECSFromScratch.Systems
{
    [UpdateAfter(typeof(EnemyDestroyerSystem))]
    [UpdateAfter(typeof(EnemySpawnerSystem))]
    public class EnemyCountDebuggerSystem : SystemBase
    {
        private EntityQuery query;

        protected override void OnCreate()
        {
            query = EntityManager.CreateEntityQuery(ComponentType.ReadOnly<Enemy>());
        }

        protected override void OnUpdate()
        {
            Debug.Log("EnemyCountDebuggerSystem");
            using (var enemies = query.ToEntityArray(Allocator.TempJob))
            {
                Debug.Log($"Il y a {enemies.Length} enemy en jeu !");
            }
        }
    }
}