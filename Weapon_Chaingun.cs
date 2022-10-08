datablock AudioProfile(TW_ChaingunFireSound)
{
   filename    = "./wav/chaingun_fire_loop.wav";
   description = HeavyClose3D;
   preload = true;
};

datablock AudioProfile(TW_ChaingunSpinLoopStartSound)
{
   filename    = "./wav/chaingun_spinup.wav";
   description = MediumClose3D;
   preload = true;
};

datablock AudioProfile(TW_ChaingunSpinLoopSound)
{
   filename    = "./wav/chaingun_spin.wav";
   description = BAADFireMediumLoop3D;
   preload = true;
};

datablock AudioProfile(TW_ChaingunSpinLoopEndSound)
{
   filename    = "./wav/chaingun_spindown.wav";
   description = MediumClose3D;
   preload = true;
};

datablock AudioProfile(TW_ChaingunUnholsterSound)
{
   filename    = "./wav/Chaingun_unholster.wav";
   description = AudioClosest3D;
   preload = true;
};

datablock ItemData(TW_ChaingunItem)
{
	category = "Weapon";  // Mission editor category
	className = "Weapon"; // For inventory system

	 // Basic Item Properties
	shapeFile = "./dts/Chaingun_na.dts";
	rotate = false;
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;

	//gui stuff
	uiName = "TW: Chaingun";
	iconName = "./ico/Chaingun";
	doColorShift = true;
	colorShiftColor = "0.4 0.4 0.45 1";

	 // Dynamic properties defined by the scripts
	image = TW_ChaingunImage;
	canDrop = true;

	AEAmmo = 30;
	AEType = TW_BulletAmmoItem.getID();
	AEBase = 1;
	AEUseReserve = 1;

	Auto = true;
	RPM = 600;
	recoil = "Heavy";
	uiColor = "1 1 1";
	description = "Fast firing chaingun";

	useImpactSounds = true;
	softImpactThreshold = 2;
	softImpactSound = "AEWepImpactSoft1Sound AEWepImpactSoft2Sound AEWepImpactSoft3Sound";
	hardImpactThreshold = 8;
	hardImpactSound = "AEWepImpactHard1Sound AEWepImpactHard2Sound AEWepImpactHard3Sound";
};

datablock ShapeBaseImageData(TW_ChaingunImage)
{
	shapeFile = "./dts/Chaingun_na.dts";
	emap = true;

	mountPoint = 0;
	offset = "0 0 0";
	eyeOffset = "0 0 0";
	rotation = eulerToMatrix( "0 0 0" );

	correctMuzzleVector = true;

	className = "WeaponImage";

	item = TW_ChaingunItem;
	ammo = " ";
	projectile = AETrailedProjectile;
	projectileType = Projectile;

	melee = false;
	armReady = true;
	hideHands = false;
	
	doColorShift = true;
	colorShiftColor = TW_ChaingunItem.colorShiftColor;

  loopingEndSound = TW_ChaingunSpinLoopEndSound;

	muzzleFlashScale = "1 1 1";
	bulletScale = "1 1 1";

	projectileDamage = 4;
	projectileCount = 1;
	projectileHeadshotMult = 1.0;
	projectileVelocity = 200;
	projectileTagStrength = 0.0;
	projectileTagRecovery = 1.0;

	recoilHeight = 0;
	recoilWidth = 0;
	recoilWidthMax = 0;
	recoilHeightMax = 20;

	spreadBurst = 1;
	spreadReset = 0;
	spreadBase = 750;
	spreadMin = 750;
	spreadMax = 750;

	screenshakeMin = "0.1 0.1 0.1";
	screenshakeMax = "0.15 0.15 0.15";

	farShotSound = SMGADistantSound;
	farShotDistance = 40;

	sonicWhizz = true;
	whizzSupersonic = false;
	whizzThrough = false;
	whizzDistance = 14;
	whizzChance = 100;
	whizzAngle = 80;

	stateName[0]                     	= "Activate";
	stateTimeoutValue[0]             	= 0.01;
	stateTransitionOnTimeout[0]       	= "LoadCheckA";
	stateSequence[0]			= "root";
	stateSound[0] = TW_ChaingunUnholsterSound;

	stateName[1]                     	= "Ready";
	stateScript[1]				= "onReady";
	stateTransitionOnNotLoaded[1]     = "Empty";
	stateTransitionOnTriggerDown[1]  	= "Spin";
	stateAllowImageChange[1]         	= true;
	
	stateName[4]                     	= "Spin";
	stateScript[4]				            = "onSpin";
	stateTransitionOnTimeout[4]       = "preFire";
	stateTransitionOnTriggerUp[4]     = "EndLoop";
	stateSequence[4]                  = "start";
	stateAllowImageChange[4]         	= true;
	stateTimeoutValue[4]              = 0.7;

	stateName[2]                       = "preFire";
	stateTransitionOnTimeout[2]        = "Fire";
	stateScript[2]                     = "AEOnFire";
	stateEmitter[2]					= AEBaseRifleFlashEmitter;
	stateEmitterTime[2]				= 0.05;
	stateEmitterNode[2]				= "muzzlePoint";
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

	stateName[5]				= "LoadCheckA";
	stateScript[5]				= "AEMagLoadCheck";
	stateTimeoutValue[5]			= 0.1;
	stateTransitionOnTimeout[5]		= "LoadCheckB";

	stateName[6]				= "LoadCheckB";
	stateTransitionOnAmmo[6]		= "Ready";
	stateTransitionOnNotLoaded[6] = "Empty";

	stateName[11]				= "FireLoadCheckA";
	stateScript[11]				= "AEMagLoadCheck";
	stateTimeoutValue[11]			= 0.065;
	stateTransitionOnTimeout[11]		= "FireLoadCheckB";

	stateName[12]				= "FireLoadCheckB";
	stateTransitionOnAmmo[12]		= "TrigCheck";
	stateTransitionOnNotLoaded[12]  = "EndLoop";

	stateName[21]          = "Empty";
	stateTransitionOnTriggerDown[21]  = "Dryfire";
	stateTransitionOnLoaded[21] = "Ready";
	stateScript[21]        = "AEOnEmpty";

	stateName[22]           = "Dryfire";
	stateTransitionOnTriggerUp[22] = "Empty";
	stateWaitForTimeout[22]    = false;
	stateScript[22]      = "onDryFire";
	
	stateName[23]          = "TrigCheck";
	stateTransitionOnTriggerDown[23]  = "preFire";
	stateTransitionOnTimeout[23]		= "EndLoop";
	
	stateName[24]          = "EndLoop";
	stateScript[24]				= "onEndLoop";
	stateTransitionOnTimeout[24]		= "Ready";
	stateTimeoutValue[24] = 0.7;
	stateSequence[24]      = "End";
};

function TW_ChaingunImage::onSpin(%this,%obj,%slot)
{
	%obj.playAudio(0, TW_ChaingunSpinLoopStartSound);
	%obj.aeplayThread(2, plant);
	%obj.aeplayThread(3, shiftLeft);
}

function TW_ChaingunImage::AEOnFire(%this,%obj,%slot)
{
	%obj.playAudio(0, TW_ChaingunSpinLoopSound);
  %obj.FireLoop = true;

	%obj.aeplayThread(2, plant);

	%obj.stopAudio(1);
	%obj.playAudio(1, TW_ChaingunFireSound);
	
	%obj.blockImageDismount = true;
	%obj.schedule(200, unBlockImageDismount);
	
	Parent::AEOnFire(%this, %obj, %slot); 	
}

function TW_ChaingunImage::onEndLoop(%this, %obj, %slot)
{
	%obj.playAudio(0, %this.loopingEndSound);
	%obj.FireLoop = false;
}

function TW_ChaingunImage::onDryFire(%this, %obj, %slot)
{
	%obj.aeplayThread(2, plant);
	serverPlay3D(AEDryFireSound, %obj.getHackPosition());
	%this.AEMagLoadCheck(%obj);
}

function TW_ChaingunImage::onReady(%this,%obj,%slot)
{
	%obj.baadDisplayAmmo(%this);
}

function TW_ChaingunImage::onMount(%this,%obj,%slot)
{
	%obj.aeplayThread(2, plant);
	%this.AEMountSetup(%obj, %slot);
	parent::onMount(%this,%obj,%slot);
}

function TW_ChaingunImage::onUnMount(%this,%obj,%slot)
{
	%this.AEUnmountCleanup(%obj, %slot);
	parent::onUnMount(%this,%obj,%slot);
}