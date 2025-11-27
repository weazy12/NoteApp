import {useState} from "react";
import type {CreateNoteDto} from "../../models/note.ts";
import {useAppDispatch} from "../../hooks/redux.ts";
import {createNote} from "../../store/reducers/NoteSlice.ts";


interface CreateNoteFormProps {
    onTaskCreated: () => void;
}

const CreateNoteFrom = ({onTaskCreated} : CreateNoteFormProps ) => {
    const [title,setTitle] = useState('');
    const [content, setContent] = useState('');
    const [error, setError] = useState<string|null>(null);
    const [loading, setLoading] = useState(false);

    const dispatch = useAppDispatch();

    const handleSubmit = async (e: { preventDefault: () => void; }) => {
        e.preventDefault();
        setError(null);
        setLoading(true);

        const newNote: CreateNoteDto = {title, content};
        try{
            await dispatch(createNote(newNote)).unwrap();
            setTitle('');
            setContent('');
            onTaskCreated();
        } catch (error: unknown) {
            if (error instanceof Error) {
                setError(error.message);
            } else {
                setError(String(error) || "Cannot create note.");
            }
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
            required
            />
            <textarea
            placeholder="Content"
            value={content}
            onChange={(e) => setContent(e.target.value)}
            required/>
            {error && <p>{error}</p>}
            <button type='submit' disabled={loading}>
                {loading ? "Creating..." : 'Create note'}
            </button>
        </form>
    );
};

export default CreateNoteFrom;

