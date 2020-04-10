using ECSFromScratch.Components;
using Unity.Entities;

namespace ECSFromScratch.Systems
{
    public class GlobalPoisonSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            Entities
                .ForEach((ref HitPoints hps) => { hps.Value -= 1; }
                ).Schedule();
        }
    }
}