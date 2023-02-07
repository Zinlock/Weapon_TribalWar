datablock StaticShapeData(TW_BulletTrail) { shapeFile = "./dts/bullet_trail.dts"; };

function TW_BulletTrail::onAdd(%this,%obj)
{
  %obj.schedule(0, playThread, 2, root);
  %obj.schedule(2000,delete);
}

datablock StaticShapeData(TW_ElectricTrail) { shapeFile = "./dts/electric_trail.dts"; };

function TW_ElectricTrail::onAdd(%this,%obj)
{
  %obj.schedule(0, playThread, 2, root);
  %obj.schedule(2000,delete);
}

datablock StaticShapeData(TW_PlasmaTrail) { shapeFile = "./dts/plasma_trail.dts"; };

function TW_PlasmaTrail::onAdd(%this,%obj)
{
  %obj.schedule(0, playThread, 2, root);
  %obj.schedule(2000,delete);
}

datablock StaticShapeData(TW_LaserTrail) { shapeFile = "./dts/laser_trail.dts"; };

function TW_LaserTrail::onAdd(%this,%obj)
{
  %obj.schedule(0, playThread, 2, root);
  %obj.schedule(2000,delete);
}

datablock ParticleData(TW_LauncherTrailParticle)
{
	dragCoefficient		= 3.5;
	windCoefficient		= 3.5;
	gravityCoefficient	= 0;
	inheritedVelFactor	= -0.1;
	constantAcceleration	= 0.0;
	lifetimeMS		= 800;
	lifetimeVarianceMS	= 400;
	spinSpeed		= 400.0;
	spinRandomMin		= -400.0;
	spinRandomMax		= 400.0;
	useInvAlpha		= false;
	animateTexture		= false;

	textureName		= "base/data/particles/cloud";

	colors[0]     = "1 1 1 0.0";
	colors[1]     = "0.5 0.5 0.5 0.3";
	colors[2]     = "0.1 0.1 0.1 0.1";
	colors[3]     = "0.1 0.1 0.1 0.0";

	sizes[0]	= 0.5;
	sizes[1]	= 0.7;
	sizes[2]	= 1.0;
	sizes[3]	= 0.8;

	times[0]	= 0.0;
	times[1]	= 0.1;
	times[2]	= 0.8;
	times[3]	= 1.0;
};

datablock ParticleEmitterData(TW_LauncherTrailEmitter)
{
	ejectionPeriodMS = 10;
	periodVarianceMS = 0;
	ejectionVelocity = 0;
	velocityVariance = 0.0;
	ejectionOffset   = 0.0;
	thetaMin         = 0;
	thetaMax         = 5;
	phiReferenceVel  = 0;
	phiVariance      = 360;
	overrideAdvance = false;
	particles = "TW_LauncherTrailParticle";
};

datablock ParticleData(TW_LauncherTrailFireParticle)
{
	dragCoefficient		= 3.5;
	windCoefficient		= 3.5;
	gravityCoefficient	= 0;
	inheritedVelFactor	= -0.25;
	constantAcceleration	= 0.0;
	lifetimeMS		= 800;
	lifetimeVarianceMS	= 400;
	spinSpeed		= 400.0;
	spinRandomMin		= -400.0;
	spinRandomMax		= 400.0;
	useInvAlpha		= false;
	animateTexture		= false;

	textureName		= "base/data/particles/cloud";

	colors[0]     = "1 1 1 0.1";
	colors[1]     = "0.9 0.5 0.0 0.3";
	colors[2]     = "0.1 0.05 0.025 0.1";
	colors[3]     = "0.1 0.05 0.025 0.0";

	sizes[0]	= 0.5;
	sizes[1]	= 1.0;
	sizes[2]	= 1.2;
	sizes[3]	= 0.8;

	times[0]	= 0.0;
	times[1]	= 0.1;
	times[2]	= 0.8;
	times[3]	= 1.0;
};

datablock ParticleEmitterData(TW_LauncherTrailFireEmitter)
{
	ejectionPeriodMS = 6;
	periodVarianceMS = 0;
	ejectionVelocity = 0;
	velocityVariance = 0.0;
	ejectionOffset   = 0.0;
	thetaMin         = 0;
	thetaMax         = 5;
	phiReferenceVel  = 0;
	phiVariance      = 360;
	overrideAdvance = false;
	particles = "TW_LauncherTrailFireParticle";
};

datablock ParticleData(TW_LauncherTrailPlasmaParticle)
{
	dragCoefficient		= 3.5;
	windCoefficient		= 3.5;
	gravityCoefficient	= 0;
	inheritedVelFactor	= -0.25;
	constantAcceleration	= 0.0;
	lifetimeMS		= 800;
	lifetimeVarianceMS	= 400;
	spinSpeed		= 400.0;
	spinRandomMin		= -400.0;
	spinRandomMax		= 400.0;
	useInvAlpha		= false;
	animateTexture		= false;

	textureName		= "base/data/particles/cloud";

	colors[0]     = "0 1 0 0.1";
	colors[1]     = "0.2 0.9 0.0 0.3";
	colors[2]     = "0.025 0.1 0.025 0.1";
	colors[3]     = "0.025 0.1 0.025 0.0";

	sizes[0]	= 2.5;
	sizes[1]	= 1.2;
	sizes[2]	= 1.1;
	sizes[3]	= 1.0;

	times[0]	= 0.0;
	times[1]	= 0.1;
	times[2]	= 0.8;
	times[3]	= 1.0;
};

datablock ParticleEmitterData(TW_LauncherTrailPlasmaEmitter)
{
	ejectionPeriodMS = 6;
	periodVarianceMS = 0;
	ejectionVelocity = 0;
	velocityVariance = 0.0;
	ejectionOffset   = 0.0;
	thetaMin         = 0;
	thetaMax         = 5;
	phiReferenceVel  = 0;
	phiVariance      = 360;
	overrideAdvance = false;
	particles = "TW_LauncherTrailPlasmaParticle";
};

datablock ParticleData(TW_LauncherTrailFusionParticle)
{
	dragCoefficient		= 3.5;
	windCoefficient		= 3.5;
	gravityCoefficient	= 0;
	inheritedVelFactor	= -0.25;
	constantAcceleration	= 0.0;
	lifetimeMS		= 2500;
	lifetimeVarianceMS	= 400;
	spinSpeed		= 400.0;
	spinRandomMin		= -400.0;
	spinRandomMax		= 400.0;
	useInvAlpha		= true;
	animateTexture		= false;

	textureName		= "base/data/particles/cloud";

	colors[0]     = "0 1 0 0.0";
	colors[1]     = "0.1 0.3 0.0 0.13";
	colors[2]     = "0.0 0.025 0.0 0.075";
	colors[3]     = "0.0 0.0 0.0 0.1";

	sizes[0]	= 2.2;
	sizes[1]	= 1.2;
	sizes[2]	= 1.0;
	sizes[3]	= 0.8;

	times[0]	= 0.0;
	times[1]	= 0.1;
	times[2]	= 0.8;
	times[3]	= 1.0;
};

datablock ParticleEmitterData(TW_LauncherTrailFusionEmitter)
{
	ejectionPeriodMS = 4;
	periodVarianceMS = 0;
	ejectionVelocity = 0;
	velocityVariance = 0.0;
	ejectionOffset   = 0.0;
	thetaMin         = 0;
	thetaMax         = 5;
	phiReferenceVel  = 0;
	phiVariance      = 360;
	overrideAdvance = false;
	particles = "TW_LauncherTrailFusionParticle";
};

datablock ParticleData(TW_LauncherTrailSpinParticle)
{
	dragCoefficient		= 3.5;
	windCoefficient		= 3.5;
	gravityCoefficient	= 0;
	inheritedVelFactor	= -0.25;
	constantAcceleration	= 0.0;
	lifetimeMS		= 550;
	lifetimeVarianceMS	= 0;
	spinSpeed		= 400.0;
	spinRandomMin		= -400.0;
	spinRandomMax		= 400.0;
	useInvAlpha		= false;
	animateTexture		= false;

	textureName		= "base/data/particles/dot";

	colors[0]     = "1 1 1 0.1";
	colors[1]     = "0.0 0.5 0.9 0.3";
	colors[2]     = "0.025 0.05 0.1 0.1";
	colors[3]     = "0.025 0.05 0.1 0.0";

	sizes[0]	= 1.2;
	sizes[1]	= 0.5;
	sizes[2]	= 0.2;
	sizes[3]	= 0.0;

	times[0]	= 0.0;
	times[1]	= 0.1;
	times[2]	= 0.8;
	times[3]	= 1.0;
};

datablock ParticleEmitterData(TW_LauncherTrailSpinEmitter)
{
	ejectionPeriodMS = 5;
	periodVarianceMS = 0;
	ejectionVelocity = 0;
	velocityVariance = 0.0;
	ejectionOffset   = 0.0;
	thetaMin         = 0;
	thetaMax         = 5;
	phiReferenceVel  = 0;
	phiVariance      = 360;
	overrideAdvance = false;
	particles = "TW_LauncherTrailSpinParticle";
};

datablock ParticleData(TW_LauncherTrailLaserParticle)
{
	dragCoefficient		= 3.5;
	windCoefficient		= 3.5;
	gravityCoefficient	= 0;
	inheritedVelFactor	= -0.25;
	constantAcceleration	= 0.0;
	lifetimeMS		= 550;
	lifetimeVarianceMS	= 0;
	spinSpeed		= 400.0;
	spinRandomMin		= -400.0;
	spinRandomMax		= 400.0;
	useInvAlpha		= false;
	animateTexture		= false;

	textureName		= "base/data/particles/dot";

	colors[0]     = "1 0.0 0.0 0.1";
	colors[1]     = "0.9 0.0 0.1 0.3";
	colors[2]     = "0.1 0.05 0.025 0.1";
	colors[3]     = "0.05 0.0 0.0 0.0";

	sizes[0]	= 1.2;
	sizes[1]	= 0.5;
	sizes[2]	= 0.2;
	sizes[3]	= 0.0;

	times[0]	= 0.0;
	times[1]	= 0.1;
	times[2]	= 0.8;
	times[3]	= 1.0;
};

datablock ParticleEmitterData(TW_LauncherTrailLaserEmitter)
{
	ejectionPeriodMS = 3;
	periodVarianceMS = 0;
	ejectionVelocity = 0;
	velocityVariance = 0.0;
	ejectionOffset   = 0.0;
	thetaMin         = 0;
	thetaMax         = 5;
	phiReferenceVel  = 0;
	phiVariance      = 360;
	overrideAdvance = false;
	particles = "TW_LauncherTrailLaserParticle";
};

datablock ParticleData(TW_FlamerTrailParticle)
{
	dragCoefficient		= 5;
	windCoefficient		= 3.5;
	gravityCoefficient	= -1;
	inheritedVelFactor	= 0.0;
	constantAcceleration	= 0.0;
	lifetimeMS		= 800;
	lifetimeVarianceMS	= 400;
	spinSpeed		= 400.0;
	spinRandomMin		= -400.0;
	spinRandomMax		= 400.0;
	useInvAlpha		= false;
	animateTexture		= false;

	textureName		= "base/data/particles/cloud";

	colors[0]     = "0.4 0.4 0.4 0.0";
	colors[1]     = "0.9 0.5 0.2 0.1";
	colors[2]     = "0.1 0.05 0.025 0.1";
	colors[3]     = "0.1 0.1 0.1 0.0";

	sizes[0]	= 0.7;
	sizes[1]	= 1.5;
	sizes[2]	= 2.8;
	sizes[3]	= 5.0;

	times[0]	= 0.0;
	times[1]	= 0.1;
	times[2]	= 0.8;
	times[3]	= 1.0;
};

datablock ParticleEmitterData(TW_FlamerTrailEmitter)
{
	ejectionPeriodMS = 1;
	lifeTimeMS = 100;
	periodVarianceMS = 0;
	ejectionVelocity = -50;
	velocityVariance = 30.0;
	ejectionOffset   = 1.0;
	thetaMin         = 0;
	thetaMax         = 15;
	phiReferenceVel  = 0;
	phiVariance      = 360;
	overrideAdvance = false;
	particles = "TW_FlamerTrailParticle";
};

datablock ParticleData(TW_RocketBackblastParticle)
{
	dragCoefficient      = 6;
	gravityCoefficient   = 0.0;
	inheritedVelFactor   = 0;
	constantAcceleration = 0.0;
	lifetimeMS           = 1400;
	lifetimeVarianceMS   = 1305;
	textureName          = "base/data/particles/cloud";
	spinSpeed		= 10.0;
	spinRandomMin		= -150.0;
	spinRandomMax		= 150.0;
	colors[0]     = "1.0 0.2 0.0 0.2";
	colors[1]     = "1.0 1 0.9 0.1";
   colors[2]     = "0.20 0.20 0.20 0.05";
   colors[3]     = "0.0 0.0 0.0 0.0";

	sizes[0]      = 1.75;
	sizes[1]      = 0.35;
   sizes[2]      = 0.15;
 	sizes[3]      = 0;

   times[0] = 0.0;
   times[1] = 0.02;
   times[2] = 0.9;
   times[3] = 1.0;

	useInvAlpha = false;
};

datablock ParticleEmitterData(TW_RocketBackblastEmitter)
{
   ejectionPeriodMS = 1;
   periodVarianceMS = 0;
   ejectionVelocity = -15.00;
   velocityVariance = 14.0;
   ejectionOffset   = 0;
   thetaMin         = 0;
   thetaMax         = 1;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvance = false;
   particles = "TW_RocketBackblastParticle";
};