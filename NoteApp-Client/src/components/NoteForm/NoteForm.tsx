import { useState, useEffect, useCallback } from 'react';
import { useAppDispatch, useAppSelector } from "../../hooks/redux.ts";
import { createNote, updateNote } from "../../store/reducers/NoteSlice.ts";

interface NoteFormProps {
    onTaskCreated: () => void;
}

const NoteForm = ({ onTaskCreated }: NoteFormProps) => {
    const dispatch = useAppDispatch();
    const selectedNote = useAppSelector(state => state.noteReducer.selectedNote);

    const [title, setTitle] = useState('');
    const [content, setContent] = useState('');

    const resetForm = useCallback(() => {
        setTitle('');
        setContent('');
    }, []);

    const loadNoteData = useCallback(() => {
        if (selectedNote) {
            setTitle(selectedNote.title);
            setContent(selectedNote.content);
        } else {
            resetForm();
        }
    }, [selectedNote, resetForm]);

    useEffect(() => {
        // eslint-disable-next-line react-hooks/set-state-in-effect
        loadNoteData();
    }, [loadNoteData]);

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();

        if (selectedNote) {
            await dispatch(updateNote({
                ...selectedNote,
                title,
                content
            }));
        } else {
            await dispatch(createNote({ title, content }));
        }

        resetForm();
        onTaskCreated();
    };

    return (
        <form onSubmit={handleSubmit}>
            <input
                type="text"
                value={title}
                onChange={(e) => setTitle(e.target.value)}
                placeholder="Заголовок"
                required
            />
            <textarea
                value={content}
                onChange={(e) => setContent(e.target.value)}
                placeholder="Опис"
                required
            />
            <button type="submit">
                {selectedNote ? 'Update' : 'Create'}
            </button>
        </form>
    );
};

export default NoteForm;