namespace BovineLabs.Quill.Sample
{
    using BovineLabs.Quill;
    using Unity.Collections;
    using Unity.Entities;
    using Unity.Mathematics;
    using Unity.Transforms;
    using UnityEngine;

    public partial struct DrawEntityPositionSystem : ISystem
    {
        public void OnUpdate(ref SystemState state)
        {
            var drawer = SystemAPI.GetSingleton<DrawSystem.Singleton>().CreateDrawer<DrawEntityPositionSystem>();

            if (!drawer.IsEnabled)
            {
                return;
            }

            foreach (var localTransform in SystemAPI.Query<RefRO<LocalTransform>>())
            {
                var position = localTransform.ValueRO.Position;
                var label = CreateLabel(position);
                drawer.Point(position, 0.15f, Color.cyan);
                drawer.Text128(position + new float3(0f, 1.25f, 0f), label, Color.yellow);
            }
        }

        private static FixedString128Bytes CreateLabel(float3 position)
        {
            return $"pos ({position.x:0.00}, {position.y:0.00}, {position.z:0.00})";
        }
    }
}
