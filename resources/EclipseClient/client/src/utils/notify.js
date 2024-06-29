import * as alt from 'alt-client'

alt.onServer("SERVER::CLIENT::Notify", async (type, mess, time) => {
    alt.log("Type: " + type + ", mess: " + mess + ", time: " + time);    
})