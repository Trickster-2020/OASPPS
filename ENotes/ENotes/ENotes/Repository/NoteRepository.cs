using ENotes.Data;
using ENotes.Entities;
using ENotes.shared.Contracts;
using ENotes.shared.DTOs;
using Microsoft.EntityFrameworkCore;

namespace ENotes.Repository
{
    public class NoteRepository : INoteService
    {
        private readonly EnotesDbContext enotesDbContext;

        public NoteRepository(EnotesDbContext enotesDbContext)
        {
            this.enotesDbContext = enotesDbContext;
        }
        public async Task<bool> Delete_Note(string Id)
        {
            Note? note = await this.enotesDbContext.Notes.FindAsync(Id);
            if (note is not null)
            {
                this.enotesDbContext.Notes.Remove(note);
                await this.enotesDbContext.SaveChangesAsync();
                return true;
                
            }
            return false;
        }

        public async Task<List<NoteDto>> Get_Note()
        {
            List<Note> notes = await enotesDbContext.Notes.ToListAsync();
            List<NoteDto> noteDtos = new List<NoteDto>();
            foreach (var notedata in notes)
            {
                NoteDto noteDto = new NoteDto();
                noteDto.Id = notedata.Id;
                noteDto.Title = notedata.Title;
                noteDto.Description = notedata.Description;
                noteDto.Date = notedata.Date;
                noteDto.Download_Count = notedata.Download_Count;

                noteDto.Teacher_Id = notedata.Teacher_Id;
                Teacher? teacher = await this.enotesDbContext.Teachers.FindAsync(noteDto.Teacher_Id);
                noteDto.Teacher_Name = teacher.Name;

                noteDto.Subject_Id = notedata.Subject_Id;
                Subject? subject = await this.enotesDbContext.Subjects.FindAsync(noteDto.Subject_Id);
                noteDto.Subject_Name = subject.Name;
                noteDto.Note_Data = notedata.Note_Data;

                noteDtos.Add(noteDto);
            }
            return noteDtos;
        }

        public async Task<List<NoteDto>> Get_Note(string id)
        {
            List<Note> notes = await enotesDbContext.Notes.Where(s => s.Subject_Id == id).ToListAsync();
            List<NoteDto> noteDtos = new List<NoteDto>();
            foreach (var notedata in notes)
            {
                NoteDto noteDto = new NoteDto();
                noteDto.Id = notedata.Id;
                noteDto.Title = notedata.Title;
                noteDto.Description = notedata.Description;
                noteDto.Date = notedata.Date;
                noteDto.Download_Count = notedata.Download_Count;

                noteDto.Teacher_Id = notedata.Teacher_Id;
                Teacher? teacher = await this.enotesDbContext.Teachers.FindAsync(noteDto.Teacher_Id);
                noteDto.Teacher_Name = teacher.Name;

                noteDto.Subject_Id = notedata.Subject_Id;
                Subject? subject = await this.enotesDbContext.Subjects.FindAsync(noteDto.Subject_Id);
                noteDto.Subject_Name = subject.Name;
                noteDto.Note_Data = notedata.Note_Data;

                noteDtos.Add(noteDto);
            }
            return noteDtos;

        }

        public async Task<List<NoteDto>> Get_Notes_By_Email(string Email)
        {
            Teacher? teacher = await this.enotesDbContext.Teachers.FirstOrDefaultAsync(x => x.Email == Email);
            if(teacher is not null)
            {
                List<Note> notes = await this.enotesDbContext.Notes.Where(s=>s.Teacher_Id == teacher.Id).ToListAsync();
                List<NoteDto> notesDto = new List<NoteDto>();
                foreach (var notedata in notes)
                {
                    NoteDto noteDto = new NoteDto();
                    noteDto.Id = notedata.Id;
                    noteDto.Title = notedata.Title;
                    noteDto.Description = notedata.Description;
                    noteDto.Date = notedata.Date;
                    noteDto.Download_Count = notedata.Download_Count;

                    noteDto.Teacher_Id = teacher.Id;
                    noteDto.Teacher_Name = teacher.Name;

                    noteDto.Subject_Id = notedata.Subject_Id;
                    Subject? subject = await this.enotesDbContext.Subjects.FindAsync(noteDto.Subject_Id);
                    noteDto.Subject_Name = subject.Name;
                    noteDto.Note_Data = notedata.Note_Data;

                    notesDto.Add(noteDto);
                }
                return notesDto;
            }
            return default(List<NoteDto>);

        }

        public async Task<bool> Post_Note(NoteDto note)
        {
            Note new_note = new Note();

            new_note.Id = note.Id;
            new_note.Title = note.Title;
            new_note.Description = note.Description;
            new_note.Date = note.Date;
            new_note.Download_Count = note.Download_Count;
            new_note.Teacher_Id = note.Teacher_Id;
            new_note.Subject_Id = note.Subject_Id;
            new_note.Note_Data = note.Note_Data;  
            enotesDbContext.Notes.Add(new_note);
            await enotesDbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Update_Note(NoteDto note)
        {
            Note old_note = await this.enotesDbContext.Notes.FindAsync(note.Id);
            if (old_note is not null)
            {
                old_note.Title = note.Title;
                old_note.Description = note.Description;
                old_note.Date = note.Date;
                old_note.Download_Count = note.Download_Count;
                old_note.Teacher_Id = note.Teacher_Id;
                old_note.Subject_Id = note.Subject_Id;
                old_note.Note_Data= note.Note_Data;
                await enotesDbContext.SaveChangesAsync();
                return true;
                
            }
            return false;
        }
    }
}
