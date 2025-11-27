import type {NoteDto} from "../../models/note.ts";
import {type FormEvent, useState} from "react";
import {useAppDispatch} from "../../hooks/redux.ts";
import {updateNote} from "../../store/reducers/NoteSlice.ts";
import {useTranslation} from "react-i18next";


interface EditNoteFormProps {
    note: NoteDto;             // поточна нотатка
    onTaskUpdated: () => void; // колбек після редагування
}

const UpdateNoteForm = ({ note, onTaskUpdated } : EditNoteFormProps) => {
    const [title, setTitle] = useState(note.title);
    const [content, setContent] = useState(note.content);
    const [error, setError] = useState<string | null>(null);
    const [loading, setLoading] = useState(false);

    const  dispatch = useAppDispatch();
    const {t} = useTranslation();

    const handleSubmit = async (e: FormEvent<HTMLFormElement>) => {
        e.preventDefault();
        setError(null);
        setLoading(true);
        try{
            await dispatch(updateNote({...note, title, content})).unwrap();
            onTaskUpdated();
        } catch (error: unknown) {
            if (error instanceof Error) setError(error.message);
            else setError(String(error) || "Cannot update note.");
        } finally {
            setLoading(false);
        }
    }
    return (
        <form onSubmit={handleSubmit}>
            <input
                type="text"
                value={title}
                onChange={(e) => setTitle(e.target.value)}
                placeholder="Title"
                required/>

            <textarea
                value={content}
                onChange={(e) => setContent(e.target.value)}
                placeholder="Content"
                required/>
            {error && <p style={{color: "red"}}>{error}</p>}
            <button type="submit" disabled={loading}>
                {loading ? t('updating'): t('updateNote')}
            </button>
        </form>
    );
};

export default UpdateNoteForm;