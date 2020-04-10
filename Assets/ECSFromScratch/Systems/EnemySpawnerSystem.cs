using ECSFromScratch.Components;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

namespace ECSFromScratch.Systems
{
    public class EnemySpawnerSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            Debug.Log("EnemySpawnerSystem");
            if (Input.GetKeyDown(KeyCode.A))
            {
                var enemyArchetype = EntityManager.CreateArchetype(typeof(Enemy),
                    typeof(HitPoints)
                );

                var enemies = EntityManager.CreateEntity(enemyArchetype, 1000,
                    Allocator.TempJob
                );
                enemies.Dispose();
            }
        }
    }
}