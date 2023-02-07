//Game UI sounds added as 3D sound datablocks instead of 2D

datablock AudioProfile(Block_Admin_Sound)
{
	filename = "base/data/sound/admin.wav";
	description = AudioClosest3d;
	preload = false;
};

datablock AudioProfile(Block_Break_Sound : Block_Admin_Sound)
{
	filename = "base/data/sound/breakBrick.wav";
};
datablock AudioProfile(Block_Clear_Sound : Block_Admin_Sound)
{
	filename = "base/data/sound/brickClear.wav";
};
datablock AudioProfile(Block_ChangeBrick_Sound : Block_Admin_Sound)
{
	filename = "base/data/sound/clickChange.wav";
};
datablock AudioProfile(Block_MoveBrick_Sound : Block_Admin_Sound)
{
	filename = "base/data/sound/clickMove.wav";
};
datablock AudioProfile(Block_PlantBrick_Sound : Block_Admin_Sound)
{
	filename = "base/data/sound/clickPlant.wav";
};
datablock AudioProfile(Block_RotateBrick_Sound : Block_Admin_Sound)
{
	filename = "base/data/sound/clickRotate.wav";
};
datablock AudioProfile(Block_SMoveBrick_Sound : Block_Admin_Sound)
{
	filename = "base/data/sound/clickSuperMove.wav";
};
datablock AudioProfile(Block_Connect_Sound : Block_Admin_Sound)
{
	filename = "base/data/sound/playerConnect.wav";
};
datablock AudioProfile(Block_Leave_Sound : Block_Admin_Sound)
{
	filename = "base/data/sound/playerLeave.wav";
};
datablock AudioProfile(Block_Complete_Sound : Block_Admin_Sound)
{
	filename = "base/data/sound/processComplete.wav";
};
datablock AudioProfile(Block_LoadEnd_Sound : Block_Admin_Sound)
{
	filename = "base/data/sound/uploadEnd.wav";
};
datablock AudioProfile(Block_LoadStart_Sound : Block_Admin_Sound)
{
	filename = "base/data/sound/uploadStart.wav";
};
