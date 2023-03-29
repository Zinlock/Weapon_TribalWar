datablock AudioProfile(TW_MIRVLauncherFireSound)
{
   filename    = "./wav/MIRV_Launcher_fire.wav";
   description = HeavyClose3D;
   preload = true;
};

datablock AudioProfile(TW_MIRVLauncherSplitSound)
{
   filename    = "./wav/MIRV_Launcher_split.wav";
   description = HeavyClose3D;
   preload = true;
};

datablock AudioProfile(TW_MIRVLauncherExplodeSound)
{
   filename    = "./wav/mirv_launcher_explode.wav";
   description = ExplosionFar3D;
   preload = true;
};

datablock AudioProfile(TW_MIRVLauncherFlySound)
{
   filename    = "./wav/mirv_launcher_fly.wav";
   description = AudioDefaultLooping3D;
   preload = true;
};

datablock ExplosionData(TW_MIRVClusterletExplosion)
{
	explosionShape = "Add-Ons/Weapon_Rocket_Launcher/explosionSphere1.dts";
	soundProfile = TW_MIRVLauncherExplodeSound;

	lifeTimeMS = 350;

	particleEmitter = TW_LauncherExplosionEmitter2;
	particleDensity = 40;
	particleRadius = 0.5;

	emitter[0] = TW_LauncherFlashEmitter;
	emitter[1] = TW_LauncherExplosionBlueEmitter;

	faceViewer     = true;
	explosionScale = "2 2 2";

	shakeCamera = true;
	camShakeFreq = "10.0 11.0 10.0";
	camShakeAmp = "3.0 10.0 3.0";
	camShakeDuration = 0.5;
	camShakeRadius = 100.0;

	lightStartRadius = 5;
	lightEndRadius = 15;
	lightStartColor = "0 0.5 1 1";
	lightEndColor = "0 0 0 0";

	damageRadius = 16;
	radiusDamage = 15;

	impulseRadius = 10;
	impulseForce = 1000;
};

datablock ProjectileData(TW_MIRVClusterletNHProjectile)
{
	projectileShapeName = "./dts/MIRV_Launcher_projectile.dts";
	directDamage        = 0;
	directDamageType = $DamageType::AE;
	radiusDamageType = $DamageType::AE;
	impactImpulse	   = 0;
	verticalImpulse	   = 100;
	explosion           = TW_MIRVClusterletExplosion;
	particleEmitter     = TW_LauncherTrailFusionBlueEmitter;

	brickExplosionRadius = 3;
	brickExplosionImpact = false;          //destroy a brick if we hit it directly?
	brickExplosionForce  = 30;             
	brickExplosionMaxVolume = 30;          //max volume of bricks that we can destroy
	brickExplosionMaxVolumeFloating = 60;  //max volume of bricks that we can destroy if they aren't connected to the ground (should always be >= brickExplosionMaxVolume)

	explodeOnDeath = true;
	explodeOnPlayerImpact = true;

	sound = TW_MirvLauncherFlySound;

	muzzleVelocity      = 100;
	velInheritFactor    = 0;

	armingDelay         = 500;
	lifetime            = 30000;
	fadeDelay           = 29990;
	bounceElasticity    = 0.15;
	bounceFriction       = 0.5;
	isBallistic         = true;
	gravityMod = 1.0;

	hasLight    = true;
	lightRadius = 5.0;
	lightColor  = "0.0 0.5 1.0";

	uiName = "";
};

datablock ExplosionData(TW_MIRVLauncherExplosion)
{
	explosionShape = "Add-Ons/Weapon_Rocket_Launcher/explosionSphere1.dts";
	soundProfile = TW_MIRVLauncherSplitSound;

	lifeTimeMS = 350;

	particleEmitter = TW_LauncherExplosionEmitter2;
	particleDensity = 40;
	particleRadius = 0.5;

	emitter[0] = TW_LauncherFlashEmitter;
	emitter[1] = TW_LauncherExplosionBlueEmitter;

	faceViewer     = true;
	explosionScale = "2 2 2";

	shakeCamera = true;
	camShakeFreq = "10.0 11.0 10.0";
	camShakeAmp = "3.0 10.0 3.0";
	camShakeDuration = 0.5;
	camShakeRadius = 100.0;

	lightStartRadius = 5;
	lightEndRadius = 15;
	lightStartColor = "0 0.5 1 1";
	lightEndColor = "0 0 0 0";

	damageRadius = 10;
	radiusDamage = 35;

	impulseRadius = 14;
	impulseForce = 1500;
};

datablock ProjectileData(TW_MirvLauncherNHProjectile)
{
	projectileShapeName = "./dts/MIRV_Launcher_canister.dts";
	directDamage        = 0;
	directDamageType = $DamageType::AE;
	radiusDamageType = $DamageType::AE;
	impactImpulse	   = 50;
	verticalImpulse	   = 500;
	explosion           = TW_MirvLauncherExplosion;
	particleEmitter     = TW_LauncherTrailFusionBlueEmitter;

	brickExplosionRadius = 3;
	brickExplosionImpact = false;          //destroy a brick if we hit it directly?
	brickExplosionForce  = 30;             
	brickExplosionMaxVolume = 30;          //max volume of bricks that we can destroy
	brickExplosionMaxVolumeFloating = 60;  //max volume of bricks that we can destroy if they aren't connected to the ground (should always be >= brickExplosionMaxVolume)

	explodeOnDeath = true;
	explodeOnPlayerImpact = false;

	sound = TW_MirvLauncherFlySound;

	muzzleVelocity      = 100;
	velInheritFactor    = 0;

	armingDelay         = 2000;
	lifetime            = 6000;
	fadeDelay           = 5990;
	bounceElasticity    = 0.15;
	bounceFriction       = 0.5;
	isBallistic         = true;
	gravityMod = 1.0;

	hasLight    = true;
	lightRadius = 5.0;
	lightColor  = "0.0 0.5 1.0";

	uiName = "";
};

function TW_MirvLauncherNHProjectile::onAdd(%db, %proj)
{
	if(%proj.directDamage == 1)
		%proj.trigger = %proj.schedule(1000, MIRVTrigger);
}

function TW_MirvLauncherNHProjectile::onExplode(%db, %proj, %pos)
{
	cancel(%proj.trigger);
	
	Parent::onExplode(%db, %proj, %pos);
}

function Projectile::MIRVTrigger(%proj)
{
	%pos = %proj.getPosition();
	%vec = vectorNormalize(%proj.getVelocity());

	%spread = 7.5;
	
	ProjectileFire(TW_MIRVClusterletNHProjectile, %pos, %vec, %spread, 8, 0, %proj.sourceObject, %proj.client, vectorLen(%proj.getVelocity()));

	%proj.FuseExplode();
}

datablock ItemData(TW_MirvLauncherNHItem)
{
	category = "Weapon";
	className = "Weapon";

	shapeFile = "./dts/MIRV_Launcher_na.dts";
	rotate = false;
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;

	uiName = "TW: Mirv Launcher";
	iconName = "./ico/MirvLauncher";
	doColorShift = true;
	colorShiftColor = "0.25 0.25 0.25 1";

	image = TW_MirvLauncherNHImage;
	canDrop = true;

	AEAmmo = 1;
	AEType = TW_MIRVAmmoItem.getID();
	AEBase = 1;

	RPM = 4;
	recoil = "Low";
	uiColor = "1 1 1";
	description = "Single shot cluster launcher, 75% velocity inheritance, 1s arming delay";

	useImpactSounds = true;
	softImpactThreshold = 2;
	softImpactSound = "AEWepImpactSoft1Sound AEWepImpactSoft2Sound AEWepImpactSoft3Sound";
	hardImpactThreshold = 8;
	hardImpactSound = "AEWepImpactHard1Sound AEWepImpactHard2Sound AEWepImpactHard3Sound";

	tribalClass = "timed";
};

datablock ShapeBaseImageData(TW_MirvLauncherNHImage)
{
	shapeFile = "./dts/MIRV_Launcher_na.dts";
	emap = true;

	mountPoint = 0;
	offset = "0 0.08 -0.04";
	eyeOffset = "0 0 0";
	rotation = eulerToMatrix( "0 0 0" );
 
	correctMuzzleVector = true;

	className = "WeaponImage";

	item = TW_MirvLauncherNHItem;
	ammo = " ";
	projectile = TW_MirvLauncherNHProjectile;
	projectileType = Projectile;

	shellExitDir        = "-1 0 0.5";
	shellExitOffset     = "0 0 0";
	shellExitVariance   = 25;	
	shellVelocity       = 5.0;
	
	melee = false;
	armReady = true;
	hideHands = false;

	doColorShift = true;
	colorShiftColor = TW_MirvLauncherNHItem.colorShiftColor;

	muzzleFlashScale = "1 1 1";
	bulletScale = "1 1 1";

	screenshakeMin = "0.3 0.3 0.3"; 
	screenshakeMax = "1 1 1";

	projectileDamage = 1;
	projectileCount = 1;
	projectileHeadshotMult = 1.0;
	projectileVelocity = 100;
	projectileTagStrength = 0;
	projectileTagRecovery = 1.0;
	projectileInheritance = 0.75;
	projectileZOffset = 0;

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
	stateEmitter[2]					= TW_RocketBackblastEmitter;
	stateEmitterTime[2]			= 0.05;
	stateEmitterNode[2]			= tailNode;
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
	stateTransitionOnAmmo[6]		= "Ready";
	stateTransitionOnNotLoaded[6] = "Empty";
	stateTransitionOnNoAmmo[6]		= "Reload";

	stateName[7]				= "Reload";
	stateTimeoutValue[7]			= 0.65;
	stateScript[7]				= "onReloadStart";
	stateTransitionOnTimeout[7]		= "ReloadMagIn";
	stateWaitForTimeout[7]			= true;
	stateSequence[7]			= "ReloadStart";

	stateName[9]				= "ReloadMagIn";
	stateTimeoutValue[9]			= 0.8;
	stateScript[9]				= "onReloadMagIn";
	stateTransitionOnTimeout[9]		= "ReloadEnd";
	stateWaitForTimeout[9]			= true;
	stateSequence[9]			= "ReloadIn";
	stateSound[9]				= TW_GrenadeLauncherReloadSound;
	
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

function TW_MirvLauncherNHImage::AEOnFire(%this,%obj,%slot)
{
  %obj.schedule(0, "aeplayThread", "2", "jump");
	%obj.stopAudio(0); 
  %obj.playAudio(0, TW_MirvLauncherFireSound);
  
	%obj.blockImageDismount = true;
	%obj.schedule(400, unBlockImageDismount);

	Parent::AEOnFire(%this, %obj, %slot);
}

function TW_MirvLauncherNHImage::onDryFire(%this, %obj, %slot)
{
	%obj.aeplayThread(2, plant);
	serverPlay3D(TW_DryFireSound, %obj.getHackPosition());
}

function TW_MirvLauncherNHImage::onReloadMagIn(%this,%obj,%slot)
{
  TW_MIRVLauncherImage::onReloadMagIn(%this,%obj,%slot);
}

function TW_MirvLauncherNHImage::onReloadEnd(%this,%obj,%slot)
{
	TW_MIRVLauncherImage::onReloadEnd(%this,%obj,%slot);

	%obj.MirvMissile = -1;
}

function TW_MirvLauncherNHImage::onReloadStart(%this,%obj,%slot)
{
	TW_MIRVLauncherImage::onReloadStart(%this,%obj,%slot);
}

function TW_MirvLauncherNHImage::onReady(%this,%obj,%slot)
{
	%obj.baadDisplayAmmo(%this);

	if(getSimTime() - %obj.reloadTime[%this.getID()] <= %this.stateTimeoutValue[0] * 1000 + 1000)
		%obj.schedule(0, setImageAmmo, %slot, 0);
}

function TW_MirvLauncherNHImage::onMount(%this,%obj,%slot)
{
	%obj.aeplayThread(2, plant);
	%this.AEMountSetup(%obj, %slot);

	parent::onMount(%this,%obj,%slot);
}

function TW_MirvLauncherNHImage::onUnMount(%this,%obj,%slot)
{
	%this.AEUnmountCleanup(%obj, %slot);

	cancel(%obj.reload2Schedule);

	parent::onUnMount(%this,%obj,%slot);	
}