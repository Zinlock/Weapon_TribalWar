datablock AudioProfile(TW_PlasmaRifleFireSound)
{
   filename    = "./wav/Plasma_Rifle_fire.wav";
   description = HeavyClose3D;
   preload = true;
};

datablock AudioProfile(TW_PlasmaRifleExplodeSound)
{
   filename    = "./wav/Plasma_Rifle_explode.wav";
   description = AudioDefault3D;
   preload = true;
};

datablock AudioProfile(TW_PlasmaRifleFlySound)
{
   filename    = "./wav/Plasma_Rifle_fly.wav";
   description = AudioDefault3D;
   preload = true;
};

datablock AudioProfile(TW_PlasmaRifleUnholsterSound)
{
   filename    = "./wav/Plasma_Rifle_unholster.wav";
   description = AudioClosest3D;
   preload = true;
};

datablock ExplosionData(TW_PlasmaRifleExplosion)
{
	explosionShape = "Add-Ons/Weapon_Rocket_Launcher/explosionSphere1.dts";
	soundProfile = TW_PlasmaRifleExplodeSound;

	lifeTimeMS = 350;

	particleEmitter = TW_LauncherExplosionEmitter2;
	particleDensity = 15;
	particleRadius = 0.5;

	emitter[0] = TW_LauncherFlashEmitter;
	emitter[1] = TW_LauncherExplosionPlasmaEmitter;

	faceViewer     = true;
	explosionScale = "1 1 1";

	shakeCamera = true;
	camShakeFreq = "10.0 11.0 10.0";
	camShakeAmp = "3.0 10.0 3.0";
	camShakeDuration = 0.5;
	camShakeRadius = 100.0;

	lightStartRadius = 2;
	lightEndRadius = 5;
	lightStartColor = "0 1 0 1";
	lightEndColor = "0 0 0 0";

	damageRadius = 7;
	radiusDamage = 30;

	impulseRadius = 6;
	impulseForce = 500;
};

datablock ProjectileData(TW_PlasmaRifleProjectile)
{
	projectileShapeName = "./dts/plasma_rifle_projectile.dts";
	directDamage        = 50;
	directDamageType = $DamageType::AE;
	radiusDamageType = $DamageType::AE;
	impactImpulse	   = 1;
	verticalImpulse	   = 1000;
	explosion           = TW_PlasmaRifleExplosion;
	particleEmitter     = TW_LauncherTrailPlasmaEmitter;

	brickExplosionRadius = 3;
	brickExplosionImpact = false;          //destroy a brick if we hit it directly?
	brickExplosionForce  = 30;             
	brickExplosionMaxVolume = 30;          //max volume of bricks that we can destroy
	brickExplosionMaxVolumeFloating = 60;  //max volume of bricks that we can destroy if they aren't connected to the ground (should always be >= brickExplosionMaxVolume)

	explodeOnDeath = true;
	explodeOnPlayerImpact = true;

	sound = TW_PlasmaRifleFlySound;

	muzzleVelocity      = 100;
	velInheritFactor    = 0;

	armingDelay         = 0;
	lifetime            = 6000;
	fadeDelay           = 5990;
	bounceElasticity    = 0.5;
	bounceFriction       = 0.20;
	isBallistic         = false;
	gravityMod = 0.0;

	hasLight    = true;
	lightRadius = 5.0;
	lightColor  = "0 1.0 0.0";

	uiName = "";
};

function TW_PlasmaRifleProjectile::onCollision(%this, %obj, %col, %fade, %pos, %normal, %velocity)
{
	AETrailedProjectile::onCollision(%this, %obj, %col, %fade, %pos, %normal, %velocity);
}

function TW_PlasmaRifleProjectile::onExplode(%this, %obj, %col, %fade, %pos, %normal, %velocity)
{
	AETrailedProjectile::onExplode(%this, %obj, %col, %fade, %pos, %normal, %velocity);
}

function TW_PlasmaRifleProjectile::Damage(%this, %obj, %col, %fade, %pos, %normal)
{
	AETrailedProjectile::Damage(%this, %obj, %col, %fade, %pos, %normal);
}

datablock ItemData(TW_PlasmaRifleItem)
{
	category = "Weapon";
	className = "Weapon";

	shapeFile = "./dts/Plasma_Rifle_na.dts";
	rotate = false;
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;

	uiName = "TW: Plasma Rifle";
	iconName = "./ico/Plasma_Rifle";
	doColorShift = true;
	colorShiftColor = "0.4 0.5 0.4 1";

	image = TW_PlasmaRifleImage;
	canDrop = true;

	AEAmmo = 6;
	AEType = TW_PlasmaAmmoItem.getID();
	AEBase = 1;

	RPM = 4;
	recoil = "Low";
	uiColor = "1 1 1";
	description = "Plasma orb launcher, 35% velocity inheritance";

	useImpactSounds = true;
	softImpactThreshold = 2;
	softImpactSound = "AEWepImpactSoft1Sound AEWepImpactSoft2Sound AEWepImpactSoft3Sound";
	hardImpactThreshold = 8;
	hardImpactSound = "AEWepImpactHard1Sound AEWepImpactHard2Sound AEWepImpactHard3Sound";

	tribalClass = "impact";
};

datablock ShapeBaseImageData(TW_PlasmaRifleImage)
{
	shapeFile = "./dts/Plasma_Rifle_na.dts";
	emap = true;

	mountPoint = 0;
	offset = "0 0 0";
	eyeOffset = "0 0 0";
	rotation = eulerToMatrix( "0 0 0" );
 
	correctMuzzleVector = true;

	className = "WeaponImage";

	item = TW_PlasmaRifleItem;
	ammo = " ";
	projectile = TW_PlasmaRifleProjectile;
	projectileType = Projectile;

	shellExitDir        = "-1 0 0.5";
	shellExitOffset     = "0 0 0";
	shellExitVariance   = 25;	
	shellVelocity       = 5.0;
	
	melee = false;
	armReady = true;
	hideHands = false;

	doColorShift = true;
	colorShiftColor = TW_PlasmaRifleItem.colorShiftColor;

	muzzleFlashScale = "1 1 1";
	bulletScale = "1 1 1";

	screenshakeMin = "0.3 0.3 0.3"; 
	screenshakeMax = "1 1 1";

	projectileDamage = 18;
	projectileCount = 1;
	projectileHeadshotMult = 1.0;
	projectileVelocity = 100;
	projectileTagStrength = 0;
	projectileTagRecovery = 1.0;
	projectileInheritance = 0.35;

	alwaysSpawnProjectile = true;

	recoilHeight = 0;
	recoilWidth = 0;
	recoilWidthMax = 0;
	recoilHeightMax = 20;

	spreadBurst = 1;
	spreadReset = 0;
	spreadBase = 0;
	spreadMin = 0;
	spreadMax = 0;

	stateName[0]                     	= "Activate";
	stateTimeoutValue[0]             	= 0.1;
	stateTransitionOnTimeout[0]       	= "LoadCheckA";
	stateSequence[0]			= "root";
	stateSound[0] = TW_PlasmaRifleUnholsterSound;

	stateName[1]                     	= "Ready";
	stateScript[1]				= "onReady";
	stateTransitionOnNotLoaded[1]     = "Empty";
	stateTransitionOnNoAmmo[1]       	= "Reload";
	stateTransitionOnTriggerDown[1]  	= "preFire";
	stateAllowImageChange[1]         	= true;

	stateName[2]                       = "preFire";
	stateTransitionOnTimeout[2]        = "Fire";
	stateScript[2]                     = "AEOnFire";
	stateFire[2]                       = true;

	stateName[3]                    = "Fire";
	stateTransitionOnTimeout[3]     = "SemiAutoCheck";
	stateTimeoutValue[3]             	= 0.75;
	stateEmitter[3]					= rocketLauncherSmokeEmitter;
	stateEmitterTime[3]				= 0.05;
	stateEmitterNode[3]				= "muzzlePoint";
	stateAllowImageChange[3]        = false;
	stateSequence[3]                = "noDisc";
	stateWaitForTimeout[3]			= true;
	
	stateName[5]				= "LoadCheckA";
	stateScript[5]				= "AEMagLoadCheck";
	stateTimeoutValue[5]			= 0.1;
	stateTransitionOnTimeout[5]		= "LoadCheckB";
	
	stateName[6]				= "LoadCheckB";
	stateTransitionOnAmmo[6]		= "Ready";
	stateTransitionOnNotLoaded[6] = "Empty";
	stateTransitionOnNoAmmo[6]		= "Reload";

	stateName[7]				= "Reload";
	stateTimeoutValue[7]			= 0.2;
	stateScript[7]				= "onReloadStart";
	stateTransitionOnTimeout[7]		= "ReloadMagOut";
	stateWaitForTimeout[7]			= true;
	stateSequence[7]			= "noDisc";
	
	stateName[8]				= "ReloadMagOut";
	stateTimeoutValue[8]			= 0.9;
	stateScript[8]				= "onReloadMagOut";
	stateTransitionOnTimeout[8]		= "ReloadMagIn";
	stateWaitForTimeout[8]			= true;
	stateSequence[8]			= "root";
	stateSound[8]				= TW_GenericReload2Sound;

	stateName[9]				= "ReloadMagIn";
	stateTimeoutValue[9]			= 0.6;
	stateScript[9]				= "onReloadMagIn";
	stateTransitionOnTimeout[9]		= "ReloadEnd";
	stateWaitForTimeout[9]			= true;
	stateSequence[9]			= "root";
	
	stateName[10]				= "ReloadEnd";
	stateTimeoutValue[10]			= 0.2;
	stateTransitionOnTimeout[10]		= "Reloaded";
	stateWaitForTimeout[10]			= true;
	stateScript[10]			  = "onReloadEnd";
	stateSequence[10]			= "root";
	
	stateName[11]				= "FireLoadCheckA";
	stateScript[11]				= "AEMagLoadCheck";
	stateTimeoutValue[11]			= 0.065;
	stateTransitionOnTimeout[11]		= "FireLoadCheckB";
	
	stateName[12]				= "FireLoadCheckB";
	stateTransitionOnNoAmmo[12]		= "Reload";
	stateTransitionOnAmmo[12]  = "Ready";
	stateTransitionOnNotLoaded[12]  = "Ready";
		
	stateName[14]				= "Reloaded";
	stateTimeoutValue[14]			= 0.1;
	stateScript[14]				= "AEMagReloadAll";
	stateTransitionOnTimeout[14]		= "Ready";
	
	stateName[20]				= "ReadyLoop";
	stateTransitionOnTimeout[20]		= "Ready";

	stateName[21]          = "Empty";
	stateTransitionOnTriggerDown[21]  = "Dryfire";
	stateTransitionOnLoaded[21] = "Reload";
	stateScript[21]        = "AEOnEmpty";

	stateName[22]           = "Dryfire";
	stateTransitionOnTriggerUp[22] = "Empty";
	stateWaitForTimeout[22]    = false;
	stateScript[22]      = "onDryFire";
	
	stateName[23]           = "SemiAutoCheck";
	stateTransitionOnTriggerUp[23]	  	= "FireLoadCheckA";
};

function TW_PlasmaRifleImage::AEOnFire(%this,%obj,%slot)
{	
  %obj.schedule(0, "aeplayThread", "2", "jump");
	%obj.stopAudio(0); 
  %obj.playAudio(0, TW_PlasmaRifleFireSound);
  
	%obj.blockImageDismount = true;
	%obj.schedule(400, unBlockImageDismount);

	Parent::AEOnFire(%this, %obj, %slot);
}

function TW_PlasmaRifleImage::onDryFire(%this, %obj, %slot)
{
	%obj.aeplayThread(2, plant);
	serverPlay3D(AEDryFireSound, %obj.getHackPosition());
}

function TW_PlasmaRifleImage::onReloadMagOut(%this,%obj,%slot)
{
  %obj.aeplayThread("3", "shiftRight");
}

function TW_PlasmaRifleImage::onReloadMagIn(%this,%obj,%slot)
{
  %obj.aeplayThread("3", "plant");
	%obj.playAudio(1, TW_PlasmaRifleReloadSound);
	%obj.playAudio(2, TW_TacticalReloadBolt2Sound);
}

function TW_PlasmaRifleImage::onReloadEnd(%this,%obj,%slot)
{
	Parent::AEMagReloadAll(%this, %obj, %slot);
}

function TW_PlasmaRifleImage::onReloadStart(%this,%obj,%slot)
{

}

function TW_PlasmaRifleImage::onReady(%this,%obj,%slot)
{
	%obj.baadDisplayAmmo(%this);

	if(getSimTime() - %obj.reloadTime[%this.getID()] <= %this.stateTimeoutValue[0] * 1000 + 1000)
		%obj.schedule(0, setImageAmmo, %slot, 0);
}

function TW_PlasmaRifleImage::onMount(%this,%obj,%slot)
{
	%obj.aeplayThread(2, plant);
	%this.AEMountSetup(%obj, %slot);
	parent::onMount(%this,%obj,%slot);
}

function TW_PlasmaRifleImage::onUnMount(%this,%obj,%slot)
{
	%this.AEUnmountCleanup(%obj, %slot);

	cancel(%obj.reload2Schedule);

	parent::onUnMount(%this,%obj,%slot);	
}