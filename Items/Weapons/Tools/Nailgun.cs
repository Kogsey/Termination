using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Termination.Items.Weapons.Tools
{
    public class Nailgun : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Now with duel uses!... on a Nailgun yeah.");
        }

        public override void SetDefaults()
        {
            item.damage = 15;
            item.melee = true;
            item.width = 40;
            item.height = 40;
            item.useTime = 20;
            item.useAnimation = 20;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.noMelee = true;
            item.knockBack = 6;
            item.value = 1060;
            item.rare = ItemRarityID.Green;
            item.UseSound = SoundID.Item31;
            item.autoReuse = true;
            item.shoot = ProjectileID.Bee;
            item.shootSpeed = 5f;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddRecipeGroup("IronBar", 6);
            recipe.AddRecipeGroup("Wood", 20);
            recipe.AddIngredient(ItemID.Wire, 13);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                item.useStyle = ItemUseStyleID.HoldingOut;
                item.UseSound = SoundID.Item31;
                item.useTime = 50;
                item.useAnimation = 50;
                item.damage = 30;
                item.shoot = ProjectileID.Bullet;
            }
            else
            {
                item.useStyle = ItemUseStyleID.HoldingOut;
                item.UseSound = SoundID.Item36;
                item.useTime = 5;
                item.useAnimation = 5;
                item.damage = 10;
                item.shoot = ProjectileID.Bullet;
            }
            return base.CanUseItem(player);
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            if (player.altFunctionUse == 2)
            {
                target.AddBuff(BuffID.Bleeding, 60);
            }
            else
            {
                target.AddBuff(BuffID.OnFire, 60);
            }
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (player.altFunctionUse == 2)
            {
                int numberProjectiles = 4 + Main.rand.Next(2);

                for (int i = 0; i < numberProjectiles; i++)
                {
                    Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(30));
                    Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI);
                }

                Vector2 muzzleOffset = Vector2.Normalize(new Vector2(speedX, speedY)) * 25f;

                if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
                {
                    position += muzzleOffset;
                }
            }
            return true;
        }
    }
}