import { useAppDispatch, useAppSelector } from "../../hooks/redux.ts";
import { useEffect, useState} from "react";
import { fetchNotes} from "../../store/reducers/NoteSlice.ts";
import NotesList from "../NotesList/NotesList.tsx";
import styles from "./Board.module.scss";
import CreateNoteFrom from "../CreateNoteForm/CreateNoteFrom.tsx";

const Board = () => {
    const dispatch = useAppDispatch();
    const {notes, loading,error} = useAppSelector(state => state.noteReducer);
    const [openModal, setOpenModal] = useState(false)

    useEffect(() => {
        dispatch(fetchNotes());
    }, [dispatch]);

    const handleTaskCreated = () =>{
        setOpenModal(false);
    }


    return (
        <div className={styles.board}>
            <h1 className={styles.header}>Note list</h1>
            <button onClick={() => setOpenModal(true)}>
                Create new note
            </button>
            {openModal && (
                <div className={styles.overlay}>
                    <div className={styles.modal}>
                        <CreateNoteFrom onTaskCreated={handleTaskCreated}/>
                        <button onClick={() => setOpenModal(false)}>Cancel</button>
                    </div>

                </div>
            )}
            {loading && <p>Loading...</p>}
            {error && <p className="error">{error}</p>}
            <NotesList notes={notes} />
        </div>
    );
};

export default Board;