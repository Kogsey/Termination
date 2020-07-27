using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Termination.Items.BossSummons
{
    public class SusRemote : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("suspicious looking remote");
            Tooltip.SetDefault("I should't need to explain this");
        }

        public override void SetDefaults()
        {
            item.width = 34;
            item.height = 38;
            item.maxStack = 20;
            item.rare = ItemRarityID.Cyan;
            item.useAnimation = 45;
            item.useTime = 45;
            item.useStyle = ItemUseStyleID.HoldingUp;
            item.UseSound = SoundID.Item44;
            item.consumable = true;
        }

        public override bool CanUseItem(Player player)
        {
            return !NPC.AnyNPCs(mod.NPCType("ElectronicEye")) && !NPC.AnyNPCs(mod.NPCType("BallMetal")) && !NPC.AnyNPCs(mod.NPCType("BallMetal2")) && !NPC.AnyNPCs(mod.NPCType("ElectronicEyeDrone"));
        }

        public override bool UseItem(Player player)
        {
            NPC.SpawnOnPlayer(player.whoAmI, mod.NPCType("ElectronicEye"));
            Main.PlaySound(SoundID.Roar, (int)player.position.X, (int)player.position.Y, 0);
            return true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.LunarBar, 5);
            recipe.AddIngredient(null, "Araaxiumbar", 5);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}