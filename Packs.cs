package TWPacks
{
	function Armor::onAdd(%db, %pl)
	{
		Parent::onAdd(%db, %pl);

		if(isObject(%pl) && %pl.IsA("Player"))
			%pl.schedule(0, TWPackLoop);
	}
};
activatePackage(TWPacks);

function Player::TWPackLoop(%pl)
{
	cancel(%pl.twpl);

	%db = %pl.getDatablock();

	for(%i = 0; %i < %db.maxTools; %i++)
	{
		%itm = %pl.tool[%i];
		if(%itm.isPackItem)
		{
			cancel(%pl.packSchedule[%itm]);

			if(!%pl.hasPack[%itm])
			{
				%pl.hasPack[%itm] = true;
				%itm.onPackPickup(%pl);
			}

			%pl.packSchedule[%itm] = %itm.schedule(200, onPackLost, %pl);

			%itm.onPackTick(%pl);
		}
	}

	%pl.twpl = %pl.schedule(100, TWPackLoop);
}

function ItemData::onPackPickup(%db, %pl) { }
function ItemData::onPackTick(%db, %pl) { }
function ItemData::onPackLost(%db, %pl) { }

function WeaponImage::twPackDesc(%db, %pl, %slot)
{
	if(isObject(%itm = %db.item) && %pl.IsA("Player"))
	{
		%pl.client.centerPrint("<font:impact:16><color:4477FF>" @ %itm.title @ "<br><font:arial:16><color:FFFFFF>" @ %itm.description, 3);
	}
}