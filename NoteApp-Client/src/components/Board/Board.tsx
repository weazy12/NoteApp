import { useAppDispatch, useAppSelector } from "../../hooks/redux.ts";
import { useEffect, useState } from "react";
import { fetchNotes, clearSelectedNote } from "../../store/reducers/NoteSlice.ts";
import NotesList from "../NotesList/NotesList.tsx";
import NoteForm from "../NoteForm/NoteForm.tsx";
import styles from "./Board.module.scss";

const Board = () => {
    const [openModal, setOpenModal] = useState(false);

    const dispatch = useAppDispatch();
    const { notes, loading, error, selectedNote } = useAppSelector(state => state.noteReducer);

    useEffect(() => {
        dispatch(fetchNotes());
    }, [dispatch]);

    // Автоматично відкриваємо модалку при виборі нотатки для редагування
    useEffect(() => {
        if (selectedNote) {
            // eslint-disable-next-line react-hooks/set-state-in-effect
            setOpenModal(true);
        }
    }, [selectedNote]);

    const handleTaskCreated = () => {
        dispatch(fetchNotes());
        setOpenModal(false);
        dispatch(clearSelectedNote());
    };

    const handleCloseModal = () => {
        setOpenModal(false);
        dispatch(clearSelectedNote());
    };

    return (
        <div className={styles.board}>
            <h1 className={styles.header}>Note list</h1>
            <button
                className={styles.createBtn}
                onClick={() => setOpenModal(true)}
            >
                + Create task
            </button>
            {openModal && (
                <div className={styles.overlay}>
                    <div className={styles.modal}>
                        <h2>{selectedNote ? 'Edit Task' : 'Create Task'}</h2>
                        <NoteForm onTaskCreated={handleTaskCreated} />
                        <button
                            className={styles.closeButton}
                            onClick={handleCloseModal}
                        >Close
                        </button>
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