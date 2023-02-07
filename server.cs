%errorA = ForceRequiredAddOn("Weapon_AEBase");
%errorB = ForceRequiredAddOn("Weapon_Grenades");

if(%errorA == $Error::AddOn_NotFound)
{
	error("Weapon_TribalWar Error: required add-on Weapon_AEBase not found");
	return;
}

if(%errorB == $Error::AddOn_NotFound)
{
	error("Weapon_TribalWar Error: required add-on Weapon_Grenades not found");
	return;
}

function tw(%path)
{
	if(%path $= "")
		exec("./server.cs");
	else
		exec("./" @ %path @ ".cs");
}

function ProjectileData::Damage (%this, %obj, %col, %fade, %pos, %normal) // This game sucks (overwriting to bypass the 100 damage limit)
{
	if (%this.directDamage <= 0)
	{
		return;
	}
	%damageType = $DamageType::Direct;
	if (%this.DirectDamageType)
	{
		%damageType = %this.DirectDamageType;
	}
	%scale = getWord (%obj.getScale (), 2);
	%directDamage = %this.directDamage * %scale;
	if (%col.getType () & $TypeMasks::PlayerObjectType)
	{
		%col.Damage (%obj, %pos, %directDamage, %damageType);
	}
	else 
	{
		%col.Damage (%obj, %pos, %directDamage, %damageType);
	}
}

exec("./Particle_Explosion.cs");
exec("./Particle_Trail.cs");
exec("./Item_Ammo.cs");
exec("./Sound_Blockland.cs");
exec("./Sound_Reload.cs");
exec("./Support_ArmingDelayFix.cs");
exec("./Support_HomingProjectiles.cs");

exec("./Weapon_Thumper.cs");
exec("./Weapon_Spinfusor.cs");
exec("./Weapon_Flamethrower.cs");
exec("./Weapon_SniperRifle.cs");
exec("./Weapon_DoubleShotgun.cs");
exec("./Weapon_PlasmaRifle.cs");
exec("./Weapon_Chaingun.cs");
exec("./Weapon_Machinegun.cs");
exec("./Weapon_MachinePistol.cs");
exec("./Weapon_HeatSeeker.cs");
exec("./Weapon_GrenadeLauncher.cs");
exec("./Weapon_BurstProxy.cs");
exec("./Weapon_FusionMortar.cs");
exec("./Weapon_HomingHydra.cs");
exec("./Weapon_MIRVLauncher.cs");
exec("./Weapon_RepairPack.cs");
exec("./Weapon_ChargeCannon.cs");
exec("./Weapon_Blaster.cs");
exec("./Weapon_JumpGun.cs");
exec("./Weapon_Shocklance.cs");
exec("./Weapon_PhaseRifle.cs");
