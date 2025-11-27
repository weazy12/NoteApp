import type {NoteItemProps} from "./NoteItem.props.ts";
import styles from './NoteItem.module.scss';
import {deleteNote} from "../../store/reducers/NoteSlice.ts";
import {useAppDispatch} from "../../hooks/redux.ts";
import UpdateNoteForm from "../EditNoteForm/UpdateNoteForm.tsx";
import {useState} from "react";


const NoteItem = ({note}: NoteItemProps) => {
    const dispatch = useAppDispatch();
    const [isEditing, setIsEditing] = useState(false);

    const handleDelete = () => {
            dispatch(deleteNote(note.id));
    };
    const handleUpdated = () => {
        setIsEditing(false);
    };
    return (
        <div className={styles["noteItem"]}>
                <div className="noteItem-content">
                    <h3 className="noteItem-title">{note.title}</h3>
                    <p className="noteItem-description">{note.content}</p>
                </div>
                <div className={styles['noteButtons']}>
                    <button
                        className={styles['noteDeleteBtn']}
                        onClick={handleDelete}
                    >
                        Delete
                    </button>
                    <button onClick={() => setIsEditing(true)}>
                        Edit
                    </button>
                    {isEditing && (
                            <div className={styles.overlay}>
                                <div className={styles.modal}>
                                    <UpdateNoteForm note={note} onTaskUpdated={handleUpdated} />
                                    <button onClick={() => setIsEditing(false)}>Cancel</button>
                                </div>
                            </div>
                        )}
                </div>
        </div>
    );
};

export default NoteItem;