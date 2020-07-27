using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Termination.Items.Weapons.ELD
{
    public class ELD2 : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName
            .SetDefault("Entity Launching Chaingun");
            Tooltip.SetDefault("-Still debugging duel use is PAINFULL");
        }

        public override void SetDefaults()
        {
            item.ranged = true;
            item.damage = 100;
            item.width = 42;
            item.height = 30;
            item.useTime = 20;
            item.useAnimation = 20;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.noMelee = true;
            item.knockBack = 1f;
            item.value = Item.sellPrice(0, 10, 0, 0);
            item.rare = ItemRarityID.Orange;
            item.UseSound = SoundID.Item116;
            item.autoReuse = true;
            item.shootSpeed = 5f;
            item.shoot = ProjectileID.PurificationPowder;
            item.useAmmo = mod.ItemType("BeeC");
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "AdvancedCurcuit", 4);
            recipe.AddIngredient(null, "Molexiumbar", 14);
            recipe.AddIngredient(null, "DeadMagicExtract", 14);
            recipe.AddIngredient(null, "ELD", 1);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        [System.Obsolete]
        public override void GetWeaponDamage(Player player, ref int damage)
        {
            damage = (int)(damage * player.bulletDamage + 5E-06);
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-10, 0);
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Vector2 muzzleOffset = Vector2.Normalize(new Vector2(speedX, speedY)) * 25f;        // How can I make the shots appear out of the muzzle exactly?
            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))                // Also, when I do this, how do I prevent shooting through tiles?
            {
                position += muzzleOffset;
            }

            int numberProjectiles = 3;

            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(20));
                Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI);
            }
            Projectile.NewProjectile(position.X, position.Y, speedX, speedY, ProjectileID.BlackBolt, damage, knockBack, player.whoAmI);
            return true;
        }
    }
}