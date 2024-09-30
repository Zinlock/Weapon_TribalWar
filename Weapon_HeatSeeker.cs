datablock AudioProfile(TW_HeatSeekerFireSound)
{
   filename    = "./wav/Heat_Seeker_fire.wav";
   description = HeavyClose3D;
   preload = true;
};

datablock AudioProfile(TW_HeatSeekerExplodeSound)
{
   filename    = "./wav/Heat_Seeker_explode.wav";
   description = ExplosionFarLoud3D;
   preload = true;
};

datablock AudioProfile(TW_HeatSeekerFlySound)
{
   filename    = "./wav/Heat_Seeker_Fly.wav";
   description = AudioDefaultLooping3D;
   preload = true;
};

datablock AudioProfile(TW_HeatSeekerLockingSound)
{
   filename    = "./wav/Heat_Seeker_Locking.wav";
   description = AudioDefault3D;
   preload = true;
};

datablock AudioProfile(TW_HeatSeekerLockedSound)
{
   filename    = "./wav/Heat_Seeker_Locked.wav";
   description = AudioDefault3D;
   preload = true;
};

datablock AudioProfile(TW_HeatSeekerWarnLockingSound)
{
   filename    = "./wav/Heat_Seeker_Warn_Locking.wav";
   description = AudioDefault3D;
   preload = true;
};

datablock AudioProfile(TW_HeatSeekerWarnLockedSound)
{
   filename    = "./wav/Heat_Seeker_Warn_Locked.wav";
   description = AudioDefault3D;
   preload = true;
};

datablock AudioProfile(TW_MortarUnholsterSound)
{
   filename    = "./wav/fusion_mortar_unholster.wav";
   description = AudioClosest3D;
   preload = true;
};

datablock ExplosionData(TW_HeatSeekerExplosion)
{
	explosionShape = "Add-Ons/Weapon_Rocket_Launcher/explosionSphere1.dts";
	soundProfile = TW_HeatSeekerExplodeSound;

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

datablock ProjectileData(TW_HeatSeekerProjectile)
{
	projectileShapeName = "./dts/heat_seeker_projectile.dts";
	directDamage        = 10;
	directDamageType = $DamageType::AE;
	radiusDamageType = $DamageType::AE;
	impactImpulse	   = 1;
	verticalImpulse	   = 1000;
	explosion           = TW_HeatSeekerExplosion;
	particleEmitter     = TW_LauncherTrailEmitter;

	brickExplosionRadius = 3;
	brickExplosionImpact = false;          //destroy a brick if we hit it directly?
	brickExplosionForce  = 30;             
	brickExplosionMaxVolume = 30;          //max volume of bricks that we can destroy
	brickExplosionMaxVolumeFloating = 60;  //max volume of bricks that we can destroy if they aren't connected to the ground (should always be >= brickExplosionMaxVolume)

	explodeOnDeath = true;
	explodeOnPlayerImpact = true;

	sound = TW_HeatSeekerFlySound;

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

	vehicleDamageMult = 2.25;

	flaresCanBait = true;
	flaresCanDestroy = true;

	homingProjectile = true;
	homingAccuracy = 80;
	homingAccuracyClose = 200;
	homingCloseDist = 4;
	homingFarDist = 64;
	homingRadius = 0;
	homingEscapeDistance = -1;
	homingLockOnLimit = 0;
	homingCanRetry = true;
	homingAutomatic = false;
	homingTickTime = 200;
};

function TW_HeatSeekerProjectile::damage(%this,%obj,%col,%fade,%pos,%normal)
{
	%damageType = $DamageType::Direct;
	if(%this.DirectDamageType)
		%damageType = %this.DirectDamageType;

	%scale = getWord(%obj.getScale(), 2);
	%directDamage = %this.directDamage * %scale;

	if(%col.getType() & $TypeMasks::PlayerObjectType && !%col.getDataBlock().isTurretArmor)
		%col.damage(%obj, %pos, %directDamage, %damageType);
	else
		%col.damage(%obj, %pos, %directDamage * %this.vehicleDamageMult, %damageType);
}

function TW_HeatSeekerProjectile::radiusDamage(%this, %obj, %col, %distanceFactor, %pos, %damageAmt)
{
	if(%distanceFactor <= 0)
		return;
	else if(%distanceFactor > 1)
		%distanceFactor = 1;

	%damageAmt *= %distanceFactor;

	if(%damageAmt)
	{
		%damageType = $DamageType::Radius;
		if(%this.RadiusDamageType)
				%damageType = %this.RadiusDamageType;

		if(%col.getType() & $TypeMasks::PlayerObjectType && !%col.getDataBlock().isTurretArmor)
			%col.damage(%obj, %pos, %damageAmt, %damageType);
		else
			%col.damage(%obj, %pos, %damageAmt * %this.vehicleDamageMult, %damageType);
	}
}

function TW_HeatSeekerProjectile::onAdd(%db, %proj)
{
	if(!%proj.isHoming)
	{
		%proj.target = %proj.sourceObject.heatTarget;
		%proj.target.lastLockedSound = 0;
		%proj.target.lastHeatLock = getSimTime();
	}
}

function TW_HeatSeekerProjectile::onHomeTick(%db, %proj)
{
	%col = %proj.target;

	if(isObject(%col))
		HeatLockOnPrint(-1, %col, 0, 1);
}

datablock ItemData(TW_HeatSeekerItem)
{
	category = "Weapon";
	className = "Weapon";

	shapeFile = "./dts/Heat_Seeker_na.dts";
	rotate = false;
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;

	uiName = "TW: Heat Seeker";
	iconName = "./ico/HeatSeeker";
	doColorShift = true;
	colorShiftColor = "0.25 0.25 0.25 1";

	image = TW_HeatSeekerImage;
	canDrop = true;

	AEAmmo = 1;
	AEType = TW_RocketAmmoItem.getID();
	AEBase = 1;

	RPM = 4;
	recoil = "Low";
	uiColor = "1 1 1";
	description = "Single shot rocket launcher, 75% velocity inheritance, must lock on to fire";

	useImpactSounds = true;
	softImpactThreshold = 2;
	softImpactSound = "AEWepImpactSoft1Sound AEWepImpactSoft2Sound AEWepImpactSoft3Sound";
	hardImpactThreshold = 8;
	hardImpactSound = "AEWepImpactHard1Sound AEWepImpactHard2Sound AEWepImpactHard3Sound";

	tribalClass = "specialty";
};

datablock ShapeBaseImageData(TW_HeatSeekerImage)
{
	shapeFile = "./dts/Heat_Seeker_na.dts";
	emap = true;

	mountPoint = 0;
	offset = "0 0.08 -0.04";
	eyeOffset = "0 0 0";
	rotation = eulerToMatrix( "0 0 0" );
 
	correctMuzzleVector = true;

	className = "WeaponImage";

	item = TW_HeatSeekerItem;
	ammo = " ";
	projectile = TW_HeatSeekerProjectile;
	projectileType = Projectile;

	shellExitDir        = "-1 0 0.5";
	shellExitOffset     = "0 0 0";
	shellExitVariance   = 25;	
	shellVelocity       = 5.0;
	
	melee = false;
	armReady = true;
	hideHands = false;

	doColorShift = true;
	colorShiftColor = TW_HeatSeekerItem.colorShiftColor;

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
	projectileInheritance = 0.75;

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
	stateTimeoutValue[9]			= 0.4;
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

package playerJettingPackage
{
	function Armor::onTrigger(%db, %pl, %trig, %val)
	{
		if(%trig == 4)
		{
			%pl.jetDown = %val;
			if(%val) %pl.jetDownTime = getSimTime();
			else %pl.jetUpTime = getSimTime();
		}

		return Parent::onTrigger(%db, %pl, %trig, %val);
	}
};
activatePackage(playerJettingPackage);

function Player::isJetting(%pl)
{
	return (%pl.jetDown && %pl.getEnergyLevel() > %pl.getDataBlock().minJetEnergy);
}

function Player::isHSJetting(%pl, %time)
{
	return (%pl.jetDown && %pl.getEnergyLevel() > %pl.getDataBlock().minJetEnergy) || (getSimTime() - %pl.jetDownTime < %time);
}

function TW_HeatSeekerImage::AEOnFire(%this,%obj,%slot)
{
	if(!isObject(%obj.heatTarget) || !%obj.heatLocked)
	{
		%obj.heatTarget = -1;
		%this.onDryFire(%obj, %slot);
		return;
	}

  %obj.schedule(0, "aeplayThread", "2", "jump");
	%obj.stopAudio(0); 
  %obj.playAudio(0, TW_HeatSeekerFireSound);
  
	%obj.blockImageDismount = true;
	%obj.schedule(400, unBlockImageDismount);

	Parent::AEOnFire(%this, %obj, %slot);
	
	%obj.heatTarget = -1;
	%obj.heatTargetTime = 0;
	%obj.heatLocked = false;
}

function TW_HeatSeekerImage::onDryFire(%this, %obj, %slot)
{
	%obj.aeplayThread(2, plant);
	serverPlay3D(TW_DryFireSound, %obj.getHackPosition());
}

function TW_HeatSeekerImage::onReloadMagIn(%this,%obj,%slot)
{
  %obj.aeplayThread("3", "plant");
}

function TW_HeatSeekerImage::onReloadEnd(%this,%obj,%slot)
{
  %obj.aeplayThread("3", "shiftRight");
	%obj.playAudio(1, TW_MortarUnholsterSound);
	Parent::AEMagReloadAll(%this, %obj, %slot);
}

function TW_HeatSeekerImage::onReloadStart(%this,%obj,%slot)
{
  %obj.schedule(150, "aeplayThread", "2", "shiftLeft");
  %obj.schedule(150, "aeplayThread", "3", "plant");
	%obj.reload2Schedule = %obj.schedule(150,playAudio, 1, TW_MagnumCycleSound);
}

function TW_HeatSeekerImage::onReady(%this,%obj,%slot)
{
	%obj.baadDisplayAmmo(%this);

	if(getSimTime() - %obj.reloadTime[%this.getID()] <= %this.stateTimeoutValue[0] * 1000 + 1000)
		%obj.schedule(0, setImageAmmo, %slot, 0);
}

function TW_HeatSeekerImage::onMount(%this,%obj,%slot)
{
	%obj.aeplayThread(2, plant);
	%this.AEMountSetup(%obj, %slot);

	%lockTime = 750;
	%lockAngle = 3.5;
	%minRange = 32;
	%lockRange = 512;
	%jetTime = 2000;
	%obj.heatLockOnLoop(%this, %slot, %lockTime, %lockAngle, %minRange, %lockRange, $TypeMasks::PlayerObjectType | $TypeMasks::VehicleObjectType, %jetTime);

	parent::onMount(%this,%obj,%slot);
}

function TW_HeatSeekerImage::onUnMount(%this,%obj,%slot)
{
	%this.AEUnmountCleanup(%obj, %slot);

	cancel(%obj.reload2Schedule);

	parent::onUnMount(%this,%obj,%slot);	
}

function TW_HeatSeekerImage::heatTargetFound(%this, %obj, %slot, %col)
{
	HeatLockOnPrint(%obj, %col, 0, 0);

	return true;
}

function TW_HeatSeekerImage::heatTargetMaintained(%this, %obj, %slot, %col)
{
	HeatLockOnPrint(%obj, %col, 0, 0);
}

function TW_HeatSeekerImage::heatTargetLocked(%this, %obj, %slot, %col)
{
	HeatLockOnPrint(%obj, %col, 1, 0);
}

function HeatLockOnPrint(%obj, %col, %valA, %valB)
{
	if(isObject(%obj.client))
	{	
		if(getSimTime() - %obj.lastLockSound >= 250)
		{
			%obj.lastLockSound = getSimTime();

			if(%valA)
			{
				%obj.client.centerPrint("<color:FF4411><font:impact:32>----- LOCKED -----<br><font:impact:24>" @ strupr(%col.client.name), 2);
				%obj.client.play2D(TW_HeatSeekerLockedSound);
			}
			else
			{
				%obj.client.centerPrint("<color:FFCC33><font:impact:32>----- LOCKING -----<br><font:impact:24>" @ strupr(%col.client.name), 2);
				%obj.client.play2D(TW_HeatSeekerLockingSound);
			}
		}
	}

	if(isObject(%col.client))
	{
		if(%valB)
			%col.lastLockedTime = getSimTime();

		if(getSimTime() - %col.lastLockedSound >= 1000)
		{
			if(!%valB && getSimTime() - %col.lastLockedTime >= 1000)
			{
				%col.client.play2D(TW_HeatSeekerWarnLockingSound);
				%col.client.centerPrint("<color:FFCC33><font:impact:32>! MISSILE LOCKING !", 2);
			}
			else
			{
				%col.client.play2D(TW_HeatSeekerWarnLockedSound);
				%col.client.centerPrint("<color:FF4411><font:impact:32>!!! MISSILE LOCKED !!!", 2);
			}

			%col.lastLockedSound = getSimTime();
		}
	}
	else if(%col.getMountedObjectCount() > 0)//%col.getType() & $TypeMasks::VehicleObjectType)
	{
		%coldb = %col.getDatablock();
		for(%i = 0; %i < %coldb.numMountPoints; %i++)
		{
			%targ = %col.getMountedObject(%i);
			if(isObject(%targ))
				HeatLockOnPrint(-1, %targ, 0, %valB);
		}
	}
}

function TW_HeatSeekerImage::heatTargetLost(%this, %obj, %slot, %col)
{
	%obj.targetLoseTime = getSimTime();
}

function TW_HeatSeekerImage::heatNoTarget(%this, %obj, %slot, %col)
{
	if(getSimTime() - %obj.targetLoseTime > 2000)
		%obj.client.centerPrint("<color:44FF44><font:impact:32>----- NO TARGET -----<br><font:impact:24>", 2);
	else
		%obj.client.centerPrint("<color:44FF44><font:impact:32>----- TARGET LOST -----<br><font:impact:24>", 2);
}

function Player::heatLockOnLoop(%pl, %img, %slot, %time, %angle, %minRange, %range, %mask, %jetTime)
{
	cancel(%pl.heatLock);

	if(!isObject(%pl) || %pl.getDamagePercent() >= 1.0)
		return;

	if(%pl.getMountedImage(0) != %img)
	{
		if(isObject(%pl.heatTarget))
			%img.heatTargetLost(%pl, %slot, %pl.heatTarget);
		
		%pl.heatTarget = -1;
		%pl.heatTargetTime = 0;
		%pl.heatLocked = false;
		return;
	}

	if(%pl.getImageState(%slot) $= "Ready")
	{
		%eye = %pl.getEyePoint();
		%vec = %pl.getEyeVector();
		if(!isObject(%pl.heatTarget))
		{
			initContainerRadiusSearch(%eye, %range, %mask);
			while(isObject(%col = containerSearchNext()))
			{
				if(miniGameCanDamage(%pl.client, %col) != 1)
					continue;

				if(%pl.client == %col.client)
					continue;
				
				if(minigameIsFriendly(%pl, %col))
					continue;

				if(%col.isCloaked || %col.isCloaked())
					continue;

				%pos = %col.getWorldBoxCenter();

				if(%col.getType() & $TypeMasks::PlayerObjectType)
					%pos = %col.getCenterPos();

				if(%minRange > 0 && vectorDist(%pl.getHackPosition(), %pos) < %minRange)
					continue;
				
				if((%col.getType() & $TypeMasks::PlayerObjectType) && !%col.getDataBlock().isTurretArmor && !%col.isHSJetting(%jetTime))
					continue;

				// if(isObject(%col.lockOnSet) && %col.lockOnSet.getCount() >= %maxLockOn && %maxLockOn > 0)
				// 	continue;
				
				%ray = containerRayCast(%eye, %pos, $TypeMasks::FxBrickObjectType | $trapStaticTypemask, %pl, %col);

				if(isObject(%ray))
					continue;
				
				%ang = mRadToDeg(mAcos(vectorDot(%vec, vectorNormalize(vectorSub(%pos, %pl.getHackPosition())))));
				if(%ang > %angle)
					continue;

				if(%img.heatTargetFound(%pl, %slot, %col))
				{
					%pl.heatTarget = %col;
					%pl.heatTargetTime = getSimTime();

					if(%time <= 0)
						%pl.heatLocked = true;
					else
						%pl.heatLocked = false;

					break;
				}
			}
		}
		else
		{
			%col = %pl.heatTarget;

			%pos = %col.getWorldBoxCenter();

			if(%col.getType() & $TypeMasks::PlayerObjectType)
				%pos = %col.getCenterPos();
			
			%ang = mRadToDeg(mAcos(vectorDot(%vec, vectorNormalize(vectorSub(%pos, %pl.getHackPosition())))));

			%ray = containerRayCast(%eye, %pos, $TypeMasks::FxBrickObjectType | $TypeMasks::InteriorObjectType | $trapStaticTypemask, %pl, %col);
			if(!isObject(%ray) && %ang <= %angle && (%minRange <= 0 || vectorDist(%pl.getHackPosition(), %pos) > %minRange) && (!(%col.getType() & $TypeMasks::PlayerObjectType) || %col.getDataBlock().isTurretArmor || %col.isHSJetting(%jetTime)))
			{
				if(getSimTime() - %pl.heatTargetTime < %time && %time > 0)
				{
					%img.heatTargetMaintained(%pl, %slot, %col);
					%pl.heatLocked = false;
				}
				else
				{
					%img.heatTargetLocked(%pl, %slot, %col);
					%pl.heatLocked = true;
				}
			}
			else
			{
				%img.heatTargetLost(%pl, %slot, %col);
				%pl.heatTarget = -1;
				%pl.heatTargetTime = 0;
				%pl.heatLocked = false;
			}
		}

		if(!isObject(%pl.heatTarget))
			%img.heatNoTarget(%pl, %slot);
	}

	%pl.heatLock = %pl.schedule(50, heatLockOnLoop, %img, %slot, %time, %angle, %minRange, %range, %mask, %jetTime);
}