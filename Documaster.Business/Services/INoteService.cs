using Documaster.Model.Entities;

namespace Documaster.Business.Services
{
    public interface INoteService
    {
        Note Get(int id, int customizeTabId);
        Note CreateNote(int projectId, int customizeTabId, string text);
        bool UpdateNotes(Note note);
    }
}
