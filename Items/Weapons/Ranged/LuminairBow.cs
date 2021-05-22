using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Termination.Items.Weapons.Ranged
{
    public class LuminairBow : ModItem
    {
        public override void SetDefaults()
        {
            item.damage = 74;
            item.width = 18;
            item.height = 56;
            item.ranged = true;
            item.useTime = 30;
            item.shoot = ProjectileID.WoodenArrowFriendly;
            item.shootSpeed = 12f;
            item.useAnimation = 30;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.knockBack = 5;
            item.value = 25000;
            item.useAmmo = AmmoID.Arrow;
            item.rare = 10;
            item.Termination().extendedrarity = 12;
            item.UseSound = SoundID.Item5;
            item.autoReuse = true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Luminair Bow");
            Tooltip.SetDefault("And your enemies fell before you..." +
                " \nturns wooden arrows into luminair ones");
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (type == ProjectileID.WoodenArrowFriendly)
            {
                type = mod.ProjectileType("LuminairArrowProj");
            }

            float numberProjectiles = 5; //5 shots
            float rotation = MathHelper.ToRadians(10);
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))); // Watch out for dividing by 0 if there is only 1 projectile.
                Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI);
            }
            return false;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "HardenedAlloy", 18);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}