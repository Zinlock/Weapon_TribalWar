datablock ParticleData(TW_LauncherExplosionParticle)
{
	dragCoefficient		= 3.5;
	windCoefficient		= 3.5;
	gravityCoefficient	= -1;
	inheritedVelFactor	= 0.0;
	constantAcceleration	= 0.0;
	lifetimeMS		= 2000;
	lifetimeVarianceMS	= 400;
	spinSpeed		= 25.0;
	spinRandomMin		= -25.0;
	spinRandomMax		= 25.0;
	useInvAlpha		= false;
	animateTexture		= false;
	//framesPerSec		= 1;

	textureName		= "base/data/particles/cloud";
	//animTexName		= "~/data/particles/cloud";

	// Interpolation variables
   colors[0]     = "1 1 1 0.1";
   colors[1]     = "0.9 0.5 0.0 0.3";
   colors[2]     = "0.1 0.05 0.025 0.1";
   colors[3]     = "0.1 0.05 0.025 0.0";

	sizes[0]	= 4.0;
	sizes[1]	= 6.3;
   sizes[2]	= 6.5;
   sizes[3]	= 4.5;

	times[0]	= 0.0;
	times[1]	= 0.1;
   times[2]	= 0.8;
   times[3]	= 1.0;
};

datablock ParticleEmitterData(TW_LauncherExplosionEmitter)
{
	ejectionPeriodMS = 1;
	periodVarianceMS = 0;
	lifeTimeMS	   = 21;
	ejectionVelocity = 8;
	velocityVariance = 3.0;
	ejectionOffset   = 2.0;
	thetaMin         = 00;
	thetaMax         = 180;
	phiReferenceVel  = 0;
	phiVariance      = 360;
	overrideAdvance = false;
	particles = "TW_LauncherExplosionParticle";
};

datablock ParticleData(TW_LauncherExplosionBlueParticle)
{
	dragCoefficient		= 3.5;
	windCoefficient		= 3.5;
	gravityCoefficient	= -1;
	inheritedVelFactor	= 0.0;
	constantAcceleration	= 0.0;
	lifetimeMS		= 2000;
	lifetimeVarianceMS	= 400;
	spinSpeed		= 25.0;
	spinRandomMin		= -25.0;
	spinRandomMax		= 25.0;
	useInvAlpha		= false;
	animateTexture		= false;
	//framesPerSec		= 1;

	textureName		= "base/data/particles/cloud";
	//animTexName		= "~/data/particles/cloud";

	// Interpolation variables
   colors[0]     = "1 1 1 0.1";
   colors[1]     = "0.0 0.5 0.9 0.3";
   colors[2]     = "0.025 0.05 0.1 0.1";
   colors[3]     = "0.025 0.05 0.1 0.0";

	sizes[0]	= 4.0;
	sizes[1]	= 6.3;
   sizes[2]	= 6.5;
   sizes[3]	= 4.5;

	times[0]	= 0.0;
	times[1]	= 0.1;
   times[2]	= 0.8;
   times[3]	= 1.0;
};

datablock ParticleEmitterData(TW_LauncherExplosionBlueEmitter)
{
	ejectionPeriodMS = 1;
	periodVarianceMS = 0;
	lifeTimeMS	   = 21;
	ejectionVelocity = 8;
	velocityVariance = 3.0;
	ejectionOffset   = 2.0;
	thetaMin         = 00;
	thetaMax         = 180;
	phiReferenceVel  = 0;
	phiVariance      = 360;
	overrideAdvance = false;
	particles = "TW_LauncherExplosionBlueParticle";
};

datablock ParticleData(TW_LauncherExplosionPlasmaParticle)
{
	dragCoefficient		= 3.5;
	windCoefficient		= 3.5;
	gravityCoefficient	= -1;
	inheritedVelFactor	= 0.0;
	constantAcceleration	= 0.0;
	lifetimeMS		= 2000;
	lifetimeVarianceMS	= 400;
	spinSpeed		= 25.0;
	spinRandomMin		= -25.0;
	spinRandomMax		= 25.0;
	useInvAlpha		= false;
	animateTexture		= false;

	textureName		= "base/data/particles/smoke";

	colors[0]     = "0 1 0 0.1";
	colors[1]     = "0.2 0.9 0.0 0.3";
	colors[2]     = "0.025 0.1 0.025 0.1";
	colors[3]     = "0.025 0.1 0.025 0.0";

	sizes[0]	= 4.0;
	sizes[1]	= 6.3;
	sizes[2]	= 6.5;
	sizes[3]	= 4.5;

	times[0]	= 0.0;
	times[1]	= 0.1;
	times[2]	= 0.8;
	times[3]	= 1.0;
};

datablock ParticleEmitterData(TW_LauncherExplosionPlasmaEmitter)
{
	ejectionPeriodMS = 1;
	periodVarianceMS = 0;
	lifeTimeMS	   = 21;
	ejectionVelocity = 8;
	velocityVariance = 3.0;
	ejectionOffset   = 0.35;
	thetaMin         = 00;
	thetaMax         = 90;
	phiReferenceVel  = 0;
	phiVariance      = 360;
	overrideAdvance = false;
	particles = "TW_LauncherExplosionPlasmaParticle";
};

datablock ParticleData(TW_LauncherFlashParticle)
{
	dragCoefficient      = 5;
	gravityCoefficient   = -0.5;
	inheritedVelFactor   = 0.2;
	constantAcceleration = 0.0;
	lifetimeMS           = 60;
	lifetimeVarianceMS   = 40;
	textureName          = "./dts/blastflash2";
	spinSpeed		= 10.0;
	spinRandomMin		= -500.0;
	spinRandomMax		= 500.0;

	colors[0]     = "1 1 1 1";
	colors[1]     = "0.9 0.5 0.0 0.9";

	sizes[0]      = 10.55;
	sizes[1]      = 18.55;

	useInvAlpha = false;
};

datablock ParticleEmitterData(TW_LauncherFlashEmitter)
{
	ejectionPeriodMS = 20;
	periodVarianceMS = 10;
	lifeTimeMS	   = 121;
	ejectionVelocity = 0.0;
	velocityVariance = 0.0;
	ejectionOffset   = 2.0;
	thetaMin         = 00;
	thetaMax         = 180;
	phiReferenceVel  = 0;
	phiVariance      = 360;
	overrideAdvance = false;
	particles = "TW_LauncherFlashParticle";
};

datablock ParticleData(TW_LauncherExplosionParticle2)
{
	dragCoefficient		= 5.0;
	windCoefficient		= 1.0;
	gravityCoefficient	= -0.50;
	inheritedVelFactor	= 0.0;
	constantAcceleration	= 0.0;
	lifetimeMS		= 1000;
	lifetimeVarianceMS	= 400;
	spinSpeed		= 5.0;
	spinRandomMin		= -5.0;
	spinRandomMax		= 5.0;
	useInvAlpha		= true;
	animateTexture		= false;

	textureName		= "base/data/particles/cloud";

	colors[0]     = "0.95 0.9 0.8 0.4";
	colors[1]     = "0.1 0.05 0.025 0.1";
	colors[2]     = "0.1 0.05 0.025 0.0";

	sizes[0]	= 7.0;
	sizes[1]	= 10.0;
	sizes[2]	= 10.5;

	times[0]	= 0.0;
	times[1]	= 0.02;
	times[2]	= 1.0;
};

datablock ParticleEmitterData(TW_LauncherExplosionEmitter2)
{
   ejectionPeriodMS = 1;
   periodVarianceMS = 0;
   lifeTimeMS	   = 21;
   ejectionVelocity = 12;
   velocityVariance = 2.0;
   ejectionOffset   = 1.0;
   thetaMin         = 0;
   thetaMax         = 180;
   phiReferenceVel  = 30;
   phiVariance      = 32;
   overrideAdvance = false;
   particles = "TW_LauncherExplosionParticle2";
};

datablock ParticleData(TW_MortarSmokeParticle)
{
	dragCoefficient		= 3.5;
	windCoefficient		= 3.5;
	gravityCoefficient	= 0;
	inheritedVelFactor	= 0;
	constantAcceleration	= 0.0;
	lifetimeMS		= 2500;
	lifetimeVarianceMS	= 0;
	spinSpeed		= 40.0;
	spinRandomMin		= -40.0;
	spinRandomMax		= 40.0;
	useInvAlpha		= true;
	animateTexture		= false;

	textureName		= "base/data/particles/cloud";

	colors[0]     = "0 0 0 0.1";
	colors[1]     = "0 0 0 0.2";
	colors[2]     = "0 0 0 0.1";
	colors[3]     = "0 0 0 0.0";

	sizes[0]	= 4.2;
	sizes[1]	= 6.2;
	sizes[2]	= 4.2;
	sizes[3]	= 2.0;

	times[0]	= 0.0;
	times[1]	= 0.1;
	times[2]	= 0.8;
	times[3]	= 1.0;
};

datablock ParticleEmitterData(TW_MortarSmokeEmitter)
{
	ejectionPeriodMS = 1;
	periodVarianceMS = 0;
	ejectionVelocity = 26;
	velocityVariance = 12.0;
	ejectionOffset   = 0.5;
	thetaMin         = 0;
	thetaMax         = 180;
	phiReferenceVel  = 0;
	phiVariance      = 360;
	overrideAdvance = false;
	particles = "TW_MortarSmokeParticle";
};

datablock ParticleData(TW_MortarExplosionParticle)
{
	dragCoefficient		= 3.5;
	windCoefficient		= 3.5;
	gravityCoefficient	= 0;
	inheritedVelFactor	= 0;
	constantAcceleration	= 0.0;
	lifetimeMS		= 2000;
	lifetimeVarianceMS	= 0;
	spinSpeed		= 40.0;
	spinRandomMin		= -40.0;
	spinRandomMax		= 40.0;
	useInvAlpha		= false;
	animateTexture		= false;

	textureName		= "base/data/particles/cloud";

	colors[0]     = "1 1 1 0.5";
	colors[1]     = "0.1 0.9 0.0 0.9";
	colors[2]     = "0.1 0.1 0.0 0.2";
	colors[3]     = "0.1 0.1 0.0 0.0";

	sizes[0]	= 4.2;
	sizes[1]	= 6.2;
	sizes[2]	= 4.2;
	sizes[3]	= 2.0;

	times[0]	= 0.0;
	times[1]	= 0.1;
	times[2]	= 0.8;
	times[3]	= 1.0;
};

datablock ParticleEmitterData(TW_MortarExplosionEmitter)
{
	ejectionPeriodMS = 2;
	periodVarianceMS = 0;
	ejectionVelocity = 15;
	velocityVariance = 7.0;
	ejectionOffset   = 0.0;
	thetaMin         = 0;
	thetaMax         = 180;
	phiReferenceVel  = 0;
	phiVariance      = 360;
	overrideAdvance = false;
	particles = "TW_MortarExplosionParticle";
};

datablock ParticleData(TW_MortarTrailParticle)
{
	dragCoefficient		= 3.5;
	windCoefficient		= 3.5;
	gravityCoefficient	= -0.25;
	inheritedVelFactor	= 0.2;
	constantAcceleration	= 0.0;
	lifetimeMS		= 2000;
	lifetimeVarianceMS	= 0;
	spinSpeed		= 400.0;
	spinRandomMin		= -400.0;
	spinRandomMax		= 400.0;
	useInvAlpha		= true;
	animateTexture		= false;

	textureName		= "base/data/particles/cloud";

	colors[0]     = "0.3 0.5 0.3 0.5";
	colors[1]     = "0.2 0.7 0.3 0.7";
	colors[2]     = "0.2 0.1 0.1 0.4";
	colors[3]     = "0.0 0.1 0.0 0.0";

	sizes[0]	= 0.5;
	sizes[1]	= 1.2;
	sizes[2]	= 2.5;
	sizes[3]	= 2.0;

	times[0]	= 0.0;
	times[1]	= 0.1;
	times[2]	= 0.8;
	times[3]	= 1.0;
};

datablock ParticleEmitterData(TW_MortarTrailEmitter)
{
	ejectionPeriodMS = 15;
	periodVarianceMS = 0;
	ejectionVelocity = 0;
	velocityVariance = 0.0;
	ejectionOffset   = 0.0;
	thetaMin         = 0;
	thetaMax         = 5;
	phiReferenceVel  = 0;
	phiVariance      = 360;
	overrideAdvance = false;
	particles = "TW_MortarTrailParticle";
};