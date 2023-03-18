datablock AudioProfile(TW_JumpGunFireSound)
{
	filename    = "./wav/JumpGun_fire.wav";
	description = AudioDefault3D;
	preload = true;
};

datablock AudioProfile(TW_JumpGunImpactSound)
{
	filename    = "./wav/JumpGun_hit.wav";
	description = AudioDefault3D;
	preload = true;
};

datablock ExplosionData(TW_JumpGunExplosion)
{
	soundProfile = TW_JumpGunImpactSound;

	lifeTimeMS = 350;

	emitter[0] = TW_LauncherFlashEmitter;

	faceViewer     = true;
	explosionScale = "1 1 1";

	shakeCamera = true;
	camShakeFreq = "10.0 11.0 10.0";
	camShakeAmp = "1.0 2.0 1.0";
	camShakeDuration = 1.0;
	camShakeRadius = 10.0;

	lightStartRadius = 1;
	lightEndRadius = 4;
	lightStartColor = "1 1 1 1";
	lightEndColor = "0 0 0 0";

	damageRadius = 0.0001;
	radiusDamage = 0.0001;

	impulseRadius = 8;
	impulseForce = 5000;
};

datablock ProjectileData(TW_JumpGunProjectile)
{
	projectileShapeName = "./dts/whirl_projectile.dts";
	directDamage        = 0;
	directDamageType = $DamageType::Direct;
	radiusDamageType = $DamageType::Radius;
	impactImpulse	   = 500;
	verticalImpulse	   = 500;
	explosion           = TW_JumpGunExplosion;
	particleEmitter     = TW_LauncherTrailEmitter;

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

datablock ItemData(TW_JumpGunItem)
{
	category = "Weapon";
	className = "Weapon";

	shapeFile = "./dts/jump_gun.dts";
	rotate = false;
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;

	uiName = "TW: Jump Gun";
	iconName = "./ico/JumpGun";
	doColorShift = true;
	colorShiftColor = "0.5 0.5 0.5 1";

	image = TW_JumpGunImage;
	canDrop = true;

	useImpactSounds = true;
	softImpactThreshold = 2;
	softImpactSound = "AEWepImpactSoft1Sound AEWepImpactSoft2Sound AEWepImpactSoft3Sound";
	hardImpactThreshold = 8;
	hardImpactSound = "AEWepImpactHard1Sound AEWepImpactHard2Sound AEWepImpactHard3Sound";
};

datablock ShapeBaseImageData(TW_JumpGunImage)
{
	shapeFile = "./dts/jump_gun.dts";
	emap = true;

	mountPoint = 0;
	offset = "0 0.35 0.4";
	eyeOffset = "0 0 0";
	rotation = eulerToMatrix( "0 0 0" );
 
	correctMuzzleVector = true;

	className = "WeaponImage";

	item = TW_JumpGunItem;
	ammo = " ";

	shellExitDir        = "-1 0 0.5";
	shellExitOffset     = "0 0 0";
	shellExitVariance   = 25;	
	shellVelocity       = 5.0;

	projectile = TW_JumpGunProjectile;
	projectileCount = 1;
	projectileSpeed = 100;
	projectileSpread = 0.0;
	projectileInheritance = 0.5;

	melee = false;
	armReady = true;
	hideHands = false;

	doColorShift = true;
	colorShiftColor = TW_JumpGunItem.colorShiftColor;

	minEnergy = 30.0;
	energyUse = 15.0;

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
	stateTimeoutValue[2]             	= 0.3;
	stateAllowImageChange[2]        = false;
	stateSequence[2]                = "Fire";
	stateWaitForTimeout[2]			= true;
	
	stateName[3]				= "Delay";
	stateTimeoutValue[3]			= 0.2;
	stateTransitionOnTimeout[3]		= "SemiAutoCheck";

	stateName[5]                     	= "SemiAutoCheck";
	stateTransitionOnTriggerUp[5]  	  = "Ready";

	stateName[6]				          = "Empty";
	stateTransitionOnAmmo[6]      = "Ready";
	stateTransitionOnTimeout[6]		= "Empty";
	stateTimeoutValue[6]			    = 0.1;
	stateScript[6]                = "onEmptyLoop";
};

function TW_JumpGunImage::onReadyLoop(%this,%obj,%slot)
{
	%obj.setImageAmmo(%slot, %obj.getEnergyLevel() > %this.minEnergy);
}

function TW_JumpGunImage::onEmptyLoop(%this,%obj,%slot)
{
	TW_JumpGunImage::onReadyLoop(%this,%obj,%slot);
}

function TW_JumpGunImage::onFire(%this,%obj,%slot)
{
	if(%obj.getEnergyLevel() > %this.minEnergy && %obj.getDamagePercent() < 1.0)
	{
		%obj.stopAudio(0);
		%obj.playAudio(0, TW_JumpGunFireSound);
		
		%obj.aeplayThread(2, plant);

		%obj.setEnergyLevel(%obj.getEnergyLevel() - %this.energyUse);

		%vec = vectorAdd(%obj.getMuzzleVector(%slot), vectorScale(%obj.getVelocity(), (1 / %this.projectileSpeed) * %this.projectileInheritance));
		ProjectileFire(%this.projectile, %obj.getMuzzlePoint(%slot), %vec, %this.projectileSpread, %this.projectileCount, %slot, %obj, %obj.client, %this.projectileSpeed);
	}
}

function TW_JumpGunImage::onMount(%this,%obj,%slot)
{
	%obj.aeplayThread(2, plant);
	parent::onMount(%this,%obj,%slot);
}