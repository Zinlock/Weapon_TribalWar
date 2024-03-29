// todo: make a unique model for these

datablock ItemData(TW_EnergyPackItem)
{
	category = "Weapon";
	className = "Weapon";

	shapeFile = "./dts/pack.dts";
	rotate = false;
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;

	uiName = "TP: Energy";
	iconName = "./ico/Energy";
	doColorShift = true;
	colorShiftColor = "0.15 0.15 0.5 1";

	image = TW_EnergyPackImage;
	canDrop = true;

	useImpactSounds = true;
	softImpactThreshold = 2;
	softImpactSound = "AEWepImpactSoft1Sound AEWepImpactSoft2Sound AEWepImpactSoft3Sound";
	hardImpactThreshold = 8;
	hardImpactSound = "AEWepImpactHard1Sound AEWepImpactHard2Sound AEWepImpactHard3Sound";

	title = "Energy Pack";
	description = "Periodically recharges your energy";
	// description = "Increases your energy recharge rate by 30%";

	isPackItem = true;

	energyRegen = 6;
	energyDelay = 1000;
	// energyMult = 1.3;
};

function TW_EnergyPackItem::onPackTick(%db, %pl)
{
	if(getSimTime() - %pl.lastEnergyTick > %db.energyDelay)
	{
		%pl.lastEnergyTick = getSimTime();
		%pl.setEnergyLevel(%pl.getEnergyLevel() + %db.energyRegen);
	}

	// %rate = %pl.getDatablock().rechargeRate;
	// if(%pl.getRechargeRate() != %rate * %db.energyMult)
	// 	%pl.setRechargeRate(%rate * %db.energyMult);
}

function TW_EnergyPackItem::onPackLost(%db, %pl)
{
	// %pl.setRechargeRate(%pl.getDatablock().rechargeRate);
}

datablock ShapeBaseImageData(TW_EnergyPackImage)
{
	shapeFile = "./dts/pack.dts";
	emap = true;

	mountPoint = 0;
	offset = "0.3 0.3 0.05";
	eyeOffset = "0 0 0";
	rotation = eulerToMatrix( "0 0 90" );
 
	correctMuzzleVector = true;

	className = "WeaponImage";

	item = TW_EnergyPackItem;
	ammo = " ";
	projectile = "";
	projectileType = Projectile;

	shellExitDir        = "-1 0 0.5";
	shellExitOffset     = "0 0 0";
	shellExitVariance   = 25;	
	shellVelocity       = 5.0;
	
	melee = false;
	armReady = true;
	hideHands = false;

	doColorShift = true;
	colorShiftColor = TW_EnergyPackItem.colorShiftColor;

	stateName[0]                     	= "Activate";
	stateTimeoutValue[0]             	= 0.1;
	stateTransitionOnTimeout[0]       = "Ready";
	stateSequence[0]			            = "root";

	stateName[1]                     	= "Ready";
	stateScript[1]                    = "twPackDesc";
};
