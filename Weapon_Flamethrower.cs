datablock AudioProfile(TW_FlamethrowerFireSound)
{
   filename    = "./wav/Flamethrower_loop.wav";
   description = AudioDefaultLooping3D;
   preload = true;
};

datablock AudioProfile(TW_FlamethrowerFireEndSound)
{
   filename    = "./wav/Flamethrower_end.wav";
   description = AudioDefault3D;
   preload = true;
};

datablock ProjectileData(TW_FlamethrowerProjectile)
{
	projectileShapeName = "base/data/shapes/empty.dts";
	directDamage        = 50;
	directDamageType = $DamageType::AE;
	radiusDamageType = $DamageType::AE;
	impactImpulse	   = 1;
	verticalImpulse	   = 1000;
	particleEmitter     = TW_FlamerTrailEmitter;

	brickExplosionRadius = 0;
	brickExplosionImpact = false;          //destroy a brick if we hit it directly?
	brickExplosionForce  = 0;             
	brickExplosionMaxVolume = 0;          //max volume of bricks that we can destroy
	brickExplosionMaxVolumeFloating = 0;  //max volume of bricks that we can destroy if they aren't connected to the ground (should always be >= brickExplosionMaxVolume)

	explodeOnPlayerImpact = true;

	muzzleVelocity      = 100;
	velInheritFactor    = 0;

	armingDelay         = 0;
	lifetime            = 350;
	fadeDelay           = 340;
	bounceElasticity    = 0.5;
	bounceFriction       = 0.20;
	isBallistic         = true;
	gravityMod = 1.0;

	hasLight    = false;
	lightRadius = 5.0;
	lightColor  = "1 0.5 0.0";

	uiName = "";
};

function TW_FlamethrowerProjectile::onCollision(%this, %obj, %col, %fade, %pos, %normal, %velocity)
{
	if(minigameCanDamage(%obj, %col) == 1)
	{
		if(%col.IsA("Player") || %col.IsA("AIPlayer"))
		{
			%obj.firstBurnTick = true;
			%fd = 4;
			%ad = 3.5;
			%col.molotovAfterBurn(%ad, 250, 6, %fd, %obj);
		}
		else if(isFunction(%col.getClassName(), "Damage"))
			%col.damage(%obj, %pos, 2, $DamageType::mollyDirect);
	}
}

function TW_FlamethrowerProjectile::onExplode(%this, %obj, %col, %fade, %pos, %normal, %velocity)
{
	AETrailedProjectile::onExplode(%this, %obj, %col, %fade, %pos, %normal, %velocity);
}

function TW_FlamethrowerProjectile::Damage(%this, %obj, %col, %fade, %pos, %normal)
{
	AETrailedProjectile::Damage(%this, %obj, %col, %fade, %pos, %normal);
}

datablock ItemData(TW_FlamethrowerItem)
{
	category = "Weapon";
	className = "Weapon";

	shapeFile = "./dts/Flamethrower_na.dts";
	rotate = false;
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;

	uiName = "TW: Flamethrower";
	iconName = "./ico/Flamethrower";
	doColorShift = true;
	colorShiftColor = "0.55 0.55 0.55 1";

	image = TW_FlamethrowerImage;
	canDrop = true;

	AEAmmo = 40;
	AEType = TW_NapalmAmmoItem.getID();
	AEBase = 1;
	AEUseReserve = 1;

	RPM = 4;
	recoil = "Low";
	uiColor = "1 1 1";
	description = "Short range flamethrower, 50% velocity inheritance";

	useImpactSounds = true;
	softImpactThreshold = 2;
	softImpactSound = "AEWepImpactSoft1Sound AEWepImpactSoft2Sound AEWepImpactSoft3Sound";
	hardImpactThreshold = 8;
	hardImpactSound = "AEWepImpactHard1Sound AEWepImpactHard2Sound AEWepImpactHard3Sound";

	tribalClass = "short";
};

datablock ShapeBaseImageData(TW_FlamethrowerImage)
{
	shapeFile = "./dts/Flamethrower_na.dts";
	emap = true;

	mountPoint = 0;
	offset = "0 0.08 -0.04";
	eyeOffset = "0 0 0";
	rotation = eulerToMatrix( "0 0 0" );
 
	correctMuzzleVector = true;

	className = "WeaponImage";

	item = TW_FlamethrowerItem;
	ammo = " ";
	projectile = TW_FlamethrowerProjectile;
	projectileType = Projectile;

	shellExitDir        = "-1 0 0.5";
	shellExitOffset     = "0 0 0";
	shellExitVariance   = 25;	
	shellVelocity       = 5.0;
	
	melee = false;
	armReady = true;
	hideHands = false;

	doColorShift = true;
	colorShiftColor = TW_FlamethrowerItem.colorShiftColor;

	muzzleFlashScale = "0 0 0";
	bulletScale = "1 1 1";

	screenshakeMin = "0.3 0.3 0.3"; 
	screenshakeMax = "1 1 1";

	projectileDamage = 20;
	projectileCount = 1;
	projectileHeadshotMult = 1.0;
	projectileVelocity = 60;
	projectileTagStrength = 0;
	projectileTagRecovery = 1.0;
	projectileInheritance = 0.5;

	alwaysSpawnProjectile = true;

	recoilHeight = 0;
	recoilWidth = 0;
	recoilWidthMax = 0;
	recoilHeightMax = 0;

	spreadBurst = 1;
	spreadReset = 100;
	spreadBase = 1500;
	spreadMin = 1500;
	spreadMax = 1500;

	stateName[0]                     	= "Activate";
	stateTimeoutValue[0]             	= 0.01;
	stateTransitionOnTimeout[0]       	= "LoadCheckA";
	stateSequence[0]			= "root";
	stateSound[0] = TW_LauncherUnholsterSound;

	stateName[1]                     	= "Ready";
	stateScript[1]				= "onReady";
	stateTransitionOnNotLoaded[1]     = "Empty";
	stateTransitionOnTriggerDown[1]  	= "preFire";
	stateAllowImageChange[1]         	= true;

	stateName[2]                       = "preFire";
	stateTransitionOnTimeout[2]        = "Fire";
	stateScript[2]                     = "AEOnFire";
	stateFire[2]                       = true;

	stateName[3]                    = "Fire";
	stateTransitionOnTimeout[3]     = "FireLoadCheckA";
	stateTimeoutValue[3]             	= 0.01;
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
	
	stateName[11]				= "FireLoadCheckA";
	stateScript[11]				= "AEMagLoadCheck";
	stateTimeoutValue[11]			= 0.025;
	stateTransitionOnTimeout[11]		= "FireLoadCheckB";
	
	stateName[12]				= "FireLoadCheckB";
	stateTransitionOnAmmo[12]  = "Ready";
	stateTransitionOnNotLoaded[12]  = "Ready";
	
	stateName[20]				= "ReadyLoop";
	stateTransitionOnTimeout[20]		= "Ready";

	stateName[21]          = "Empty";
	stateTransitionOnTriggerDown[21]  = "Dryfire";
	stateTransitionOnLoaded[21] = "Ready";
	stateScript[21]        = "AEOnEmpty";

	stateName[22]           = "Dryfire";
	stateTransitionOnTriggerUp[22] = "Empty";
	stateWaitForTimeout[22]    = false;
	stateScript[22]      = "onDryFire";
};

function TW_FlamethrowerImage::AEOnFire(%this,%obj,%slot)
{	
	if(isEventPending(%obj.lc))
		cancel(%obj.lc);
	else
  	%obj.playAudio(0, TW_FlamethrowerFireSound);
	
	%obj.lc = %obj.schedule(200, playAudio, 0, TW_FlamethrowerFireEndSound);
  
	%obj.blockImageDismount = true;
	%obj.schedule(400, unBlockImageDismount);

	Parent::AEOnFire(%this, %obj, %slot);
}

function TW_FlamethrowerImage::onDryFire(%this, %obj, %slot)
{
	%obj.aeplayThread(2, plant);
	serverPlay3D(AEDryFireSound, %obj.getHackPosition());
	%this.AEMagLoadCheck(%obj);
}

function TW_FlamethrowerImage::onReloadMagOut(%this,%obj,%slot)
{
  %obj.aeplayThread("3", "shiftRight");
}

function TW_FlamethrowerImage::onReloadMagIn(%this,%obj,%slot)
{
  %obj.aeplayThread("3", "plant");
}

function TW_FlamethrowerImage::onReloadEnd(%this,%obj,%slot)
{
  %obj.aeplayThread("3", "shiftAway");
	%obj.playAudio(1, Block_PlantBrick_Sound);
	Parent::AEMagReloadAll(%this, %obj, %slot);
}

function TW_FlamethrowerImage::onReloadStart(%this,%obj,%slot)
{
  %obj.schedule(150, "aeplayThread", "2", "shiftLeft");
  %obj.schedule(150, "aeplayThread", "3", "plant");
	%obj.reload2Schedule = %obj.schedule(150,playAudio, 1, TW_MagnumCycleSound);
}

function TW_FlamethrowerImage::onReady(%this,%obj,%slot)
{
	%obj.baadDisplayAmmo(%this);

	if(getSimTime() - %obj.reloadTime[%this.getID()] <= %this.stateTimeoutValue[0] * 1000 + 1000)
		%obj.schedule(0, setImageAmmo, %slot, 0);
}

function TW_FlamethrowerImage::onMount(%this,%obj,%slot)
{
	%obj.aeplayThread(2, plant);
	%this.AEMountSetup(%obj, %slot);
	parent::onMount(%this,%obj,%slot);
}

function TW_FlamethrowerImage::onUnMount(%this,%obj,%slot)
{
	%this.AEUnmountCleanup(%obj, %slot);

	cancel(%obj.reload2Schedule);

	parent::onUnMount(%this,%obj,%slot);	
}