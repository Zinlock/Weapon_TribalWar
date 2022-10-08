
////////////////////////////////////////////////////////

//we need some add-ons for this, so force them to load
	%errorA = ForceRequiredAddOn("Weapon_Gun");
	%errorB = ForceRequiredAddOn("Emote_Critical");
	%errorC = ForceRequiredAddOn("Projectile_GravityRocket");
	%errorD = ForceRequiredAddOn("Weapon_Rocket_Launcher");

if(%errorA == $Error::AddOn_NotFound)
   error("ERROR: Weapon_Package_QuakeType - required add-on Weapon_Gun not found");
else if(%errorB == $Error::AddOn_NotFound)
   error("ERROR: Weapon_Package_QuakeType - required add-on Emote_Critical not found");
else if(%errorC == $Error::AddOn_NotFound)
   error("ERROR: Weapon_Package_QuakeType - required add-on Projectile_GravityRocket not found");
else if(%errorD == $Error::AddOn_NotFound)
   error("ERROR: Weapon_Package_QuakeType - required add-on Weapon_Rocket_Launcher not found");
else
{
	exec("./Support_RaycastingWeapons.cs");
	exec("./Support_DrawLines.cs");
	exec("./Support_AltDatablock.cs");
	exec("./Support_HomingProjectiles.cs");
	exec("./Support_Items.cs");

	exec("./Effect_Electrocute.cs");
	exec("./Effect_Fire.cs");

	exec("./Weapon_Rotary L.cs");
	exec("./Weapon_Rocket L.cs");
	exec("./Weapon_Nailgun.cs");
	exec("./Weapon_Shotgun.cs");
	exec("./Weapon_Chaingun.cs");
	exec("./Weapon_Sniper Rifle.cs");
	exec("./Weapon_Machine Gun.cs");
	exec("./Weapon_Static Beam.cs");
	exec("./Weapon_Railgun.cs");
	exec("./Weapon_Missile Storm.cs");
	exec("./Weapon_Super Shotgun.cs");
	exec("./Weapon_NoTime.cs");
	exec("./Weapon_TwinSpamRifle.cs");
	exec("./Weapon_SawTomahawk.cs");
	exec("./Weapon_SawbladeRifle.cs");
	exec("./Weapon_DoubleMagnum.cs");
	exec("./Weapon_Nitro Stream.cs");
	exec("./Weapon_FireballMissile.cs");
	exec("./Weapon_StrongMissile.cs");
	exec("./Weapon_AlloyCannon.cs");
	exec("./Weapon_RocketPunch.cs");
	exec("./Weapon_SpiralMissile.cs");
	exec("./Weapon_ShotgunMissile.cs");
	exec("./Weapon_TwinBayonets.cs");
	exec("./Weapon_Volley Gun.cs");
	exec("./Weapon_Spin Shotgun.cs");
	exec("./Weapon_Fireball Rifle.cs");
	exec("./Weapon_TwinSMG.cs");
	exec("./Weapon_Tracer.cs");
	exec("./Weapon_Rapid Rifle.cs");
	exec("./Weapon_Lifeblower.cs");
	exec("./Weapon_PlasmifireAkimbo.cs");
	exec("./Weapon_BunkerBuster.cs");
	exec("./Weapon_Homing Hydra.cs");
	exec("./Weapon_T_Heaters.cs");
	exec("./Weapon_KANJam.cs");
	exec("./Weapon_CQNitroStream.cs");
	exec("./Weapon_Quaker.cs");
	exec("./Weapon_TurboPunch.cs");
	exec("./Weapon_UZITetrakaikimbo.cs");
	exec("./Weapon_GrenadeShotgun.cs");
	exec("./Weapon_CalibreCannon.cs");
	exec("./Weapon_AssaultRaygun.cs");
	exec("./Weapon_Charge Cannon.cs");
	exec("./Weapon_Flak.cs");
	exec("./Weapon_Heavy Pistol.cs");
	exec("./Weapon_Thief Pistol.cs");
	exec("./Weapon_Homahawk.cs");
	exec("./Weapon_Breach Rifle.cs");
	exec("./Weapon_Mundane Rifle.cs");
}

////////////////////////////////////////////////////////

datablock ExplosionData(QuakeLittleRecoilExplosion)
{
   explosionShape = "";

   lifeTimeMS = 150;

   faceViewer     = true;
   explosionScale = "1 1 1";

   shakeCamera = true;
  camShakeFreq = "1 1 1";
  camShakeAmp = "0.1 0.3 0.2";
   camShakeDuration = 0.5;
   camShakeRadius = 10.0;
};

datablock ProjectileData(QuakeLittleRecoilProjectile)
{
	lifetime						= 10;
	fadeDelay						= 10;
	explodeondeath						= true;
	explosion						= QuakeLittleRecoilExplosion;

};

datablock ExplosionData(QuakeRecoilExplosion)
{
   explosionShape = "";

   lifeTimeMS = 150;

   faceViewer     = true;
   explosionScale = "1 1 1";

   shakeCamera = true;
  camShakeFreq = "2 2 2";
  camShakeAmp = "0.3 0.5 0.4";
   camShakeDuration = 0.5;
   camShakeRadius = 10.0;
};

datablock ProjectileData(QuakeRecoilProjectile)
{
	lifetime						= 10;
	fadeDelay						= 10;
	explodeondeath						= true;
	explosion						= QuakeRecoilExplosion;

};

datablock ExplosionData(QuakeBigRecoilExplosion)
{
   explosionShape = "";

   lifeTimeMS = 150;

   faceViewer     = true;
   explosionScale = "1 1 1";

   shakeCamera = true;
  camShakeFreq = "3 3 3";
  camShakeAmp = "0.6 0.8 0.7";
   camShakeDuration = 0.5;
   camShakeRadius = 10.0;
};

datablock ProjectileData(QuakeBigRecoilProjectile)
{
	lifetime						= 10;
	fadeDelay						= 10;
	explodeondeath						= true;
	explosion						= QuakeBigRecoilExplosion;

};

datablock ExplosionData(QuakeHugeRecoilExplosion)
{
   explosionShape = "";

   lifeTimeMS = 150;

   faceViewer     = true;
   explosionScale = "1 1 1";

   shakeCamera = true;
  camShakeFreq = "5 5 5";
  camShakeAmp = "1.1 1.3 1.2";
   camShakeDuration = 0.5;
   camShakeRadius = 10.0;
};

datablock ProjectileData(QuakeHugeRecoilProjectile)
{
	lifetime						= 10;
	fadeDelay						= 10;
	explodeondeath						= true;
	explosion						= QuakeHugeRecoilExplosion;

};
