datablock AudioProfile(TW_SniperRifleFireSound)
{
	filename    = "./wav/Sniper_Rifle_fire2.wav";
	description = HeavyClose3D;
	preload = true;
};

datablock AudioProfile(TW_SniperRifleBoltSound)
{
	filename    = "./wav/Sniper_Rifle_bolt.wav";
	description = AudioClose3D;
	preload = true;
};

datablock ItemData(TW_SniperRifleItem)
{
	category = "Weapon";
	className = "Weapon";

	shapeFile = "./dts/Sniper_Rifle_na.dts";
	rotate = false;
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;

	uiName = "TW: Sniper Rifle";
	iconName = "./ico/Sniper_Rifle";
	doColorShift = true;
	colorShiftColor = "0.3 0.3 0.4 1";

	image = TW_SniperRifleImage;
	canDrop = true;

	AEAmmo = 3;
	AEType = TW_BoltAmmoItem.getID();
	AEBase = 1;

	RPM = 4;
	recoil = "Low";
	uiColor = "1 1 1";
	description = "Accurate bolt-action rifle, damage increases over long ranges";

	useImpactSounds = true;
	softImpactThreshold = 2;
	softImpactSound = "AEWepImpactSoft1Sound AEWepImpactSoft2Sound AEWepImpactSoft3Sound";
	hardImpactThreshold = 8;
	hardImpactSound = "AEWepImpactHard1Sound AEWepImpactHard2Sound AEWepImpactHard3Sound";

	tribalClass = "specialty";
};

datablock ShapeBaseImageData(TW_SniperRifleImage)
{
	shapeFile = "./dts/Sniper_Rifle_na.dts";
	emap = true;

	mountPoint = 0;
	offset = "0 -0.02 0.06";
	eyeOffset = "0 0 0";
	rotation = eulerToMatrix( "0 0 0" );
 
	correctMuzzleVector = true;

	className = "WeaponImage";

	item = TW_SniperRifleItem;
	ammo = " ";
	projectile = AEProjectile;
	projectileType = Projectile;

	shellExitDir        = "-1 0 0.5";
	shellExitOffset     = "0 0 0";
	shellExitVariance   = 25;	
	shellVelocity       = 5.0;
	
	melee = false;
	armReady = true;
	hideHands = false;

	doColorShift = true;
	colorShiftColor = TW_SniperRifleItem.colorShiftColor;

	muzzleFlashScale = "1 1 1";
	bulletScale = "1 1 1";

	screenshakeMin = "0.3 0.3 0.3"; 
	screenshakeMax = "1 1 1";

	projectileDamage = 35;
	projectileCount = 1;
	projectileHeadshotMult = 2;
	projectileVelocity = 200;
	projectileTagStrength = 0;
	projectileTagRecovery = 1.0;
	projectileInheritance = 0.0;

	projectileRDismemberHead = 1;

	projectileFalloffStart = 48;
	projectileFalloffEnd = 128;
	projectileFalloffDamage = 150 / (35 * 2);

	recoilHeight = 0;
	recoilWidth = 0;
	recoilWidthMax = 0;
	recoilHeightMax = 20;

	spreadBurst = 1;
	spreadReset = 0;
	spreadBase = 0;
	spreadMin = 0;
	spreadMax = 0;

	farShotSound = SniperBDistantSound;
	farShotDistance = 40;

	sonicWhizz = true;
	whizzSupersonic = 2;
	whizzThrough = false;
	whizzDistance = 12;
	whizzChance = 100;
	whizzAngle = 80;

	staticRealHitscan = true;
	staticTotalRange = 2000;
	staticSpawnFakeProjectiles = true;
	staticTracerEffect = TW_BulletTrail;
	
	stateName[0]                     	= "Activate";
	stateTimeoutValue[0]             	= 0.01;
	stateTransitionOnTimeout[0]       	= "LoadCheckA";
	stateSequence[0]			= "root";

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

	stateName[3]                    = "Fire";
	stateTransitionOnTimeout[3]     = "SemiAutoCheck";
	stateTimeoutValue[3]             	= 0.65;
	stateEmitter[3]					= AEBaseSmokeBigEmitter;
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
	stateTransitionOnAmmo[6]		= "Bolt";
	stateTransitionOnNotLoaded[6] = "Empty";
	stateTransitionOnNoAmmo[6]		= "Reload";

	stateName[7]				= "Reload";
	stateTimeoutValue[7]			= 0.9;
	stateScript[7]				= "onReloadStart";
	stateTransitionOnTimeout[7]		= "ReloadMagOut";
	stateWaitForTimeout[7]			= true;
	stateSequence[7]			= "ReloadStart";

	stateName[8]				= "ReloadMagOut";
	stateTimeoutValue[8]			= 0.4;
	stateScript[8]				= "onReloadMagOut";
	stateTransitionOnTimeout[8]		= "ReloadMagIn";
	stateWaitForTimeout[8]			= true;
	stateSequence[8]			= "ReloadOut";
	
	stateName[9]				= "ReloadMagIn";
	stateTimeoutValue[9]			= 0.2;
	stateScript[9]				= "onReloadMagIn";
	stateTransitionOnTimeout[9]		= "ReloadEnd";
	stateWaitForTimeout[9]			= true;
	stateSequence[9]			= "ReloadIn";
	
	stateName[10]				= "ReloadEnd";
	stateTimeoutValue[10]			= 0.1;
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
	stateTransitionOnAmmo[12]  = "Bolt";
	stateTransitionOnNotLoaded[12]  = "Bolt";
		
	stateName[14]				= "Reloaded";
	stateTimeoutValue[14]			= 0.1;
	stateScript[14]				= "AEMagReloadAll";
	stateTransitionOnTimeout[14]		= "Bolt";

	stateName[15]				= "Bolt";
	stateTimeoutValue[15]			= 0.6;
	stateScript[15]				= "onReloadBolt";
	stateSequence[15]       = "reload";
	stateSound[15] = TW_SniperRifleBoltSound;
	stateTransitionOnTimeout[15]		= "BoltWait";
	
	stateName[16]				= "BoltWait";
	stateTimeoutValue[16]			= 0.25;
	stateTransitionOnTimeout[16]		= "Ready";
	
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

function TW_SniperRifleImage::AEOnFire(%this,%obj,%slot)
{	
  %obj.schedule(0, "aeplayThread", "2", "jump");
	%obj.stopAudio(0); 
  %obj.playAudio(0, TW_SniperRifleFireSound);
  
	%obj.blockImageDismount = true;
	%obj.schedule(600, unBlockImageDismount);

	Parent::AEOnFire(%this, %obj, %slot);
}

function TW_SniperRifleImage::onDryFire(%this, %obj, %slot)
{
	%obj.aeplayThread(2, plant);
	serverPlay3D(AEDryFireSound, %obj.getHackPosition());
}

function TW_SniperRifleImage::onReloadMagOut(%this,%obj,%slot)
{
  %obj.aeplayThread("3", "shiftLeft");
	%obj.playAudio(1, Block_ChangeBrick_Sound);
}

function TW_SniperRifleImage::onReloadMagIn(%this,%obj,%slot)
{
  %obj.aeplayThread("3", "plant");
	%obj.playAudio(1, TW_TacticalReloadBolt2Sound);
}

function TW_SniperRifleImage::onReloadBolt(%this,%obj,%slot)
{
  %obj.aeplayThread("3", "plant");
  %obj.aeplayThread("2", "shiftLeft");
  %obj.schedule(300, "aeplayThread", "3", "plant");
}

function TW_SniperRifleImage::onReloadEnd(%this,%obj,%slot)
{
	Parent::AEMagReloadAll(%this, %obj, %slot);
}

function TW_SniperRifleImage::onReloadStart(%this,%obj,%slot)
{
	%obj.playAudio(1, Block_PlantBrick_Sound);
  %obj.schedule(170, "aeplayThread", "2", "shiftRight");
  %obj.schedule(0, "aeplayThread", "3", "plant");
	%obj.reload2Schedule = %obj.schedule(170,playAudio, 1, Block_MoveBrick_Sound);
}

function TW_SniperRifleImage::onReady(%this,%obj,%slot)
{
	%obj.baadDisplayAmmo(%this);

	if(getSimTime() - %obj.reloadTime[%this.getID()] <= %this.stateTimeoutValue[0] * 1000 + 1000)
		%obj.schedule(0, setImageAmmo, %slot, 0);
}

function TW_SniperRifleImage::onMount(%this,%obj,%slot)
{
	%this.AEMountSetup(%obj, %slot);
	parent::onMount(%this,%obj,%slot);
}

function TW_SniperRifleImage::onUnMount(%this,%obj,%slot)
{
	%this.AEUnmountCleanup(%obj, %slot);

	cancel(%obj.reload2Schedule);

	parent::onUnMount(%this,%obj,%slot);	
}