import type {NoteItemProps} from "./NoteItem.props.ts";
import styles from './NoteItem.module.scss';
import {deleteNote, setSelectedNote} from "../../store/reducers/NoteSlice.ts";
import {useAppDispatch} from "../../hooks/redux.ts";


const NoteItem = ({note}: NoteItemProps) => {
    const dispatch = useAppDispatch();

    const handleDelete = () => {
            dispatch(deleteNote(note.id));
    };
    const handleEdit = () => {
        dispatch(setSelectedNote(note));
    };
    return (
        <div className={styles["noteItem"]}>
            <div className="noteItem-content">
                <h3 className="noteItem-title">{note.title}</h3>
                <p className="noteItem-description">{note.content}</p>
            </div>
            <div className={styles['noteButtons']}>
                <button className={styles['noteEditBtn']} onClick={handleEdit}>Edit</button>
                <button className={styles['noteDeleteBtn']} onClick={handleDelete}>Delete</button>
            </div>
        </div>
    );
};

export default NoteItem;