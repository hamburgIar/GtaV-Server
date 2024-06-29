import * as alt from 'alt-client'
import * as native from 'natives'
import * as main from './main.js'

alt.onServer("SERVER::CLIENT::LeaveVehicle", () => {
    native.taskLeaveAnyVehicle(main.localPlayer, 0, 16|64)
})

alt.onServer("SERVER::CLIENT::GetCamPos", () => {
    const camPos = alt.getCamPos();
    const camRot = native.getGameplayCamRot(2);
    
    console.log(`CamPos: ${camPos.x}, ${camPos.y}, ${camPos.z}`);
    console.log(`CamRot: ${camRot.x}, ${camRot.y}, ${camRot.z}`);
})