import type {NotesListProps} from "./NotesList.props.ts";
import NoteItem from "../NoteItem/NoteItem.tsx";


const NotesList = ({notes}: NotesListProps) => {
    return (
        <div>
            <div>
                {notes.map(note => (
                    <NoteItem key={note.id} note={note} />
                ))}
            </div>
        </div>
    );
};

export default NotesList;