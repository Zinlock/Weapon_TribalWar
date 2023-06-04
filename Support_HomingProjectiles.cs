// Originally by Space Guy, modified by Oxy (260031)

// homingProjectile = true; 		// makes a homing projectile
// homingRadius = 20; 					// radius to detect players in
// homingAccuracy = 100; 				// how accurately/fast should the projectile turn to the locked on player
// homingAccuracyClose = 1000;	// how accurately should the projectile turn to the locked on player while close to it
// homingCloseDist = 16; 				// accuracy falloff start range
// homingFarDist = 128; 				// accuracy falloff end range
// homingLockOnLimit = 2; 			// how many projectiles can be locked on to a player before they can no longer be locked on to by the projectile
// homingEscapeDistance = 25; 	// how far the projectile has to be to stop following the player, must be >= homingRadius
// homingAutomatic = true;			// lets the projectile look for targets itself

function Projectile::getDamagePercent(%obj)
{
	return 0.0;
}

function Projectile::getHackPosition(%obj)
{
	return %obj.getPosition();
}

function Projectile::homeLoop(%obj)
{
	if(isObject(%obj.lastObject))
		%obj.lastObject.delete();

	if(!isObject(%obj.client))
		return;
	
	if(!isObject(%obj))
		return;
	
	if(%obj.doneHoming)
		return;
	
	%client = %obj.client;
	%pos = %obj.getPosition();
	%muzzle = vectorLen(%obj.getVelocity());
	%found = %obj.target;

	if(%muzzle <= 1)
		return;
	
	if(!isObject(%obj.target) || %obj.target.getDamagePercent() >= 1.0)
	{
		if(%obj.homingAutomatic)
		{
			%found = -1;
			%obj.target = -1;

			%searchMasks = $TypeMasks::PlayerObjectType;
			InitContainerRadiusSearch(%pos, %obj.homingRadius, %searchMasks);
			while ((%searchObj = containerSearchNext()) != 0 )
			{
				if(miniGameCanDamage(%client,%searchObj))
				{
					if(%client == %searchObj.client)
						continue;
					
					if(minigameIsFriendly(%obj,%searchObj))
						continue;

					if(%searchObj.isCloaked)
						continue;

					if(isObject(%searchObj.lockOnSet) && %searchObj.lockOnSet.getCount() >= %obj.homingLockOnLimit && %obj.homingLockOnLimit > 0)
						continue;
					
					%found = %searchObj;
					break;
				}
			}
			
			if(isObject(%found))
				%obj.target = %found;
			else
			{
				%obj.hs = %obj.schedule(150, homeLoop);
				return;
			}
		}
		else
		{
			%obj.hs = %obj.schedule(150, homeLoop);
			return;
		}
	}
	
	%dist = vectorDist(%obj.getPosition(), %found.getHackPosition());

	if(%dist > %obj.homingRadius && %obj.homingEscapeDistance > 0)
	{
		if(%obj.homingEscapeDistance >= %obj.homingRadius && %dist > %obj.homingEscapeDistance)
		{
			%found = -1;
			%obj.target = -1;

			if(!%obj.homingCanRetry)
				return;
			
			%obj.hs = %obj.schedule(150, homeLoop);
			return;
		}
	}

	if(!isObject(%found.lockOnSet))
	{
		%found.lockOnSet = new SimSet();
		MissionCleanup.add(%found.lockOnSet);
	}
	
	if(%found.getType() & $TypeMasks::PlayerObjectType)
		%end = %found.getHackPosition();
	else
		%end = %found.getWorldBoxCenter();
	
	%vec = vectorNormalize(vectorSub(%end,%pos));
	
	//%addVec = vectorAdd(%obj.getVelocity(),vectorScale(%vec,180/vectorDist(%pos,%end)*(%muzzle*(%obj.homingAccuracy / 100))));

  %accu = %obj.homingAccuracyClose + ((mClampF(%dist - %obj.homingFarDist, 0, %obj.homingFarDist - %obj.homingCloseDist) / (%obj.homingFarDist - %obj.homingCloseDist))) * (%obj.homingAccuracy - %obj.homingAccuracyClose);
	%addVec = vectorAdd(vectorNormalize(%obj.getVelocity()), vectorScale(%vec, %accu / 100));
	%vec = vectorNormalize(%addVec);
	
	%p = new Projectile()
	{
		dataBlock = %obj.dataBlock;
		initialPosition = %pos;
		initialVelocity = vectorScale(%vec,%muzzle);
		sourceObject = %obj.sourceObject;
		client = %obj.client;
		sourceSlot = 0;
		originPoint = %obj.originPoint;
		isHoming = true;
		target = %obj.target;
		reflectTime = %obj.reflectTime;
		lastObject = %obj;
		homingRadius = %obj.homingRadius;
		homingLockOnLimit = %obj.homingLockOnLimit;
		homingAccuracy = %obj.homingAccuracy;
		homingAccuracyClose = %obj.homingAccuracyClose;
		homingCloseDist = %obj.homingCloseDist;
		homingFarDist = %obj.homingFarDist;
		homingEscapeDistance = %obj.homingEscapeDistance;
		homingCanRetry = %obj.homingCanRetry;
		homingAutomatic = %obj.homingAutomatic;
		homingTickTime = %obj.homingTickTime;
	};

	if(isObject(%p))
	{
		MissionCleanup.add(%p);
		%p.setScale(%obj.getScale());
		%time = %p.homingTickTime;

		if(%time < 32)
			%time = 32;

		%p.hs = %p.schedule(%time, homeLoop);
		%p.dataBlock.onHomeTick(%p);
		%found.lockOnSet.add(%p);
	}

	%obj.delete();
}

if(!isFunction(ProjectileData, onAdd))
	eval("function ProjectileData::onAdd(%this,%obj) { }");

if(!isFunction(ProjectileData, onHomeTick))
	eval("function ProjectileData::onHomeTick(%this,%obj) { }");

package ProjectileHome
{
	function Projectile::onAdd(%obj)
	{
		Parent::onAdd(%obj);

		%db = %obj.getdatablock();

		%db.onAdd(%obj);

		if(%db.homingProjectile && !%obj.isHoming)
		{
			if(%obj.homingRadius $= "") %obj.homingRadius = %db.homingRadius;
			if(%obj.homingLockOnLimit $= "") %obj.homingLockOnLimit = %db.homingLockOnLimit;
			if(%obj.homingAccuracy $= "") %obj.homingAccuracy = %db.homingAccuracy;
			if(%obj.homingAccuracyClose $= "") %obj.homingAccuracyClose = %db.homingAccuracyClose;
			if(%obj.homingCloseDist $= "") %obj.homingCloseDist = %db.homingCloseDist;
			if(%obj.homingFarDist $= "") %obj.homingFarDist = %db.homingFarDist;
			if(%obj.homingEscapeDistance $= "") %obj.homingEscapeDistance = %db.homingEscapeDistance;
			if(%obj.homingCanRetry $= "") %obj.homingCanRetry = %db.homingCanRetry;
			if(%obj.homingAutomatic $= "") %obj.homingAutomatic = %db.homingAutomatic;
			if(%obj.homingTickTime $= "") %obj.homingTickTime = %db.homingTickTime;

			%obj.homeLoop();
		}
	}

	function ProjectileData::onExplode(%db, %obj, %pos)
	{
		if(isObject(%obj.target))
		{
			%obj.doneHoming = true;
			cancel(%obj.hs);
		}

		Parent::onExplode(%db, %obj, %pos);
	}

	function Armor::onRemove(%db, %pl, %x)
	{
		if(isObject(%los = %pl.lockOnSet))
			%los.delete();

		return Parent::onRemove(%db, %pl, %x);
	}
};
activatePackage(ProjectileHome);

function minigameIsFriendly(%src, %col)
{
	if(isObject(%src.client))
		%src = %src.client;
	else if(%src.getClassName() !$= "GameConnection")
		return 0;

	if(%src == %col.client)
		return 1;
	
	%mg = getMinigameFromObject(%src);

	if(%mg != getMinigameFromObject(%col))
		return 0;

	if(%mg.ZAPT_enabled && (isObject(%col.client) || %col.getClassName() $= "GameConnection"))
		return 1;

	if(%mg.isSlayerMinigame)
	{
		if(%col.getType() & $TypeMasks::VehicleObjectType)
		{
			if(isObject(%col.spawnBrick))
				return %src.slyrTeam.isAlliedTeam(%mg.teams.getTeamFromName(%col.spawnBrick.getControllingTeam()));
			else
				return 0;
		}
		else
		{
			if(isObject(%col.client))
				return %src.slyrTeam.isAlliedTeam(%col.client.slyrTeam);
			else
				return (miniGameCanDamage(%src, %col) != 1);
		}
	}

	return 0;
}