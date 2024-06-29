import * as alt from 'alt-client'

export const localPlayer = alt.Player.local;

export let loggedIn = false;
export let isAdmin = false;
export let mainBrowser = null;
export let adminLvl = 0;
export let login = "";
export let regPromo = "";

function setMainBrowser() {
    if (mainBrowser != null) {
        mainBrowser.destroy();
        mainBrowser = null;
    }
    
    //'http://resource/client/cef/dist/index.html'
    mainBrowser = new alt.WebView("http://localhost:5173/");
    mainBrowser.focus();

    mainBrowser.on("CEF::CLIENT::SendToServer", (eventName, ...data) => {
        alt.emitServer(eventName, ...data);
    });
}

export function SetMaxStats() {
    alt.setStat("stamina", 100);
    alt.setStat("strength", 100);
    alt.setStat("lung_capacity", 100);
    alt.setStat("wheelie_ability", 100);
    alt.setStat("flying_ability", 100);
    alt.setStat("shooting_ability", 100);
    alt.setStat("stealth_ability", 100);
}

export function SetInfo(_isAdmin, _adminLvl, _loggedIn = true, _login, _regPromo) {
    isAdmin = _isAdmin;
    adminLvl = _adminLvl;
    loggedIn = _loggedIn;
    login = _login;
    regPromo = _regPromo;
}

setMainBrowser();
SetMaxStats();