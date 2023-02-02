// Support_ArmingDelayFix by Oxy (260031)
// This small script fixes ballistic projectiles not exploding when their
// arming delay runs out while they are stationary

package ArmingDelayFix
{
	function Projectile::onAdd(%obj)
	{
		Parent::onAdd(%obj);

		%db = %obj.getDataBlock();
		if(%db.isBallistic && %db.armingDelay > 0)
			%obj.schedule(%db.armingDelay * 32, armCheck);
	}
};
activatePackage(ArmingDelayFix);

function Projectile::armCheck(%obj)
{
	if(vectorLen(%obj.getVelocity()) < 0.05)
		%obj.explode();
}