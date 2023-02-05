datablock AudioProfile(TW_MachinePistolFireLoopSound)
{
   filename    = "./wav/Machine_Pistol_fire_loop.wav";
   description = BAADFireMediumLoop3D;
   preload = true;
};

datablock AudioProfile(TW_MachinePistolFireEndSound)
{
   filename    = "./wav/Machine_Pistol_fire_tail.wav";
   description = MediumClose3D;
   preload = true;
};

datablock ItemData(TW_MachinePistolItem)
{
	category = "Weapon";  // Mission editor category
	className = "Weapon"; // For inventory system

	 // Basic Item Properties
	shapeFile = "./dts/Machine_Pistol_na.dts";
	rotate = false;
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;

	//gui stuff
	uiName = "TW: Machine Pistol";
	iconName = "./ico/Machine_Pistol";
	doColorShift = true;
	colorShiftColor = "0.45 0.45 0.45 1";

	 // Dynamic properties defined by the scripts
	image = TW_MachinePistolImage;
	canDrop = true;

	AEAmmo = 30;
	AEType = TW_BulletAmmoItem.getID();
	AEBase = 1;

	Auto = true;
	RPM = 600;
	recoil = "Heavy";
	uiColor = "1 1 1";
	description = "Slow firing but accurate machine gun";

	useImpactSounds = true;
	softImpactThreshold = 2;
	softImpactSound = "AEWepImpactSoft1Sound AEWepImpactSoft2Sound AEWepImpactSoft3Sound";
	hardImpactThreshold = 8;
	hardImpactSound = "AEWepImpactHard1Sound AEWepImpactHard2Sound AEWepImpactHard3Sound";
};

datablock ShapeBaseImageData(TW_MachinePistolImage)
{
	shapeFile = "./dts/Machine_Pistol_na.dts";
	emap = true;

	mountPoint = 0;
	offset = "0 0 0";
	eyeOffset = "0 0 0";
	rotation = eulerToMatrix( "0 0 0" );

	correctMuzzleVector = true;

	className = "WeaponImage";

	item = TW_MachinePistolItem;
	ammo = " ";
	projectile = TW_LongAEProjectile;
	projectileType = Projectile;

	melee = false;
	armReady = true;
	hideHands = false;
	
	doColorShift = true;
	colorShiftColor = TW_MachinePistolItem.colorShiftColor;

  loopingEndSound = TW_MachinePistolFireEndSound;

	muzzleFlashScale = "1 1 1";
	bulletScale = "1 1 1";

	projectileDamage = 4;
	projectileCount = 1;
	projectileHeadshotMult = 1.25;
	projectileVelocity = 150;
	projectileTagStrength = 0.0;
	projectileTagRecovery = 1.0;

	recoilHeight = 0;
	recoilWidth = 0;
	recoilWidthMax = 0;
	recoilHeightMax = 20;

	spreadBurst = 3;
	spreadReset = 0;
	spreadBase = 500;
	spreadMin = 500;
	spreadMax = 500;

	screenshakeMin = "0.1 0.1 0.1";
	screenshakeMax = "0.15 0.15 0.15";

	farShotSound = RifleCDistantSound;
	farShotDistance = 40;

	sonicWhizz = true;
	whizzSupersonic = false;
	whizzThrough = false;
	whizzDistance = 14;
	whizzChance = 60;
	whizzAngle = 80;

	stateName[0]                     	= "Activate";
	stateTimeoutValue[0]             	= 0.01;
	stateTransitionOnTimeout[0]       	= "LoadCheckA";
	stateSequence[0]			= "root";
	stateSound[0] = TW_ChaingunUnholsterSound;

	stateName[1]                     	= "Ready";
	stateScript[1]				= "onReady";
	stateTransitionOnNotLoaded[1]     = "Empty";
	stateTransitionOnNoAmmo[1]       	= "Reload";
	stateTransitionOnTriggerDown[1]  	= "preFire";
	stateAllowImageChange[1]         	= true;

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
	stateTransitionOnNoAmmo[6]		= "Reload";

	stateName[7]				= "Reload";
	stateTimeoutValue[7]			= 0.5;
	stateScript[7]				= "onReloadStart";
	stateTransitionOnTimeout[7]		= "ReloadMagOut";
	stateWaitForTimeout[7]			= true;
	stateSequence[7]			= "ReloadStart";

	stateName[8]				= "ReloadMagOut";
	stateTimeoutValue[8]			= 0.6;
	stateScript[8]				= "onReloadMagOut";
	stateTransitionOnTimeout[8]		= "ReloadMagIn";
	stateWaitForTimeout[8]			= true;
	stateSequence[8]			= "MagOut";

	stateName[9]				= "ReloadMagIn";
	stateTimeoutValue[9]			= 0.35;
	stateScript[9]				= "onReloadMagIn";
	stateTransitionOnTimeout[9]		= "ReloadEnd";
	stateWaitForTimeout[9]			= true;
	stateSequence[9]			= "MagIn";

	stateName[10]				= "ReloadEnd";
	stateTimeoutValue[10]			= 0.3;
	stateScript[10]				= "onReloadEnd";
	stateTransitionOnTimeout[10]		= "Ready";
	stateWaitForTimeout[10]			= true;
	stateSequence[10]			= "ReloadEnd";

	stateName[11]				= "FireLoadCheckA";
	stateScript[11]				= "AEMagLoadCheck";
	stateTimeoutValue[11]			= 0.045;
	stateTransitionOnTimeout[11]		= "FireLoadCheckB";

	stateName[12]				= "FireLoadCheckB";
	stateTransitionOnAmmo[12]		= "TrigCheck";
	stateTransitionOnNoAmmo[12]		= "EndLoopEmpty";
	stateTransitionOnNotLoaded[12]  = "EndLoop";

	stateName[14]				= "Reloaded";
	stateTimeoutValue[14]			= 0.2;
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
	
	stateName[23]          = "TrigCheck";
	stateTransitionOnTriggerDown[23]  = "preFire";
	stateTransitionOnTimeout[23]		= "EndLoop";
	
	stateName[24]          = "EndLoop";
	stateScript[24]				= "onEndLoop";
	stateTransitionOnTimeout[24]		= "Ready";
	
	stateName[25]          = "EndLoopEmpty";
	stateScript[25]				= "onEndLoop";
	stateTransitionOnTimeout[25]		= "Reload";
};

function TW_MachinePistolImage::AEOnFire(%this,%obj,%slot)
{
	%obj.aeplayThread(0, plant);
	%obj.playAudio(0, TW_MachinePistolFireLoopSound);
	%obj.FireLoop = true;
	
	%obj.blockImageDismount = true;
	%obj.schedule(200, unBlockImageDismount);
	
	Parent::AEOnFire(%this, %obj, %slot); 	
}

function TW_MachinePistolImage::onEndLoop(%this, %obj, %slot)
{
	%obj.playAudio(0, %this.loopingEndSound);
	%obj.FireLoop = false;
}

function TW_MachinePistolImage::onDryFire(%this, %obj, %slot)
{
	%obj.aeplayThread(2, plant);
	serverPlay3D(AEDryFireSound, %obj.getHackPosition());
}

function TW_MachinePistolImage::onReloadMagIn(%this,%obj,%slot)
{
  %obj.aeplayThread("2", "shiftRight");
  %obj.aeplayThread("3", "leftRecoil");
	%obj.playAudio(1, TW_TacticalReloadOut4Sound);
}

function TW_MachinePistolImage::onReloadMagOut(%this,%obj,%slot)
{
	%obj.aeplayThread("2", "plant");
	%obj.playAudio(1, TW_TacticalReloadOut3Sound);
}

function TW_MachinePistolImage::onReloadEnd(%this,%obj,%slot)
{
	Parent::AEMagReloadAll(%this, %obj, %slot);
	%obj.aeplayThread("2", "plant");
	%obj.aeplayThread("3", "shiftLeft");
	%obj.playAudio(1, TW_TacticalReloadBolt2Sound);
}

function TW_MachinePistolImage::onReloadStart(%this,%obj,%slot)
{
	%obj.aeplayThread(2, shiftRight);
	%obj.playAudio(1, TW_TacticalReloadOut2Sound);
}

function TW_MachinePistolImage::onReady(%this,%obj,%slot)
{
	%obj.baadDisplayAmmo(%this);

	if(getSimTime() - %obj.reloadTime[%this.getID()] <= %this.stateTimeoutValue[0] * 1000 + 1000)
		%obj.schedule(0, setImageAmmo, %slot, 0);
}

function TW_MachinePistolImage::onMount(%this,%obj,%slot)
{
	%obj.aeplayThread(2, plant);
	%this.AEMountSetup(%obj, %slot);
	parent::onMount(%this,%obj,%slot);
}

function TW_MachinePistolImage::onUnMount(%this,%obj,%slot)
{
	%this.AEUnmountCleanup(%obj, %slot);
	parent::onUnMount(%this,%obj,%slot);
}