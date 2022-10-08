datablock AudioProfile(TW_BurstProxyFireSound)
{
   filename    = "./wav/burst_proxy_fire.wav";
   description = HeavyClose3D;
   preload = true;
};

datablock AudioProfile(TW_BurstProxyFly1Sound)
{
   filename    = "./wav/burst_proxy_flyA.wav";
   description = AudioDefaultLooping3D;
   preload = true;
};

datablock AudioProfile(TW_BurstProxyFly2Sound)
{
   filename    = "./wav/burst_proxy_flyB.wav";
   description = AudioDefaultLooping3D;
   preload = true;
};

datablock ExplosionData(TW_BurstProxyExplosion)
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

	damageRadius = 8;
	radiusDamage = 25;
 
	impulseRadius = 10;
	impulseForce = 600;
};

datablock ProjectileData(TW_BurstProxyProjectile)
{
	projectileShapeName = "./dts/proxy_projectile.dts";
	directDamage        = 50;
	directDamageType = $DamageType::AE;
	radiusDamageType = $DamageType::AE;
	impactImpulse	   = 1;
	verticalImpulse	   = 1000;
	explosion           = TW_BurstProxyExplosion;
	particleEmitter     = TW_LauncherTrailEmitter;

	brickExplosionRadius = 3;
	brickExplosionImpact = false;          //destroy a brick if we hit it directly?
	brickExplosionForce  = 30;             
	brickExplosionMaxVolume = 30;          //max volume of bricks that we can destroy
	brickExplosionMaxVolumeFloating = 60;  //max volume of bricks that we can destroy if they aren't connected to the ground (should always be >= brickExplosionMaxVolume)

	explodeOnDeath = true;
	explodeOnPlayerImpact = true;

	sound = TW_BurstProxyFly1Sound;

	muzzleVelocity      = 100;
	velInheritFactor    = 0;

	armingDelay         = 0;
	lifetime            = 3500;
	fadeDelay           = 3490;
	bounceElasticity    = 0.5;
	bounceFriction       = 0.20;
	isBallistic         = true;
	gravityMod = 0.2;

	hasLight    = true;
	lightRadius = 5.0;
	lightColor  = "1 0.5 0.0";
	
	PrjLoop_enabled = true;
	PrjLoop_maxTicks = -1;
	PrjLoop_tickTime = 45;

	uiName = "";
};

datablock ProjectileData(TW_BurstProxyProjectile2 : TW_BurstProxyProjectile)
{
	sound = TW_BurstProxyFly2Sound;
};

function TW_BurstProxyProjectile::onCollision(%this, %obj, %col, %fade, %pos, %normal, %velocity)
{
	AETrailedProjectile::onCollision(%this, %obj, %col, %fade, %pos, %normal, %velocity);
}

function TW_BurstProxyProjectile::onExplode(%this, %obj, %col, %fade, %pos, %normal, %velocity)
{
	AETrailedProjectile::onExplode(%this, %obj, %col, %fade, %pos, %normal, %velocity);
}

function TW_BurstProxyProjectile::Damage(%this, %obj, %col, %fade, %pos, %normal)
{
	AETrailedProjectile::Damage(%this, %obj, %col, %fade, %pos, %normal);
}

function TW_BurstProxyProjectile::PrjLoop_onTick(%this, %obj)
{
	initContainerRadiusSearch(%obj.getPosition(), %this.explosion.damageRadius / 2, $TypeMasks::PlayerObjectType); // | $TypeMasks::VehicleObjectType);
	while(isObject(%col = containerSearchNext()))
	{
		if(%col.getDamagePercent() < 1.0 && minigameCanDamage(%obj, %col) == 1 && !minigameIsFriendly(%obj, %col))
		{
			%obj.explode();
			return;
		}
	}
}

function TW_BurstProxyProjectile2::onCollision(%this, %obj, %col, %fade, %pos, %normal, %velocity)
{
	AETrailedProjectile::onCollision(%this, %obj, %col, %fade, %pos, %normal, %velocity);
}

function TW_BurstProxyProjectile2::onExplode(%this, %obj, %col, %fade, %pos, %normal, %velocity)
{
	AETrailedProjectile::onExplode(%this, %obj, %col, %fade, %pos, %normal, %velocity);
}

function TW_BurstProxyProjectile2::Damage(%this, %obj, %col, %fade, %pos, %normal)
{
	AETrailedProjectile::Damage(%this, %obj, %col, %fade, %pos, %normal);
}

function TW_BurstProxyProjectile2::PrjLoop_onTick(%this, %obj)
{
	TW_BurstProxyProjectile::PrjLoop_onTick(%this, %obj);
}

datablock ItemData(TW_BurstProxyItem)
{
	category = "Weapon";
	className = "Weapon";

	shapeFile = "./dts/burst_proxy_na.dts";
	rotate = false;
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;

	uiName = "TW: Burst Proxy";
	iconName = "./ico/Burst_Proxy";
	doColorShift = true;
	colorShiftColor = "0.15 0.15 0.15 1";

	image = TW_BurstProxyImage;
	canDrop = true;
	
	AEAmmo = 6;
	AEType = TW_GrenadeAmmoItem.getID(); 
	AEBase = 1;

  RPM = 60;
  Recoil = "Medium";
	uiColor = "1 1 1";
  description = "Burst action flak gun, 50% velocity inheritance, triggered by vehicles";

	useImpactSounds = true;
	softImpactThreshold = 2;
	softImpactSound = "AEWepImpactSoft1Sound AEWepImpactSoft2Sound AEWepImpactSoft3Sound";
	hardImpactThreshold = 8;
	hardImpactSound = "AEWepImpactHard1Sound AEWepImpactHard2Sound AEWepImpactHard3Sound";

	tribalClass = "specialty";
};

datablock ShapeBaseImageData(TW_BurstProxyImage)
{
	shapeFile = "./dts/burst_proxy_na.dts";
	emap = true;

	mountPoint = 0;
	offset = "0 0 0";
	eyeOffset = 0;
	rotation = eulerToMatrix( "0 0 0" );

	correctMuzzleVector = true;

	className = "WeaponImage";

	item = TW_BurstProxyItem;
	ammo = " ";
	projectile = TW_BurstProxyProjectile;
	projectileType = Projectile;

	casing = "";
	shellExitDir        = "1 0 0.5";
	shellExitOffset     = "0 0 0";
	shellExitVariance   = 25;	
	shellVelocity       = 5.0;

	melee = false;
	armReady = true;
	doColorShift = true;
	colorShiftColor = TW_BurstProxyItem.colorShiftColor;
	muzzleFlashScale = "1 1 1";

	projectileDamage = 0;
	projectileCount = 1;
	projectileHeadshotMult = 1.0;
	projectileVelocity = 160;
	projectileTagStrength = 0.0;
	projectileTagRecovery = 1.0;
	projectileInheritance = 0.5;

	recoilHeight = 0;
	recoilWidth = 0;
	recoilWidthMax = 0;
	recoilHeightMax = 20;

	spreadBurst = 1;
	spreadBase = 100;
	spreadReset = 0;
	spreadMin = 100;
	spreadMax = 100;

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
	stateEmitter[2]					= TW_RocketBackblastEmitter;
	stateEmitterTime[2]			= 0.09;
	stateEmitterNode[2]			= tailNode;
	stateFire[2]                       = true;
	stateEjectShell[2]                       = true;

	stateName[3]                    = "Fire";
	stateTransitionOnTimeout[3]     = "FireLoadCheckA";
	stateEmitter[3]					= AEBaseSmokeEmitter;
	stateEmitterTime[3]				= 0.05;
	stateEmitterNode[3]				= "muzzlePoint";
	stateAllowImageChange[3]        = false;
	stateSequence[3]                = "Fire";
	stateWaitForTimeout[3]			= true;
	
	stateName[4]				= "FireLoadCheckA";
	stateScript[4]				= "AEMagLoadCheck";
	stateTimeoutValue[4]			= 0.135;
	stateTransitionOnTimeout[4]		= "FireLoadCheckB";
	
	stateName[5]				= "FireLoadCheckB";
	stateTransitionOnAmmo[5]		= "preFire2";
	stateTransitionOnNoAmmo[5]		= "Reload";
	stateTransitionOnNotLoaded[5]  = "Ready";
// fire 2
	stateName[6]                       = "preFire2";
	stateTransitionOnTimeout[6]        = "Fire2";
	stateScript[6]                     = "AEOnFire2";
	stateEmitter[6]					= TW_RocketBackblastEmitter;
	stateEmitterTime[6]			= 0.09;
	stateEmitterNode[6]			= tailNode;
	stateFire[6]                       = true;
	stateEjectShell[6]                       = true;

	stateName[7]                    = "Fire2";
	stateTransitionOnTimeout[7]     = "FireLoadCheckA2";
	stateEmitter[7]					= AEBaseSmokeEmitter;
	stateEmitterTime[7]				= 0.05;
	stateEmitterNode[7]				= "muzzlePoint";
	stateAllowImageChange[7]        = false;
	stateSequence[7]                = "Fire";
	stateWaitForTimeout[7]			= true;
	
	stateName[8]				= "FireLoadCheckA2";
	stateScript[8]				= "AEMagLoadCheck";
	stateTimeoutValue[8]			= 0.075;
	stateTransitionOnTimeout[8]		= "FireLoadCheckB2";
	
	stateName[9]				= "FireLoadCheckB2";
	stateTransitionOnAmmo[9]		= "preFire3";
	stateTransitionOnNoAmmo[9]		= "Reload";
	stateTransitionOnNotLoaded[9]  = "Ready";
// fire 3
	stateName[10]                       = "preFire3";
	stateTransitionOnTimeout[10]        = "Fire3";
	stateScript[10]                     = "AEOnFire";
	stateEmitter[10]					= TW_RocketBackblastEmitter;
	stateEmitterTime[10]			= 0.09;
	stateEmitterNode[10]			= tailNode;
	stateFire[10]                       = true;
	stateEjectShell[10]                       = true;

	stateName[11]                    = "Fire3";
	stateTransitionOnTimeout[11]     = "FireLoadCheckA3";
	stateEmitter[11]					= AEBaseSmokeEmitter;
	stateEmitterTime[11]				= 0.05;
	stateEmitterNode[11]				= "muzzlePoint";
	stateAllowImageChange[11]        = false;
	stateSequence[11]                = "Fire";
	stateWaitForTimeout[11]			= true;
	
	stateName[12]				= "FireLoadCheckA3";
	stateScript[12]				= "AEMagLoadCheck";
	stateTimeoutValue[12]			= 0.3;
	stateTransitionOnTimeout[12]		= "FireLoadCheckB3";
	
	stateName[13]				= "FireLoadCheckB3";
	stateTransitionOnAmmo[13]		= "Ready";
	stateTransitionOnNoAmmo[13]		= "Reload";	
	stateTransitionOnNotLoaded[13]  = "Ready";

	stateName[14]				= "LoadCheckA";
	stateScript[14]				= "AELoadCheck";
	stateTimeoutValue[14]			= 0.1;
	stateTransitionOnTimeout[14]		= "LoadCheckB";
						
	stateName[15]				= "LoadCheckB";
	stateTransitionOnAmmo[15]		= "Ready";
	stateTransitionOnNotLoaded[15] = "Empty";
	stateTransitionOnNoAmmo[15]		= "ReloadStart";	

	stateName[16]			  	= "ReloadStart";
	stateScript[16]				= "onReloadStart";
	stateTransitionOnTimeout[16]	  	= "Reload";
	stateTimeoutValue[16]		  	= 0.1;
	stateWaitForTimeout[16]		  	= false;
	stateSequence[16]			= "ReloadStart";

	stateName[23]				= "Reload";
	stateTransitionOnTimeout[23]     	= "Reloaded";
	stateWaitForTimeout[23]			= false;
	stateTimeoutValue[23]			= 0.35;
	stateSequence[23]			= "Reload";
	stateScript[23]				= "LoadEffect";
	
	stateName[24]				= "Reloaded";
	stateTransitionOnTimeout[24]     	= "CheckAmmoA";
	stateWaitForTimeout[24]			= false;
	
	stateName[25]				= "CheckAmmoA";
	stateTransitionOnTriggerDown[25]	= "AnotherAmmoCheck";
	stateScript[25]				= "AEShotgunLoadCheck";
	stateTransitionOnTimeout[25]		= "CheckAmmoB";	
	
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
	stateTransitionOnTriggerUp[22]	  	= "Ready";
};

function TW_BurstProxyImage::AEOnFire(%this,%obj,%slot)
{
	%obj.aeplayThread(2, shiftLeft);
	%obj.aeplayThread(3, shiftRight);
	%obj.blockImageDismount = true;
	%obj.schedule(500, unBlockImageDismount);

	cancel(%obj.reloadSoundSchedule);
	cancel(%obj.insertshellSchedule);

	%obj.stopAudio(0); 
	%obj.playAudio(0, TW_BurstProxyFireSound);

	Parent::AEOnFire(%this, %obj, %slot);
}

function TW_BurstProxyImage::AEOnFire2(%this,%obj,%slot)
{
	%this.projectile = TW_BurstProxyProjectile2;
	%this.AEOnFire(%obj, %slot);
	%this.projectile = TW_BurstProxyProjectile;
}
function TW_BurstProxyImage::onReloadStart(%this, %obj, %slot)
{
	%obj.aeplayThread(2, shiftleft);
}

function TW_BurstProxyImage::onReloadEnd(%this,%obj,%slot)
{
	Parent::AEMagReloadAll(%this, %obj, %slot);
}

function TW_BurstProxyImage::onDryFire(%this, %obj, %slot)
{
	%obj.aeplayThread(2, plant);
	serverPlay3D(AEDryFireSound, %obj.getHackPosition());
}

function TW_BurstProxyImage::onReady(%this,%obj,%slot)
{
	if(getSimTime() - %obj.reloadTime[%this.getID()] <= %this.stateTimeoutValue[0] * 1000 + 1000)
		%obj.schedule(0, setImageAmmo, %slot, 0);
	
	%obj.baadDisplayAmmo(%this);
	%this.AEPreLoadAmmoReserveCheck(%obj, %slot);
	%this.AEPreAmmoCheck(%obj, %slot);
}

function TW_BurstProxyImage::onMount(%this,%obj,%slot)
{
	%this.AEMountSetup(%obj, %slot);
	parent::onMount(%this,%obj,%slot);
}

function TW_BurstProxyImage::onUnMount(%this, %obj, %slot)
{	
	%this.AEUnmountCleanup(%obj, %slot);

	cancel(%obj.reloadSoundSchedule);
	cancel(%obj.pumpSoundSchedule);
	cancel(%obj.insertshellSchedule);
	parent::onUnMount(%this,%obj,%slot);	
}

function TW_BurstProxyImage::LoadEffect(%this,%obj,%slot)
{
	%obj.stopAudio(1);
	%obj.playAudio(1, TW_GrenadeCycleSound);
	%obj.aeplayThread("3", "plant");
	%obj.aeplayThread("2", "shiftright");
	%obj.insertshellSchedule = %this.schedule(200,AEShotgunLoadOne,%obj,%slot);
}