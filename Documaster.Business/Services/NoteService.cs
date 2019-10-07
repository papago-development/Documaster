using Documaster.Model.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Documaster.Business.Services
{
    public class NoteService : INoteService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<Note> _noteRepository;

        public NoteService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _noteRepository = _unitOfWork.Repository<Note>();
        }

        public Note Get(int id, int customizeTabId)
        {
            return _noteRepository.Get(x=>x.ProjectId == id && x.CustomizeTabId == customizeTabId).FirstOrDefault();
        }
        
        public Note CreateNote(int projectId, int customizeTabId, string text)
        {
            var newNote = new Note
            {
                ProjectId = projectId,
                CustomizeTabId = customizeTabId,
                Text = text
            };

            var created = _noteRepository.Create(newNote);
            _unitOfWork.SaveChanges();
            return created;
        }

        public bool UpdateNotes(Note note)
        {
            var updated = _noteRepository.Update(note, new List<string> { "Text" });
            _unitOfWork.SaveChanges();
            return updated;
        }
    }
}
