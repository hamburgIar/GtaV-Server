import * as alt from 'alt-client'
import * as native from 'natives'
import * as main from './main.js'

let authCam = null;

const authCamPosition = { x: 383.6353759765625, y: -368.37457275390625, z: 47.49184036254883 }
const authCamRotation = { x: 3.125769853591919, y: 0.05573022738099098, z: 8.965824127197266 }

alt.onServer("SERVER::CLIENT::AuthClient", async () => {
    native.freezeEntityPosition(main.localPlayer.scriptID, true);

    authCam = native.createCamWithParams("DEFAULT_SCRIPTED_CAMERA", authCamPosition.x, authCamPosition.y, authCamPosition.z, authCamRotation.x, authCamRotation.y, authCamRotation.z, 60, true, 0);

    native.pointCamAtCoord(authCam, authCamPosition.x, authCamPosition.y, authCamPosition.z);
    native.setCamActive(authCam, true);
    native.renderScriptCams(true, false, 0, false, false, 0);
    native.displayRadar(false);
    
    alt.toggleGameControls(false);

    main.mainBrowser.emit("CLIENT::CEF::OpenPage", "Auth", true);

    alt.showCursor(true);
})

async function destroyAuth() {
    native.renderScriptCams(0, false, 0, false, false, 0);

    alt.showCursor(false);
    alt.toggleGameControls(true);

    native.freezeEntityPosition(main.localPlayer.scriptID, false);
    native.displayRadar(true);

    main.mainBrowser.emit("CLIENT::CEF::OpenPage", "Auth", false)
}

alt.onServer("SERVER::CLIENT::AuthError", async (error) => {
    main.mainBrowser.emit("CLIENT::CEF::AuthError", error)
})

alt.onServer("SERVER::CLIENT::SLogin", async () => {
    await destroyAuth();
    
    console.log("Login:", main.login, "Promo:", main.regPromo, "Admin:", main.isAdmin, "AdminLvl:", main.adminLvl, "LoggedIn:", main.loggedIn);
})

alt.onServer("SERVER::CLIENT::SRegistration", async () => {
    await destroyAuth();

    main.mainBrowser.emit("CLIENT::CEF::OpenPage", "CharacterCreation", true);
})