datablock AudioProfile(TW_FusionMortarFireSound)
{
   filename    = "./wav/fusion_mortar_fire.wav";
   description = HeavyClose3D;
   preload = true;
};

datablock AudioProfile(TW_FusionMortarExplodeSound)
{
   filename    = "./wav/fusion_mortar_explode.wav";
   description = ExplosionFarLoud3D;
   preload = true;
};

datablock AudioProfile(TW_FusionMortarFlySound)
{
   filename    = "./wav/fusion_mortar_fly.wav";
   description = AudioDefaultLooping3D;
   preload = true;
};

datablock ExplosionData(TW_FusionMortarExplosion)
{
	explosionShape = "Add-Ons/Weapon_Rocket_Launcher/explosionSphere1.dts";
	soundProfile = TW_FusionMortarExplodeSound;

	lifeTimeMS = 350;

	particleEmitter = TW_MortarExplosionEmitter;
	particleDensity = 100;
	particleRadius = 4.0;

	emitter[0] = TW_MortarSmokeEmitter;

	faceViewer     = true;
	explosionScale = "1 1 1";

	shakeCamera = true;
	camShakeFreq = "10.0 11.0 10.0";
	camShakeAmp = "3.0 10.0 3.0";
	camShakeDuration = 0.5;
	camShakeRadius = 100.0;

	lightStartRadius = 4;
	lightEndRadius = 12;
	lightStartColor = "0 1 0 1";
	lightEndColor = "0 0 0 0";

	damageRadius = 15;
	radiusDamage = 80;

	impulseRadius = 17;
	impulseForce = 1500;
};

datablock ProjectileData(TW_FusionMortarProjectile)
{
	projectileShapeName = "./dts/fusion_mortar_projectile.dts";
	directDamage        = 50;
	directDamageType = $DamageType::AE;
	radiusDamageType = $DamageType::AE;
	impactImpulse	   = 1;
	verticalImpulse	   = 1000;
	explosion           = TW_FusionMortarExplosion;
	particleEmitter     = TW_MortarTrailEmitter;

	brickExplosionRadius = 3;
	brickExplosionImpact = false;          //destroy a brick if we hit it directly?
	brickExplosionForce  = 30;             
	brickExplosionMaxVolume = 30;          //max volume of bricks that we can destroy
	brickExplosionMaxVolumeFloating = 60;  //max volume of bricks that we can destroy if they aren't connected to the ground (should always be >= brickExplosionMaxVolume)

	explodeOnDeath = true;
	explodeOnPlayerImpact = true;

	sound = TW_FusionMortarFlySound;

	muzzleVelocity      = 100;
	velInheritFactor    = 0;

	armingDelay         = 1000;
	lifetime            = 9500;
	fadeDelay           = 9490;
	bounceElasticity    = 0.15;
	bounceFriction       = 0.5;
	isBallistic         = true;
	gravityMod = 1.0;

	hasLight    = true;
	lightRadius = 5.0;
	lightColor  = "0.0 1.0 0.0";

	uiName = "";
};

function TW_FusionMortarProjectile::onCollision(%this, %obj, %col, %fade, %pos, %normal, %velocity)
{
	AETrailedProjectile::onCollision(%this, %obj, %col, %fade, %pos, %normal, %velocity);
}

function TW_FusionMortarProjectile::onExplode(%this, %obj, %col, %fade, %pos, %normal, %velocity)
{
	AETrailedProjectile::onExplode(%this, %obj, %col, %fade, %pos, %normal, %velocity);
}

function TW_FusionMortarProjectile::Damage(%this, %obj, %col, %fade, %pos, %normal)
{
	AETrailedProjectile::Damage(%this, %obj, %col, %fade, %pos, %normal);
}

datablock ItemData(TW_FusionMortarItem)
{
	category = "Weapon";
	className = "Weapon";

	shapeFile = "./dts/fusion_mortar_na.dts";
	rotate = false;
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;

	uiName = "TW: Fusion Mortar";
	iconName = "./ico/fusion_mortar";
	doColorShift = true;
	colorShiftColor = "0.2 0.25 0.15 1";

	image = TW_FusionMortarImage;
	canDrop = true;

	AEAmmo = 3;
	AEType = TW_MortarAmmoItem.getID();
	AEBase = 1;

	RPM = 4;
	recoil = "Low";
	uiColor = "1 1 1";
	description = "Two shot grenade launcher, 75% velocity inheritance, 1s arming delay";

	useImpactSounds = true;
	softImpactThreshold = 2;
	softImpactSound = "AEWepImpactSoft1Sound AEWepImpactSoft2Sound AEWepImpactSoft3Sound";
	hardImpactThreshold = 8;
	hardImpactSound = "AEWepImpactHard1Sound AEWepImpactHard2Sound AEWepImpactHard3Sound";

	tribalClass = "timed";
};

datablock ShapeBaseImageData(TW_FusionMortarImage)
{
	shapeFile = "./dts/fusion_mortar_na.dts";
	emap = true;

	mountPoint = 0;
	offset = "0 0 0.075";
	eyeOffset = "0 0 0";
	rotation = eulerToMatrix( "0 0 0" );
 
	correctMuzzleVector = true;

	className = "WeaponImage";

	item = TW_FusionMortarItem;
	ammo = " ";
	projectile = TW_FusionMortarProjectile;
	projectileType = Projectile;

	shellExitDir        = "-1 0 0.5";
	shellExitOffset     = "0 0 0";
	shellExitVariance   = 25;	
	shellVelocity       = 5.0;
	
	melee = false;
	armReady = true;
	hideHands = false;

	doColorShift = true;
	colorShiftColor = TW_FusionMortarItem.colorShiftColor;

	muzzleFlashScale = "1 1 1";
	bulletScale = "1 1 1";

	screenshakeMin = "0.3 0.3 0.3"; 
	screenshakeMax = "1 1 1";

	projectileDamage = 25;
	projectileCount = 1;
	projectileHeadshotMult = 1.0;
	projectileVelocity = 125;
	projectileTagStrength = 0;
	projectileTagRecovery = 1.0;
	projectileInheritance = 0.75;

	alwaysSpawnProjectile = true;
	projectileVehicleDamageMult = 1.5;

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
	stateTimeoutValue[0]             	= 0.25;
	stateTransitionOnTimeout[0]       	= "LoadCheckA";
	stateSequence[0]			= "root";
	stateSound[0] = TW_MortarUnholsterSound;

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
	stateTimeoutValue[3]             	= 1.0;
	stateEmitter[3]					= rocketLauncherSmokeEmitter;
	stateEmitterTime[3]				= 0.05;
	stateEmitterNode[3]				= "muzzlePoint";
	stateAllowImageChange[3]        = false;
	stateSequence[3]                = "Fire";
	stateWaitForTimeout[3]			= true;
	
	stateName[5]				= "LoadCheckA";
	stateScript[5]				= "AEMagLoadCheck";
	stateTimeoutValue[5]			= 0.1;
	stateTransitionOnTimeout[5]		= "LoadCheckB";
	
	stateName[6]				= "LoadCheckB";
	stateTransitionOnAmmo[6]		= "Pump";
	stateTransitionOnNotLoaded[6] = "Empty";
	stateTransitionOnNoAmmo[6]		= "Reload";

	stateName[7]				= "Reload";
	stateTimeoutValue[7]			= 0.5;
	stateScript[7]				= "onReloadStart";
	stateTransitionOnTimeout[7]		= "ReloadMagOut";
	stateWaitForTimeout[7]			= true;
	stateSequence[7]			= "ReloadStart";

	stateName[24]				= "ReloadMagOut";
	stateTimeoutValue[24]			= 0.4;
	stateScript[24]				= "onReloadMagOut";
	stateTransitionOnTimeout[24]		= "ReloadMagIn";
	stateWaitForTimeout[24]			= true;
	stateSequence[24]			= "ReloadOut";
	
	stateName[9]				= "ReloadMagIn";
	stateTimeoutValue[9]			= 0.5;
	stateScript[9]				= "onReloadMagIn";
	stateTransitionOnTimeout[9]		= "ReloadEnd";
	stateWaitForTimeout[9]			= true;
	stateSequence[9]			= "ReloadIn";
	
	stateName[10]				= "ReloadEnd";
	stateTimeoutValue[10]			= 0.4;
	stateTransitionOnTimeout[10]		= "Reloaded";
	stateWaitForTimeout[10]			= true;
	stateScript[10]			  = "onReloadEnd";
	stateSequence[10]			= "ReloadEnd";
	
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
	stateTransitionOnTimeout[14]		= "Pump";

	stateName[21]          = "Empty";
	stateTransitionOnTriggerDown[21]  = "Dryfire";
	stateTransitionOnLoaded[21] = "Reload";
	stateScript[21]        = "AEOnEmpty";

	stateName[22]           = "Dryfire";
	stateTransitionOnTriggerUp[22] = "Empty";
	stateWaitForTimeout[22]    = false;
	stateScript[22]      = "onDryFire";
	
	stateName[23]           = "SemiAutoCheck";
	stateTransitionOnTriggerUp[23]	  	= "Pump";

	stateName[25]           = "Pump";
	stateScript[25]  = "onPump";
	stateSound[25] = TW_MortarCycleSound;
	stateTransitionOnTimeout[25]	  	= "FireLoadCheckA";
	stateTimeoutValue[25]    = 0.75;
};

function TW_FusionMortarImage::AEOnFire(%this,%obj,%slot)
{	
  %obj.schedule(0, "aeplayThread", "2", "jump");
	%obj.stopAudio(0); 
  %obj.playAudio(0, TW_FusionMortarFireSound);
  
	%obj.blockImageDismount = true;
	%obj.schedule(400, unBlockImageDismount);

	Parent::AEOnFire(%this, %obj, %slot);
}

function TW_FusionMortarImage::onDryFire(%this, %obj, %slot)
{
	%obj.aeplayThread(2, plant);
	serverPlay3D(AEDryFireSound, %obj.getHackPosition());
}

function TW_FusionMortarImage::onPump(%this,%obj,%slot)
{
  %obj.aeplayThread("3", "shiftRight");
}

function TW_FusionMortarImage::onReloadMagOut(%this,%obj,%slot)
{
  %obj.aeplayThread("3", "shiftRight");
	%obj.playAudio(1, TW_TacticalReloadOut2Sound);
}

function TW_FusionMortarImage::onReloadMagIn(%this,%obj,%slot)
{
  %obj.aeplayThread("3", "shiftRight");
	%obj.playAudio(2, TW_TacticalReloadTap2Sound);
  %obj.schedule(150, "aeplayThread", "2", "shiftLeft");
  %obj.schedule(150, "aeplayThread", "3", "shiftRight");
	%obj.reload2Schedule = %obj.schedule(100,playAudio, 1, TW_TacticalReloadTap3Sound);
}

function TW_FusionMortarImage::onReloadEnd(%this,%obj,%slot)
{
  %obj.aeplayThread("3", "shiftLeft");
  %obj.schedule(150, "aeplayThread", "3", "plant");
	%obj.playAudio(2, TW_TacticalReloadTap2Sound);
	%obj.reload2Schedule = %obj.schedule(100,playAudio, 1, TW_TacticalReloadClick3Sound);
	Parent::AEMagReloadAll(%this, %obj, %slot);
}

function TW_FusionMortarImage::onReloadStart(%this,%obj,%slot)
{
  %obj.schedule(150, "aeplayThread", "2", "shiftLeft");
  %obj.schedule(150, "aeplayThread", "3", "plant");
	%obj.reload2Schedule = %obj.schedule(150,playAudio, 1, TW_MagnumCycleSound);
}

function TW_FusionMortarImage::onReady(%this,%obj,%slot)
{
	%obj.baadDisplayAmmo(%this);

	if(getSimTime() - %obj.reloadTime[%this.getID()] <= %this.stateTimeoutValue[0] * 1000 + 1000)
		%obj.schedule(0, setImageAmmo, %slot, 0);
}

function TW_FusionMortarImage::onMount(%this,%obj,%slot)
{
	%obj.aeplayThread(2, plant);
	%this.AEMountSetup(%obj, %slot);
	parent::onMount(%this,%obj,%slot);
}

function TW_FusionMortarImage::onUnMount(%this,%obj,%slot)
{
	%this.AEUnmountCleanup(%obj, %slot);

	cancel(%obj.reload2Schedule);

	parent::onUnMount(%this,%obj,%slot);	
}