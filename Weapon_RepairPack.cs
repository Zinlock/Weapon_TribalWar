datablock AudioProfile(TW_RepairGunUnholsterSound)
{
   filename    = "./wav/repair_gun_unholster.wav";
   description = AudioClosest3D;
   preload = true;
};

datablock AudioProfile(TW_RepairGunFireSound)
{
   filename    = "./wav/repair_gun_loop.wav";
   description = AudioDefaultLooping3D;
   preload = true;
};

datablock StaticShapeData(TW_RepairGunTrail) { shapeFile = "./dts/repair_gun_trail.dts"; };

datablock ItemData(TW_RepairGunItem)
{
	category = "Weapon";
	className = "Weapon";

	shapeFile = "./dts/repair_gun.dts";
	rotate = false;
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;

	uiName = "TW: Repair Gun";
	iconName = "./ico/Repair_Gun";
	doColorShift = true;
	colorShiftColor = "0.45 0.15 0.15 1";

	image = TW_RepairGunImage;
	canDrop = true;

	useImpactSounds = true;
	softImpactThreshold = 2;
	softImpactSound = "AEWepImpactSoft1Sound AEWepImpactSoft2Sound AEWepImpactSoft3Sound";
	hardImpactThreshold = 8;
	hardImpactSound = "AEWepImpactHard1Sound AEWepImpactHard2Sound AEWepImpactHard3Sound";

	tribalClass = "pack";
};

datablock ShapeBaseImageData(TW_RepairGunImage)
{
	shapeFile = "./dts/repair_gun.dts";
	emap = true;

	mountPoint = 0;
	offset = "0 0 0";
	eyeOffset = "0 0 0";
	rotation = eulerToMatrix( "0 0 0" );
 
	correctMuzzleVector = true;

	className = "WeaponImage";

	item = TW_RepairGunItem;
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
	colorShiftColor = TW_RepairGunItem.colorShiftColor;

	repairRange = 8;
	repairAngle = 45;

	minEnergy = 5.0;
	energyUse = 1.0;
	repairAmt = 0.5;
	repairAmtUtil = 1.0;

	stateName[0]                     	= "Activate";
	stateTimeoutValue[0]             	= 0.1;
	stateTransitionOnTimeout[0]       	= "Ready";
	stateSequence[0]			= "root";
	stateSound[0] = TW_RepairGunUnholsterSound;

	stateName[1]                     	= "Ready";
	stateTransitionOnTriggerDown[1]  	= "Fire";
	stateAllowImageChange[1]         	= true;

	stateName[2]                    = "Fire";
	stateTransitionOnTimeout[2]     = "Delay";
	stateScript[2]                     = "onFire";
	stateFire[2]                       = true;
	stateTimeoutValue[2]             	= 0.032;
	stateAllowImageChange[2]        = false;
	stateSequence[2]                = "Fire";
	stateWaitForTimeout[2]			= true;
	
	stateName[3]				= "Delay";
	stateTimeoutValue[3]			= 0.032;
	stateTransitionOnTimeout[3]		= "Ready";
};

function TW_RepairGunImage::onFire(%this,%obj,%slot)
{
	if(%obj.getDamagePercent() >= 1.0)
		return;

	if(%obj.lastRepairTime $= "")
		%obj.lastRepairTime = getSimTime() - 32;

	if(%obj.getEnergyLevel() > %this.minEnergy)
	{
		%pos = %obj.getMuzzlePoint(%slot);

		if(isObject(%obj.turretBase))
			%pos = getWords(%obj.getSlotTransform(0), 0, 2);

		if(isObject(%obj.repairTarget))
		{
			%col = %obj.repairTarget;

			if(vectorDist(%pos, %col.getCenterPos()) > %this.repairRange ||
				 mRadToDeg(mAcos(vectorDot(vectorNormalize(vectorSub(%col.getCenterPos(), %pos)), %obj.getLookVector()))) > %this.repairAngle ||
				 %col.getDamagePercent() <= 0.0 && !%coldb.isTurretArmor ||
				 %col.getDamagePercent() >= 1.0 && !%coldb.isTurretArmor)
				%obj.repairTarget = -1;
		}

		if(!isObject(%obj.repairTarget))
		{
			%targ = 0;
			%targdb = 0;
			%dist = 999;

			initContainerRadiusSearch(%pos, %this.repairRange, $TypeMasks::PlayerObjectType | $TypeMasks::CorpseObjectType | $TypeMasks::VehicleObjectType);
			while(isObject(%col = containerSearchNext()))
			{
				if(!isObject(%obj.turretBase) && minigameCanDamage(%obj, %col) != -1 || minigameCanDamage(%obj.turretBase, %col) != -1)
				{
					%coldb = %col.getDataBlock();

					if(%col == %obj || %col == %obj.turretBase)
						continue;
					
					if(%col.getDamagePercent() <= 0.0 && (!%coldb.isTurretArmor || !isObject(%coldb.turretHeadData)))
						continue;
					
					if(isObject(%col.turretBase) && %col.turretBase.getDamagePercent() <= 0.0 || isObject(%col.turretHead) && %col.getDamagePercent() <= 0.0)
						continue;

					if(%col.getDamagePercent() >= 1.0 && (!%coldb.isTurretArmor || !isObject(%col.spawnBrick)))
						continue;

					if(vectorDist(%pos, %col.getCenterPos()) > %dist)
						continue;
					
					if(mRadToDeg(mAcos(vectorDot(vectorNormalize(vectorSub(%col.getCenterPos(), %pos)), %obj.getLookVector()))) > %this.repairAngle)
						continue;
					
					if(isObject(containerRayCast(%pos, %col.getCenterPos(), $trapStaticTypemask | $TypeMasks::InteriorObjectType | $TypeMasks::TerrainObjectType | $TypeMasks::FxBrickObjectType)))
						continue;
					
					%targ = %col;
					%targdb = %coldb;
					%dist = vectorDist(%pos, %col.getCenterPos());

					if(%targdb.isTurretArmor && isObject(%targ.turretBase))
					{
						%targ = %targ.turretBase;
						%targdb = %targ.getDataBlock();
					}
				}
			}

			if(!isObject(%obj.turretBase) && !isObject(%targ) && %obj.getDamagePercent() > 0.0)
			{
				%targ = %obj;
				%targdb = %obj.getDatablock();
				%dist = vectorDist(%pos, %obj.getCenterPos());
			}
		}
		else
		{
			%targ = %obj.repairTarget;
			%targdb = %targ.getDataBlock();
			%dist = vectorDist(%pos, %targ.getCenterPos());
		}

		if(isObject(%targ))
		{
			%obj.repairTarget = %targ;

			if(isEventPending(%obj.lc))
				cancel(%obj.lc);
			else
				%obj.playAudio(1, TW_RepairGunFireSound);
			
			%obj.lc = %obj.schedule(175, stopAudio, 1);
			
			%end = %targ.getCenterPos();
			%dir = vectorNormalize(vectorSub(%end, %pos));

			if(!isObject(%trail = %obj.repairTrail))
			{
				%trail = new StaticShape() { dataBlock = TW_RepairGunTrail; };
				%obj.repairTrail = %trail;
			}
			else cancel(%trail.cleanup);

			%trail.playThread(2, root);
			%trail.cleanup = %trail.schedule(2000,delete);
			
			%x = getWord(%dir,0) / 2;
			%y = (getWord(%dir,1) + 1) / 2;
			%z = getWord(%dir,2) / 2;

			%trail.setTransform(%pos SPC VectorNormalize(%x SPC %y SPC %z) SPC mDegToRad(180));
			
			%size = getRandom() * 0.5 + 1;
			%trail.setScale(%size SPC %dist SPC %size);

			%obj.setEnergyLevel(%obj.getEnergyLevel() - %this.energyUse);

			if(%targdb.isTurretArmor)
				%targ.turretRepair(%this.repairAmtUtil, %obj);
			else
				%targ.setDamageLevel(%targ.getDamageLevel() - ((%targ.getType() & $TypeMasks::PlayerObjectType) ? %this.repairAmt : %this.repairAmtUtil));
			
			if(isObject(%obj.client))
			{
				%obj.client.centerPrint("<color:329DDF><font:impact:24>---- REPAIRING ----<br>" @ mFloor(((%targdb.maxDamage - %targ.getDamageLevel()) / %targdb.maxDamage) * 100) @ "%", 1);
			
				%obj.blockImageDismount = true;
				%obj.schedule(400, unBlockImageDismount);
			}
		}
	}
	else
	{
		%time = getSimTime() - %obj.lastRepairTime;
		%obj.setEnergyLevel(%obj.getEnergyLevel() - %obj.getRechargeRate() * (%time / 32));
		%obj.client.centerPrint("<color:DF9D32><font:impact:24>---- NO ENERGY ----", 1);
	}

	%obj.lastRepairTime = getSimTime();
}

function TW_RepairGunImage::onMount(%this,%obj,%slot)
{
	%obj.aeplayThread(2, plant);
	parent::onMount(%this,%obj,%slot);
}