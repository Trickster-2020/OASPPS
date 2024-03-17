using ENotes.shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENotes.shared.Contracts
{
    public interface INoteService
    {
        public Task<List<NoteDto>> Get_Note();
        public Task<List<NoteDto>> Get_Note(string id);
        public Task<bool> Post_Note(NoteDto note);
        public Task<bool> Delete_Note(String Id);
        public Task<bool> Update_Note(NoteDto note);
        public Task<List<NoteDto>> Get_Notes_By_Email(String Email);
    }
}
