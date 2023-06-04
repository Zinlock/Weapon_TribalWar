datablock ParticleData(TW_FlareFuseParticle)
{
	dragCoefficient		= 1.0;
	windCoefficient		= 0.0;
	gravityCoefficient	= 1.0;
	inheritedVelFactor	= 1.0;
	constantAcceleration	= 0.0;
	lifetimeMS		= 1000;
	lifetimeVarianceMS	= 0;
	spinSpeed		= 10.0;
	spinRandomMin		= -50.0;
	spinRandomMax		= 50.0;
	useInvAlpha		= false;
	animateTexture		= false;

	textureName		= "base/data/particles/star1";

	colors[0]	= "1 0.5 0.3 0.0";
	colors[1]	= "0.9 0.3 0.0 1.0";
	colors[2]	= "0.6 0.0 0.0 0.0";

	sizes[0]	= 1.0;
	sizes[1]	= 0.5;
	sizes[2]	= 0.2;

	times[0]	= 0.0;
	times[1]	= 0.2;
	times[2]	= 1.0;
};

datablock ParticleEmitterData(TW_FlareFuseEmitter)
{
	ejectionPeriodMS = 20;
	periodVarianceMS = 0;
	ejectionVelocity = 10;
	velocityVariance = 2;
	ejectionOffset = 0.1;
	thetaMin         = 0.0;
	thetaMax         = 10.0;
	phiReferenceVel  = 0;
	phiVariance      = 360;
	overrideAdvance = false;

  particles = "TW_FlareFuseParticle";
};

datablock ProjectileData(TW_FlareProjectile)
{
	projectileShapeName = "./dts/flare_projectile.dts";
	directDamage        = 0;
	directDamageType  = $DamageType::flareDirect;
	radiusDamageType  = $DamageType::flareDirect;
	impactImpulse	   = 0;
	verticalImpulse	   = 0;
	explosion           = gunExplosion;
	particleEmitter     = TW_FlareFuseEmitter;

	muzzleVelocity      = 15;
	velInheritFactor    = 0;
	explodeOnPlayerImpact = false;
	explodeOnDeath        = true;

	brickExplosionRadius = 0;
	brickExplosionImpact = false;
	brickExplosionForce  = 0;
	brickExplosionMaxVolume = 0;
	brickExplosionMaxVolumeFloating = 0;

	armingDelay         = 4999;
	lifetime            = 5000;
	fadeDelay           = 5000;
	bounceElasticity    = 0.25;
	bounceFriction      = 0.5;
	isBallistic         = true;
	gravityMod = 0.1;

	hasLight    = false;
	lightRadius = 3.0;
	lightColor  = "0 0 0.5";
	
	PrjLoop_enabled = true;
	PrjLoop_maxTicks = -1;
	PrjLoop_tickTime = 50;

	isHeatFlare = true;
	flareRadius = 64;
	flareExplosionRadius = 2;

	uiName = "TW flare";
};

datablock ItemData(TW_FlareItem)
{
	category = "Weapon";
	className = "Weapon";

	shapeFile = "./dts/flare.dts";
	rotate = false;
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;

	uiName = "TW: Flare";
	iconName = "./ico/flare";
	doColorShift = false;

	image = TW_FlareImage;
	canDrop = true;
};

datablock ShapeBaseImageData(TW_FlareImage)
{
	shapeFile = "./dts/flare.dts";
	emap = true;

	item = TW_FlareItem;

	mountPoint = 0;
	offset = "0 0.1 0";
	eyeOffset = 0;
	rotation = eulerToMatrix( "0 0 0" );
	className = "WeaponImage";
	armReady = true;

	doColorShift = TW_FlareItem.doColorShift;
	colorShiftColor = TW_FlareItem.colorShiftColor;

	weaponUseCount = 4;
	weaponReserveMax = 12;

	projectileType = Projectile;
	projectile = TW_FlareProjectile;

	stateName[0]                     = "Ready";
	stateScript[0]								= "onReady";
	stateSequence[0]			 = "root";
	stateTransitionOnTriggerDown[0]  = "Charge";

	stateName[1]                     = "Charge";
	stateTransitionOnTimeout[1]      = "Cancel";
	stateScript[1]                   = "onChargeStart";
	stateSequence[1]			 = "noSpoon";
	stateTimeoutValue[1]		   = 0.1;//3.5;
	stateTransitionOnTriggerUp[1] = "Fire";
	stateWaitForTimeout[1] = false;

	stateName[4] 				= "Cancel";
	stateScript[4]                   = "onChargeStop";
	stateSequence[4]			 = "noSpoon";
	stateTransitionOnTimeout[4] = "Next";
	stateTimeoutValue[4]				= 0.1;

	stateName[3]                     = "Next";
	stateTimeoutValue[3]		   = 0.4;
	stateTransitionOnTimeout[3]      = "Ready";
	stateWaitForTimeout[3] = true;

	stateName[2]                     = "Fire";
	stateTransitionOnTimeout[2]      = "Next";
	stateScript[2]                   = "onFire";
	stateEjectShell[2] 				= true;
	stateTimeoutValue[2]		   = 0.3;
};

function TW_FlareImage::onReady(%this, %obj, %slot)
{
	%obj.weaponAmmoStart();
}

function TW_FlareImage::onChargeStop(%this, %obj, %slot) { %this.onFire(%obj, %slot); }

function TW_FlareImage::onChargeStart(%this, %obj, %slot) { }

function TW_FlareImage::onFire(%this, %obj, %slot)
{
	%obj.playThread(2, shiftDown);
	%obj.weaponAmmoUse();
	serverPlay3D(grenade_throwSound, %obj.getMuzzlePoint(%slot));
	%projs = ProjectileFire(%this.Projectile, %obj.getMuzzlePoint(%slot), %obj.getMuzzleVector(%slot), 0, 1, %slot, %obj, %obj.client);
	
	//%obj.unMountImage(%slot);
}

function TW_FlareProjectile::PrjLoop_onTick(%this, %obj)
{
	%pos = %obj.getPosition();

	if(!isObject(%obj.flareTarget))
	{
		%mask = $TypeMasks::fxBrickObjectType | $TypeMasks::StaticObjectType | $TypeMasks::StaticShapeObjectType | $TypeMasks::VehicleObjectType | $TypeMasks::TerrainObjectType | $TypeMasks::InteriorObjectType;

		initContainerRadiusSearch(%pos, %this.flareRadius, $TypeMasks::ProjectileObjectType);
		while(isObject(%col = containerSearchNext()))
		{
			if(%col == %obj)
				continue;

			%ray = containerRayCast(%pos, %col.getPosition(), %mask, %obj, %col);
			if(isObject(%ray))
				continue;

			%db = %col.getDataBlock();
			if(!%db.homingProjectile || !%db.flaresCanBait)
				continue;

			if(!%col.isHoming || !isObject(%col.target) || %col.target.getDataBlock().isHeatFlare)
				continue;

			if(%col.target == %obj)
			{
				%obj.flareTarget = %col;
				break;
			}

			%obj.flareTarget = %col;
			%col.target = %obj;
			break;
		}
	}

	if(isObject(%obj.flareTarget))
	{
		%col = %obj.flareTarget;

		if(%col.getDataBlock().flaresCanDestroy)
		{
			%next = vectorAdd(%col.getPosition(), vectorScale(%col.getVelocity(), 50/1000));
			if(vectorDist(%col.getPosition(), %pos) < %this.flareExplosionRadius || vectorDist(%next, %pos) < %this.flareExplosionRadius)
			{
				%col.explode();
				%obj.explode();
			}
		}
	}
}