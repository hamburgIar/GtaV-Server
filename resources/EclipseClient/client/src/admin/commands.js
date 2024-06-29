import * as alt from 'alt-client'
import * as native from 'natives'
import * as main from '../player/main.js'
import * as drawText from '../utils/drawText.js'

let isVehInfoWork = false;
let VehTickNum = null;

alt.onServer("SERVER::CLIENT::FreezePlayer", () => {
    native.freezeEntityPosition(main.localPlayer.scriptID, true)
})

alt.onServer("SERVER::CLIENT::UnFreezePlayer", () => {
    native.freezeEntityPosition(main.localPlayer.scriptID, false)
})

alt.onServer("SERVER::CLIENT::3DVehInfo", () => {
    isVehInfoWork = !isVehInfoWork;

    if (isVehInfoWork) {
        VehTickNum = alt.everyTick(() => {
            try {
                const vehicles = alt.Vehicle.streamedIn;

                for (let i = 0; i < vehicles.length; i++) {
                    if (vehicles[i].valid) {
                        const vehicleModel = native.getDisplayNameFromVehicleModel(vehicles[i].model);
                        const vehiclePos = { ...vehicles[i].pos };
                        const vehicleSpeed = vehicles[i].speed * 3.6;

                        const driverScriptId = native.getPedInVehicleSeat(vehicles[i].scriptID, -1, false);
                        const driverId = driverScriptId === 0 ? "Нету" : alt.Player.getByScriptID(driverScriptId).remoteID;

                        drawText.text3d(`Model: ${vehicleModel.toLowerCase()} | DriverId: ${driverId} | VehicleID: ${vehicles[i].remoteID} | Speed: ${Math.round(vehicleSpeed)}`, vehiclePos.x, vehiclePos.y, vehiclePos.z, 0.3, 4, 255, 255, 255, 255);
                    }
                }
            } catch (error) {
                alt.clearEveryTick(VehTickNum);
                VehTickNum = null;
            }
        });
    } else {
        if (VehTickNum !== null) {
            alt.clearEveryTick(VehTickNum);
            VehTickNum = null;
        }
    }
});


alt.onServer("SERVER::CLIENT::GetWaypointCoord", async () => {
    const waypoint = native.getFirstBlipInfoId(8);

    if (!native.doesBlipExist(waypoint)) {
        return;
    }

    const coords = native.getBlipInfoIdCoord(waypoint);
    
    alt.emitServer("CLIENT::SERVER::WaypointCoord", coords)
})