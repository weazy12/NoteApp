import { useAppDispatch, useAppSelector } from "../../hooks/redux.ts";
import { useEffect, useState} from "react";
import { fetchNotes} from "../../store/reducers/NoteSlice.ts";
import NotesList from "../NotesList/NotesList.tsx";
import styles from "./Board.module.scss";
import CreateNoteFrom from "../CreateNoteForm/CreateNoteFrom.tsx";
import {useTranslation} from "react-i18next";

const Board = () => {
    const dispatch = useAppDispatch();
    const {notes, loading,error} = useAppSelector(state => state.noteReducer);
    const [openModal, setOpenModal] = useState(false);


    const { t, i18n } = useTranslation();

    const changeLanguage = (lng: string) => {
        i18n.changeLanguage(lng);
    };

    useEffect(() => {
        dispatch(fetchNotes());
    }, [dispatch]);

    const handleTaskCreated = () =>{
        setOpenModal(false);
    }


    return (
        <div className={styles.board}>
            <h1 className={styles.header}>{t('noteList')}</h1>
            <div style={{ marginBottom: "10px" }}>
                <button onClick={() => changeLanguage('en')}>EN</button>
                <button onClick={() => changeLanguage('ua')}>UA</button>
            </div>
            <button onClick={() => setOpenModal(true)}>
                {t('createNote')}
            </button>
            {openModal && (
                <div className={styles.overlay}>
                    <div className={styles.modal}>
                        <CreateNoteFrom onTaskCreated={handleTaskCreated}/>
                        <button onClick={() => setOpenModal(false)}>{t('cancel')}</button>
                    </div>

                </div>
            )}
            {loading && <p>{t('loading')}...</p>}
            {error && <p className="error">{error}</p>}
            <NotesList notes={notes} />
        </div>
    );
};

export default Board;