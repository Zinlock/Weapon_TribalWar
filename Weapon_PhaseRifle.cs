datablock AudioProfile(TW_PhaseRifleFireSound)
{
	filename    = "./wav/phase_rifle_fire.wav";
	description = HeavyClose3D;
	preload = true;
};

datablock AudioProfile(TW_PhaseRifleUnholsterSound)
{
	filename    = "./wav/phase_rifle_unholster.wav";
	description = AudioClosest3D;
	preload = true;
};

datablock ItemData(TW_PhaseRifleItem)
{
	category = "Weapon";
	className = "Weapon";

	shapeFile = "./dts/phase_rifle_na.dts";
	rotate = false;
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;

	uiName = "TW: Phase Rifle";
	iconName = "./ico/phase_rifle";
	doColorShift = true;
	colorShiftColor = "0.5 0.25 0.25 1";

	image = TW_PhaseRifleImage;
	canDrop = true;

	AEAmmo = 2;
	AEType = TW_BoltAmmoItem.getID();
	AEBase = 1;

	RPM = 4;
	recoil = "Low";
	uiColor = "1 1 1";
	description = "Laser rifle, damage increases with player energy";

	useImpactSounds = true;
	softImpactThreshold = 2;
	softImpactSound = "AEWepImpactSoft1Sound AEWepImpactSoft2Sound AEWepImpactSoft3Sound";
	hardImpactThreshold = 8;
	hardImpactSound = "AEWepImpactHard1Sound AEWepImpactHard2Sound AEWepImpactHard3Sound";
};

datablock ShapeBaseImageData(TW_PhaseRifleImage)
{
	shapeFile = "./dts/phase_rifle_na.dts";
	emap = true;

	mountPoint = 0;
	offset = "0 0 0";
	eyeOffset = "0 0 0";
	rotation = eulerToMatrix( "0 0 0" );
 
	correctMuzzleVector = true;

	className = "WeaponImage";

	item = TW_PhaseRifleItem;
	ammo = " ";
	projectile = TW_ShortAEProjectile;
	projectileType = Projectile;

	shellExitDir        = "-1 0 0.5";
	shellExitOffset     = "0 0 0";
	shellExitVariance   = 25;	
	shellVelocity       = 5.0;
	
	melee = false;
	armReady = true;
	hideHands = false;

	doColorShift = true;
	colorShiftColor = TW_PhaseRifleItem.colorShiftColor;

	muzzleFlashScale = "1 1 1";
	bulletScale = "1 1 1";

	screenshakeMin = "0.1 0.1 0.1"; 
	screenshakeMax = "0.2 0.2 0.2";

	projectileBaseDamage = 25;
	projectileEnergyAdd = 35;

	projectileDamage = 25;
	projectileCount = 1;
	projectileHeadshotMult = 1.25;
	projectileVelocity = 200;
	projectileTagStrength = 0;
	projectileTagRecovery = 1.0;
	projectileInheritance = 0.0;

	recoilHeight = 0;
	recoilWidth = 0;
	recoilWidthMax = 0;
	recoilHeightMax = 20;

	spreadBurst = 1;
	spreadReset = 0;
	spreadBase = 50;
	spreadMin = 50;
	spreadMax = 50;
	
	sonicWhizz = true;
	whizzSupersonic = 2;
	whizzThrough = false;
	whizzDistance = 12;
	whizzChance = 100;
	whizzAngle = 80;

	staticRealHitscan = true;
	staticTotalRange = 500;
	staticSpawnFakeProjectiles = true;
	staticTracerEffect = TW_LaserTrail;

	stateName[0]                     	= "Activate";
	stateTimeoutValue[0]             	= 0.1;
	stateTransitionOnTimeout[0]       	= "LoadCheckA";
	stateSequence[0]			= "root";
	stateSound[0] = TW_PhaseRifleUnholsterSound;

	stateName[1]                     	= "Ready";
	stateScript[1]				= "onReady";
	stateTransitionOnNotLoaded[1]     = "Empty";
	stateTransitionOnNoAmmo[1]       	= "Reload";
	stateTransitionOnTriggerDown[1]  	= "preFire";
	stateAllowImageChange[1]         	= true;

	stateName[2]                       = "preFire";
	stateTransitionOnTimeout[2]        = "Fire";
	stateScript[2]                     = "AEOnFire";
	stateFire[2]                       = true;

	stateName[3]                    = "Fire";
	stateTransitionOnTimeout[3]     = "SemiAutoCheck";
	stateTimeoutValue[3]             	= 0.75;
	stateEmitter[3]					= AEBaseRifleFlashEmitter;
	stateEmitterTime[3]				= 0.05;
	stateEmitterNode[3]				= "muzzlePoint";
	stateAllowImageChange[3]        = false;
	stateSequence[3]                = "noDisc";
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
	stateTimeoutValue[7]			= 0.2;
	stateScript[7]				= "onReloadStart";
	stateTransitionOnTimeout[7]		= "ReloadMagOut";
	stateWaitForTimeout[7]			= true;
	stateSequence[7]			= "noDisc";
	
	stateName[8]				= "ReloadMagOut";
	stateTimeoutValue[8]			= 0.6;
	stateScript[8]				= "onReloadMagOut";
	stateTransitionOnTimeout[8]		= "ReloadMagIn";
	stateWaitForTimeout[8]			= true;
	stateSequence[8]			= "root";
	stateSound[8]				= TW_GenericReload2Sound;

	stateName[9]				= "ReloadMagIn";
	stateTimeoutValue[9]			= 0.6;
	stateScript[9]				= "onReloadMagIn";
	stateTransitionOnTimeout[9]		= "ReloadEnd";
	stateWaitForTimeout[9]			= true;
	stateSequence[9]			= "root";
	
	stateName[10]				= "ReloadEnd";
	stateTimeoutValue[10]			= 0.2;
	stateTransitionOnTimeout[10]		= "Reloaded";
	stateWaitForTimeout[10]			= true;
	stateScript[10]			  = "onReloadEnd";
	stateSequence[10]			= "root";
	
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

function TW_PhaseRifleImage::AEOnFire(%this,%obj,%slot)
{	
  %obj.schedule(0, "aeplayThread", "2", "plant");
	%obj.stopAudio(0); 
  %obj.playAudio(0, TW_PhaseRifleFireSound);
  
	%obj.blockImageDismount = true;
	%obj.schedule(400, unBlockImageDismount);

	%energy = %obj.getEnergyLevel() / %obj.getDatablock().maxEnergy;
	%obj.setEnergyLevel(0);
	
	%dmg = mFloatLerp(%this.projectileBaseDamage + %this.projectileEnergyAdd, %this.projectileBaseDamage, %energy);

	%this.projectileDamage = %dmg;

	Parent::AEOnFire(%this, %obj, %slot);
	
	%this.projectileDamage = %this.projectileBaseDamage;
}

function TW_PhaseRifleImage::onDryFire(%this, %obj, %slot)
{
	%obj.aeplayThread(2, plant);
	serverPlay3D(AEDryFireSound, %obj.getHackPosition());
}

function TW_PhaseRifleImage::onReloadMagOut(%this,%obj,%slot)
{
  %obj.aeplayThread("3", "shiftRight");
}

function TW_PhaseRifleImage::onReloadMagIn(%this,%obj,%slot)
{
  %obj.aeplayThread("3", "plant");
	%obj.playAudio(2, TW_TacticalReloadBolt2Sound);
}

function TW_PhaseRifleImage::onReloadEnd(%this,%obj,%slot)
{
	Parent::AEMagReloadAll(%this, %obj, %slot);
}

function TW_PhaseRifleImage::onReloadStart(%this,%obj,%slot)
{

}

function TW_PhaseRifleImage::onReady(%this,%obj,%slot)
{
	%obj.baadDisplayAmmo(%this);

	if(getSimTime() - %obj.reloadTime[%this.getID()] <= %this.stateTimeoutValue[0] * 1000 + 1000)
		%obj.schedule(0, setImageAmmo, %slot, 0);
}

function TW_PhaseRifleImage::onMount(%this,%obj,%slot)
{
	%obj.aeplayThread(2, plant);
	%this.AEMountSetup(%obj, %slot);
	parent::onMount(%this,%obj,%slot);
}

function TW_PhaseRifleImage::onUnMount(%this,%obj,%slot)
{
	%this.AEUnmountCleanup(%obj, %slot);

	cancel(%obj.reload2Schedule);

	parent::onUnMount(%this,%obj,%slot);	
}