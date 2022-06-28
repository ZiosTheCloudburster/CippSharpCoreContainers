using System;
using UnityEngine;

namespace CippSharp.Core.Containers
{
    [CreateAssetMenu(menuName = nameof(CippSharp)+"/Data Assets/Color Loot Table Asset")]
    public class ColorLootTableAsset : ALootTableDataAsset<ColorLootTableAsset.StringColorPairWithLootChance, Color>
    {
        [Serializable] 
        public class StringColorPairWithLootChance : StringColorPair, ILootPair
        {
            [field: SerializeField, Range(0.000f, 100.000f)] 
            public float LootChance { get; protected set; } = 0;
            
            public object Loot => value;
            
            public StringColorPairWithLootChance(string name, Color color) : base(name, color)
            {
                
            }

            public StringColorPairWithLootChance(string name, Color color, float lootChance) : base(name, color)
            {
                this.LootChance = lootChance;
            }
        }

        public override Color DropElement()
        {
            return base.DropElement();
        }
    }
}
