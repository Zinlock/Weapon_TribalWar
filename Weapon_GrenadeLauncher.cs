datablock AudioProfile(TW_GrenadeLauncherFireSound)
{
   filename    = "./wav/Grenade_Launcher_fire.wav";
   description = HeavyClose3D;
   preload = true;
};

datablock ExplosionData(TW_GrenadeLauncherExplosion)
{
	explosionShape = "Add-Ons/Weapon_Rocket_Launcher/explosionSphere1.dts";
	soundProfile = TW_ThumperExplodeSound;

	lifeTimeMS = 350;

	particleEmitter = TW_LauncherExplosionEmitter2;
	particleDensity = 40;
	particleRadius = 0.5;

	emitter[0] = TW_LauncherFlashEmitter;
	emitter[1] = TW_LauncherExplosionEmitter;

	faceViewer     = true;
	explosionScale = "1 1 1";

	shakeCamera = true;
	camShakeFreq = "10.0 11.0 10.0";
	camShakeAmp = "3.0 10.0 3.0";
	camShakeDuration = 0.5;
	camShakeRadius = 100.0;

	lightStartRadius = 5;
	lightEndRadius = 15;
	lightStartColor = "1 0.5 0 1";
	lightEndColor = "0 0 0 0";

	damageRadius = 13;
	radiusDamage = 30;

	impulseRadius = 14;
	impulseForce = 1500;
};

datablock ProjectileData(TW_GrenadeLauncherProjectile)
{
	projectileShapeName = "./dts/grenade_projectile.dts";
	directDamage        = 50;
	directDamageType = $DamageType::AE;
	radiusDamageType = $DamageType::AE;
	impactImpulse	   = 1;
	verticalImpulse	   = 1000;
	explosion           = TW_GrenadeLauncherExplosion;
	particleEmitter     = TW_LauncherTrailEmitter;

	brickExplosionRadius = 3;
	brickExplosionImpact = false;          //destroy a brick if we hit it directly?
	brickExplosionForce  = 30;             
	brickExplosionMaxVolume = 30;          //max volume of bricks that we can destroy
	brickExplosionMaxVolumeFloating = 60;  //max volume of bricks that we can destroy if they aren't connected to the ground (should always be >= brickExplosionMaxVolume)

	explodeOnDeath = true;
	explodeOnPlayerImpact = true;

	sound = TW_GrenadeFlySound;

	muzzleVelocity      = 100;
	velInheritFactor    = 0;

	armingDelay         = 750;
	lifetime            = 3500;
	fadeDelay           = 3490;
	bounceElasticity    = 0.15;
	bounceFriction       = 0.5;
	isBallistic         = true;
	gravityMod = 1.0;

	hasLight    = true;
	lightRadius = 5.0;
	lightColor  = "1 0.5 0.0";

	uiName = "";
};

function TW_GrenadeLauncherProjectile::onCollision(%this, %obj, %col, %fade, %pos, %normal, %velocity)
{
	AETrailedProjectile::onCollision(%this, %obj, %col, %fade, %pos, %normal, %velocity);
}

function TW_GrenadeLauncherProjectile::onExplode(%this, %obj, %col, %fade, %pos, %normal, %velocity)
{
	AETrailedProjectile::onExplode(%this, %obj, %col, %fade, %pos, %normal, %velocity);
}

function TW_GrenadeLauncherProjectile::Damage(%this, %obj, %col, %fade, %pos, %normal)
{
	AETrailedProjectile::Damage(%this, %obj, %col, %fade, %pos, %normal);
}

datablock ItemData(TW_GrenadeLauncherItem)
{
	category = "Weapon";
	className = "Weapon";

	shapeFile = "./dts/Grenade_Launcher_na.dts";
	rotate = false;
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;

	uiName = "TW: Grenade Launcher";
	iconName = "./ico/Grenade_Launcher";
	doColorShift = true;
	colorShiftColor = "0.6 0.6 0.6 1";

	image = TW_GrenadeLauncherImage;
	canDrop = true;
	
	AEAmmo = 3;
	AEType = TW_GrenadeAmmoItem.getID(); 
	AEBase = 1;

  RPM = 60;
  Recoil = "Medium";
	uiColor = "1 1 1";
  description = "Triple shot grenade launcher, 100% velocity inheritance, 0.75s arming delay";

	useImpactSounds = true;
	softImpactThreshold = 2;
	softImpactSound = "AEWepImpactSoft1Sound AEWepImpactSoft2Sound AEWepImpactSoft3Sound";
	hardImpactThreshold = 8;
	hardImpactSound = "AEWepImpactHard1Sound AEWepImpactHard2Sound AEWepImpactHard3Sound";
};

datablock ShapeBaseImageData(TW_GrenadeLauncherImage)
{
	shapeFile = "./dts/Grenade_Launcher_na.dts";
	emap = true;

	mountPoint = 0;
	offset = "0 0 0";
	eyeOffset = 0;
	rotation = eulerToMatrix( "0 0 0" );

	correctMuzzleVector = true;

	className = "WeaponImage";

	item = TW_GrenadeLauncherItem;
	ammo = " ";
	projectile = TW_GrenadeLauncherProjectile;
	projectileType = Projectile;

	casing = "";
	shellExitDir        = "1 0 0.5";
	shellExitOffset     = "0 0 0";
	shellExitVariance   = 25;	
	shellVelocity       = 5.0;

	melee = false;
	armReady = true;
	doColorShift = true;
	colorShiftColor = TW_GrenadeLauncherItem.colorShiftColor;
	muzzleFlashScale = "1 1 1";

	projectileDamage = 20;
	projectileCount = 1;
	projectileHeadshotMult = 1.0;
	projectileVelocity = 100;
	projectileTagStrength = 0.0;
	projectileTagRecovery = 1.0;
	projectileInheritance = 1.0;

	recoilHeight = 0;
	recoilWidth = 0;
	recoilWidthMax = 0;
	recoilHeightMax = 20;

	spreadBurst = 1;
	spreadBase = 0;
	spreadReset = 0;
	spreadMin = 0;
	spreadMax = 0;

	screenshakeMin = "0.25 0.25 0.25"; 
	screenshakeMax = "0.5 0.5 0.5";

	stateName[0]                     	= "Activate";
	stateTimeoutValue[0]             	= 0.1;
	stateTransitionOnTimeout[0]       	= "LoadCheckA";
	stateSequence[0]			= "root";
	stateSound[0] = TW_LauncherUnholsterSound;

	stateName[1]                    	= "Ready";
	stateScript[1]				= "onReady";
	stateTransitionOnTriggerDown[1] 	= "preFire";
	stateTransitionOnNotLoaded[1]     = "Empty";
	stateTransitionOnNoAmmo[1]		= "ReloadStart";
	stateAllowImageChange[1]		= true;

	stateName[2]                       = "preFire";
	stateTransitionOnTimeout[2]        = "Fire";
	stateScript[2]                     = "AEOnFire";
	stateEmitter[2]					= AEBaseShotgunFlashEmitter;
	stateEmitterTime[2]				= 0.05;
	stateEmitterNode[2]				= "muzzlePoint";
	stateFire[2]                       = true;

	stateName[3]                    = "Fire";
	stateTransitionOnTimeout[3]     = "SemiAutoCheck";
	stateTimeoutValue[3]            	= 0.5;
	stateEmitter[3]					= AEBaseSmokeBigEmitter;
	stateEmitterTime[3]				= 0.05;
	stateEmitterNode[3]				= "muzzlePoint";
	stateAllowImageChange[3]        = false;
	stateSequence[3]                = "Fire";
	stateWaitForTimeout[3]			= true;
	
	stateName[4]                    	= "Pump";
	stateTimeoutValue[4]            	= 0.35;
	stateScript[4]                  	= "onPump";
	stateTransitionOnTimeout[4]     	= "FireLoadCheckA";
	stateAllowImageChange[4]        	= false;
	stateSequence[4]			= "reload";
	stateWaitForTimeout[4]		  	= true;
	stateEjectShell[4]                = false;
	
	stateName[5]				= "FireLoadCheckA";
	stateScript[5]				= "AEMagLoadCheck";
	stateTimeoutValue[5]			= 0.05;
	stateTransitionOnTimeout[5]		= "FireLoadCheckB";
	
	stateName[6]				= "FireLoadCheckB";
	stateTransitionOnAmmo[6]		= "Ready";
	stateTransitionOnNotLoaded[6]  = "Ready";
	stateTransitionOnNoAmmo[6]		= "ReloadStart";	
	
	stateName[7]				= "LoadCheckA";
	stateScript[7]				= "AELoadCheck";
	stateTimeoutValue[7]			= 0.1;
	stateTransitionOnTimeout[7]		= "LoadCheckB";
						
	stateName[8]				= "LoadCheckB";
	stateTransitionOnAmmo[8]		= "Ready";
	stateTransitionOnNotLoaded[8] = "Empty";
	stateTransitionOnNoAmmo[8]		= "ReloadStart";	

	stateName[9]			  	= "ReloadStart";
	stateScript[9]				= "onReloadStart";
	stateTransitionOnTimeout[9]	  	= "Reload";
	stateTimeoutValue[9]		  	= 0.1;
	stateWaitForTimeout[9]		  	= false;
	stateSequence[9]			= "ReloadStart";

	stateName[14]				= "Reload";
	stateTransitionOnTimeout[14]     	= "Reloaded";
	stateWaitForTimeout[14]			= false;
	stateTimeoutValue[14]			= 0.6;
	stateSequence[14]			= "Reload";
	stateScript[14]				= "LoadEffect";
	
	stateName[15]				= "Reloaded";
	stateTransitionOnTimeout[15]     	= "CheckAmmoA";
	stateWaitForTimeout[15]			= false;
	
	stateName[16]				= "CheckAmmoA";
	stateTransitionOnTriggerDown[16]	= "AnotherAmmoCheck";
	stateScript[16]				= "AEShotgunLoadCheck";
	stateTransitionOnTimeout[16]		= "CheckAmmoB";	
	
	stateName[17]				= "CheckAmmoB";
	stateTransitionOnTriggerDown[17]	= "AnotherAmmoCheck";
	stateTransitionOnNotLoaded[17]  = "EndReload";
	stateTransitionOnAmmo[17]		= "EndReload";
	stateTransitionOnNoAmmo[17]		= "Reload";
	
	stateName[18]			  	= "EndReload";
	stateTransitionOnTriggerDown[18]	= "AnotherAmmoCheck";
	stateScript[18]				= "onEndReload";
	stateTimeoutValue[18]		  	= 0.2;
	stateSequence[18]			= "ReloadEnd";
	stateTransitionOnTimeout[18]	  	= "Ready";
	stateWaitForTimeout[18]		  	= false;

	stateName[19]          = "Empty";
	stateTransitionOnTriggerDown[19]  = "Dryfire";
	stateTransitionOnLoaded[19] = "ReloadStart";
	stateScript[19]        = "AEOnEmpty";

	stateName[20]           = "Dryfire";
	stateTransitionOnTriggerUp[20] = "Empty";
	stateWaitForTimeout[20]    = false;
	stateScript[20]      = "onDryFire";
	
	stateName[21]           = "AnotherAmmoCheck";
	stateTransitionOnTimeout[21]	  	= "preFire";
	stateScript[21]				= "AELoadCheck";
	
	stateName[22]           = "SemiAutoCheck";
	stateTransitionOnTriggerUp[22]	  	= "Pump";
};

function TW_GrenadeLauncherImage::AEOnFire(%this,%obj,%slot)
{
	%obj.aeplayThread(2, activate);
	%obj.blockImageDismount = true;
	%obj.schedule(500, unBlockImageDismount);

	cancel(%obj.reloadSoundSchedule);
	cancel(%obj.insertshellSchedule);
	%obj.stopAudio(0); 
	%obj.playAudio(0, TW_GrenadeLauncherFireSound);

	Parent::AEOnFire(%this, %obj, %slot);
}

function TW_GrenadeLauncherImage::onReloadStart(%this, %obj, %slot)
{
	%obj.aeplayThread(2, shiftleft);
}

function TW_GrenadeLauncherImage::onReloadEnd(%this,%obj,%slot)
{
	Parent::AEMagReloadAll(%this, %obj, %slot);
}

function TW_GrenadeLauncherImage::onDryFire(%this, %obj, %slot)
{
	%obj.aeplayThread(2, plant);
	serverPlay3D(AEDryFireSound, %obj.getHackPosition());
}

function TW_GrenadeLauncherImage::onReady(%this,%obj,%slot)
{
	if(getSimTime() - %obj.reloadTime[%this.getID()] <= %this.stateTimeoutValue[0] * 1000 + 1000)
		%obj.schedule(0, setImageAmmo, %slot, 0);
	
	%obj.baadDisplayAmmo(%this);
	%this.AEPreLoadAmmoReserveCheck(%obj, %slot);
	%this.AEPreAmmoCheck(%obj, %slot);
}

function TW_GrenadeLauncherImage::onMount(%this,%obj,%slot)
{
	%this.AEMountSetup(%obj, %slot);
	parent::onMount(%this,%obj,%slot);
}

function TW_GrenadeLauncherImage::onUnMount(%this, %obj, %slot)
{	
	%this.AEUnmountCleanup(%obj, %slot);

	cancel(%obj.reloadSoundSchedule);
	cancel(%obj.pumpSoundSchedule);
	cancel(%obj.insertshellSchedule);
	parent::onUnMount(%this,%obj,%slot);	
}

function TW_GrenadeLauncherImage::LoadEffect(%this,%obj,%slot)
{
	%obj.playAudio(1, TW_GrenadeCycleSound);
	%obj.aeplayThread("3", "plant");
	%obj.aeplayThread("2", "shiftright");
	%obj.insertshellSchedule = %this.schedule(200,AEShotgunLoadOne,%obj,%slot);
}

function TW_GrenadeLauncherImage::onPump(%this,%obj,%slot)
{
	%obj.aeplayThread("3", "plant");
	%obj.aeplayThread("2", "shiftright");
	%obj.playAudio(1, TW_GrenadeCycleSound);
}