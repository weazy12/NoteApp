import type {CreateNoteDto, NoteDto} from "../../models/note.ts";
import {createAsyncThunk, createSlice} from "@reduxjs/toolkit";
import axios from "axios";
import {API_BASE_URL} from "../../const/api.ts";

interface NotesState {
    notes: NoteDto[];
    loading: boolean;
    error: string | null;
    selectedNote: NoteDto| null;
}

const initialState: NotesState = {
    notes: [],
    loading: false,
    error: null,
    selectedNote: null,
}


export const fetchNotes = createAsyncThunk(
    'notes/fetchAllNotes',
    async (_, thunkAPI) =>{
        try{
            const response = await axios.get<NoteDto[]>(API_BASE_URL);
            return response.data;
        } catch(e){
            return thunkAPI.rejectWithValue(`Cannot load notes. with message ${e}`);
        }
    }
)
export const createNote = createAsyncThunk(
    'notes/createNote',
    async (note: CreateNoteDto, thunkAPI) => {
        try {
            const response = await axios.post<NoteDto>(API_BASE_URL, note);
            return response.data;
        } catch (e) {
            return thunkAPI.rejectWithValue(`Cannot create note with message ${e}.`);
        }
    }
);
export const deleteNote = createAsyncThunk(
    'notes/deleteNote',
    async (id: number, thunkAPI) => {
        try {
            await axios.delete(`${API_BASE_URL}/${id}`);
            return id; // повертаємо id, щоб видалити з state
        } catch (e) {
            return thunkAPI.rejectWithValue(`Cannot delete note. ${e}`);
        }
    }
);

export const updateNote = createAsyncThunk(
    'notes/updateNote',
    async (note: NoteDto, thunkAPI) => {
        try {
            const response = await axios.put<NoteDto>(`${API_BASE_URL}/${note.id}`, {
                title: note.title,
                content: note.content
            });
            return response.data;
        } catch (e) {
            return thunkAPI.rejectWithValue(`Cannot update note. ${e}`);
        }
    }
);

export const noteSlice = createSlice({
    name: 'notes',
    initialState,
    reducers:{
        setSelectedNote: (state, action) => {
            state.selectedNote = action.payload;
        },
        clearSelectedNote: (state) => {
            state.selectedNote = null;
        }
    },
    extraReducers: (builder) => {
        builder
            //Fetch all notes
            .addCase(fetchNotes.pending, (state) => {
                state.loading = true;
                state.error = null;
            })
            .addCase(fetchNotes.fulfilled, (state, action) => {
                state.loading = false;
                state.notes = action.payload;
            })
            .addCase(fetchNotes.rejected, (state, action) => {
                state.loading = false;
                state.error = action.payload as string;
            })
            //Create note
            .addCase(createNote.pending, (state) => {
                state.loading = true;
                state.error = null;
            })
            .addCase(createNote.fulfilled, (state, action) => {
                state.loading = false;
                state.notes.unshift(action.payload);
            })
            .addCase(createNote.rejected, (state, action) => {
                state.loading = false;
                state.error = action.payload as string;
            })
            // Delete note
            .addCase(deleteNote.pending, (state) => {
                state.loading = true;
                state.error = null;
            })
            .addCase(deleteNote.fulfilled, (state, action) => {
                state.loading = false;
                state.notes = state.notes.filter(n => n.id !== action.payload);
            })
            .addCase(deleteNote.rejected, (state, action) => {
                state.loading = false;
                state.error = action.payload as string;
            })
            //Update note
            .addCase(updateNote.pending, (state) => {
                state.loading = true;
                state.error = null;
            })
            .addCase(updateNote.fulfilled, (state, action) => {
                state.loading = false;
                const index = state.notes.findIndex(n => n.id === action.payload.id);
                if (index !== -1) {
                    state.notes[index] = action.payload;
                }
            })
            .addCase(updateNote.rejected, (state, action) => {
                state.loading = false;
                state.error = action.payload as string;
            });

    }
})

export const { setSelectedNote, clearSelectedNote } = noteSlice.actions;
export default noteSlice.reducer;