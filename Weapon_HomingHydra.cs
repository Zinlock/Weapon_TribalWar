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
	emitter[1] = TW_LauncherExplosionEmitter;

	faceViewer     = true;
	explosionScale = "2 2 2";

	shakeCamera = true;
	camShakeFreq = "10.0 11.0 10.0";
	camShakeAmp = "3.0 10.0 3.0";
	camShakeDuration = 0.5;
	camShakeRadius = 100.0;

	lightStartRadius = 5;
	lightEndRadius = 15;
	lightStartColor = "1 0.5 0 1";
	lightEndColor = "0 0 0 0";

	damageRadius = 10;
	radiusDamage = 25;

	impulseRadius = 10;
	impulseForce = 1500;
};

datablock ProjectileData(TW_MIRVClusterletProjectile)
{
	projectileShapeName = "./dts/MIRV_Launcher_projectile.dts";
	directDamage        = 5;
	directDamageType = $DamageType::AE;
	radiusDamageType = $DamageType::AE;
	impactImpulse	   = 1;
	verticalImpulse	   = 1000;
	explosion           = TW_MIRVClusterletExplosion;
	particleEmitter     = TW_LauncherTrailEmitter;

	brickExplosionRadius = 3;
	brickExplosionImpact = false;          //destroy a brick if we hit it directly?
	brickExplosionForce  = 30;             
	brickExplosionMaxVolume = 30;          //max volume of bricks that we can destroy
	brickExplosionMaxVolumeFloating = 60;  //max volume of bricks that we can destroy if they aren't connected to the ground (should always be >= brickExplosionMaxVolume)

	explodeOnDeath = true;
	explodeOnPlayerImpact = true;

	sound = TW_MIRVLauncherFlySound;

	muzzleVelocity      = 100;
	velInheritFactor    = 0;

	armingDelay         = 0;
	lifetime            = 5500;
	fadeDelay           = 5490;
	bounceElasticity    = 0.5;
	bounceFriction       = 0.20;
	isBallistic         = false;
	gravityMod = 0.0;

	hasLight    = true;
	lightRadius = 5.0;
	lightColor  = "1 0.5 0.0";

	uiName = "";

	homingProjectile = true;
	homingAccuracy = 40;
	homingAccuracyClose = 400;
	homingCloseDist = 8;
	homingFarDist = 32;
	homingRadius = 0;
	homingEscapeDistance = -1;
	homingLockOnLimit = 0;
	homingCanRetry = true;
	homingAutomatic = false;
};

function TW_MIRVClusterletProjectile::onHomeTick(%db, %proj)
{
	%col = %proj.target;
	if(isObject(%col))
		HeatLockOnPrint(-1, %col, 0, 1);
}

datablock ExplosionData(TW_MIRVLauncherExplosion)
{
	explosionShape = "Add-Ons/Weapon_Rocket_Launcher/explosionSphere1.dts";
	soundProfile = TW_MIRVLauncherSplitSound;

	lifeTimeMS = 350;

	particleEmitter = TW_LauncherExplosionEmitter2;
	particleDensity = 40;
	particleRadius = 0.5;

	emitter[0] = TW_LauncherFlashEmitter;
	emitter[1] = TW_LauncherExplosionEmitter;

	faceViewer     = true;
	explosionScale = "2 2 2";

	shakeCamera = true;
	camShakeFreq = "10.0 11.0 10.0";
	camShakeAmp = "3.0 10.0 3.0";
	camShakeDuration = 0.5;
	camShakeRadius = 100.0;

	lightStartRadius = 5;
	lightEndRadius = 15;
	lightStartColor = "1 0.5 0 1";
	lightEndColor = "0 0 0 0";

	damageRadius = 10;
	radiusDamage = 35;

	impulseRadius = 14;
	impulseForce = 1500;
};

datablock ProjectileData(TW_MIRVLauncherProjectile)
{
	projectileShapeName = "./dts/MIRV_Launcher_canister.dts";
	directDamage        = 0;
	directDamageType = $DamageType::AE;
	radiusDamageType = $DamageType::AE;
	impactImpulse	   = 0;
	verticalImpulse	   = 0;
	explosion           = TW_MIRVLauncherExplosion;
	particleEmitter     = TW_LauncherTrailEmitter;

	brickExplosionRadius = 3;
	brickExplosionImpact = false;          //destroy a brick if we hit it directly?
	brickExplosionForce  = 30;             
	brickExplosionMaxVolume = 30;          //max volume of bricks that we can destroy
	brickExplosionMaxVolumeFloating = 60;  //max volume of bricks that we can destroy if they aren't connected to the ground (should always be >= brickExplosionMaxVolume)

	explodeOnDeath = true;
	explodeOnPlayerImpact = false;

	sound = TW_MIRVLauncherFlySound;

	muzzleVelocity      = 100;
	velInheritFactor    = 0;

	armingDelay         = 1990;
	lifetime            = 2000;
	fadeDelay           = 1990;
	bounceElasticity    = 0.999;
	bounceFriction       = 0.20;
	isBallistic         = true;
	gravityMod = 1.0;

	hasLight    = true;
	lightRadius = 5.0;
	lightColor  = "1 0.5 0.0";

	uiName = "";

	PrjLoop_enabled = true;
	PrjLoop_maxTicks = -1;
	PrjLoop_tickTime = 100;

	HeatRange = 512;
};

function TW_MIRVLauncherProjectile::onExplode(%db, %proj, %pos)
{
	%vec = %proj.getForwardVector();
	%ray = containerRayCast(vectorAdd(%pos, "0 0 0.1"), vectorAdd(%pos, "0 0 -3"), $TypeMasks::FxBrickObjectType | $TypeMasks::InteriorObjectType | $TypeMasks::StaticObjectType);
	
	if(!isObject(%ray))
	{
		for(%i = 0; %i < %proj.targets; %i++)
		{
			if(%i >= 6)
				break;

			%cluster = ProjectileFire(TW_MIRVClusterletProjectile, %pos, %vec, 1000, 1, 0, %proj.sourceObject, %proj.client, 40);
			%cluster.target = %proj.target[%i];
		}
	}
	
	Parent::onExplode(%db, %proj, %pos);
}

function TW_MIRVLauncherProjectile::PrjLoop_onTick(%db, %obj)
{
	%obj.targets = 0;

	initContainerRadiusSearch(%obj.getPosition(), %db.HeatRange, $TypeMasks::PlayerObjectType);
	while(isObject(%col = containerSearchNext()))
	{
		%pos = %col.getWorldBoxCenter();

		if(%col.getType() & $TypeMasks::PlayerObjectType)
			%pos = %col.getHackPosition();
		
		%ray = containerRayCast(%obj.getPosition(), %pos, $TypeMasks::FxBrickObjectType | $TypeMasks::InteriorObjectType | $TypeMasks::StaticObjectType, %obj, %col);
		
		if(!isObject(%ray) && %col.getDamagePercent() < 1.0 && minigameCanDamage(%obj, %col) == 1 && !minigameIsFriendly(%obj, %col))
		{
			HeatLockOnPrint(-1, %col, 0, 0);
			%obj.target[%obj.targets] = %col;
			%obj.targets++;
		}
	}
}

datablock ItemData(TW_MIRVLauncherItem)
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

	uiName = "TW: Homing Hydra";
	iconName = "./ico/MirvLauncher";
	doColorShift = true;
	colorShiftColor = "0.75 0.25 0.25 1";

	image = TW_MIRVLauncherImage;
	canDrop = true;

	AEAmmo = 4;
	AEType = TW_MIRVAmmoItem.getID();
	AEBase = 1;

	RPM = 4;
	recoil = "Low";
	uiColor = "1 1 1";
	description = "Holy Shit";

	useImpactSounds = true;
	softImpactThreshold = 2;
	softImpactSound = "AEWepImpactSoft1Sound AEWepImpactSoft2Sound AEWepImpactSoft3Sound";
	hardImpactThreshold = 8;
	hardImpactSound = "AEWepImpactHard1Sound AEWepImpactHard2Sound AEWepImpactHard3Sound";

	tribalClass = "timed";
};

datablock ShapeBaseImageData(TW_MIRVLauncherImage)
{
	shapeFile = "./dts/MIRV_Launcher_na.dts";
	emap = true;

	mountPoint = 0;
	offset = "0 0.08 -0.04";
	eyeOffset = "0 0 0";
	rotation = eulerToMatrix( "0 0 0" );
 
	correctMuzzleVector = true;

	className = "WeaponImage";

	item = TW_MIRVLauncherItem;
	ammo = " ";
	projectile = TW_MIRVLauncherProjectile;
	projectileType = Projectile;

	shellExitDir        = "-1 0 0.5";
	shellExitOffset     = "0 0 0";
	shellExitVariance   = 25;	
	shellVelocity       = 5.0;
	
	melee = false;
	armReady = true;
	hideHands = false;

	doColorShift = true;
	colorShiftColor = TW_MIRVLauncherItem.colorShiftColor;

	muzzleFlashScale = "1 1 1";
	bulletScale = "1 1 1";

	screenshakeMin = "0.3 0.3 0.3"; 
	screenshakeMax = "1 1 1";

	projectileDamage = 20;
	projectileCount = 2;
	projectileHeadshotMult = 1.0;
	projectileVelocity = 65;
	projectileTagStrength = 0;
	projectileTagRecovery = 1.0;
	projectileInheritance = 0.75;
	projectileZOffset = 10;

	alwaysSpawnProjectile = true;
	projectileVehicleDamageMult = 0.5;

	recoilHeight = 0;
	recoilWidth = 0;
	recoilWidthMax = 0;
	recoilHeightMax = 20;

	spreadBurst = 1;
	spreadReset = 0;
	spreadBase = 2000;
	spreadMin = 2000;
	spreadMax = 2000;

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
	stateTimeoutValue[3]             	= 0.65;
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

function TW_MIRVLauncherImage::AEOnFire(%this,%obj,%slot)
{
  %obj.schedule(0, "aeplayThread", "2", "jump");
	%obj.stopAudio(0); 
  %obj.playAudio(0, TW_MIRVLauncherFireSound);
  
	%obj.blockImageDismount = true;
	%obj.schedule(400, unBlockImageDismount);

	Parent::AEOnFire(%this, %obj, %slot);
}

function TW_MIRVLauncherImage::onDryFire(%this, %obj, %slot)
{
	%obj.aeplayThread(2, plant);
	serverPlay3D(TW_DryFireSound, %obj.getHackPosition());
}

function TW_MIRVLauncherImage::onReloadMagIn(%this,%obj,%slot)
{
  %obj.aeplayThread("3", "plant");
}

function TW_MIRVLauncherImage::onReloadEnd(%this,%obj,%slot)
{
  %obj.aeplayThread("3", "shiftRight");
	%obj.playAudio(1, TW_MortarUnholsterSound);
	Parent::AEMagReloadAll(%this, %obj, %slot);
}

function TW_MIRVLauncherImage::onReloadStart(%this,%obj,%slot)
{
  %obj.schedule(150, "aeplayThread", "2", "shiftLeft");
  %obj.schedule(150, "aeplayThread", "3", "plant");
	%obj.reload2Schedule = %obj.schedule(150,playAudio, 1, TW_MagnumCycleSound);
}

function TW_MIRVLauncherImage::onReady(%this,%obj,%slot)
{
	%obj.baadDisplayAmmo(%this);

	if(getSimTime() - %obj.reloadTime[%this.getID()] <= %this.stateTimeoutValue[0] * 1000 + 1000)
		%obj.schedule(0, setImageAmmo, %slot, 0);
}

function TW_MIRVLauncherImage::onMount(%this,%obj,%slot)
{
	%obj.aeplayThread(2, plant);
	%this.AEMountSetup(%obj, %slot);

	parent::onMount(%this,%obj,%slot);
}

function TW_MIRVLauncherImage::onUnMount(%this,%obj,%slot)
{
	%this.AEUnmountCleanup(%obj, %slot);

	cancel(%obj.reload2Schedule);

	parent::onUnMount(%this,%obj,%slot);	
}