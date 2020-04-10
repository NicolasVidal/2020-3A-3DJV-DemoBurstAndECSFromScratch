using ECSFromScratch.Components;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

namespace ECSFromScratch.Systems
{
    [UpdateAfter(typeof(EnemySpawnerSystem))]
    public class EnemyDestroyerSystem : SystemBase
    {
        private EntityQuery query;

        protected override void OnCreate()
        {
            query = EntityManager.CreateEntityQuery(ComponentType.ReadOnly<Enemy>());
        }

        protected override void OnUpdate()
        {
            Debug.Log("EnemyDestroyerSystem");
            if (Input.GetKeyDown(KeyCode.D))
            {
                using (var enemies = query.ToEntityArray(Allocator.TempJob))
                {
                    if (enemies.Length > 0)
                    {
                        EntityManager.DestroyEntity(enemies[0]);
                    }
                }
            }
        }
    }
}