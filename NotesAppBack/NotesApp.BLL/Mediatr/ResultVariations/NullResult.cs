using FluentResults;

namespace NotesApp.BLL.Mediatr.ResultVariations
{
    public class NullResult<T> : Result<T>
    {
        public NullResult()
            : base()
        {
        }
    }
}
