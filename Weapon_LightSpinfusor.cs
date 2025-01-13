datablock AudioProfile(TW_LightSpinfusorFireSound)
{
   filename    = "./wav/spinfusor_light_fire.wav";
   description = HeavyClose3D;
   preload = true;
};

datablock AudioProfile(TW_LightSpinfusorExplodeSound)
{
   filename    = "./wav/spinfusor_light_explode.wav";
   description = ExplosionFarLoud3D;
   preload = true;
};

datablock AudioProfile(TW_LightSpinfusorFlySound)
{
   filename    = "./wav/spinfusor_light_fly.wav";
   description = AudioDefaultLooping3D;
   preload = true;
};

datablock AudioProfile(TW_SpinfusorUnholsterSound)
{
   filename    = "./wav/spinfusor_unholster.wav";
   description = AudioClosest3D;
   preload = true;
};

datablock ExplosionData(TW_LightSpinfusorExplosion)
{
	explosionShape = "Add-Ons/Weapon_Rocket_Launcher/explosionSphere1.dts";
	soundProfile = TW_LightSpinfusorExplodeSound;

	lifeTimeMS = 350;

	particleEmitter = TW_LauncherExplosionEmitter2;
	particleDensity = 40;
	particleRadius = 0.5;

	emitter[0] = TW_LauncherFlashEmitter;
	emitter[1] = TW_LauncherExplosionBlueEmitter;

	faceViewer     = true;
	explosionScale = "1 1 1";

	shakeCamera = true;
	camShakeFreq = "10.0 11.0 10.0";
	camShakeAmp = "3.0 10.0 3.0";
	camShakeDuration = 0.5;
	camShakeRadius = 100.0;

	lightStartRadius = 5;
	lightEndRadius = 15;
	lightStartColor = "0 0.5 1 1";
	lightEndColor = "0 0 0 0";

	damageRadius = 12;
	radiusDamage = 35;

	impulseRadius = 10;
	impulseForce = 1500;
};

datablock ProjectileData(TW_LightSpinfusorProjectile)
{
	projectileShapeName = "./dts/spinfusor_projectile.1.dts";
	directDamage        = 50;
	directDamageType = $DamageType::AE;
	radiusDamageType = $DamageType::AE;
	impactImpulse	   = 1;
	verticalImpulse	   = 1000;
	explosion           = TW_LightSpinfusorExplosion;
	particleEmitter     = TW_LauncherTrailSpinEmitter;

	brickExplosionRadius = 3;
	brickExplosionImpact = false;
	brickExplosionForce  = 30;
	brickExplosionMaxVolume = 30;
	brickExplosionMaxVolumeFloating = 60;

	explodeOnDeath = true;
	explodeOnPlayerImpact = true;

	sound = TW_LightSpinfusorFlySound;

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
	lightColor  = "0.0 0.5 1.0";

	uiName = "";
};

function TW_LightSpinfusorProjectile::onCollision(%this, %obj, %col, %fade, %pos, %normal, %velocity)
{
	AETrailedProjectile::onCollision(%this, %obj, %col, %fade, %pos, %normal, %velocity);
}

function TW_LightSpinfusorProjectile::onExplode(%this, %obj, %col, %fade, %pos, %normal, %velocity)
{
	AETrailedProjectile::onExplode(%this, %obj, %col, %fade, %pos, %normal, %velocity);
}

function TW_LightSpinfusorProjectile::Damage(%this, %obj, %col, %fade, %pos, %normal)
{
	AETrailedProjectile::Damage(%this, %obj, %col, %fade, %pos, %normal);
}

datablock ItemData(TW_LightSpinfusorItem)
{
	category = "Weapon";
	className = "Weapon";

	shapeFile = "./dts/light_spinfusor.dts";
	rotate = false;
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;

	uiName = "TW: Light Spinfusor";
	iconName = "./ico/LightSpinfusor";
	doColorShift = true;
	colorShiftColor = "0.5 0.65 0.85 1";

	image = TW_LightSpinfusorImage;
	canDrop = true;

	AEAmmo = 1;
	AEType = TW_SpinfusorAmmoItem.getID();
	AEBase = 1;

	RPM = 4;
	recoil = "Low";
	uiColor = "1 1 1";
	description = "Standard disc launcher, 50% velocity inheritance";

	useImpactSounds = true;
	softImpactThreshold = 2;
	softImpactSound = "AEWepImpactSoft1Sound AEWepImpactSoft2Sound AEWepImpactSoft3Sound";
	hardImpactThreshold = 8;
	hardImpactSound = "AEWepImpactHard1Sound AEWepImpactHard2Sound AEWepImpactHard3Sound";

	tribalClass = "impact";
};

datablock ShapeBaseImageData(TW_LightSpinfusorImage)
{
	shapeFile = "./dts/light_spinfusor.dts";
	emap = true;

	mountPoint = 0;
	offset = "0 0 0";
	eyeOffset = "0 0 0";
	rotation = eulerToMatrix( "0 0 0" );
 
	correctMuzzleVector = true;

	className = "WeaponImage";

	item = TW_LightSpinfusorItem;
	ammo = " ";
	projectile = TW_LightSpinfusorProjectile;
	projectileType = Projectile;

	shellExitDir        = "-1 0 0.5";
	shellExitOffset     = "0 0 0";
	shellExitVariance   = 25;	
	shellVelocity       = 5.0;
	
	melee = false;
	armReady = true;
	hideHands = false;

	doColorShift = true;
	colorShiftColor = TW_LightSpinfusorItem.colorShiftColor;

	muzzleFlashScale = "1 1 1";
	bulletScale = "1 1 1";

	screenshakeMin = "0.3 0.3 0.3"; 
	screenshakeMax = "1 1 1";

	projectileDamage = 20;
	projectileCount = 1;
	projectileHeadshotMult = 1.0;
	projectileVelocity = 125;
	projectileTagStrength = 0;
	projectileTagRecovery = 1.0;
	projectileInheritance = 0.5;

	alwaysSpawnProjectile = true;
	projectileVehicleDamageMult = 0.5;

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
	stateSound[0] = TW_SpinfusorUnholsterSound;

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
	stateTimeoutValue[3]             	= 0.45;
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
	stateTimeoutValue[7]			= 0.3;
	stateScript[7]				= "onReloadStart";
	stateTransitionOnTimeout[7]		= "ReloadMagIn";
	stateWaitForTimeout[7]			= true;
	stateSequence[7]			= "noDisc";
	
	stateName[9]				= "ReloadMagIn";
	stateTimeoutValue[9]			= 0.3;
	stateScript[9]				= "onReloadMagIn";
	stateTransitionOnTimeout[9]		= "ReloadEnd";
	stateWaitForTimeout[9]			= true;
	stateSequence[9]			= "root";
	stateSound[9]				= TW_LightSpinfusorReloadSound;
	
	stateName[10]				= "ReloadEnd";
	stateTimeoutValue[10]			= 0.4;
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

function TW_LightSpinfusorImage::AEOnFire(%this,%obj,%slot)
{	
  %obj.schedule(0, "aeplayThread", "2", "jump");
	%obj.stopAudio(0); 
  %obj.playAudio(0, TW_LightSpinfusorFireSound);
  
	%obj.blockImageDismount = true;
	%obj.schedule(400, unBlockImageDismount);

	Parent::AEOnFire(%this, %obj, %slot);
}

function TW_LightSpinfusorImage::onDryFire(%this, %obj, %slot)
{
	%obj.aeplayThread(2, plant);
	serverPlay3D(AEDryFireSound, %obj.getHackPosition());
}

function TW_LightSpinfusorImage::onReloadMagIn(%this,%obj,%slot)
{
  %obj.aeplayThread("3", "plant");
}

function TW_LightSpinfusorImage::onReloadEnd(%this,%obj,%slot)
{
	Parent::AEMagReloadAll(%this, %obj, %slot);
}

function TW_LightSpinfusorImage::onReloadStart(%this,%obj,%slot)
{

}

function TW_LightSpinfusorImage::onReady(%this,%obj,%slot)
{
	%obj.baadDisplayAmmo(%this);

	if(getSimTime() - %obj.reloadTime[%this.getID()] <= %this.stateTimeoutValue[0] * 1000 + 1000)
		%obj.schedule(0, setImageAmmo, %slot, 0);
}

function TW_LightSpinfusorImage::onMount(%this,%obj,%slot)
{
	%obj.aeplayThread(2, plant);
	%this.AEMountSetup(%obj, %slot);
	parent::onMount(%this,%obj,%slot);
}

function TW_LightSpinfusorImage::onUnMount(%this,%obj,%slot)
{
	%this.AEUnmountCleanup(%obj, %slot);

	cancel(%obj.reload2Schedule);

	parent::onUnMount(%this,%obj,%slot);	
}