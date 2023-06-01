datablock AudioProfile(TW_ELFGunFireSound)
{
   filename    = "./wav/charge_cannon_fire.wav";
   description = AudioDefaultLooping3D;
   preload = true;
};

datablock AudioProfile(TW_ELFGunImpactSound)
{
   filename    = "./wav/charge_cannon_hit.wav";
   description = AudioDefaultLooping3D;
   preload = true;
};

datablock StaticShapeData(TW_ELFGunTrail) { shapeFile = "./dts/charge_cannon_trail.dts"; };

datablock ItemData(TW_ELFGunItem)
{
	category = "Weapon";
	className = "Weapon";

	shapeFile = "./dts/charge_cannon_na.dts";
	rotate = false;
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;

	uiName = "TW: Charge Cannon";
	iconName = "./ico/Charge";
	doColorShift = true;
	colorShiftColor = "0.25 0.25 0.35 1";

	image = TW_ELFGunImage;
	canDrop = true;

	useImpactSounds = true;
	softImpactThreshold = 2;
	softImpactSound = "AEWepImpactSoft1Sound AEWepImpactSoft2Sound AEWepImpactSoft3Sound";
	hardImpactThreshold = 8;
	hardImpactSound = "AEWepImpactHard1Sound AEWepImpactHard2Sound AEWepImpactHard3Sound";

	tribalClass = "short";
};

datablock ShapeBaseImageData(TW_ELFGunImage)
{
	shapeFile = "./dts/charge_cannon_na.dts";
	emap = true;

	mountPoint = 0;
	offset = "0 0 0";
	eyeOffset = "0 0 0";
	rotation = eulerToMatrix( "0 0 0" );
 
	correctMuzzleVector = true;

	className = "WeaponImage";

	item = TW_ELFGunItem;
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
	colorShiftColor = TW_ELFGunItem.colorShiftColor;

	elfRange = 20;
	elfAngle = 30;
	elfDrain = 3.0;
	elfDamage = 1.0;

	stateName[0]                     	= "Activate";
	stateTimeoutValue[0]             	= 0.1;
	stateTransitionOnTimeout[0]       	= "Ready";
	stateSequence[0]			= "root";
	stateSound[0] = TW_LauncherUnholsterSound;

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

function TW_ELFGunImage::onFire(%this,%obj,%slot)
{
	if(%obj.getDamagePercent() < 1.0)
	{
		%pos = %obj.getMuzzlePoint(%slot);

		if(isObject(%obj.turretBase))
			%pos = getWords(%obj.getSlotTransform(0), 0, 2);

		if(isObject(%obj.elfTarget))
		{
			%col = %obj.elfTarget;

			if(vectorDist(%pos, %col.getCenterPos()) > %this.elfRange ||
				 mRadToDeg(mAcos(vectorDot(vectorNormalize(vectorSub(%col.getCenterPos(), %pos)), %obj.getLookVector()))) > %this.elfAngle ||
				 %col.getDamagePercent() <= 0.0 && !%coldb.isTurretArmor ||
				 %col.getDamagePercent() >= 1.0 && !%coldb.isTurretArmor)
				%obj.elfTarget = -1;
		}

		if(!isObject(%obj.elfTarget))
		{
			%targ = 0;
			%targdb = 0;
			%dist = 999;

			initContainerRadiusSearch(%pos, %this.elfRange, $TypeMasks::PlayerObjectType | $TypeMasks::CorpseObjectType | $TypeMasks::VehicleObjectType);
			while(isObject(%col = containerSearchNext()))
			{
				if(!isObject(%obj.turretBase) && minigameCanDamage(%obj, %col) == 1 || minigameCanDamage(%obj.turretBase, %col) == 1)
				{
					%coldb = %col.getDataBlock();

					if(%col == %obj)
						continue;
					
					if(%col.getDamagePercent() >= 1.0)
						continue;
					
					if(%coldb.isTurretArmor)
					{
						if(isObject(%col.turretHead) && !%col.turretHead.isPowered || isObject(%col.turretBase) && %col.turretBase.isDisabled ||
							!isObject(%col.turretHead) && !%col.isPowered || !isObject(%col.turretBase) && %col.isDisabled)
							continue;

						if(!%coldb.energyShield)
							continue;
					}
					
					if(vectorDist(%pos, %col.getCenterPos()) > %dist)
						continue;
					
					if(mRadToDeg(mAcos(vectorDot(vectorNormalize(vectorSub(%col.getCenterPos(), %pos)), %obj.getLookVector()))) > %this.elfAngle)
						continue;
					
					if(isObject(containerRayCast(%pos, %col.getCenterPos(), $TypeMasks::StaticObjectType | $TypeMasks::InteriorObjectType | $TypeMasks::TerrainObjectType | $TypeMasks::FxBrickObjectType)))
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
		}
		else
		{
			%targ = %obj.elfTarget;
			%targdb = %targ.getDataBlock();
			%dist = vectorDist(%pos, %targ.getCenterPos());
		}

		if(isObject(%targ))
		{
			%obj.elfTarget = %targ;

			if(isEventPending(%obj.lc))
				cancel(%obj.lc);
			else
				%obj.playAudio(1, TW_ELFGunFireSound);
			
			%obj.lc = %obj.schedule(175, stopAudio, 1);
			
			if(isEventPending(%targ.lce))
				cancel(%targ.lce);
			else
				%targ.playAudio(2, TW_ELFGunImpactSound);
			
			%targ.lce = %targ.schedule(175, stopAudio, 2);
			
			%end = %targ.getCenterPos();
			%dir = vectorNormalize(vectorSub(%end, %pos));

			if(!isObject(%trail = %obj.elfTrail))
			{
				%trail = new StaticShape() { dataBlock = TW_ELFGunTrail; };
				%obj.elfTrail = %trail;
			}
			else cancel(%trail.cleanup);

			%trail.playThread(2, root);
			%trail.cleanup = %trail.schedule(2000,delete);
			
			%x = getWord(%dir,0) / 2;
			%y = (getWord(%dir,1) + 1) / 2;
			%z = getWord(%dir,2) / 2;

			%trail.setTransform(%pos SPC VectorNormalize(%x SPC %y SPC %z) SPC mDegToRad(180));
			
			%size = getRandom() * 0.5 + 2;
			%trail.setScale(%size SPC %dist SPC %size);

			%targ.setEnergyLevel(%targ.getEnergyLevel() - %this.elfDrain);

			if(%targ.getEnergyLevel() <= 10)
				%targ.damage(%obj, %targ.getCenterPos(), %this.elfDamage, $DamageType::Direct);
			
			if(isObject(%obj.client))
			{
				%obj.blockImageDismount = true;
				%obj.schedule(400, unBlockImageDismount);
			}
		}
	}
}

function TW_ELFGunImage::onMount(%this,%obj,%slot)
{
	%obj.aeplayThread(2, plant);
	parent::onMount(%this,%obj,%slot);
}