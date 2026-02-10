using Friflo.Engine.ECS;
using LitMotion;
using UnityEngine;
using UnityEngine.Pool;
using VContainer;

[LevelScope]
public class CraftSignalImpl : GenericSignal<CraftSignal> {

    [Inject]
    private readonly EntityStore world;
    [Inject]
    private readonly ItemProvider itemProvider;

    protected override void Signal(Signal<CraftSignal> signal) {
        var buffer = world.GetCommandBuffer();
        if (world.GetRecipeRegistry() is { IsNull: false } registry) {
            var player = world.GetPlayer();
            var relations = player.GetRelations<CraftRelation>();
            if (relations.Length == 0) {
                return;
            }

            using (var pooledObject = ListPool<LootDef>.Get(out var tempList)) {
                foreach (var relation in relations) {
                    tempList.Add(relation.Entity.GetComponent<DefinitionComponent>().GetValue<LootDef>());
                }

                var recipeKey = RecipeKey.FromLoot(tempList);
                ref var registryComp = ref registry.GetComponent<RecipeRegistryComponent>();
                if (registryComp.Registry.TryGetValue(recipeKey, out var lootDef)) {
                    var lootEntity = itemProvider.GetItemEntity(lootDef);
                    var invEntity = world.GetInventoryWnd();
                    lootEntity.GetGameObject().SetActive(true);
                    lootEntity.GetTransform().SetParent(invEntity.GetComponent<InventoryComponent>().PlayerContent, false);
                    player.AddRelation(new InventoryRelation { Entity = lootEntity });
                    world.GetInventoryWnd().AddRelation(new ShowsRelation { Entity = lootEntity });
                    foreach (var relation in relations) {
                        buffer.DeleteEntity(relation.Entity.Id);
                    }

                    var image = lootEntity.GetComponent<ImageComponent>().Value;
                    LMotion.Create(1f, 0.4f, 0.3f)
                        .WithLoops(6, LoopType.Yoyo)
                        .Bind(alpha => {
                            var c = image.color;
                            c.a = alpha;
                            image.color = c;
                        });
                } else {
                    if (relations.Length > 0) {
                        if (!relations[0].Entity.HasComponent<MotionComponent>()) {
                            foreach (var relation in relations) {
                                var entity = relation.Entity;
                                var image = entity.GetComponent<ImageComponent>().Value;
                                var motionHandle = LMotion.Create(image.color, Color.red, 0.3f)
                                    .WithLoops(4, LoopType.Yoyo)
                                    .Bind(c => image.color = c);
                                entity.AddComponent(new MotionComponent { Value = motionHandle });
                            }
                        }
                    }
                }
            }
        }

        buffer.Playback();
    }

    public override bool IsSupported(Entity entity, EntityDefinition entityDef) => entityDef.GetType() == typeof(InventoryWindowDef);

}