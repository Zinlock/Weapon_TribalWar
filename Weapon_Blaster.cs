datablock AudioProfile(TW_BlasterFireSound)
{
	filename    = "./wav/blaster_fire.wav";
	description = AudioDefault3D;
	preload = true;
};

datablock AudioProfile(TW_BlasterBounceSound)
{
	filename    = "./wav/blaster_bounce.wav";
	description = AudioClose3D;
	preload = true;
};

datablock AudioProfile(TW_BlasterImpactSound)
{
	filename    = "./wav/blaster_hit.wav";
	description = AudioClose3D;
	preload = true;
};

datablock ExplosionData(TW_BlasterExplosion)
{
	soundProfile = TW_BlasterImpactSound;

	lifeTimeMS = 350;

	emitter[0] = TW_ImpactRedEmitter;

	faceViewer     = true;
	explosionScale = "1 1 1";

	shakeCamera = false;
	camShakeFreq = "10.0 11.0 10.0";
	camShakeAmp = "3.0 10.0 3.0";
	camShakeDuration = 0.5;
	camShakeRadius = 100.0;

	lightStartRadius = 1;
	lightEndRadius = 4;
	lightStartColor = "1 0.0 0 1";
	lightEndColor = "0 0 0 0";

	damageRadius = 0;
	radiusDamage = 0;

	impulseRadius = 10;
	impulseForce = 200;
};

datablock ProjectileData(TW_BlasterProjectile)
{
	projectileShapeName = "./dts/laser_projectile.dts";
	directDamage        = 15;
	directDamageType = $DamageType::Direct;
	radiusDamageType = $DamageType::Radius;
	impactImpulse	   = 100;
	verticalImpulse	   = 300;
	explosion           = TW_BlasterExplosion;
	particleEmitter     = TW_LauncherTrailLaserEmitter;

	brickExplosionRadius = 0;
	brickExplosionImpact = false;          //destroy a brick if we hit it directly?
	brickExplosionForce  = 0;             
	brickExplosionMaxVolume = 0;          //max volume of bricks that we can destroy
	brickExplosionMaxVolumeFloating = 0;  //max volume of bricks that we can destroy if they aren't connected to the ground (should always be >= brickExplosionMaxVolume)

	explodeOnDeath = true;
	explodeOnPlayerImpact = true;

	muzzleVelocity      = 100;
	velInheritFactor    = 0;

	armingDelay         = 2000;
	lifetime            = 4000;
	fadeDelay           = 3990;
	bounceElasticity    = 0.9;
	bounceFriction       = 0.1;
	isBallistic         = true;
	gravityMod = 0.0;

	hasLight    = true;
	lightRadius = 5.0;
	lightColor  = "1.0 0.0 0.0";

	uiName = "";
};

function TW_BlasterProjectile::onCollision(%this, %obj, %col, %fade, %pos, %normal, %velocity)
{
	serverPlay3D(TW_BlasterBounceSound, %pos);

	Parent::onCollision(%this, %obj, %col, %fade, %pos, %normal, %velocity);
}

datablock ItemData(TW_BlasterItem)
{
	category = "Weapon";
	className = "Weapon";

	shapeFile = "./dts/blaster.dts";
	rotate = false;
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;

	uiName = "TW: Blaster";
	iconName = "./ico/Blaster";
	doColorShift = true;
	colorShiftColor = "0.75 0.45 0.45 1";

	image = TW_BlasterImage;
	canDrop = true;

	useImpactSounds = true;
	softImpactThreshold = 2;
	softImpactSound = "AEWepImpactSoft1Sound AEWepImpactSoft2Sound AEWepImpactSoft3Sound";
	hardImpactThreshold = 8;
	hardImpactSound = "AEWepImpactHard1Sound AEWepImpactHard2Sound AEWepImpactHard3Sound";
};

datablock ShapeBaseImageData(TW_BlasterImage)
{
	shapeFile = "./dts/blaster.dts";
	emap = true;

	mountPoint = 0;
	offset = "0 0 0";
	eyeOffset = "0 0 0";
	rotation = eulerToMatrix( "0 0 0" );
 
	correctMuzzleVector = true;

	className = "WeaponImage";

	item = TW_BlasterItem;
	ammo = " ";

	shellExitDir        = "-1 0 0.5";
	shellExitOffset     = "0 0 0";
	shellExitVariance   = 25;	
	shellVelocity       = 5.0;

	projectile = TW_BlasterProjectile;
	projectileCount = 1;
	projectileSpeed = 100; 
	projectileSpread = 0.1;
	projectileInheritance = 0.5;

	melee = false;
	armReady = true;
	hideHands = false;

	doColorShift = true;
	colorShiftColor = TW_BlasterItem.colorShiftColor;

	minEnergy = 15.0;
	energyUse = 10.0;

	stateName[0]                     	= "Activate";
	stateTimeoutValue[0]             	= 0.1;
	stateTransitionOnTimeout[0]       	= "Ready";
	stateSequence[0]			= "root";
	stateSound[0] = TW_SpinfusorUnholsterSound;

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
	stateTimeoutValue[2]             	= 0.1;
	stateAllowImageChange[2]        = false;
	stateSequence[2]                = "Fire";
	stateWaitForTimeout[2]			= true;
	
	stateName[3]				= "Delay";
	stateTimeoutValue[3]			= 0.2;
	stateTransitionOnTimeout[3]		= "Ready";
	
	stateName[4]				          = "Empty";
	stateTransitionOnAmmo[4]      = "Ready";
	stateTransitionOnTimeout[4]		= "Empty";
	stateTimeoutValue[4]			    = 0.1;
	stateScript[1]                = "onEmptyLoop";
};

function TW_BlasterImage::onReadyLoop(%this,%obj,%slot)
{
	%obj.setImageAmmo(%slot, %obj.getEnergyLevel() > %this.minEnergy);
}

function TW_BlasterImage::onEmptyLoop(%this,%obj,%slot)
{
	TW_BlasterImage::onReadyLoop(%this,%obj,%slot);
}

function TW_BlasterImage::onFire(%this,%obj,%slot)
{
	if(%obj.getEnergyLevel() > %this.minEnergy && %obj.getDamagePercent() < 1.0)
	{
		%obj.stopAudio(0);
		%obj.playAudio(0, TW_BlasterFireSound);
		
		%obj.aeplayThread(2, plant);

		%obj.setEnergyLevel(%obj.getEnergyLevel() - %this.energyUse);

		%vec = vectorAdd(%obj.getMuzzleVector(%slot), vectorScale(%obj.getVelocity(), (1 / %this.projectileSpeed) * %this.projectileInheritance));
		ProjectileFire(%this.projectile, %obj.getMuzzlePoint(%slot), %vec, %this.projectileSpread, %this.projectileCount, %slot, %obj, %obj.client, %this.projectileSpeed);
	}
}

function TW_BlasterImage::onMount(%this,%obj,%slot)
{
	%obj.aeplayThread(2, plant);
	parent::onMount(%this,%obj,%slot);
}