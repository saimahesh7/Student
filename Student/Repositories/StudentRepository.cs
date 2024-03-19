using Microsoft.EntityFrameworkCore;
using Student.Data;
using Student.Models.Domain;

namespace Student.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly StudentDbContext dbContext;

        public StudentRepository(StudentDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Students> CreateStudentAsync(Students students)
        {
            //Insert the domain data into the database using dbcontext
            await dbContext.Students.AddAsync(students);
            await dbContext.SaveChangesAsync();

            return students;
        }

        public async Task<List<Students>> GetAllStudentsAsync()
        {
            //Get The Domain Data from the Database using DbContext
            var studentsDomain=await dbContext.Students.ToListAsync();

            //Return that Domain data to the Method GetAllStudentsAsync
            return studentsDomain;

        }

        public async Task<Students?> GetStudentAsync(Guid id)
        {
            //Check wether the given id is present in database are not
            var student = await dbContext.Students.FirstOrDefaultAsync(x => x.Id == id);
            
            if (student == null)
            {
                return null;
            }
            return student;
        }

        public async Task<Students?> RemoveStudentAsync(Guid id)
        {
            //Check wheteher the given Id is present in database are not
            var studentdomain=await dbContext.Students.FirstOrDefaultAsync(x=>x.Id== id);
            if (studentdomain != null)
            {
                //remove the Student from database and save changes
                dbContext.Students.Remove(studentdomain);
                await dbContext.SaveChangesAsync();

                return studentdomain;
            }
            //Else if the Id is null in database
            else return null;
        }

        public async Task<Students?> UpdateAsync(Guid id, Students students)
        {
            //Check wether the given id is present in database are not
            var student = await dbContext.Students.FirstOrDefaultAsync(x=>x.Id == id);

            if (student!=null)
            {
                //Update the database with new data from the Domain data
                student.Id = id;        
                student.Name= students.Name;
                student.Email= students.Email;
                student.Phone= students.Phone;
                student.Address= students.Address;

                await dbContext.SaveChangesAsync();

                return student;
            }
            return null;
        }
    }
}
