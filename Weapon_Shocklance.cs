datablock AudioProfile(TW_ShocklanceFireSound)
{
	filename    = "./wav/Shocklance_fire.wav";
	description = AudioDefault3D;
	preload = true;
};

datablock AudioProfile(TW_ShocklanceMissSound)
{
	filename    = "./wav/Shocklance_miss.wav";
	description = AudioDefault3D;
	preload = true;
};

datablock AudioProfile(TW_ShocklanceReloadSound)
{
	filename    = "./wav/Shocklance_reload.wav";
	description = AudioDefault3D;
	preload = true;
};

datablock AudioProfile(TW_ShocklanceUnholsterSound)
{
	filename    = "./wav/Shocklance_unholster.wav";
	description = AudioDefault3D;
	preload = true;
};

datablock ExplosionData(TW_ShocklanceExplosion)
{
	soundProfile = "";

	lifeTimeMS = 350;

	emitter[0] = TW_ImpactShockEmitter;

	faceViewer     = true;
	explosionScale = "1 1 1";

	shakeCamera = false;
	camShakeFreq = "10.0 11.0 10.0";
	camShakeAmp = "3.0 10.0 3.0";
	camShakeDuration = 0.5;
	camShakeRadius = 100.0;

	lightStartRadius = 1;
	lightEndRadius = 4;
	lightStartColor = "0 0.5 1 1";
	lightEndColor = "0 0 0 0";

	damageRadius = 0;
	radiusDamage = 0;

	impulseRadius = 0;
	impulseForce = 0;
};

datablock ProjectileData(TW_ShocklanceProjectile)
{
	projectileShapeName = "base/data/shapes/empty.dts";
	directDamage        = 80;
	directDamageType = $DamageType::Direct;
	radiusDamageType = $DamageType::Radius;
	impactImpulse	   = 700;
	verticalImpulse	   = 500;
	explosion           = TW_ShocklanceExplosion;
	particleEmitter     = "";

	brickExplosionRadius = 0;
	brickExplosionImpact = false;          //destroy a brick if we hit it directly?
	brickExplosionForce  = 0;             
	brickExplosionMaxVolume = 0;          //max volume of bricks that we can destroy
	brickExplosionMaxVolumeFloating = 0;  //max volume of bricks that we can destroy if they aren't connected to the ground (should always be >= brickExplosionMaxVolume)

	explodeOnDeath = true;
	explodeOnPlayerImpact = true;

	muzzleVelocity      = 100;
	velInheritFactor    = 0;

	armingDelay         = 0;
	lifetime            = 1000;
	fadeDelay           = 990;
	bounceElasticity    = 0.9;
	bounceFriction       = 0.1;
	isBallistic         = false;
	gravityMod = 0.0;

	hasLight    = false;
	lightRadius = 5.0;
	lightColor  = "1.0 0.0 0.0";

	uiName = "";
};

datablock StaticShapeData(TW_ShocklanceTrail) { shapeFile = "./dts/electric_trail.dts"; };

function TW_ShocklanceTrail::onAdd(%this,%obj)
{
  %obj.schedule(0, playThread, 2, root);
  %obj.schedule(2000,delete);
}

datablock ItemData(TW_ShocklanceItem)
{
	category = "Weapon";
	className = "Weapon";

	shapeFile = "./dts/shocklance.dts";
	rotate = false;
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;

	uiName = "TW: Shocklance";
	iconName = "./ico/Shocklance";
	doColorShift = true;
	colorShiftColor = "0.5 0.5 0.5 1";

	image = TW_ShocklanceImage;
	canDrop = true;

	useImpactSounds = true;
	softImpactThreshold = 2;
	softImpactSound = "AEWepImpactSoft1Sound AEWepImpactSoft2Sound AEWepImpactSoft3Sound";
	hardImpactThreshold = 8;
	hardImpactSound = "AEWepImpactHard1Sound AEWepImpactHard2Sound AEWepImpactHard3Sound";
};

datablock ShapeBaseImageData(TW_ShocklanceImage)
{
	shapeFile = "./dts/shocklance.dts";
	emap = true;

	mountPoint = 0;
	offset = "0 0 0";
	eyeOffset = "0 0 0";
	rotation = eulerToMatrix( "0 0 0" );
 
	correctMuzzleVector = true;

	className = "WeaponImage";

	item = TW_ShocklanceItem;
	ammo = " ";

	shellExitDir        = "-1 0 0.5";
	shellExitOffset     = "0 0 0";
	shellExitVariance   = 25;	
	shellVelocity       = 5.0;

	projectile = TW_ShocklanceProjectile;
	projectileCount = 1;
	projectileSpeed = 200;
	projectileSpread = 0.0;
	projectileInheritance = 0.0;

	melee = false;
	armReady = true;
	hideHands = false;

	doColorShift = true;
	colorShiftColor = TW_ShocklanceItem.colorShiftColor;

	minEnergy = 20.0;
	energyUse = 20.0;

	shockRange = 16;
	shockDelayMiss = 2.0;
	shockDelayHit = 1.5;

	stateName[0]                     	= "Activate";
	stateTimeoutValue[0]             	= 0.2;
	stateTransitionOnTimeout[0]       = "Ready";
	stateSequence[0]			= "root";
	stateSound[0] = TW_ShocklanceUnholsterSound;

	stateName[1]                     	= "Ready";
	stateTransitionOnTriggerDown[1]  	= "Fire";
	stateTransitionOnNoAmmo[1]        = "Empty";
	stateAllowImageChange[1]         	= true;
	stateTransitionOnTimeout[1]       = "Ready";
	stateTimeoutValue[1]              = 0.1;
	stateScript[1]                    = "onReadyLoop";

	stateName[2]                    = "Fire";
	stateTransitionOnTimeout[2]     = "Delay";
	stateScript[2]                     = "onFire";
	stateFire[2]                       = true;
	stateTimeoutValue[2]             	= 0.5;
	stateAllowImageChange[2]        = false;
	stateSequence[2]                = "Fire";
	stateWaitForTimeout[2]			= true;
	
	stateName[3]				= "Delay";
	stateTimeoutValue[3]			= 1.5;
	stateTransitionOnTimeout[3]		= "SemiAutoCheck";
	stateTransitionOnNotLoaded[3] = "QuickDelay";
	
	stateName[4]				= "QuickDelay";
	stateTimeoutValue[4]			= 1.0;
	stateTransitionOnTimeout[4]		= "SemiAutoCheck";

	stateName[5]                     	= "SemiAutoCheck";
	stateTransitionOnTriggerUp[5]  	  = "Ready";

	stateName[6]				          = "Empty";
	stateTransitionOnAmmo[6]      = "Ready";
	stateTransitionOnTimeout[6]		= "Empty";
	stateTimeoutValue[6]			    = 0.1;
	stateScript[6]                = "onEmptyLoop";
};

function TW_ShocklanceImage::onMount(%this,%obj,%slot)
{
	%this.schedule(100, onAmmoCheck, %obj, %slot);
}

function TW_ShocklanceImage::onAmmoCheck(%this,%obj,%slot)
{
	if(%obj.shocklanceHit && getSimTime() - %obj.shockFireTime < %this.shockDelayHit  * 1000 ||
	  !%obj.shocklanceHit && getSimTime() - %obj.shockFireTime < %this.shockDelayMiss * 1000)
		%obj.setImageAmmo(%slot, 0);
	else
		%obj.setImageAmmo(%slot, %obj.getEnergyLevel() > %this.minEnergy);
}

function TW_ShocklanceImage::onReadyLoop(%this,%obj,%slot)
{
	TW_ShocklanceImage::onAmmoCheck(%this,%obj,%slot);

	if(%obj.firedShocklance)
	{
		%obj.firedShocklance = false;

		%obj.stopAudio(1);
		%obj.playAudio(1, TW_ShocklanceReloadSound);
		
		%obj.setImageLoaded(%slot, 1);
	}
}

function TW_ShocklanceImage::onEmptyLoop(%this,%obj,%slot)
{
	TW_ShocklanceImage::onAmmoCheck(%this,%obj,%slot);
	
	if(!%obj.firedShocklance)
		%obj.firedShocklance = true;
}

function TW_ShocklanceImage::onFire(%this,%obj,%slot)
{
	if(%obj.getEnergyLevel() > %this.minEnergy && %obj.getDamagePercent() < 1.0)
	{
		%obj.stopAudio(0);
		%obj.aeplayThread(2, plant);
		%obj.blockImageDismount = true;

		%obj.setEnergyLevel(%obj.getEnergyLevel() - %this.energyUse);

		%mask = $TypeMasks::FxBrickObjectType |
		        $TypeMasks::PlayerObjectType  |
						$TypeMasks::VehicleObjectType |
						$TypeMasks::InteriorObjectType|
						$TypeMasks::StaticObjectType  |
						$TypeMasks::TerrainObjectType ;

		%pos = %obj.getMuzzlePoint(%slot);
		%vec = %obj.getMuzzleVector(%slot);
		%ray = containerRayCast(%pos, vectorAdd(%pos, vectorScale(%vec, %this.shockRange)), %mask, %obj);

		%obj.firedShocklance = true;
		%obj.shockFireTime = getSimTime();

		if(isObject(%ray) && minigameCanDamage(%obj, %ray) == 1 && %ray.getType() & ($TypeMasks::PlayerObjectType | $TypeMasks::VehicleObjectType))
		{
			%obj.playAudio(0, TW_ShocklanceFireSound);
			%obj.schedule(500, unBlockImageDismount);

			%obj.setImageLoaded(%slot, 0);

			%shape = new StaticShape()
			{
				datablock = TW_ShocklanceTrail;
			};
			MissionCleanup.add(%shape);

			%x = getWord(%vec,0) / 2;
			%y = (getWord(%vec,1) + 1) / 2;
			%z = getWord(%vec,2) / 2;

			%tracer = 1.0;
			%shape.setTransform(%pos SPC VectorNormalize(%x SPC %y SPC %z) SPC mDegToRad(180));
			%shape.setScale(%tracer SPC vectorDist(%pos,posFromRaycast(%ray)) SPC %tracer);

			%dot = VectorDot(%vec, %ray.getForwardVector());
		
			if(%dot < 0.5 || %ray.getType() & $TypeMasks::VehicleObjectType)
				ProjectileFire(%this.projectile, vectorAdd(posFromRaycast(%ray), vectorScale(%vec, -0.05)), %vec, %this.projectileSpread, %this.projectileCount, %slot, %obj, %obj.client, %this.projectileSpeed);
			else
			{
				%obj.aeplayThread(0, activate);
				ProjectileFire(%this.projectile, vectorAdd(posFromRaycast(%ray), vectorScale(%vec, -0.05)), %vec, %this.projectileSpread, %this.projectileCount * 2, %slot, %obj, %obj.client, %this.projectileSpeed);
			}

			%obj.shocklanceHit = true;
		}
		else
		{
			%obj.playAudio(0, TW_ShocklanceMissSound);
			%obj.schedule(1000, unBlockImageDismount);

			%obj.setImageLoaded(%slot, 1);
			
			%obj.shocklanceHit = false;
		}
	}
}

function TW_ShocklanceImage::onMount(%this,%obj,%slot)
{
	%obj.aeplayThread(2, plant);
	parent::onMount(%this,%obj,%slot);
}