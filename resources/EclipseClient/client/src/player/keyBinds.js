import * as alt from 'alt-client'

let isCursorVisible = false;

alt.on("keydown", (key) => {
    if (key === 114) {
        isCursorVisible = !isCursorVisible;
        alt.showCursor(isCursorVisible);
        alt.toggleGameControls(!isCursorVisible);
    }
})