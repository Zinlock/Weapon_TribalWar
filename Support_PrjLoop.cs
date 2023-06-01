package Support_PrjLoop
{
	function Projectile::onAdd(%obj)
	{
		%db = %obj.getdatablock();
		
		if(%db.PrjLoop_enabled)
			%obj.PrjLoop_Tick = %obj.schedule(%db.PrjLoop_tickTime, PrjLoop_tick);
		
		Parent::onAdd(%obj);
	}
};
activatePackage(Support_PrjLoop);

function Projectile::PrjLoop_tick(%prj)
{
	%db = %prj.getDatablock();
	%db.PrjLoop_onTick(%prj);
	%prj.ticks++;
	
	if(%prj.ticks >= %db.PrjLoop_maxTicks && %db.PrjLoop_maxTicks != -1)
	{
		if(%db.PrjLoop_killAfterLimit)
		{
			%prj.delete();
		}
		return;
	}

	%prj.PrjLoop_Tick = %prj.schedule(%db.PrjLoop_TickTime, PrjLoop_Tick);
}

function ProjectileData::PrjLoop_onTick(%db, %prj)
{
	// Put whatever code you want in your custom projectile's datablock and not here
	// This just prevents warnings in console
}

function PrjLoop_emitPrj(%origPrj, %emittedPrj, %speed, %amount)
{
	for(%i = 0; %i < %amount; %i++)
	{
		%a = getRandom(0,360) / 360 * 2 * $pi;
		%b = getRandom(0,360) / 360 * 2 * $pi;
		
		%vec = mcos(%a) SPC msin(%a) SPC mcos(%b);
		
		%p = new Projectile()
		{
			dataBlock = %emittedPrj;
			
			initialVelocity = vectorScale(%vec, %speed);
			initialPosition = %origPrj.getPosition();
			
			sourceObject = %origPrj.sourceObject;
			sourceSlot = %origPrj.sourceSlot;
			client = %origPrj.client;
		};
	}
}
