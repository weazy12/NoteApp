import {combineReducers, configureStore} from "@reduxjs/toolkit";
import noteReducer from './reducers/NoteSlice.ts'

const rootReducer = combineReducers({
    noteReducer
});


export const setupStore = () =>{
    return configureStore({
        reducer: rootReducer
    })
}
export type RootState = ReturnType<typeof rootReducer>
export type AppStore = ReturnType<typeof setupStore>
export type AppDispatch = AppStore['dispatch']